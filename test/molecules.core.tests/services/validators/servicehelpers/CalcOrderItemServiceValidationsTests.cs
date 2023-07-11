using FluentAssertions;
using FluentValidation;
using molecules.core.services.validators.servicehelpers;
using molecules.core.valueobjects.CalcOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.services.validators.servicehelpers
{
    public class CalcOrderItemServiceValidationsTests
    {
        public class CalcOrderItemServiceValidationsConstrcutrTests : CalcOrderItemServiceValidationsTests
        {
            [Fact]
            public void CalcOrderItemServiceValidationsConstrcutrTests_WhenValidatorIsNull_ThrowsArgumentNullException()
            {
                // Arrange
                IValidator<CreateCalcOrderItem> validator = null;

                // Act
                Action act = () => new CalcOrderItemServiceValidations(validator);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }
        }
    }
}
