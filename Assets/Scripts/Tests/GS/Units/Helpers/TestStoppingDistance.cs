using System.Collections;
using GS.Units;
using GS.Units.Helpers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.GS.Units.Helpers
{
    public class TestStoppingDistance
    {
        // A Test behaves as an ordinary method
        [Test]
        public void MapIsSetupCorrectly()
        {
            Assert.AreEqual(2, StoppingDistance.StoppingDistanceMap[1]);
            Assert.AreEqual(3, StoppingDistance.StoppingDistanceMap[4]);
            Assert.AreEqual(4, StoppingDistance.StoppingDistanceMap[12]);
            Assert.AreEqual(5, StoppingDistance.StoppingDistanceMap[20]);
            Assert.AreEqual(6, StoppingDistance.StoppingDistanceMap[28]);
            Assert.AreEqual(7, StoppingDistance.StoppingDistanceMap[48]);
        }

        [Test]
        public void FindKeyReturnsNearestLowestResult()
        {
            Assert.AreEqual(2, StoppingDistance.FindByKey(1));
            Assert.AreEqual(2, StoppingDistance.FindByKey(2));
            Assert.AreEqual(2, StoppingDistance.FindByKey(3));
            
            Assert.AreEqual(3, StoppingDistance.FindByKey(4));
            Assert.AreEqual(3, StoppingDistance.FindByKey(8));
            Assert.AreEqual(3, StoppingDistance.FindByKey(11));
            
            Assert.AreEqual(4, StoppingDistance.FindByKey(12));
            Assert.AreEqual(4, StoppingDistance.FindByKey(15));
            Assert.AreEqual(4, StoppingDistance.FindByKey(19));
            
            Assert.AreEqual(5, StoppingDistance.FindByKey(20));
            Assert.AreEqual(5, StoppingDistance.FindByKey(25));
            Assert.AreEqual(5, StoppingDistance.FindByKey(27));
            
            Assert.AreEqual(6, StoppingDistance.FindByKey(28));
            Assert.AreEqual(6, StoppingDistance.FindByKey(35));
            Assert.AreEqual(6, StoppingDistance.FindByKey(47));
            
            Assert.AreEqual(7, StoppingDistance.FindByKey(48));
        }

        [Test]
        public void FindKeyReturnsNearestLowestResultEvenIfKeyIsTooSmall()
        {
            Assert.AreEqual(2, StoppingDistance.FindByKey(0));
        }
        
        [Test]
        public void FindKeyReturnsNearestLowestResultEvenIfKeyIsTooLarge()
        {
            Assert.AreEqual(7, StoppingDistance.FindByKey(49));
            Assert.AreEqual(7, StoppingDistance.FindByKey(123));
        }

        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator SomeWithEnumeratorPasses()
        // {
        //     GameObject gameGameObject = 
        //         Object.Instantiate(Resources.Load<GameObject>("Test/Unit"));
        //     //
        //     var unit = gameGameObject.GetComponent<Unit>();
        //     // var position = unit.transform.position + new Vector3(10, 0, 10);
        //     // unit.MoveTowards(position);
        //     //
        //     yield return new WaitForSeconds(0.1f);
        //     // // 6
        //     // Assert.Equals(unit.transform.position, position);
        //     // // 7
        //     Object.Destroy(unit.gameObject);
        // }
    }
}
