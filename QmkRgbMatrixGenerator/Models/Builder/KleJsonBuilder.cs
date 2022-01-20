using QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor;
using QmkRgbMatrixGenerator.Models.ProxyModels;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Builder
{
    public class KleJsonBuilder
    {
        public KleJsonBuilder() { }

        public string Build(ILayoutModel layout)
        {
            var kle = this.ConvertToKle(layout);

            return JsonSerializer.PrettyPrint(JsonSerializer.Serialize(kle));
        }

        private KleLayoutModel ConvertToKle(ILayoutModel layout)
        {
            var kleLayout = new KleLayoutModel();

            foreach (var row in layout.Rows)
            {
                kleLayout.AddRow(this.ConvertToKleRow(row));
            }

            return kleLayout;
        }

        private KleRowModel ConvertToKleRow(IRowModel row)
        {
            var kleRow = new KleRowModel();

            foreach (var key in row.Keys)
            {
                if (!key.IsActive)
                {
                    continue;
                }

                var kleKey = new KleKeyModel()
                {
                    LegendFrontCenter = key.Id,
                    Option = new KleOptionModel(),
                };

                kleRow.AddKey(kleKey);
            }

            return kleRow;
        }
    }
}
