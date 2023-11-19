using molecules;
using molecules.core;
using molecules.core.tests;
using molecules.core.tests.valueobjects;
using molecules.core.tests.valueobjects.CalcOrderItem;
using molecules.core.valueobjects;
using molecules.core.valueobjects.BasisSet;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.tests.valueobjects.CalcOrderItem
{
    public class CalcOrderItemDetailsTests
    {
        public class CalcOrderItemDetailsConstructorTests : CalcOrderItemDetailsTests
        {
            [Fact]
            public void Should_Initialise_Members_When_Default_Constructor_Called()
            {
                // Act

                CalcOrderItemDetails details = new CalcOrderItemDetails(0,"",CalcBasisSetCode.B3_21G, CalcOrderItemType.GeoOpt);

                // Assert

                Assert.Equal(0, details.Charge);

                Assert.Equal(CalcOrderItemType.GeoOpt, details.Type);

                Assert.Equal(CalcBasisSetCode.BSTO3G, details.BasisSetCode);

                Assert.Empty(details.XYZ);
            }
        }

    }
}
