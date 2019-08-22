using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.ColorSpaces;

namespace Mosaic.Tests.ColorSpaces
{
    [TestClass]
    public class RgbToHsvConverterTests
    {
        [TestMethod]
        [Ignore]
        public void ShouldConvertFromRgbToHsv()
        {
            for (var h = 0; h < 360; h++)
                for (var s = 0f; s <= 1; s += 0.1f)
                    for (var v = 0f; v <= 1; v += 0.1f)
                    {
                        var hsv = new Hsv(h, s, v);
                        var rgb = hsv.ToRgb();
                        var hsv2 = rgb.ToHsv();

                        Assert.AreEqual(hsv.H, hsv2.H);
                    }
        }
    }
}