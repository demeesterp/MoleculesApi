using FakeItEasy;
using FluentAssertions;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.Factories;
using molecules.core.valueobjects;
using molecules.core.valueobjects.CalcOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace molecules.core.tests.factories
{
    public class CalcOrderFactoryTests
    {
        #region dependencies

        private readonly ICalcOrderItemFactory _calcOrderItemFactory;

        private readonly CalcOrderFactory calcOrderFactory;

        #endregion

        public CalcOrderFactoryTests()
        {
            _calcOrderItemFactory = A.Fake<ICalcOrderItemFactory>();
            calcOrderFactory = new CalcOrderFactory(_calcOrderItemFactory);
        }

        public static CalcOrderDbEntity CreateDummyCalcOrderDbEntity(List<CalcOrderItemDbEntity> items)
        {
            CalcOrderDbEntity retval = new CalcOrderDbEntity();
            retval.Id = 1;
            retval.Name = "Test";
            retval.Description = "Test";
            retval.CalcOrderItems = items;
            return retval;
        }

        [Fact]
        public void Should_Create_Valid_CalcOrder_From_Valid_CalcOrderItemDbEntity()
        {
            // Arrange
            var dbEntity = CreateDummyCalcOrderDbEntity(new List<CalcOrderItemDbEntity>()
            {
                CalcOrderItemFactoryTests.CreateDummyCalcOrderItem(CalcOrderItemType.AllKinds.ToString())
            });

            // Act
            var result = calcOrderFactory.CreateCalcOrder(dbEntity);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(dbEntity.Id);
            result.Details.Name.Should().Be(dbEntity.Name);
            result.Details.Description.Should().Be(dbEntity.Description);

            A.CallTo(() => _calcOrderItemFactory.CreateCalcOrderItem(A<CalcOrderItemDbEntity>.Ignored))
                .MustHaveHappenedOnceExactly();
        }


    }
}
