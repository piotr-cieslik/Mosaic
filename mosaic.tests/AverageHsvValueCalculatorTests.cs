using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace mosaic.tests
{
    [TestClass]
    public class AverageHsvValueCalculatorTests
    {
        //0 0
        //0 0
        [TestMethod]
        public void ShouldReturn_0_WhenWholeImageIsBlack()
        {
            var bitmap = new Bitmap(2, 2);
            var graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.Black, 0, 0, 2, 2);

            var result = AverageHsvValueCalculator.Calculate(bitmap);
            var expected = 0;

            Assert.AreEqual(expected, result);
        }

        //255 0
        //255 0
        [TestMethod]
        public void ShouldReturn_128_WhenHalfOfImageIsWhiteAndHalfOfItIsBlack()
        {
            var bitmap = new Bitmap(2, 2);
            var graphic = Graphics.FromImage(bitmap);

            graphic.FillRectangle(Brushes.White, 0, 0, 1, 2);
            graphic.FillRectangle(Brushes.Black, 1, 0, 1, 2);

            var result = AverageHsvValueCalculator.Calculate(bitmap);
            var expected = 127;

            Assert.AreEqual(expected, result);
        }

        //255 255
        //255 255
        [TestMethod]
        public void ShouldReturn_255_WhenWholeImageIsWhite()
        {
            var bitmap = new Bitmap(2, 2);
            var graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 2, 2);

            var result = AverageHsvValueCalculator.Calculate(bitmap);
            var expected = 255;

            Assert.AreEqual(expected, result);
        }
    }
}