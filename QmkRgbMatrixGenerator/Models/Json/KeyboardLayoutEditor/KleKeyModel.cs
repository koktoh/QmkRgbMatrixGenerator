using System.Collections.Generic;
using System.Linq;
using QmkRgbMatrixGenerator.Extensions;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    [JsonFormatter(typeof(KleKeyModelFormatter))]
    public class KleKeyModel
    {
        public string LegendTopLeft { get; set; }
        public string LegendTopCenter { get; set; }
        public string LegendTopRight { get; set; }
        public string LegendCenterLeft { get; set; }
        public string LegendCenter { get; set; }
        public string LegendCenterRight { get; set; }
        public string LegendBottomLeft { get; set; }
        public string LegendBottomCenter { get; set; }
        public string LegendBottomRight { get; set; }
        public string LegendFrontLeft { get; set; }
        public string LegendFrontCenter { get; set; }
        public string LegendFrontRight { get; set; }

        public string Legend => this.BuildLegend();
        public KleOptionModel Option { get; set; }

        public KleKeyModel() : this(string.Empty) { }
        public KleKeyModel(string legend) : this(legend, null) { }
        public KleKeyModel(string legend, KleOptionModel option)
        {
            this.ParseLegend(legend);
            this.Option = option;
        }

        private string BuildLegend()
        {
            var legend = this.EnumerateLegendParts().Join("\n");
            return legend.TrimEnd("\n");
        }

        private IEnumerable<string> EnumerateLegendParts()
        {
            yield return this.LegendTopLeft.Escape();
            yield return this.LegendBottomLeft.Escape();
            yield return this.LegendTopRight.Escape();
            yield return this.LegendBottomRight.Escape();
            yield return this.LegendFrontLeft.Escape();
            yield return this.LegendFrontRight.Escape();
            yield return this.LegendCenterLeft.Escape();
            yield return this.LegendCenterRight.Escape();
            yield return this.LegendTopCenter.Escape();
            yield return this.LegendCenter.Escape();
            yield return this.LegendBottomCenter.Escape();
            yield return this.LegendFrontCenter.Escape();
        }

        private void ParseLegend(string rawLegend)
        {
            if (rawLegend.IsNullOrWhiteSpace())
            {
                return;
            }

            var splited = rawLegend.Split("\n");

            this.LegendTopLeft = splited.ElementAtOrDefault(0);
            this.LegendBottomLeft = splited.ElementAtOrDefault(1);
            this.LegendTopRight = splited.ElementAtOrDefault(2);
            this.LegendBottomRight = splited.ElementAtOrDefault(3);
            this.LegendFrontLeft = splited.ElementAtOrDefault(4);
            this.LegendFrontRight = splited.ElementAtOrDefault(5);
            this.LegendCenterLeft = splited.ElementAtOrDefault(6);
            this.LegendCenterRight = splited.ElementAtOrDefault(7);
            this.LegendTopCenter = splited.ElementAtOrDefault(8);
            this.LegendCenter = splited.ElementAtOrDefault(9);
            this.LegendBottomCenter = splited.ElementAtOrDefault(10);
            this.LegendFrontCenter = splited.ElementAtOrDefault(11);

        }
    }
}
