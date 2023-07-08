using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using molecules.core.services.validators.servicehelpers;
using molecules.core.valueobjects.CalcOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.services.validators.servicehelpers
{
    public class CalcOrderServiceValidationsTests
    {
        #region dependencies

        private readonly IValidator<CreateCalcOrder> _createCalcOrderValidator;
        private readonly IValidator<UpdateCalcOrder> _updateCalcOrderValidator;
        private CalcOrderServiceValidations Service { get; set; }

        #endregion

        public CalcOrderServiceValidationsTests() 
        { 

            this._updateCalcOrderValidator = A.Fake<IValidator<UpdateCalcOrder>>();
            this._createCalcOrderValidator = A.Fake<IValidator<CreateCalcOrder>>();
            this.Service = new CalcOrderServiceValidations(this._createCalcOrderValidator, this._updateCalcOrderValidator);   
        }

        public class CalcOrderServiceValidationsConstructorTests : CalcOrderServiceValidationsTests
        {

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCreateCalcOrderValidatorIsNull()
            {
                // Arrange
                IValidator<CreateCalcOrder> createCalcOrderValidator = null;
                IValidator<UpdateCalcOrder> updateCalcOrderValidator = A.Fake<IValidator<UpdateCalcOrder>>();

                // Act
                Action act = () => new CalcOrderServiceValidations(createCalcOrderValidator, updateCalcOrderValidator);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenUpdateCalcOrderValidatorIsNull()
            {
                // Arrange
                IValidator<CreateCalcOrder> createCalcOrderValidator = A.Fake<IValidator<CreateCalcOrder>>();
                IValidator<UpdateCalcOrder> updateCalcOrderValidator = null;

                // Act
                Action act = () => new CalcOrderServiceValidations(createCalcOrderValidator, updateCalcOrderValidator);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }
        
        }

        [Fact]
        public void ShouldValidateCreateCalcOrder()
        {
            // Arrange
            var createCalcOrder = A.Fake<CreateCalcOrder>();

            // Act
            this.Service.Validate(createCalcOrder);

            // Assert
            A.CallTo(() => this._createCalcOrderValidator.ValidateAndThrow(createCalcOrder)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ShouldValidateUpdateCalcOrder()
        {
            // Arrange
            var updateCalcOrder = A.Fake<UpdateCalcOrder>();

            // Act
            this.Service.Validate(updateCalcOrder);

            // Assert
            A.CallTo(() => this._updateCalcOrderValidator.ValidateAndThrow(updateCalcOrder)).MustHaveHappenedOnceExactly();
        }

    }
}
