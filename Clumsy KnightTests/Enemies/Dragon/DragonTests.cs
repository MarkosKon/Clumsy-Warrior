using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clumsy_Knight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
namespace Clumsy_Knight.Tests
{
    [TestClass()]
    public class DragonTests
    {
        [TestMethod()]
        public void DragonTest()
        {
            Dragon dragon = new Dragon(DifficultyLevel.normal, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(dragon.health, 300);
            Assert.AreEqual(dragon.damage, 2);
            dragon = new Dragon(DifficultyLevel.hard, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(dragon.health, 500);
            Assert.AreEqual(dragon.damage, 4);
        }
    }
}
