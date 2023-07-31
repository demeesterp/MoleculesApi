using FakeItEasy;
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
        public class CalcOrderItemServiceValidationsConstructorTests : CalcOrderItemServiceValidationsTests
        {
            [Fact]
            public void CalcOrderItemServiceValidationsConstructorTests_WhenCreateValidatorIsNull_ThrowsArgumentNullException()
            {
                // Arrange
                IValidator<CreateCalcOrderItem> createvalidator = null;
                IValidator<UpdateCalcOrderItem> updateValidator = A.Fake<IValidator<UpdateCalcOrderItem>>();

                // Act
                Action act = () => new CalcOrderItemServiceValidations(createvalidator, updateValidator);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void CalcOrderItemServiceValidationsConstructorTests_WhenUpdateValidatorIsNull_ThrowsArgumentNullException()
            {
                // Arrange
                IValidator<CreateCalcOrderItem> createvalidator = A.Fake<IValidator<CreateCalcOrderItem>>();
                IValidator<UpdateCalcOrderItem> updateValidator = null;

                // Act
                Action act = () => new CalcOrderItemServiceValidations(createvalidator, updateValidator);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }
        }
    }
}
