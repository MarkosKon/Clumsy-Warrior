using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clumsy_Knight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Clumsy_Knight.Tests
{
    [TestClass()]
    public class SkeletonTests
    {
        [TestMethod()]
        public void SkeletonTest()
        {
            Skeleton skeleton = new Skeleton(DifficultyLevel.normal, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(skeleton.health, 80);
            Assert.AreEqual(skeleton.damage, 2);
            skeleton = new Skeleton(DifficultyLevel.hard, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(skeleton.health, 200);
            Assert.AreEqual(skeleton.damage, 3);
        }
    }
}
