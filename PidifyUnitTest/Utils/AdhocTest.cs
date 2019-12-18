using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pidify;
using Pidify.Canvases;
using Pidify.Plottables;
using Pidify.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PidifyUnitTest.Utils
{
    [TestClass]
    public class AdhocTest
    {
        [TestMethod]
        public void UsageExample_UsingRequestMode()
        {
            var area = Area.NewInstance(PointPair.NewInstance(.0f, .0f, 1f, .5f), 1337);

            // Creates the text plottable with StartAt. Note that the color is not applied in some modes.
            var textStartAt = TextPlottable.Builder
                .NewInstance("Using StartAt", TextInfo.NewInstance(FontType.Helvetica, 12f, Color.DarkRed))
                .SetStartAt(0.8f, 0.4f)
                .SetGravity(GravityType.Bottom, GravityType.Right)
                .Build();
            area.AddPlottable(textStartAt);

            // Creates the text plottable with CenterIn. Note that the color is not applied in some modes.
            var textCenterIn = TextPlottable.Builder
                .NewInstance("Using CenterIn", TextInfo.NewInstance(FontType.Helvetica, 12f, Color.DarkBlue))
                .SetCenterIn(PointPair.NewInstance(.5f, .4f, .7f, .6f))
                .SetGravity(GravityType.Bottom)
                .Build();
            area.AddPlottable(textCenterIn);

            // Creates the image plottable. Note that the image is not displayed in some modes.
            var image = ImagePlottable.Builder.NewInstance(FileUtil.RelativePath(@"Resources/test-monkey-jpg-file.jpg"))
                .SetScale(ScaleType.FitStart, PointPair.NewInstance(.1f, .1f, .4f, .4f))
                .SetGravity(GravityType.Right, GravityType.Top)
                .Build();
            area.AddPlottable(image);

            // Applies the mode on the canvas.
            var pdfCanvas = new PdfSharpCanvas()
                .RequestMode(ModeParam.Calibration);

            // Creates the PDF.
            area.Draw(pdfCanvas);
            pdfCanvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/using-request-mode.pdf")));
        }

        [TestMethod]
        public void Test_ImageGravityBottomThinArea()
        {
            var area1 = Area.NewInstance(PointPair.NewInstance(.15f, 0.00f, .85f, 0.15f), 1337);

            // Creates the plottable image and adds it to the area.
            var image1 = ImagePlottable.Builder.NewInstance(FileUtil.RelativePath(@"Resources/test-monkey-jpg-file.jpg"))
                .SetScale(ScaleType.FitStart, PointPair.NewInstance(0, 0, 0.6f, 0.5f))
                .SetGravity(GravityType.Right, GravityType.Bottom)
                .Build();
            area1.AddPlottable(image1);

            // Creates the area.
            var area2 = Area.NewInstance(PointPair.NewInstance(.4f, .20f, .60f, 0.80f), 1337);

            // Creates the plottable image and adds it to the area.
            var image2 = ImagePlottable.Builder.NewInstance(Resource.circle40)
                .SetScale(ScaleType.FitStart, PointPair.NewInstance(0, .25f, 0.5f, 0.5f))
                .SetGravity(GravityType.Top, GravityType.Right)
                .Build();
            area2.AddPlottable(image2);

            // Creates the pdf canvas and draws the area on top of it.
            var pdfCanvas = new PdfSharpCanvas();
            area1.Draw(pdfCanvas);
            area2.Draw(pdfCanvas);

            // Saves the result pdf.
            pdfCanvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/gravity-bottom.pdf")));
        }

        [TestMethod]
        public void Text_CommentPlottable()
        {

            var area = Area.NewInstance(PointPair.NewInstance(.0f, .0f, 1f, .5f), 1337);

            var text = TextPlottable.Builder
                .NewInstance("Hello world", TextInfo.NewInstance(FontType.Helvetica, 12f, Color.Red))
                .Build();

            area.AddPlottable(text);

            var commentsPlottable = CommentsPlottable.Builder.NewInstance(0.05f, "Comments", TextInfo.NewInstance(FontType.Helvetica, 16f, Color.Black))
                .SetLines(8, 0.5f)
                .SetStartAt(0.25f, 0.2f)
                .Build();
            area.AddPlottable(commentsPlottable);

            var signaturePlottable = SignaturePlottable.Builder.NewInstance(0.4f, "President/CEO")
                .SetStartAt(0.2f, 0.8f)
                .Build();
            area.AddPlottable(signaturePlottable);

            var v2 = TextPlottable.Builder
                .NewPidifyVersionInstance(TextInfo.NewInstance(FontType.Helvetica, 12f, Color.DarkGreen), "Pidify")
                .SetStartAt(0f, 0.3f)
                .Build();

            area.AddPlottable(v2);

            var pdfCanvas = new PdfSharpCanvas();
            area.Draw(pdfCanvas);

            pdfCanvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/comments.pdf")));
        }

        [TestMethod]
        public void UsageExample_FancyGraph()
        {
            var area = Area.NewInstance(PointPair.NewInstance(.0f, .0f, 1f, .5f), 1337);
            
            var sine = new List<float>();
            for (var a = 0.0; a < 2 * Math.PI; a += Math.PI / 180)
            {
                sine.Add((float)Math.Sin(a));
            }

            // Uses pre created colors with alpha on a white background.
            var legendColor = Colors.ApplyAlpha(0.25f, Colors.Blue);
            var horizontalFillColor = Colors.ApplyAlpha(0.25f, Colors.Green);

            var graph = GraphPlottable.Builder.NewInstance()
                .SetBorder(LineInfo.NewInstance(Colors.DarkGrey, 1f))
                .AddLine(sine, LineInfo.NewInstance(Colors.DarkGrey))
                .AddLegend("Sine", TextInfo.NewInstance(FontType.Helvetica, 40f, legendColor), 0.75f, 0.1f)
                .AddVerticalLine(LineInfo.DefaultDashed, 90, "90")
                .AddHorizontalLine(LineInfo.NewInstance(Colors.Green, 1f), 0.5f, "0.5")
                .AddHorizontalLine(LineInfo.NewInstance(Colors.Green, 1f), -0.5f, "-0.5")
                .SetHorizontalFill(-.5f, .5f, horizontalFillColor)
                .SetAxisLimits(null, Tuple.Create(-1.1f, 1.1f))
                .Build();
            area.AddPlottable(graph);

            var pdfCanvas = new PdfSharpCanvas();
            area.Draw(pdfCanvas);
            pdfCanvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/fancy-graph.pdf")));
        }

        [TestMethod]
        public void Test_Text()
        {
            var area1 = Area.NewInstance(PointPair.NewInstance(0.0f, 0.0f, 1.0f, 0.5f), 1337);

            var textPlottable1 = TextPlottable.Builder.NewInstance("Plottable1", TextInfo.NewInstance(FontType.Helvetica, 20f))
                .SetCenterIn(PointPair.NewInstance(0.4f, 0.4f, 0.6f, 0.6f)) // The text will be centered inside this box
                //.AddStartAt(0, 0)
                .SetGravity(GravityType.Top, GravityType.Right)
                .Build();

            var textPlottable2 = TextPlottable.Builder.NewInstance("Plottable2", TextInfo.NewInstance(FontType.Helvetica, 12f))
                .SetGravity(GravityType.Bottom, GravityType.Right)
                .SetStartAt(0.2f, 0.2f)
                .Build();

            var textPlottable3 = TextPlottable.Builder.NewInstance("Plottable3", TextInfo.NewInstance(FontType.Helvetica, 12f))
                .SetGravity(GravityType.Bottom)
                .SetStartAt(0f, 0f)
                .Build();

            area1.AddPlottable(textPlottable1);
            area1.AddPlottable(textPlottable2);
            area1.AddPlottable(textPlottable3);

            var t1 = TextPlottable.Builder.NewInstance("Test1", TextInfo.NewInstance(FontType.Arial, 12))
                .SetStartAt(0.5f, 0.0f)
                .Build();
            var t2 = TextPlottable.Builder.NewInstance("Test2", TextInfo.NewInstance(FontType.Arial, 12))
                .SetStartAt(0.5f, 0.1f)
                .Build();
            var t3 = TextPlottable.Builder.NewInstance("Test3", TextInfo.NewInstance(FontType.Arial, 12))
                .SetStartAt(0.5f, 0.2f)
                .Build();

            area1.AddPlottable(t1)
                .AddPlottable(t2)
                .AddPlottable(t3);
            var pdfCanvas = new PdfSharpCanvas();
            area1.Draw(pdfCanvas);

            pdfCanvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/text.pdf")));
        }

        [TestMethod]
        public void UsageExample_Extensive()
        {
            // Use same page for all plottables.
            const int pageId = 1337;

            //
            // Adds the title and the sub-title.
            //
            var header = PointPair.NewInstance(0.2f, 0f, 0.8f, 0.2f);
            var headerArea = Area.NewInstance(header, pageId);

            var titleTextInfo = TextInfo.NewInstance(FontType.Helvetica, 24, Colors.DarkGrey);
            var titlePlottable = TextPlottable.Builder.NewInstance("Sine and Cosine", titleTextInfo)
                .SetGravity(GravityType.Top)
                .Build();

            var subTitleTextInfo = TextInfo.NewInstance(FontType.Helvetica, 16, Colors.Grey, FontStyle.Italic);
            var subTitlePlottable = TextPlottable.Builder
                .NewInstance("December 2019", subTitleTextInfo)
                .SetCenterIn(PointPair.NewInstance(0.25f, 0.2f, 0.75f, 0.4f))
                .Build();

            // Adds the plottables to the area.
            headerArea.AddPlottable(titlePlottable)
                .AddPlottable(subTitlePlottable);

            //
            // Adds the graph.
            //
            var north = PointPair.NewInstance(0.2f, 0.2f, 0.8f, 0.5f);
            var northArea = Area.NewInstance(north, pageId);

            // Generates the data.
            var sine = new List<float>();
            var cosine = new List<float>();
            for (var a = 0.0; a < 2 * Math.PI; a += Math.PI / 180)
            {
                sine.Add((float)Math.Sin(a));
                cosine.Add((float)Math.Cos(a));
            }

            // Creates the plottable.
            var sineLegendTextInfo = TextInfo.NewInstance(FontType.Arial, 20f, Colors.Red);
            var cosineLegendTextInfo = TextInfo.NewInstance(FontType.Arial, 20f, Colors.Blue);
            var graph = GraphPlottable.Builder.NewInstance()
                .AddLine(sine, LineInfo.NewInstance(sineLegendTextInfo.Color, 2f))
                .AddLine(cosine, LineInfo.NewInstance(cosineLegendTextInfo.Color, 2f))
                .AddLegend("Sine", sineLegendTextInfo, 0.6f, 0.1f)
                .AddLegend("Cosine", cosineLegendTextInfo, 0.6f, 0.2f)
                .SetBorder(LineInfo.NewInstance(Colors.DarkGrey))
                .Build();

            northArea.AddPlottable(graph);

            //
            // Adds the inficon logo and the comment section.
            //
            var south = PointPair.NewInstance(0.2f, 0.5f, 0.8f, 0.8f);
            var southArea = Area.NewInstance(south, pageId);

            var facebookPlottable = ImagePlottable.Builder.NewInstance(Resource.fb)
                .SetScale(ScaleType.FitCenter, PointPair.NewInstance(0, 0, 0.5f, 0.5f))
                .SetGravity(GravityType.Right)
                .Build();

            southArea.AddPlottable(facebookPlottable);


            // Creates the comments area.
            var commentsPlottable = CommentsPlottable.Builder
                .NewInstance(.1f, "Comments", TextInfo.NewInstance(FontType.Helvetica, 12, Colors.DarkGrey))
                .SetStartAt(0, .5f)
                .SetLines(4, 1, LineInfo.NewInstance(Colors.Grey, .5f))
                .Build();

            southArea.AddPlottable(commentsPlottable);

            //
            // Adds the divider line and the version info.
            //
            var footer = PointPair.NewInstance(0, .95f, 1, 1);
            var footerArea = Area.NewInstance(footer, pageId);

            var dividerPlottable = LinePlottable.NewInstance(PointPair.LineTop, LineInfo.Default);

            footerArea.AddPlottable(dividerPlottable);

            // Creates the version text.

            var versionTextInfo = TextInfo.NewInstance(FontType.Helvetica, 8f, Colors.DarkGrey);
            var versionPlottable = TextPlottable.Builder.NewPidifyVersionInstance(versionTextInfo, "pidify-")
                .SetGravity(GravityType.Bottom, GravityType.Left)
                .Build();
            footerArea.AddPlottable(versionPlottable);

            //
            // Draws all areas on the canvas.
            //
            var canvas = new PdfSharpCanvas();

            headerArea.Draw(canvas);
            northArea.Draw(canvas);
            southArea.Draw(canvas);
            footerArea.Draw(canvas);

            //
            // Generates the PDF.
            //
            canvas.End(FileUtil.IncrementFilenameIfExists(FileUtil.RelativePath(@"Resources/extensive.pdf")));
        }
    }
}
