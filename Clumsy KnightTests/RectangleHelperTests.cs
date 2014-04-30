namespace Clumsy_Knight.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    [TestClass()]
    public class RectangleHelperTests
    {
        [TestMethod()]
        public void isOnTopOfTest()
        {
            Rectangle rect1 = new Rectangle();
            Rectangle rect2 = new Rectangle();
            for (int i=-100; i<=100; i++)
            {
                for (int j = -5; j <= 5; j++)
                { 
                    rect1 = new Rectangle(i, j, 100, 100);
                    rect2 = new Rectangle(0, 100, 100, 100);
                    Assert.IsTrue(rect1.isOnTopOf(rect2));
                }
            }
        }

        [TestMethod()]
        public void isOnBottomOfTest()
        {
            Rectangle rect1 = new Rectangle();
            Rectangle rect2 = new Rectangle();
            for (int i = -100; i <= 100; i++)
            {
                for (int j = -5; j <= 5; j++)
                {
                    rect1 = new Rectangle(i, j, 100, 100);
                    rect2 = new Rectangle(0, -100, 100, 100);
                    Assert.IsTrue(rect1.isOnBottomOf(rect2));
                }
            }
        }

        [TestMethod()]
        public void isOnLeftOfTest()
        {
            Rectangle rect1 = new Rectangle();
            Rectangle rect2 = new Rectangle();
            for (int i = -5; i <= 5; i++)
            {
                for (int j = -40; j <= 40; j++)
                {
                    rect1 = new Rectangle(i, j, 100, 100);
                    rect2 = new Rectangle(100, 0, 100, 100);
                    Assert.IsTrue(rect1.isOnLeftOf(rect2));
                }
            }
        }

        [TestMethod()]
        public void isOnRightOfTest()
        {
            Rectangle rect1 = new Rectangle();
            Rectangle rect2 = new Rectangle();
            for (int i = -5; i <= 5; i++)
            {
                for (int j = -40; j <= 40; j++)
                {
                    rect1 = new Rectangle(i, j, 100, 100);
                    rect2 = new Rectangle(-100, 0, 100, 100);
                    Assert.IsTrue(rect1.isOnRightOf(rect2));
                }
            }
        }
    }
}
