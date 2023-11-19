using molecules.core.aggregates;
using molecules.core.valueobjects;
using molecules.core.valueobjects.CalcOrderItem;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using molecules.core.valueobjects.BasisSet;

namespace molecules.core.tests.aggregates
{

    public class CalcOrderItemTests
    {
        public class CalcOrderItemConstructorTests : CalcOrderItemTests
        {

            [Fact]
            public void Should_Initialise_Members_When_Default_Constructor_Called()
            {             
                // Act
                CalcOrderItem item = new CalcOrderItem(0, "", 
                                new CalcOrderItemDetails(0, "", 
                                                            CalcBasisSetCode.BSTO3G,
                                                                CalcOrderItemType.MolecularProperties));

                // Assert
                Assert.Equal(0, item.Id);
                Assert.Equal("", item.MoleculeName);
                Assert.NotNull(item.Details);

            }

            [Fact]
            public void Should_Throw_ArgumentException_When_MoleculeName_Is_Null()
            {
                // Arrange
                string moleculeName = null;

                // Act
                Action action = () =>
                         new CalcOrderItem(0, "", 
                                        new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties));

                // Assert
                Assert.Throws<ArgumentException>(action);
            }

            [Fact]
            public void Should_Throw_ArgumentException_When_MoleculeName_Is_Empty()
            {
                // Arrange
                string moleculeName = string.Empty;

                // Act
                Action action = () =>
                         new CalcOrderItem(0, "",
                                        new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties));

                // Assert
                Assert.Throws<ArgumentException>(action);
            }

            [Fact]
            public void Should_Initialise_Members_When_MoleculeName_Is_Valid()
            {
                // Arrange
                string moleculeName = "MoleculeName";

                // Act
                CalcOrderItem item = new CalcOrderItem(0, "",
                                        new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties));

                // Assert
                Assert.Equal(0, item.Id);
                Assert.Equal(moleculeName, item.MoleculeName);
                Assert.NotNull(item.Details);
            }
        }

        public class UpdateDetailsTests : CalcOrderItemTests
        {
            [Fact]
            public void Should_Throw_ArgumentNullException_When_Details_Is_Null()
            {
                // Arrange
                CalcOrderItem item = new CalcOrderItem(0, "",
                                        new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties));
                CalcOrderItemDetails details = null;

                // Act
                Action action = () => item.UpdateDetails(details);

                // Assert
                Assert.Throws<ArgumentNullException>(action);
            }

            [Fact]
            public void Should_Update_Details_When_Details_Is_Valid()
            {
                // Arrange
                CalcOrderItem item = new CalcOrderItem(0, "",
                                        new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties));
                CalcOrderItemDetails details = new CalcOrderItemDetails(0, "", CalcBasisSetCode.BSTO3G, CalcOrderItemType.MolecularProperties);

                // Act
                item.UpdateDetails(details);

                // Assert
                Assert.Equal(details, item.Details);
            }
        }

    }
}
