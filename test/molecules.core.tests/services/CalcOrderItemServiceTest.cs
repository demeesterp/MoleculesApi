using Microsoft.Extensions.Logging;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.core.Factories;
using molecules.core.services.validators.servicehelpers;
using molecules.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using molecules.core.tests.factories;
using molecules.core.valueobjects;
using molecules.core.aggregates;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.valueobjects.CalcOrderItem;
using molecules.core.valueobjects.BasisSet;

namespace molecules.core.tests.services
{
    public class CalcOrderItemServiceTest
    {
        private readonly ICalcOrderItemFactory              _calcOrderItemFactory;

        private readonly ICalcOrderItemRepository           _calcOrderItemRepository;

        private readonly ICalcOrderItemServiceValidations   _calcOrderItemServiceValidations;

        private readonly ILogger<CalcOrderItemService>      _logger;

        private readonly CalcOrderItemService               _calcOrderItemService;

        public CalcOrderItemServiceTest()
        {
            _calcOrderItemFactory               = A.Fake<ICalcOrderItemFactory>();
            _calcOrderItemRepository            = A.Fake<ICalcOrderItemRepository>();
            _calcOrderItemServiceValidations    = A.Fake<ICalcOrderItemServiceValidations>();
            _logger                             = A.Fake<ILogger<CalcOrderItemService>>();

            _calcOrderItemService = new CalcOrderItemService(_calcOrderItemRepository,
                                                               _calcOrderItemFactory,
                                                                     _calcOrderItemServiceValidations,
                                                                            _logger);
        }

        public static CreateCalcOrderItem CreateFromCalcOrderItemDbEntity(CalcOrderItemDbEntity dbEntity)
        {
            CreateCalcOrderItem retval = new CreateCalcOrderItem();
            retval.MoleculeName 
                = dbEntity.MoleculeName;
            retval.Details.Charge 
                = dbEntity.Charge;
            retval.Details.XYZ 
                = dbEntity.XYZ;
            
            if (Enum.TryParse(dbEntity.CalcType, out CalcType calcType))
            {
                retval.Details.CalcType = calcType;
            }
            
            retval.Details.BasisSetCode
                = Enum.Parse<CalcBasisSetCode>(dbEntity.BasissetCode);
            
            return retval;
        }

        public class CalcOrderItemServiceConstructorTest : CalcOrderItemServiceTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCalcOrderItemFactoryIsNull()
            {
                // Arrange
                ICalcOrderItemFactory calcOrderItemFactory = null;

                // Act
                Action act = () => new CalcOrderItemService(_calcOrderItemRepository,
                                                              calcOrderItemFactory,
                                                                 _calcOrderItemServiceValidations,
                                                                         _logger);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenLoggerIsNull()
            {
                // Arrange
                ILogger<CalcOrderItemService> logger = null;

                // Act
                Action act = () => new CalcOrderItemService(_calcOrderItemRepository,
                                                                _calcOrderItemFactory,
                                                                   _calcOrderItemServiceValidations,
                                                                      logger);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCalcOrderItemRepositoryIsNull()
            {
                // Arrange
                ICalcOrderItemRepository calcOrderItemRepository = null;

                // Act
                Action act = () => new CalcOrderItemService(calcOrderItemRepository,
                                                                 _calcOrderItemFactory,
                                                                     _calcOrderItemServiceValidations,
                                                                       _logger);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCalcOrderItemServiceValidationsIsNull()
            {
                // Arrange
                ICalcOrderItemServiceValidations calcOrderItemServiceValidations = null;

                // Act
                Action act = () => new CalcOrderItemService(_calcOrderItemRepository,
                                                              _calcOrderItemFactory,
                                                                 calcOrderItemServiceValidations,
                                                                   _logger);

                // Assert
                act.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenAllDependenciesAreNotNull()
            {
                // Arrange
                // Act
                Action act = () => new CalcOrderItemService(_calcOrderItemRepository,
                                                               _calcOrderItemFactory,
                                                                 _calcOrderItemServiceValidations,
                                                                     _logger);

                // Assert
                act.Should().NotThrow<Exception>();
            }

        }

        public class CalcOrderItemServiceCreateAsyncTest : CalcOrderItemServiceTest
        {

            public CalcOrderItemServiceCreateAsyncTest() : base()
            {
                A.CallTo(() => _calcOrderItemRepository.CreateAsync(A<CalcOrderItemDbEntity>.Ignored))
                    .Returns(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
            }

            [Fact]
            public async Task ShouldCallValidateOnCalcOrderItemServiceValidations()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemServiceValidations.Validate(A<CreateCalcOrderItem>.Ignored)).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallCreateAsyncOnRepository()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity( CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.CreateAsync(A<CalcOrderItemDbEntity>.Ignored)).MustHaveHappenedOnceExactly();
            }

            

            [Fact]
            public async Task ShouldCallCreateAsyncOnRepositoryWithCorrectCalcOrderItemDbEntity()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.CreateAsync(A<CalcOrderItemDbEntity>.That
                    .Matches(dbEntity => dbEntity.CalcOrderId == calcOrderId))).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallCreateAsyncOnRepositoryWithCorrectCalcOrderItemDbEntityCalcType()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.CreateAsync(A<CalcOrderItemDbEntity>.That
                    .Matches(dbEntity => dbEntity.CalcType == createCalcOrderItem.Details.CalcType.ToString()))).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallCreateAsyncOnRepositoryWithCorrectCalcOrderItemDbEntityCharge()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.CreateAsync(A<CalcOrderItemDbEntity>.That
                    .Matches(dbEntity => dbEntity.Charge == createCalcOrderItem.Details.Charge))).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallCreateCalcOrderItemOnFactory()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemFactory.CreateCalcOrderItem(A<CalcOrderItemDbEntity>.Ignored)).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallSaveChangesAsyncOnRepository()
            {
                // Arrange
                var createCalcOrderItem = CreateFromCalcOrderItemDbEntity(CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcType.GeoOpt.ToString()));
                int calcOrderId = 1;

                // Act
                await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            }

        }

        public class CalcOrderItemServiceDeleteAsyncTest : CalcOrderItemServiceTest
        {

            [Fact]
            public async Task ShouldCallDeleteAsyncOnRepository()
            {
                // Arrange
                int calcOrderItemId = 1;

                // Act
                await _calcOrderItemService.DeleteAsync(calcOrderItemId);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.DeleteAsync(calcOrderItemId)).MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task ShouldCallSaveChangesAsyncOnRepository()
            {
                // Arrange
                int calcOrderItemId = 1;

                // Act
                await _calcOrderItemService.DeleteAsync(calcOrderItemId);

                // Assert
                A.CallTo(() => _calcOrderItemRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            }

        }
        
    }
}
