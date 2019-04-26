using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndCourse.Models
{
    public class CesarCipher
    {
        //Russian alphabet
        public const string LowerLatters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public const string UpperLatters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        //frequency from wiki
        private static readonly float[] Frequency = { 0.0801f, 0.0159f, 0.0454f, 0.017f, 0.0298f, 0.0845f, 0.0004f, 0.0094f, 0.0165f, 0.0735f,
                                                     0.0121f, 0.0349f, 0.044f, 0.0321f, 0.067f, 0.1097f, 0.0281f, 0.0473f, 0.0547f, 0.0626f,
                                                     0.0262f, 0.0026f, 0.0097f, 0.0048f, 0.0144f, 0.0073f, 0.0036f, 0.0004f, 0.0190f, 0.0174f,
                                                     0.0032f, 0.0064f, 0.0201f};
        //method of shifting(encoding and decoding)
        public static string Cipher(string inputText, int shift)
        {
            if (shift < 0)
            {
                shift = 33 + shift % 33;
            }

            //comparing Russian alphabet whith input text by latters
            string CipheredText = "";
            int pos;
            foreach (var let in inputText)
            {
                if ((pos = LowerLatters.IndexOf(let)) != -1)
                {
                    CipheredText += LowerLatters[(pos + shift) % LowerLatters.Length];
                }
                else if ((pos = UpperLatters.IndexOf(let)) != -1)
                {
                    CipheredText += UpperLatters[(pos + shift) % LowerLatters.Length];
                }
                else
                {
                    CipheredText += let;
                }
            }
            return CipheredText;
        }

        //method of sorting keys by their priority 
        public static int[] FindKey(string text)
        {
            //initializing 
            text = text.ToUpper();
            var keys = new List<Key>();
            int pos;
            int fullCount = 0;
            var letterCount = new int[UpperLatters.Length];
            //finfing only russian text
            string onlyRussianLatters = "";

            foreach (var lat in text)
            {
                if ((pos = UpperLatters.IndexOf(lat)) != -1)
                {
                    letterCount[pos]++;
                    fullCount++;
                    onlyRussianLatters += lat;
                }
                else if (lat == ' ')
                {
                    onlyRussianLatters += ' ';
                }
            }
            //calculation of frequency and priority
            for (int i = 0; i < UpperLatters.Length; i++)
            {
                var key = new Key() { Shift = i, Priority = 0 };

                for (int j = 0; j < UpperLatters.Length; j++)
                {
                    pos = (i + j) % UpperLatters.Length;

                    var current = letterCount[pos] * 1f / fullCount;
                    key.Priority += Math.Abs(current - Frequency[j]);
                }
                keys.Add(key);
            }


            //sorting
            return keys.OrderBy(key => key.Priority).Select(x => x.Shift).ToArray();
        }
    }


}
