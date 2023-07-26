using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.GmsCalc.Calculations;

namespace molecules.core.valueobjects.Delivery
{
    public class CalcDeliveryItemDetails
    {
        public string? MoleculeName { get; }

        public CalcDetails? Details { get;}

        public List<ICalculation> Calculations { get; }

        public CalcDeliveryItemDetails(string moleculeName, CalcDetails calcDetails) 
        {
            MoleculeName = moleculeName;
            Details = calcDetails;
            Calculations = new List<ICalculation>()
            {
                new CHelpGChargeCalculation(),
                new GeoDiskChargeCalculation(),
                new FukuiNeutralCalculation(),
                new FukuiAcidCalculation(),
                new FukuiBaseCalculation()
            };

            if ( calcDetails.CalcType == CalcType.GeoOpt)
            {
                Calculations.Insert(0, new GeoOptCalculation());
            }

        }
    }
}
