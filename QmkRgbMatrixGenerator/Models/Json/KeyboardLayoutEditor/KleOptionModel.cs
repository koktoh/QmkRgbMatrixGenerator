using System.Linq;
using System.Runtime.Serialization;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    public class KleOptionModel
    {

        [DataMember(Name = "x")]
        public double OffsetX { get; set; }
        [DataMember(Name = "y")]
        public double OffsetY { get; set; }
        [DataMember(Name = "w")]
        public double Width { get; set; } = 1;
        [DataMember(Name = "h")]
        public double Height { get; set; } = 1;
        [DataMember(Name = "a")]
        public int Alignment { get; set; } = 0;
        [DataMember(Name = "d")]
        public bool IsDecal { get; set; } = false;

        //[IgnoreDataMember]
        //public bool ShouldSerialize =>
        //    this.ShouldSerializeOffsetX()
        //    || this.ShouldSerializeOffsetY()
        //    || this.ShouldSerializeWidth()
        //    || this.ShouldSerializeHeight();

        public bool ShouldSerializeOffsetX() => this.OffsetX != 0;
        public bool ShouldSerializeOffsetY() => this.OffsetY != 0;
        public bool ShouldSerializeWidth() => this.Width != 1;
        public bool ShouldSerializeHeight() => this.Height != 1;
        public bool ShouldSerializeIsDecal() => this.IsDecal;

        public KleOptionModel() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = this.GetType();

            if (obj.GetType() != type)
            {
                return false;
            }

            var target = (KleOptionModel)obj;

            return type.GetProperties()
                .Select(prop => type.GetProperty(prop.Name).GetValue(target).Equals(type.GetProperty(prop.Name).GetValue(this)))
                .All(x => x);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
