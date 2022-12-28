﻿using System.Xml.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PaymentAPI.Schemas;

public class SwaggerEnumFilter : ISchemaFilter
{
    private readonly XDocument _xmlComments;

    public SwaggerEnumFilter(string xmlPath)
    {
        if (File.Exists(xmlPath)) _xmlComments = XDocument.Load(xmlPath);
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (_xmlComments == null) return;

        if (schema.Enum is not { Count: > 0 } || context.Type is not { IsEnum: true }) return;

        schema.Description += "<p>Members:</p><ul>";

        var fullTypeName = context.Type.FullName;

        foreach (var enumMemberName in schema.Enum.OfType<OpenApiString>().Select(v => v.Value))
        {
            var fullEnumMemberName = $"F:{fullTypeName}.{enumMemberName}";

            var enumMemberComments = _xmlComments.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")!.Value.Equals
                    (fullEnumMemberName, StringComparison.OrdinalIgnoreCase));

            var summary = enumMemberComments?.Descendants("summary").FirstOrDefault();

            if (summary == null) continue;

            schema.Description += $"<li><i>{enumMemberName}</i> - {summary.Value.Trim()}</li>";
        }

        schema.Description += "</ul>";
    }
}