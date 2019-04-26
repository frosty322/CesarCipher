using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EndCourse.Models
{
    public class download
    {
        //method for making array of bytes for File
        public static byte[] makeDocx(string text)
        {
            XWPFDocument doc = new XWPFDocument();
            foreach (var item in text.Split('\n'))
            {
                var paragraph = doc.CreateParagraph();
                var run = paragraph.CreateRun();
                run.SetText(item);
            }

            MemoryStream stream = new MemoryStream();
            doc.Write(stream);

            return stream.ToArray();
        }
    }
}