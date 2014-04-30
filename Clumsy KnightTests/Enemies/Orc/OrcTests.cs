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
    public class OrcTests
    {
        [TestMethod()]
        public void OrcTest()
        {
            Orc orc = new Orc(DifficultyLevel.normal, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(orc.health, 200);
            Assert.AreEqual(orc.damage, 6);
            orc = new Orc(DifficultyLevel.hard, new Microsoft.Xna.Framework.Vector2(0, 0));
            Assert.AreEqual(orc.health, 300);
            Assert.AreEqual(orc.damage, 8);

        }
    }
}
