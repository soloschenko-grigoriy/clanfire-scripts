using GS.Hex;
using GS.Players;
using GS.Units;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GS.Players
{
    public class PlayerTest
    {
        private IUnit _unit1;
        private IUnit _unit2;
        private Player _player;
        private IHexGrid _grid;

        [SetUp]
        public void Setup()
        {
            _unit1 = Substitute.For<IUnit>();
            _unit2 = Substitute.For<IUnit>();

            _unit1.GameObject.Returns(new GameObject());
            _unit2.GameObject.Returns(new GameObject());

            _grid = Substitute.For<IHexGrid>();

            _player = new Player(_grid);
        }

        [Test]
        public void TestUnitsGetSpawned()
        {
            // spawn
            _player.SpawnUnits(new []{_unit1, _unit2}, new HexCoordinates(0, 0));
            
            // should spawn every single unit
            _unit1.Received().Spawn(Arg.Any<HexCell>(), _player);
            _unit2.Received().Spawn(Arg.Any<HexCell>(), _player);
        }
        
        [Test]
        public void TestThatUnitsGetSelected()
        {
            // nothing selected by default
            Assert.IsNull(_player.SelectedUnit);
            
            // select
            _player.SelectUnit(_unit2);
            
            // unit gets selected
            Assert.That(_player.SelectedUnit, Is.EqualTo(_unit2));
            _unit2.Received().Select();
        }

        [TearDown]
        public void TearDown()
        {
            _player = null;
            _grid = null;
            _unit1 = null;
            _unit2 = null;
        }
    }
}

