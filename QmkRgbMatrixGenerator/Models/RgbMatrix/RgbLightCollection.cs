using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QmkRgbMatrixGenerator.Models.RgbMatrix
{
    public class RgbLightCollection : IList<RgbLightModel>
    {
        private readonly IList<RgbLightModel> _rgbLights;

        public RgbLightModel this[int index] { get => this._rgbLights[index]; set => this._rgbLights[index] = value; }

        public int Count => this._rgbLights.Count;

        public bool IsReadOnly => this._rgbLights.IsReadOnly;

        public RgbLightCollection()
        {
            this._rgbLights = new List<RgbLightModel>();
        }

        public RgbLightCollection(IEnumerable<RgbLightModel> rgbLights)
        {
            this._rgbLights = rgbLights.ToList();
        }

        public void Add(RgbLightModel item)
        {
            this._rgbLights.Add(item);
        }

        public void Clear()
        {
            this._rgbLights.Clear();
        }

        public bool Contains(RgbLightModel item)
        {
            return this._rgbLights.Contains(item);
        }

        public void CopyTo(RgbLightModel[] array, int arrayIndex)
        {
            this._rgbLights.CopyTo(array, arrayIndex);
        }

        public IEnumerator<RgbLightModel> GetEnumerator()
        {
            return this._rgbLights.GetEnumerator();
        }

        public int IndexOf(RgbLightModel item)
        {
            return this._rgbLights.IndexOf(item);
        }

        public void Insert(int index, RgbLightModel item)
        {
            this._rgbLights.Insert(index, item);
        }

        public bool Remove(RgbLightModel item)
        {
            return this._rgbLights.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._rgbLights.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._rgbLights).GetEnumerator();
        }
    }
}
