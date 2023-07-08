using Microsoft.Extensions.Logging;
using molecules.core.services.validators.servicehelpers;
using molecules.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.tests.services
{
    public class CalcOrderServiceTests
    {
        private readonly ILogger<CalcOrderService> _logger;

        private readonly ICalcOrderServiceValidations _validations;

        private readonly CalcOrderService _calcOrderService;


        public CalcOrderServiceTests()
        {
            _logger = A.Fake<ILogger<CalcOrderService>>();
            _validations = A.Fake<ICalcOrderServiceValidations>();

            _calcOrderService = new CalcOrderService(_validations, _logger);
        }


        public class CalcOrderServiceConstructorTests : CalcOrderServiceTests
        {
            [Fact]
            public void CalcOrderServiceConstructor_WhenValidParametersPassed_ThenNoExceptionThrown()
            {
                // Arrange

                // Act
                var result = new CalcOrderService(_validations, _logger);
                
                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void CalcOrderServiceConstructor_WhenNullValidationsPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = Assert.Throws<ArgumentNullException>(() => new CalcOrderService(null, _logger));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'validations')", result.Message);
            }

            [Fact]
            public void CalcOrderServiceConstructor_WhenNullLoggerPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = Assert.Throws<ArgumentNullException>(() => new CalcOrderService(_validations, null));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'logger')", result.Message);
            }
        }


        public class CalcOrderServiceCreateTests : CalcOrderServiceTests
        {
            
        }
        

        public class CalcOrderServiceGetTests : CalcOrderServiceTests
        {
            
        }

        public class CalcOrderServiceGetByNameTests : CalcOrderServiceTests
        {
            
        }

        public class CalcOrderServiceListTests : CalcOrderServiceTests
        {
            
        }

        public class CalcOrderServiceUpdateTests : CalcOrderServiceTests
        {
            
        }

    }
}
