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
using AutoQuestionGenerator.QuestionModels.Interpreter;

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
        public static TValue GetAttributeValue<TAttribute, TValue>(
        this Type type,
        Func<TAttribute, TValue> valueSelector)
        where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        public static int[] PositionsOf<T>(this T[] input, T searchValue)
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals(searchValue))
                {
                    positions.Add(i);
                }
            }
            return positions.ToArray();
        }

        public static int PositionOf<T>(this T[] input, T searchValue)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals(searchValue))
                {
                    return i;
                }
            }
            return -1;
        }

        private static PdfWriter writer;
        private static PdfDocument pdf;
        private static readonly PageSize Page = PageSize.A4;
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
            writer = new PdfWriter(path + set.WorksetName + "-" + time + ".pdf");
            pdf = new PdfDocument(writer);
            document = new Document(pdf, Page);


            Paragraph p = new Paragraph().Add(new Text(set.WorksetName + "\n\n").SetBold().SetUnderline().SetFontSize(22f)).SetTextAlignment(TextAlignment.CENTER);
            document.Add(p);

            Table tbl = new Table(1);
            tbl.SetWidth(UnitValue.CreatePercentValue(100));

            var interpreter = new PythonInterpreter();
            var work = DatabaseConnector.GetWhere<Work>($"WorksetID={set.WorksetID}");

            for (int i = 0; i < work.Length; i++)
            {
                var question = interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + DatabaseConnector.Get<QuestionTypes>().SingleOrDefault(x => x.TypeID == work[i].QuestionType).Class, work[i].Seed);
                Table qTbl = new Table(1);
                qTbl.SetKeepTogether(true);
                qTbl.SetWidth(UnitValue.CreatePercentValue(100));

                var qCell = new Cell().Add(new Paragraph("Question " + i + ":").SetBold()).SetTextAlignment(TextAlignment.CENTER).SetBorderLeft(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER);
                var AskQ = new Cell().Add(new Paragraph(question.GetQuestion() as string)).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.TOP).SetBorder(Border.NO_BORDER);
                var ans = question.Answer("");
                qTbl.AddCell(qCell).AddCell(AskQ);
                if (ans.Contains(","))
                {
                    var answerTitles = question.Boxes().Split(',');
                    var workcell = new Cell().Add(new Paragraph("   ").SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetHeight(60f).SetBorder(Border.NO_BORDER);
                    qTbl.AddCell(workcell);
                    for (int q = 0; q < answerTitles.Length; q++)
                    {
                        try
                        {
                            var AnsCell = new Cell().Add(new Paragraph(answerTitles[q] + ": ___________________.").SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetHeight(70f).SetBorder(Border.NO_BORDER);
                            qTbl.AddCell(AnsCell);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            var AnsCell = new Cell().Add(new Paragraph("Answer" + q + ": ___________________.").SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetHeight(70f).SetBorder(Border.NO_BORDER);
                            qTbl.AddCell(AnsCell);
                        }
                    }
                }
                else
                {
                    var AnsCell = new Cell().Add(new Paragraph("Answer: ___________________.").SetTextAlignment(TextAlignment.RIGHT).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetHeight(70f).SetBorder(Border.NO_BORDER);
                    qTbl.AddCell(AnsCell);
                }

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
                if (typeof(double).IsAssignableFrom(typeof(T)) && Convert.ToDouble(item) == 0.0)
                {
                    ret += item.ToString() + ".1" + joiner;
                }
                else
                {
                    ret += item.ToString() + joiner;
                }
            }
            return ret.Substring(0, ret.Length - 1);
        }
    }
}
