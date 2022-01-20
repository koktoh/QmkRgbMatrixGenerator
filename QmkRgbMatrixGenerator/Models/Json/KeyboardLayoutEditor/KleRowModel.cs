using System.Collections.Generic;
using System.Linq;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    [JsonFormatter(typeof(KleRowModelFormatter))]
    public class KleRowModel
    {
        private readonly List<KleKeyModel> _kleKeys;

        public IEnumerable<KleKeyModel> KleKeys => this._kleKeys;

        public KleRowModel()
        {
            this._kleKeys = new List<KleKeyModel>();
        }

        public KleRowModel(IEnumerable<KleKeyModel> keys)
        {
            this._kleKeys = keys.ToList();
        }

        public void AddKey(KleKeyModel key)
        {
            this._kleKeys.Add(key);
        }

        public void AddRangeKey(IEnumerable<KleKeyModel> keys)
        {
            this._kleKeys.AddRange(keys);
        }
    }
}
