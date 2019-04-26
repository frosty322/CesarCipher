using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI;
using NPOI.XWPF.UserModel;
using System.Text;

namespace EndCourse.Models
{
    static public class ReadingFromText
    {
        //method of reading from txt
        static public string TxtReader(Stream txtFile)
        {
            string text;
            using(StreamReader sr = new StreamReader(txtFile, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
        //method of reading from docx
        static public string DocxReader(Stream docxFile)
        {
            var whole = new StringBuilder();
            var doc = new XWPFDocument(docxFile);
            foreach(XWPFParagraph prg in doc.Paragraphs)
            {
                whole.AppendLine(prg.Text);
            }
            return whole.ToString();
        }
    }
}