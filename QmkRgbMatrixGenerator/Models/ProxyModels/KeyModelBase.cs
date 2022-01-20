using System;

namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public abstract class KeyModelBase : IKeyModel
    {
        private readonly string _id;

        public string Id => this._id;

        public string Legend { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsActive { get; set; } = true;

        public KeyModelBase(string id)
        {
            this._id = id;
        }

        public virtual bool Equals(string id)
        {
            return this.Id.Equals(id);
        }

        public virtual bool Equals(IKeyModel key)
        {
            return this.Id.Equals(key.Id);
        }

        public virtual void Reflesh(IKeyModel key)
        {
            if (!this.Equals(key))
            {
                throw new ArgumentException($"{key.Id} does not equal {this.Id}");
            }

            this.Legend = key.Legend;
            this.Col = key.Col;
            this.Row = key.Row;
            this.PosX = key.PosX;
            this.PosY = key.PosY;
            this.Width = key.Width;
            this.Height = key.Height;
            this.IsActive = key.IsActive;
        }
    }
}
