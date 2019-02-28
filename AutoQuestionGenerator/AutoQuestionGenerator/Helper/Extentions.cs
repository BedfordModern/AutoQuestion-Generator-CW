using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using iText.Layout.Layout;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AutoQuestionGenerator.DatabaseModels;

namespace AutoQuestionGenerator.Helper
{
    public class BoolToIntConverter : ValueConverter<bool, int>
    {
        public BoolToIntConverter(ConverterMappingHints mappingHints = null)
            : base(
                  v => Convert.ToInt32(v),
                  v => Convert.ToBoolean(v),
                  mappingHints)
        {
        }

        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
    }

    public static class Extentions
    {

        private static PdfWriter writer;
        private static PdfDocument pdf;
        private static PageSize Page = PageSize.A4;
        private static Document document;
        public static string ToPDF(this Worksets set)
        {
            var time = DateTime.Now.ToString("dd-hh-mm hh-MM-ss");
            var path = AppContext.BaseDirectory + "/prints/";
            if (!File.Exists(path + set.WorksetName + "-" + time + ".pdf"))
            {
                Directory.CreateDirectory(path);
                File.Create(path + set.WorksetName + "-" + time + ".pdf").Close();
            }
            var writer = new PdfWriter(path + set.WorksetName + "-" + time + ".pdf");
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf, Page);


            Paragraph p = new Paragraph().Add(new Text(set.WorksetName + "\n").SetBold().SetUnderline().SetFontSize(22f)).SetTextAlignment(TextAlignment.CENTER);
            document.Add(p);

            Table tbl = new Table(1);
            tbl.SetWidth(UnitValue.CreatePercentValue(100));

            for (int i = 0; i < 10; i++)
            {
                Table qTbl = new Table(1);
                qTbl.SetWidth(UnitValue.CreatePercentValue(100));

                var qCell = new Cell().Add(new Paragraph("Question " + i + ":").SetBold()).SetTextAlignment(TextAlignment.CENTER).SetBorderLeft(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER);
                var AskQ = new Cell().Add(new Paragraph("This is a Question.")).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.TOP).SetBorder(Border.NO_BORDER);
                var AnsCell = new Cell().Add(new Paragraph("Answer: ___________________.").SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetHeight(70f).SetBorder(Border.NO_BORDER);
                qTbl.AddCell(qCell).AddCell(AskQ).AddCell(AnsCell);

                var cell = new Cell();
                cell.Add(qTbl);
                tbl.AddCell(cell);
            }


            document.Add(tbl);

            document.Close();
            return path + set.WorksetName + "-" + time + ".pdf";
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<string> Surround(this IList<string> list, char surround)
        {
            for (int i = 0; i < list.Count; i++)

            {
                list[i] = surround + list[i] + surround;
            }

            return list;
        }

        public static IList<T> Unique<T>(this IList<T> list)
        {
            var ret = new List<T>();
            foreach (var item in list)
            {
                if (!ret.Contains(item))
                {
                    ret.Add(item);
                }
            }
            list = ret;

            return ret;
        }

        public static string Connect<T>(this IList<T> list, string joiner)
        {
            string ret = "";
            foreach (var item in list)
            {
                ret += item.ToString() + joiner;
            }
            return ret.Substring(0, ret.Length - 1);
        }
    }
}
