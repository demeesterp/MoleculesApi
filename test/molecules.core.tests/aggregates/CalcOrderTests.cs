﻿using molecules.core.aggregates;

namespace molecules.core.tests.aggregates
{
    public class CalcOrderTests
    {
        public class ConstructorTests : CalcOrderTests
        {
            [Fact]
            public void Should_Initialise_Members_When_DefaultConstructor_Called()
            {
                // Arrange

                // Act
                CalcOrder calcOrder = new CalcOrder();

                // Assert
                Assert.Equal(0, calcOrder.Id);
                Assert.Equal("Default", calcOrder.CustomerName);
                Assert.Equal(string.Empty, calcOrder.Details.Name);
                Assert.Equal(string.Empty, calcOrder.Details.Description);
                Assert.NotNull(calcOrder.Items);
            }

            [Fact]
            public void Should_Initialise_Name_When_Constructor_With_Name_Parameter_Is_Called()
            {
                // Arrange
                string name = "Test Name";

                // Act
                CalcOrder calcOrder = new CalcOrder(name);

                // Assert
                Assert.Equal(0, calcOrder.Id);
                Assert.Equal("Default", calcOrder.CustomerName);
                Assert.Equal(name, calcOrder.Details.Name);
                Assert.Equal(string.Empty, calcOrder.Details.Description);
            }

            [Fact]
            public void Should_Initialise_Name_And_Description_When_Constructor_With_Name_ANd_DescriptionParameter_Is_Called()
            {
                // Arrange
                string name = "Test Name";
                string description = "Test Description";

                // Act
                CalcOrder calcOrder = new CalcOrder(name, description);

                // Assert
                Assert.Equal(0, calcOrder.Id);
                Assert.Equal(name, calcOrder.Details.Name);
                Assert.Equal("Default", calcOrder.CustomerName);
                Assert.Equal(description, calcOrder.Details.Description);
            }

            [Theory]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData("\r")]
            [InlineData("\r\n")]
            [InlineData("\t")]
            public void Should_Throw_When_Contractor_With_Invalid_Name_Is_Called(string name)
            {
                // Arrange
                Assert.Throws<ArgumentException>(() => new CalcOrder(name));
            }
        }



        public class AddItemTests : CalcOrderTests
        {
            [Fact]
            public void Should_Throw_When_AddItem_With_Null_Parameter_Is_Called()
            {
                // Arrange
                CalcOrder calcOrder = new CalcOrder();

                CalcOrderItem? toAdd = null;

                // Act
                _ = Assert.Throws<ArgumentNullException>(() => calcOrder.AddItem(toAdd));
            }
            [Fact]
            public void Should_Add_Item_ToList_WhenItem_Is_Valid()
            {
                // Arrange
                CalcOrder calcOrder = new CalcOrder();

                CalcOrderItem toAdd = new CalcOrderItem("Test Molecule");

                // Act
                calcOrder.AddItem(toAdd);

                // Assert
                Assert.Single(calcOrder.Items);
                Assert.Equal(toAdd, calcOrder.Items[0]);
            }
        }


        public class RemoveItemTests : CalcOrderTests
        {
            [Fact]
            public void Should_Remove_Item_FromList_WhenItem_Is_Valid()
            {
                // Arrange
                CalcOrder calcOrder = new CalcOrder();

                CalcOrderItem toAdd = new CalcOrderItem("Test Molecule");
                toAdd.Id = 100;
                calcOrder.AddItem(toAdd);

                // Act
                calcOrder.RemoveItem(toAdd.Id);

                // Assert
                Assert.Empty(calcOrder.Items);
            }

            [Fact]
            public void Should_Not_Remove_Item_FromList_WhenItem_Is_Not_Found()
            {
                // Arrange
                CalcOrder calcOrder = new CalcOrder();

                CalcOrderItem toAdd = new CalcOrderItem("Test Molecule");
                calcOrder.AddItem(toAdd);

                // Act
                calcOrder.RemoveItem(999);

                // Assert
                Assert.Single(calcOrder.Items);
            }   
        }

    }
}