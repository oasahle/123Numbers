using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _123Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = 1;
            var i = "1".ToArray();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var oldn = n;
            var countSiffer = i.Length;
            while (n <= 60000000)
            {
                if (Is123Number(i))
                {
                    //if (n % 10 == 0)
                    //{
                    //    watch.Stop();
                    //    Console.WriteLine($"f({n}) = {new string(i)}  {watch.ElapsedMilliseconds}");
                    //    watch = System.Diagnostics.Stopwatch.StartNew();
                    //}
                    if(countSiffer < i.Length)
                    {
                        Console.WriteLine($"f({n}) = {new string(i)}  {watch.ElapsedMilliseconds}  Antall siffer: {countSiffer} Antall n{n-oldn}");
                        oldn = n;
                        countSiffer = i.Length;
                        watch = System.Diagnostics.Stopwatch.StartNew();
                    }
                     n++;
                }
                //Console.WriteLine($"f({n}) = {i}, {i%11}, {i % 12}, {i % 13}, {i % 21}, {i % 22}, {i % 23}, {i % 31}, {i % 32}, {i % 33}");
                i = CalculateNextI(i);
            }
            Debugger.Break();
        }

        private static char[] CalculateNextI(char[] number)
        {
            if (number[number.Length-1] == '3')
            {
                var stringBuilder = new StringBuilder();
                var allThrees = true;
                for (long i = number.Length - 1; i >= 0; i--)
                {
                    if (allThrees && number[i] == '3')
                    {
                        stringBuilder.Append("1");
                        if (i == 0) stringBuilder.Append("1");
                    }
                    else
                    {
                        if (allThrees)
                        {
                            allThrees = false;
                            stringBuilder.Append((int.Parse(number[i].ToString()) + 1).ToString());
                        }
                        else
                        {
                            stringBuilder.Append(number[i]);
                        }
                    }


                }
                //stringBuilder.Append("7");
                var strAdd = Reverse(stringBuilder.ToString()).ToArray();
                return strAdd;
            }
            else
            {
                //var t = number.Substring(number.Length-1,1);
                var lastNumber = int.Parse(number.Last().ToString()) + 1;
                number[number.Length - 1] = lastNumber.ToString().ToArray()[0];

                return number;
            }



        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool Is123Number(char[] istr)
        {

            if (!istr.All(itm => itm == '1' || itm == '2' || itm == '3')) return false;
            var ones = (istr.Where(itm => itm == '1').Count());
            var twos = (istr.Where(itm => itm == '2').Count());
            var trees = (istr.Where(itm => itm == '3').Count());
            if (!IsCorrectCount(ones.ToString()) || !IsCorrectCount(twos.ToString()) || !IsCorrectCount(trees.ToString()))
            {
                //System.Console.WriteLine($"{number}");
                return false;
            }

            return true;

        }
        public static bool IsCorrectCount(string count)
        {
            return (new List<string> { "0", "1", "2", "3" }.Contains(count) || Is123Number(count.ToCharArray()));
        }
    }
}
