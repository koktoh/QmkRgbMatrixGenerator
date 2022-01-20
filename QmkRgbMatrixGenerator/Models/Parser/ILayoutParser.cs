using QmkRgbMatrixGenerator.Models.ProxyModels;

namespace QmkRgbMatrixGenerator.Models.Parser
{
    public interface ILayoutParser
    {
        public ILayoutModel Parse(string raw);
    }
}
