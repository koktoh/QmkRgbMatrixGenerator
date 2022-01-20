using QmkRgbMatrixGenerator.Models.ProxyModels;

namespace QmkRgbMatrixGenerator.Models.Parser
{
    public class LayoutParser : ILayoutParser
    {
        private readonly ILayoutParser _logicalLayoutParser;
        private readonly ILayoutParser _physicalLayoutParser;

        public LayoutParser()
        {
            this._logicalLayoutParser = new LogicalLayoutParser();
            this._physicalLayoutParser = new PhysicalLayoutParser();

        }

        public ILayoutModel Parse(string raw)
        {
            var logicalLayout = this._logicalLayoutParser.Parse(raw);
            var physicalLayout = this._physicalLayoutParser.Parse(raw);

            foreach (var row in physicalLayout.Rows)
            {
                foreach (var key in row.Keys)
                {
                    logicalLayout.TryRefleshKey(key);
                }
            }

            return logicalLayout;
        }


    }
}
