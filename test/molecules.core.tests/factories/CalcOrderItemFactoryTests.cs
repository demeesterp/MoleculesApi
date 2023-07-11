using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;
using molecules.core.Factories;
using molecules.core.valueobjects;

namespace molecules.core.tests.factories
{
    public class CalcOrderItemFactoryTests
    {

        public CalcOrderItemFactory calcOrderItemFactory { get; set; }

        public CalcOrderItemFactoryTests()
        {
            calcOrderItemFactory = new CalcOrderItemFactory();
        }

        public static CalcOrderItemDbEntity CreateDummyCalcOrderItem(string calcType)
        {
            CalcOrderItemDbEntity dbEntity = new CalcOrderItemDbEntity();
            dbEntity.XYZ = "Test";
            dbEntity.Charge = -1;
            dbEntity.CalcType = calcType;
            dbEntity.Id = 1;
            dbEntity.MoleculeName = "TestMolecule";
            dbEntity.CalcOrderId = 2;
            return dbEntity;
        }

        [Fact]
        public void CreateCalcOrderItem_Should_Return_Valid_CalcOrderItem_When_Valid_CalcType_Input()
        {
            // Arrange
            var dbEntity = CreateDummyCalcOrderItem(CalcType.NoGeoOpt.ToString());

            // Act
            var result = calcOrderItemFactory.CreateCalcOrderItem(dbEntity);

            // Assert
            result.Should().NotBeNull();

            result.MoleculeName.Should().Be(dbEntity.MoleculeName);
            result.Id.Should().Be(dbEntity.Id);
            result.Details.Charge.Should().Be(dbEntity.Charge);
            result.Details.XYZ.Should().Be(dbEntity.XYZ);
            result.Details.CalcType.ToString().Should().Be(dbEntity.CalcType);
        }

        [Fact]
        public void CreateCalcOrderItem_Should_Return_Valid_CalcOrderItem_With_GeoOpt_When_InValid_CalcType_Input()
        {

            // Arrange
            var dbEntity = CreateDummyCalcOrderItem("Test");

            // Act
            var result = calcOrderItemFactory.CreateCalcOrderItem(dbEntity);

            // Assert
            result.Should().NotBeNull();
            result.MoleculeName.Should().Be(dbEntity.MoleculeName);
            result.Id.Should().Be(dbEntity.Id);
            result.Details.Charge.Should().Be(dbEntity.Charge);
            result.Details.XYZ.Should().Be(dbEntity.XYZ);
            result.Details.CalcType.Should().Be(CalcType.GeoOpt);

        }

    }
}
