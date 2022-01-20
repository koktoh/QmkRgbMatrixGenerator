using System;
using System.Collections.Generic;
using System.Linq;

namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public abstract class RowModelBase : IRowModel
    {
        private readonly IList<IKeyModel> _keys;

        public IEnumerable<IKeyModel> Keys => this._keys;

        public RowModelBase()
        {
            this._keys = new List<IKeyModel>();
        }

        public virtual void AddKey(IKeyModel key)
        {
            this._keys.Add(key);
        }

        public virtual bool ContainsKey(string id)
        {
            return this._keys.Any(x => x.Equals(id));
        }

        public virtual bool ContainsKey(IKeyModel key)
        {
            return this.ContainsKey(key.Id);
        }

        public virtual void RefleshKey(IKeyModel key)
        {
            if (!this.ContainsKey(key))
            {
                throw new FieldAccessException($"{key.Id} does not contain.");
            }

            this._keys.First(x => x.Equals(key)).Reflesh(key);
        }

        public virtual bool TryRefleshKey(IKeyModel key)
        {
            if (!this.ContainsKey(key))
            {
                return false;
            }

            this.RefleshKey(key);

            return true;
        }
    }
}
