namespace QmkRgbMatrixGenerator.Models.ProxyModels
{
    public interface IKeyModel
    {
        public string Id { get; }
        public string Legend { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsActive { get; set; }

        public bool Equals(string id);
        public bool Equals(IKeyModel key);

        public void Reflesh(IKeyModel key);
    }
}
