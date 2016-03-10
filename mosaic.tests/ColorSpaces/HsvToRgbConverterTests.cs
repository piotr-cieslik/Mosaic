using Microsoft.VisualStudio.TestTools.UnitTesting;
using mosaic.ColorSpaces;

namespace mosaic.tests.ColorSpaces
{
    [TestClass]
    public class HsvToRgbConverterTests
    {
        private static int TestResolution = 16;

        [TestMethod]
        public void ShouldConvertHsvToRgb()
        {
            for (int r = 0; r < 256; r += TestResolution)
                for (int g = 0; g < 256; g += TestResolution)
                    for (int b = 0; b < 256; b += TestResolution)
                    {
                        var rgb = new Rgb((byte)r, (byte)g, (byte)b);
                        var hsv = rgb.ToHsv();
                        var rgb2 = hsv.ToRgb();
                        Assert.AreEqual(rgb, rgb2);
                    }
        }
    }
}