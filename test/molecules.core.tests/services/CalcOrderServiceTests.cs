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
using molecule.infrastructure.data.interfaces.Repositories;
using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecules.core.tests.services
{
    public class CalcOrderServiceTests
    {
        private readonly ILogger<CalcOrderService> _logger;

        private readonly ICalcOrderServiceValidations _validations;

        private readonly ICalcOrderRepository _repository;

        private readonly CalcOrderService _calcOrderService;

        private readonly string _name = "Name";

        private readonly string _description = "Description";


        public CalcOrderServiceTests()
        {
            _logger = A.Fake<ILogger<CalcOrderService>>();
            _validations = A.Fake<ICalcOrderServiceValidations>();
            _repository = A.Fake<ICalcOrderRepository>();

            _calcOrderService = new CalcOrderService(_validations, _repository, _logger);
        }


        public class CalcOrderServiceConstructorTests : CalcOrderServiceTests
        {
            [Fact]
            public void CalcOrderServiceConstructor_WhenValidParametersPassed_ThenNoExceptionThrown()
            {
                // Arrange

                // Act
                var result = new CalcOrderService(_validations, _repository, _logger);
                
                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void CalcOrderServiceConstructor_WhenNullValidationsPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = Assert.Throws<ArgumentNullException>(() => new CalcOrderService(null, _repository, _logger));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'validations')", result.Message);
            }

            [Fact]
            public void CalcOrderServiceConstructor_WhenNullLoggerPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = Assert.Throws<ArgumentNullException>(() => new CalcOrderService(_validations, _repository, null));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'logger')", result.Message);
            }

            [Fact]
            public void CalcOrderServiceConstructor_WhenNullRepositoryPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = Assert.Throws<ArgumentNullException>(() => new CalcOrderService(_validations, null, _logger));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'calcOrderRepository')", result.Message);
            }
        }

        public class CalcOrderServiceCreateTests : CalcOrderServiceTests
        {
         
            public CalcOrderServiceCreateTests() : base()
            {
                A.CallTo(() => _repository.CreateAsync(A<CalcOrderDbEntity>.Ignored))
                    .Returns(new CalcOrderDbEntity()
                    {
                        Name = _name
                    });
            }


            [Fact]
            public async Task CalcOrderServiceCreate_WhenValidParametersPassed_ThenNoExceptionThrown()
            {
                // Arrange
                var createCalcOrder = new CreateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.CreateAsync(createCalcOrder);

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public async Task CalcOrderServiceCreate_WhenNullCreateCalcOrderPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _calcOrderService.CreateAsync(null));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'createCalcOrder')", result.Message);
            }
            
            [Fact]
            public async Task CalcOrderServiceCreate_WhenValidParametersPassed_ThenValidatorCalled()
            {
                // Arrange
                var createCalcOrder = new CreateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.CreateAsync(createCalcOrder);

                // Assert
                A.CallTo(() => _validations.Validate(createCalcOrder)).MustHaveHappened();
            }


            [Fact]
            public async Task CalcOrderServiceCreate_WhenValidParametersPassed_Then_Respository_CreateAsyncCalled()
            {
                // Arrange
                var createCalcOrder = new CreateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.CreateAsync(createCalcOrder);

                // Assert
                A.CallTo(() => _repository.CreateAsync(A<CalcOrderDbEntity>.That.Matches(i =>i.Name == "Name" && i.Description == "Description"))).MustHaveHappened();
            }

            [Fact]
            public async Task CalcOrderServiceCreate_WhenValidParametersPassed_Then_Respository_SaveChangesAsyncCalled()
            {
                // Arrange
                var createCalcOrder = new CreateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.CreateAsync(createCalcOrder);

                // Assert
                A.CallTo(() => _repository.SaveChangesAsync()).MustHaveHappened();
            }
        }

        public class CalcOrderServiceUpdateTests : CalcOrderServiceTests
        {
            public CalcOrderServiceUpdateTests() : base()
            {
                A.CallTo(() => _repository.UpdateAsync(A<int>.Ignored, A<string>.Ignored, A<string>.Ignored))
                    .Returns(new CalcOrderDbEntity()
                    {
                        Name = _name,
                        Description = _description
                    });
            }

            [Fact]
            public async Task CalcOrderServiceUpdate_WhenValidParametersPassed_ThenNoExceptionThrown()
            {
                // Arrange
                var updateCalcOrder = new UpdateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.UpdateAsync(1, updateCalcOrder);

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public async Task CalcOrderServiceUpdate_WhenNullUpdateCalcOrderPassed_ThenArgumentNullExceptionThrown()
            {
                // Arrange

                // Act
                var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _calcOrderService.UpdateAsync(0, null));

                // Assert
                Assert.Equal("Value cannot be null. (Parameter 'updateCalcOrder')", result.Message);
            }


            [Fact]
            public async Task CalcOrderServiceCreate_WhenValidParametersPassed_ThenValidatorCalled()
            {
                // Arrange
                var createCalcOrder = new CreateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.CreateAsync(createCalcOrder);

                // Assert
                A.CallTo(() => _validations.Validate(createCalcOrder)).MustHaveHappened();
            }


            [Fact]
            public async Task CalcOrderServiceUpdate_WhenValidParametersPassed_Then_Respository_UpdateAsyncCalled()
            {
                // Arrange
                var updateCalcOrder = new UpdateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.UpdateAsync(0, updateCalcOrder);

                // Assert
                A.CallTo(() => _repository.UpdateAsync(0, _name, _description)).MustHaveHappened();
            }

            [Fact]
            public async Task CalcOrderServiceUpdate_WhenValidParametersPassed_Then_Respository_SaveChangesAsyncCalled()
            {
                // Arrange
                var updateCalcOrder = new UpdateCalcOrder(_name, _description);

                // Act
                var result = await _calcOrderService.UpdateAsync(0, updateCalcOrder);

                // Assert
                A.CallTo(() => _repository.SaveChangesAsync()).MustHaveHappened();
            }
        }

        public class CalcOrderServiceDeleteTests : CalcOrderServiceTests
        {

            [Fact]
            public async Task CalcOrderServiceDelete_Then_Respository_DeleteAsyncCalled()
            {
                // Arrange

                // Act
                await _calcOrderService.DeleteAsync(0);

                // Assert
                A.CallTo(() => _repository.DeleteAsync(0)).MustHaveHappened();
            }

            [Fact]
            public async Task CalcOrderServiceDelete_Then_Respository_SaveChangesAsyncCalled()
            {
                // Arrange

                // Act
                await _calcOrderService.DeleteAsync(0);

                // Assert
                A.CallTo(() => _repository.SaveChangesAsync()).MustHaveHappened();
            }
        }   


        public class CalcOrderServiceGetTests : CalcOrderServiceTests
        {

            [Fact]
            public async Task CalcOrderServiceGet_Then_Respository_GetAsyncCalled()
            {
                // Arrange

                // Act
                await _calcOrderService.GetAsync(0);

                // Assert
                A.CallTo(() => _repository.GetByIdAsync(0)).MustHaveHappened();
            }

        }

        public class CalcOrderServiceGetByNameTests : CalcOrderServiceTests
        {

            [Fact]
            public async Task CalcOrderServiceGetByName_Then_Respository_GetByNameAsyncCalled()
            {
                // Arrange

                // Act
                await _calcOrderService.GetByNameAsync(_name);

                // Assert
                A.CallTo(() => _repository.GetByNameAsync(_name)).MustHaveHappened();
            }   
            
        }

        public class CalcOrderServiceListTests : CalcOrderServiceTests
        {
            [Fact]
            public async Task CalcOrderServiceList_Then_Respository_ListAsyncCalled()
            {
                // Arrange

                // Act
                await _calcOrderService.ListAsync();

                // Assert
                A.CallTo(() => _repository.GetAllAsync()).MustHaveHappened();
            } 
        }

    }
}
