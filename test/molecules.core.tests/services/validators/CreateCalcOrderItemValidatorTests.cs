using molecules.core.services.validators;
using molecules.core.valueobjects.CalcOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.services.validators
{
    public class CreateCalcOrderItemValidatorTests
    {

        [Fact]
        public void CreateCalcOrderItemValidator_WhenMoleculeNameIsEmpty_ReturnsError()
        {
            // Arrange
            var createCalcOrderItem = new CreateCalcOrderItem("",null);
            var validator = new CreateCalcOrderItemValidator();

            // Act
            var result = validator.Validate(createCalcOrderItem);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("MoleculeName is required", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CreateCalcOrderItemValidator_WhenMoleculeNameIsWhitespace_ReturnsError()
        {
            // Arrange
            var createCalcOrderItem = new CreateCalcOrderItem(" ", null);
            var validator = new CreateCalcOrderItemValidator();

            // Act
            var result = validator.Validate(createCalcOrderItem);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("MoleculeName is required", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CreateCalcOrderItemValidator_WhenMoleculeNameIsTooLong_ReturnsError()
        {
            // Arrange
            var createCalcOrderItem = new CreateCalcOrderItem(new string('t', 251),null);
            var validator = new CreateCalcOrderItemValidator();

            // Act
            var result = validator.Validate(createCalcOrderItem);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("MoleculeName cannot be longer than 250 characters", result.Errors[0].ErrorMessage);
        }

    }
}
