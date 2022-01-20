using System.Collections.Generic;

namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public interface ILayoutModel
    {
        public IEnumerable<IRowModel> Rows { get; }

        public void AddRow(IRowModel row);
        public bool ContainsKey(string id);
        public bool ContainsKey(IKeyModel key);
        public void RefleshKey(IKeyModel key);
        public bool TryRefleshKey(IKeyModel key);
    }
}
