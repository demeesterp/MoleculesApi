using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Output
{
    public class GmsCalcOutput
    {

        public List<GmsCalcOuputItem> Items { get; }

        public GmsCalcOutput()
        {
            Items = new List<GmsCalcOuputItem>();
        }

        public void AddItem(string displayName, StringBuilder outputFileContent)
        {
            Items.Add(new GmsCalcOuputItem(displayName, outputFileContent));
        }

    }
}
