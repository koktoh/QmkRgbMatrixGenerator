using System;
using System.Collections.Generic;

namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public abstract class LayoutModelBase : ILayoutModel
    {
        private readonly IList<IRowModel> _rows;

        public IEnumerable<IRowModel> Rows => this._rows;

        public LayoutModelBase()
        {
            this._rows = new List<IRowModel>();
        }

        public void AddRow(IRowModel row)
        {
            this._rows.Add(row);
        }

        public virtual bool ContainsKey(string id)
        {
            foreach(var row in this._rows)
            {
                if(row.ContainsKey(id))
                {
                    return true;
                }
            }

            return false;
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

            foreach(var row in this._rows)
            {
                if(!row.ContainsKey(key))
                {
                    continue;
                }

                row.RefleshKey(key);
                break;
            }
        }

        public virtual bool TryRefleshKey(IKeyModel key)
        {
            if(!this.ContainsKey(key))
            {
                return false;
            }

            this.RefleshKey(key);

            return true;
        }
    }
}
