namespace QmkRgbMatrixGenerator.Models.RgbMatrix
{
    public class RgbLightModel
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public RgbLightFlag Flag { get; set; }
    }
}
