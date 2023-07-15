using molecules.core.services.validators;
using molecules.core.valueobjects.CalcOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.services.validators
{
    public class CreateCalcOrderValidatorTests
    {

        [Fact]
        public void CalcOrderDetailsValidator_WhenNameIsEmpty_ReturnsError()
        {
            // Arrange
            var calcOrderDetails = new CreateCalcOrder(null, String.Empty);
            var validator = new CreateCalcOrderValidator();

            // Act
            var result = validator.Validate(calcOrderDetails);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Name is required", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CalcOrderDetailsValidator_WhenNameIsWhitespace_ReturnsError()
        {
            // Arrange
            var calcOrderDetails = new CreateCalcOrder(String.Empty, String.Empty);
            var validator = new CreateCalcOrderValidator();

            // Act
            var result = validator.Validate(calcOrderDetails);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Name is required", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CalcOrderDetailsValidator_WhenNameIsTooLong_ReturnsError()
        {
            // Arrange
            var calcOrderDetails = new CreateCalcOrder(new string('t', 251),string.Empty);
            var validator = new CreateCalcOrderValidator();

            // Act
            var result = validator.Validate(calcOrderDetails);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Name cannot be longer than 250 characters", result.Errors[0].ErrorMessage);
        }

    }
}
