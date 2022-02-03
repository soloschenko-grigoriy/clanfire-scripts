using System.Collections.Generic;
using System.Linq;
using GS.Hex;
using GS.Units;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GS.Units
{
    public class UnitMovementTest
    {
        private const float Duration = 100;

        private IUnit _unit;
        private UnitMovement _movement;
        private List<IHexCell> _path;
        private IHexCell _currentCell;
        
        [SetUp]
        public void Setup()
        {
            _unit = Substitute.For<IUnit>();
            
            _movement = new GameObject().AddComponent<UnitMovement>();

            // mock current position
            _currentCell = TestUtils.MockCell();
            
            // mock path
            _path = TestUtils.MockPathFor(_unit, _currentCell);
        }

        [Test]
        public void TestItDoestMoveInitiallyAndSetsGetterWhenStarts()
        {
            // not yet moves
            Assert.IsFalse(_movement.InProgress);
            
            // started moving
            _movement.StartMoving(_unit, Duration);
            
            // check if movement has started
            Assert.IsTrue(_movement.InProgress);
        }

        [Test]
        public void TestItMovesTowardsNextCellWhenProgressionIsCalled()
        {
            // cache the next cell
            var nextCell = _path.Last();
            
            // start moving
            _movement.StartMoving(_unit, Duration);
            
            // should progress 75% forward
            const float progress = 0.75f;
            _movement.MakeProgress(TestUtils.GetFakeElapsed(progress, Duration));
            
            // moves 75% closer to the target cell
            Assert.That(_unit.Position, Is.EqualTo(Vector3.Lerp(
                _currentCell.Position,
                nextCell.Position,
                progress
            )));
        }
        
        [Test]
        public void TestItRelocatesUnitToTheNextCellWhenProgressedFully()
        {
            var nextCell = _path.Last();
            
            // start moving
            _movement.StartMoving(_unit, Duration);
            
            // check if movement has started
            Assert.IsTrue(_movement.InProgress);

            // should progress 100% forward
            const float progress = 1f;
            
            _movement.MakeProgress(TestUtils.GetFakeElapsed(progress, Duration));
            
            // updates unit's cell
            _unit.Received().SetCell(nextCell);
        }

        [TearDown]
        public void TearDown()
        {
            _unit = null;
            _movement = null;
            _path = null;
            _currentCell = null;
        }
    }
}
