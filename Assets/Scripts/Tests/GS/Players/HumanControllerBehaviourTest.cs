using GS.Hex;
using GS.Players;
using GS.Players.Controllers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GS.Players
{
    public class HumanControllerBehaviourTest
    {
        private IHumanController _controller;
        private IPlayer _player;
        private IHexGrid _grid;

        [SetUp]
        public void Setup()
        {
            _grid = Substitute.For<IHexGrid>();
            _player = Substitute.For<IPlayer>();

            _player.Grid.Returns(_grid);
            
            _controller = new HumanController(_player);
        }

        [Test]
        public void ItShouldLocateTheClickedCell()
        {
            var point = new Vector3(0, 0, 0);
            var cell = Substitute.For<IHexCell>();
            _grid.GetCell(Arg.Any<Vector3>()).Returns(cell);
                
            _controller.OnTouch(point);

            // locates the cell
            _grid.GetCell(point).Received();
        }

        [TearDown]
        public void TearDown()
        {
            _player = null;
            _grid = null;
        }
    }
}
