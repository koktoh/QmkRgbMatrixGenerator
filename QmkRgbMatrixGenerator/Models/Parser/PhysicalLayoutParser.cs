using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QmkRgbMatrixGenerator.Extensions;
using QmkRgbMatrixGenerator.Models.Extractor;
using QmkRgbMatrixGenerator.Models.ProxyModels;

namespace QmkRgbMatrixGenerator.Models.Parser
{
    public class PhysicalLayoutParser : ILayoutParser
    {
        private readonly LayoutExtractor _extractor;

        public PhysicalLayoutParser()
        {
            this._extractor = new LayoutExtractor();
        }

        public ILayoutModel Parse(string raw)
        {
            var layout = this._extractor.ExtractPhysicalLayout(raw);

            if (layout.IsNullOrWhiteSpace())
            {
                return default;
            }

            var normalized = this.Normalize(layout);
            var splited = normalized.Select(x => x.SplitComma());

            return this.ParseLayout(splited);
        }
        private IEnumerable<string> Normalize(string text)
        {
            return text.SplitNewLine()
                .Select(x => Regex.Replace(x, @"^\s*(/.*/)*\s*(.*)\s*,*\s*\\*\s*$", "$2").Trim(" ,\\\r\n"))
                .Where(x => x.HasMeaningfulValue());
        }

        private IRowModel ParseRow(int rowIndex, IEnumerable<string> rawRow)
        {
            var row = new GeneralRowModel();

            for (int colIndex = 0; colIndex < rawRow.Count(); colIndex++)
            {
                var id = rawRow.ElementAt(colIndex).Trim().ToLower();

                var key = new GeneralKeyModel(id)
                {
                    PosX = colIndex,
                    PosY = rowIndex,
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
