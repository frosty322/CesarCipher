using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EndCourse.Models;

namespace EndCourse.Controllers
{
    public class HomeController : Controller
    {
        string fileName;
        string filetext;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
        //download file whith text
        [HttpPost]
        public ActionResult Download(string text)
        {
            return File(download.makeDocx(text), "text/plain", "Result.docx");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile, string uploadtext, string range, string acionButton)
        {
            //checking of incoming values
            if (uploadFile == null && uploadtext.Length < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            // Verify that the user put something in textarea
            if (uploadtext != "")
            {
                filetext = uploadtext;
            }

            //reading file
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                fileName = uploadFile.FileName;
                if (uploadFile.FileName.EndsWith(".txt"))
                {
                    filetext = ReadingFromText.TxtReader(uploadFile.InputStream);
                }
                else
                {
                    filetext = ReadingFromText.DocxReader(uploadFile.InputStream);
                }
            }
            //action switching
            switch (acionButton)
            {
                case "encrypt": //encrypping 
                    filetext = CesarCipher.Cipher(filetext, int.Parse(range));
                    ViewData["text"] = filetext;
                    return View("Upload");
                case "decrypt with key": //decrypping whith key
                    filetext = CesarCipher.Cipher(filetext, -int.Parse(range));
                    ViewData["text"] = filetext;
                    return View("Upload");
                case "decrypt without key": //decrypping without key
                    var indexes = CesarCipher.FindKey(filetext);
                    string[] texts = new string[indexes.Length];
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        texts[i] = CesarCipher.Cipher(filetext, -indexes[i]);
                    }
                    ViewBag.index = indexes;
                    ViewBag.text = texts;
                    return View("Result");
                default:
                    return View();
            }




        }
    }
}
