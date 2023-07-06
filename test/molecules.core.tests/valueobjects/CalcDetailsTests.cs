using molecules.core.valueobjects;

namespace molecules.core.tests.valueobjects
{
    public class CalcDetailsTests
    {
        public class CalcDetailsConstructorTests : CalcDetailsTests
        {
            [Fact]
            public void Should_Initialise_Members_When_Default_Constructor_Called()
            {
                // Act
                CalcDetails details = new CalcDetails();

                // Assert
                Assert.Equal(0, details.Charge);
                Assert.Equal(CalcType.GeoOpt, details.CalcType);
                Assert.Empty(details.XYZ);
            }
        }

    }
}
