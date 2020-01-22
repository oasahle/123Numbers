using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbers123Parallellized
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = 1;
            Task<long>[] taskArray = new Task<long>[25];
            var testers = new Is123NumberClass[25];
            for (int i = 0; i < taskArray.Length; i++)
            {
                testers[i] = new Is123NumberClass(i+1);
            }
                
            for (int i = 0; i < taskArray.Length; i++)
            {
                var tester = testers[i];
                taskArray[i] = Task.Factory.StartNew(() =>
                                                   {                                                       
                                                       return tester.FindNForArray();
                                                   }
                                                    );
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var t = task.Result;
                //Debugger.Break();
            }
        }

        
    }

    public class Is123NumberClass
    {
        private char[] _stringArray;
        public Is123NumberClass(int length)
        {
            _stringArray = new string('1', length).ToArray();
        }

        

        public long FindNForArray()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long n = 0;

            var countSiffer = _stringArray.Length;
            while (n <= 60000000)
            {
                if (countSiffer < _stringArray.Length)
                {
                    Console.WriteLine($"i: {new string(_stringArray)}. Tid brukt: {watch.ElapsedMilliseconds}  Antall siffer: {countSiffer} Antall n; {n }");
                    return n;
                }

                if (Is123Number(_stringArray))
                {
                    n++;
                }
                //Console.WriteLine($"f({n}) = {i}, {i%11}, {i % 12}, {i % 13}, {i % 21}, {i % 22}, {i % 23}, {i % 31}, {i % 32}, {i % 33}");
                _stringArray = CalculateNextI(_stringArray);
            }
            return n;
        }

        private static char[] CalculateNextI(char[] number)
        {
            if (number[number.Length - 1] == '3')
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
