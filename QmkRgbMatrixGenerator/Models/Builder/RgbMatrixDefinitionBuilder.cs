using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QmkRgbMatrixGenerator.Extensions;
using QmkRgbMatrixGenerator.Models.ProxyModels;
using QmkRgbMatrixGenerator.Models.RgbMatrix;

namespace QmkRgbMatrixGenerator.Models.Builder
{
    public class RgbMatrixDefinitionBuilder
    {
        private const string NO_LED = "NO_LED";
        private const string TAB = "    ";

        public string Build(ILayoutModel layout, RgbLightCollection rgbLights, int rgbLightPosSectionCol = 8, int rgbLightFlagSectionCol = 5)
        {
            var builder = new StringBuilder();

            builder.AppendLine("#ifdef RGB_MATRIX_ENABLE");
            builder.AppendLine("led_config_t g_led_config = { {");

            builder.AppendLine(this.BuildRgbLightKeySection(layout, rgbLights));

            builder.AppendLine("}, {");

            builder.AppendLine(this.BuildRgbLightPosSection(rgbLights, rgbLightPosSectionCol));

            builder.AppendLine("}, {");

            builder.AppendLine(this.BuildRgbLightFlagSection(rgbLights, rgbLightFlagSectionCol));

            builder.AppendLine("} };");
            builder.AppendLine("#endif");

            return builder.ToString();
        }

        private string BuildRgbLightKeySection(ILayoutModel layout, RgbLightCollection rgbLights)
        {
            var strRows = new List<string>();

            foreach (var row in layout.Rows)
            {
                var builder = new StringBuilder();

                builder.Append(TAB);
                builder.Append("{ ");

                var list = row.Keys
                    .Select(key =>
                    {
                        if (!key.IsActive)
                        {
                            return NO_LED;
                        }

                        var light = rgbLights.FirstOrDefault(x => x.Id.Equals(key.Id));

                        return $"{light?.Index.ToString() ?? NO_LED,6}";
                    });

                builder.AppendJoin(", ", list);
                builder.Append(" }");

                strRows.Add(builder.ToString());
            }

            return strRows.Join($",{Environment.NewLine}");
        }

        private string BuildRgbLightPosSection(RgbLightCollection rgbLights, int splitCount)
        {
            var strRows = new List<string>();

            foreach (var row in rgbLights.GroupByIndex(splitCount))
            {
                var builder = new StringBuilder();

                builder.Append(TAB);

                var list = row.Select(light => $"{{ {light.X,3}, {light.Y,3} }}");

                builder.AppendJoin(", ", list);

                strRows.Add(builder.ToString());
            }

            return strRows.Join($",{Environment.NewLine}");
        }

        private string BuildRgbLightFlagSection(RgbLightCollection rgbLights, int splitCount)
        {
            var strRows = new List<string>();

            foreach (var row in rgbLights.GroupByIndex(splitCount))
            {
                var builder = new StringBuilder();

                builder.Append(TAB);

                var list = row.Select(light => $"{light.Flag,18}");

                builder.AppendJoin(", ", list);

                strRows.Add(builder.ToString());
            }

            return strRows.Join($",{Environment.NewLine}");
        }
    }
}
