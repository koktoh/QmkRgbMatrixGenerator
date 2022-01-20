using System.Collections.Generic;
using System.Linq;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    [JsonFormatter(typeof(KleLayoutModelFormatter))]
    public class KleLayoutModel
    {
        private readonly List<KleRowModel> _kleRows;

        public IEnumerable<KleRowModel> KleRows => this._kleRows;

        public KleLayoutModel()
        {
            this._kleRows = new List<KleRowModel>();
        }

        public KleLayoutModel(IEnumerable<KleRowModel> rows)
        {
            this._kleRows = rows.ToList();
        }

        public void AddRow(KleRowModel row)
        {
            this._kleRows.Add(row);
        }

        public void AddRangeRow(IEnumerable<KleRowModel> rows)
        {
            this._kleRows.AddRange(rows);
        }
    }
}
