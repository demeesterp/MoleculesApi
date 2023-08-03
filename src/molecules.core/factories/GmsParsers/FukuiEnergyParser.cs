using molecules.shared;

namespace molecules.core.factories.GmsParsers
{
    internal abstract class FukuiEnergyParser
    {
        #region tags

        protected abstract string GetStartTag();

        protected abstract string GetEnergyTag();

        #endregion

        public decimal Parse(List<string> input)
        {
            decimal retval = decimal.Zero;
            bool startProcessing = false;
            for (int c = 0; c < input.Count; ++c)
            {
                var line = input[c];

                if (line.Contains(GetStartTag()))
                {
                    startProcessing = true;
                }

                if (startProcessing && line.Contains(GetStartTag()))
                {
                    var results = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (results.Length == 2)
                    {
                        retval = StringConversion.ToDecimal(results[1].Trim());
                    }
                }
            }
            return retval;
        }
    }
}
