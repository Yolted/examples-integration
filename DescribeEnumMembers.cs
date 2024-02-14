using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace IntegrationSample
{
  /// <summary>
  /// Swagger schema filter to modify description of enum types so they
  /// show the XML docs attached to each member of the enum.
  /// 
  /// This is only for API docs Generation.
  /// </summary>
  public class DescribeEnumMembers : ISchemaFilter
  {
    private readonly XDocument mXmlComments;

    /// <summary>
    /// Initialize schema filter.
    /// </summary>
    /// <param name="argXmlComments">Document containing XML docs for enum members.</param>
    public DescribeEnumMembers(XDocument argXmlComments)
      => mXmlComments = argXmlComments;

    /// <summary>
    /// Apply this schema filter.
    /// </summary>
    /// <param name="argSchema">Target schema object.</param>
    /// <param name="argContext">Schema filter context.</param>
    public void Apply(OpenApiSchema argSchema, SchemaFilterContext argContext)
    {
      var EnumType = argContext.Type;

      if (!EnumType.IsEnum) return;

      var sb = new StringBuilder(argSchema.Description);

      sb.AppendLine("<p>Possible values:</p>");
      sb.AppendLine("<ul>");

      foreach (var EnumMemberName in Enum.GetNames(EnumType))
      {


        int value = (int)Enum.Parse(EnumType, EnumMemberName);

        var FullEnumMemberName = $"F:{EnumType.FullName}.{EnumMemberName}";

        var EnumMemberDescription = mXmlComments.XPathEvaluate(
          $"normalize-space(//member[@name = '{FullEnumMemberName}']/summary/text())"
        ) as string;

        if (string.IsNullOrEmpty(EnumMemberDescription)) continue;

        sb.AppendLine($"<li><b>{value}</b>: {EnumMemberDescription}</li>");
      }

      sb.AppendLine("</ul>");

      argSchema.Description = sb.ToString();
    }
  }
}
