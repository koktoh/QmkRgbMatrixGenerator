using System.Collections.Generic;

namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public interface IRowModel
    {
        public IEnumerable<IKeyModel> Keys { get; }

        public bool ContainsKey(string id);
        public bool ContainsKey(IKeyModel key);

        public void AddKey(IKeyModel key);

        public void RefleshKey(IKeyModel key);

        public bool TryRefleshKey(IKeyModel key);
    }
}
