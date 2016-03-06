﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using mosaic.ColorSpaces;
using System.Drawing;

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
                new TestCase(Color.FromArgb(0, 0, 0),       new Hsv(0, 0 ,0)),
                new TestCase(Color.FromArgb(255, 255, 255), new Hsv(0, 0 ,100)),

                new TestCase(Color.FromArgb(255, 0, 0),     new Hsv(0, 100 ,100)),
                new TestCase(Color.FromArgb(0, 255, 0),     new Hsv(120, 100 ,100)),
                new TestCase(Color.FromArgb(0, 0, 255),     new Hsv(240, 100 ,100)),

                new TestCase(Color.FromArgb(255, 255, 0),     new Hsv(60, 100 ,100)),
                new TestCase(Color.FromArgb(0, 255, 255),     new Hsv(180, 100 ,100)),
                new TestCase(Color.FromArgb(255, 0, 255),     new Hsv(300, 100 ,100)),

                new TestCase(Color.FromArgb(192, 192, 192),     new Hsv(0, 0 ,75)),
                new TestCase(Color.FromArgb(128, 128, 128),     new Hsv(0, 0 ,50)),
            };

            foreach (var testCase in testCases)
            {
                testCase.Run();
            }
        }

        private class TestCase
        {
            private readonly Color _color;
            private readonly Hsv _hsv;

            public TestCase(Color color, Hsv hsv)
            {
                _color = color;
                _hsv = hsv;
            }

            public void Run()
            {
                var result = _color.ToHsv();
                Assert.AreEqual(_hsv, result);
            }
        }
    }
}