using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JsonTestMVC.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using JsonTestMVC.Utilities;

namespace JsonTestMVC.Controllers
{
    public class TestModalController : Controller
    {
        // GET: TestModal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowBootStrapModal(int testid1, int testid2)
        {
            ViewBag.testid1 = testid1;
            ViewBag.testid2 = testid2;
            return PartialView("_TestPartial");
        }

        //Install-Package itextsharp.xmlworker -Version 5.5.13 
        //http://jsfiddle.net/a49cA/1/

        public ActionResult GeneratePDF()
        {
            bool IsPdfGenerated = false;

            List<Student> studentsVM = new List<Student>
            {
                new Student {ID=1,FirstName="Joy",      LastName="Roy",     FavouriteGames="Hocky"},
                new Student {ID=2,FirstName="Raja",     LastName="Basu",    FavouriteGames="Cricket"},
                new Student {ID=3,FirstName="Arijit",   LastName="Banerjee",FavouriteGames="Foot Ball"},
                new Student {ID=4,FirstName="Dibyendu", LastName="Saha",    FavouriteGames="Tennis"},
                new Student {ID=5,FirstName="Sanjeeb",  LastName="Das",     FavouriteGames="Hocky"},
            };

            var viewToString = StringUtilities.RenderViewToString(ControllerContext, "~/Views/Shared/_Report.cshtml", studentsVM, true);
            string filepath = HttpContext.Server.MapPath("~/PDFArchives/") + "mypdf.pdf";

            try
            {
                StringReader sr = new StringReader(viewToString);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 30f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filepath, FileMode.Create));
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                IsPdfGenerated = true;
            }
            catch (Exception ex)
            {
                IsPdfGenerated = false;
            }

            //return Json(new { fileName = filepath, errorMessage = (IsPdfGenerated ? "" : "Error occured when pdf generated") });
            return Json(new { Msg = (IsPdfGenerated ? "PDF generated properly" : "Error occured when pdf generated") });
        }

        [Route("DownloadPDF")]
        [HttpGet]
        public void DownloadPDF()
        {
            //bool IsPdfGenerated = false;

            List<Student> studentsVM = new List<Student>
            {
                new Student {ID=1,FirstName="Joy",      LastName="Roy",     FavouriteGames="Hocky"},
                new Student {ID=2,FirstName="Raja",     LastName="Basu",    FavouriteGames="Cricket"},
                new Student {ID=3,FirstName="Arijit",   LastName="Banerjee",FavouriteGames="Foot Ball"},
                new Student {ID=4,FirstName="Dibyendu", LastName="Saha",    FavouriteGames="Tennis"},
                new Student {ID=5,FirstName="Sanjeeb",  LastName="Das",     FavouriteGames="Hocky"},
            };

            var viewToString = StringUtilities.RenderViewToString(ControllerContext, "~/Views/Shared/_Report.cshtml", studentsVM, true);
            string filepath = HttpContext.Server.MapPath("~/PDFArchives/") + "mypdf.pdf";

            MemoryStream workStream = new MemoryStream();
            StringReader sr = new StringReader(viewToString);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 30f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
            //writer.CloseStream = false;
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            System.Web.HttpContext.Current.Response.ContentType = "pdf/application";
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;" +
                    "filename=sample.pdf");
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Write(pdfDoc);
            System.Web.HttpContext.Current.Response.End();
        }
    }
}