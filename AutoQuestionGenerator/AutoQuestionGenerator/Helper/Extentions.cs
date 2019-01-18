using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
