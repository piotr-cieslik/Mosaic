using Microsoft.VisualStudio.TestTools.UnitTesting;
using mosaic.ColorSpaces;

namespace mosaic.tests.ColorSpaces
{
    [TestClass]
    public class RgbToHsvConverterTests
    {
        //          Hex         (R, G, B)       (H,S,V)
        //Black	    #000000		(0,0,0)			(0º,0%,0%)
        //White	    #FFFFFF		(255,255,255)	(0º,0%,100%)
        //Red		#FF0000		(255,0,0)		(0º,100%,100%)
        //Lime	    #00FF00		(0,255,0)		(120º,100%,100%)
        //Blue	    #0000FF		(0,0,255)		(240º,100%,100%)
        //Yellow	#FFFF00		(255,255,0)		(60º,100%,100%)
        //Cyan	    #00FFFF		(0,255,255)		(180º,100%,100%)
        //Magenta	#FF00FF		(255,0,255)		(300º,100%,100%)
        //Silver	#C0C0C0		(192,192,192)	(0º,0%,75%)
        //Gray	    #808080		(128,128,128)	(0º,0%,50%)
        //Maroon	#800000		(128,0,0)		(0º,100%,50%)
        //Olive	    #808000		(128,128,0)		(60º,100%,50%)
        //Green	    #008000		(0,128,0)		(120º,100%,50%)
        //Purple	#800080		(128,0,128)		(300º,100%,50%)
        //Teal	    #008080		(0,128,128)		(180º,100%,50%)
        //Navy	    #000080		(0,0,128)		(240º,100%,50%)
        [TestMethod]
        public void ShouldConvertFromRgbToHsv()
        {
            var testCases = new[]
            {
                new TestCase(new Rgb(0, 0, 0),       new Hsv(0, 0 ,0)),
                new TestCase(new Rgb(255, 255, 255), new Hsv(0, 0 ,1)),

                new TestCase(new Rgb(255, 0, 0),     new Hsv(0, 1 ,1)),
                new TestCase(new Rgb(0, 255, 0),     new Hsv(120, 1 ,1)),
                new TestCase(new Rgb(0, 0, 255),     new Hsv(240, 1 ,1)),

                new TestCase(new Rgb(255, 255, 0),     new Hsv(60, 1 ,1)),
                new TestCase(new Rgb(0, 255, 255),     new Hsv(180, 1 ,1)),
                new TestCase(new Rgb(255, 0, 255),     new Hsv(300, 1 ,1)),
            };

            foreach (var testCase in testCases)
            {
                testCase.Run();
            }
        }

        private class TestCase
        {
            private readonly Hsv _hsv;
            private readonly Rgb _rgb;

            public TestCase(Rgb rgb, Hsv hsv)
            {
                _rgb = rgb;
                _hsv = hsv;
            }

            public void Run()
            {
                var result = _rgb.ToHsv();
                Assert.AreEqual(_hsv, result);
            }
        }
    }
}