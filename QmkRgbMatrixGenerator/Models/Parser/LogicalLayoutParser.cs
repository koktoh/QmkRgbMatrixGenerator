using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QmkRgbMatrixGenerator.Extensions;
using QmkRgbMatrixGenerator.Models.Extractor;
using QmkRgbMatrixGenerator.Models.ProxyModels;

namespace QmkRgbMatrixGenerator.Models.Parser
{
    public class LogicalLayoutParser : ILayoutParser
    {
        private const string KC_NO = "kc_no";

        private readonly LayoutExtractor _extractor;

        private int _knCount = 0;

        public LogicalLayoutParser()
        {
            this._extractor = new LayoutExtractor();
        }

        public ILayoutModel Parse(string raw)
        {
            var layout = this._extractor.ExtractLogicalLayout(raw);

            if (layout.IsNullOrWhiteSpace())
            {
                return default;
            }

            var normalized = this.Normalize(layout);
            var splited = normalized.Select(x => x.SplitComma());

            this._knCount = 0;

            return this.ParseLayout(splited);
        }

        private IEnumerable<string> Normalize(string text)
        {
            return text.SplitNewLine()
                .Select(x => Regex.Replace(x, @"^.*\{\s*(.*)\s*,*\s*\}.*$", "$1"))
                .Where(x => x.HasMeaningfulValue());
        }

        private IRowModel ParseRow(int rowIndex, IEnumerable<string> rawRow)
        {
            var row = new GeneralRowModel();

            var isActive = true;

            for (int colIndex = 0; colIndex < rawRow.Count(); colIndex++)
            {
                var id = rawRow.ElementAt(colIndex).Trim().ToLower();

                if (id.Equals(KC_NO))
                {
                    id += this._knCount++;
                    isActive = false;
                }

                var key = new GeneralKeyModel(id)
                {
                    Col = colIndex,
                    Row = rowIndex,
                    IsActive = isActive,
                };

                row.AddKey(key);
            }

            return row;
        }

        private ILayoutModel ParseLayout(IEnumerable<IEnumerable<string>> rawLayout)
        {
            var layout = new GeneralLayoutModel();

            for (int rowIndex = 0; rowIndex < rawLayout.Count(); rowIndex++)
            {
                var rawRow = rawLayout.ElementAt(rowIndex).ToArray();

                layout.AddRow(this.ParseRow(rowIndex, rawRow));
            }

            return layout;
        }
    }
}
