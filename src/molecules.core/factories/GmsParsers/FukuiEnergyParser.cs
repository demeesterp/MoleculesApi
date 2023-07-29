using molecules.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string line = string.Empty;
            for (int c = 0; c < input.Count; ++c)
            {
                line = input[c];

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
