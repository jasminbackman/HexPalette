using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HexPalette
{
    class Program
    {
        public static string Convert(int[] numbers)
        {
            string hex = "#";
            for (int i = 0; i < 3; i++)
            {
                hex += GetHex(numbers[i] / 16) + GetHex((numbers[i] % 16));
            }
            return hex;

        }

        private static string GetHex(int number)
        {
            if (number < 0)
                return "0";

            if (number < 10)
                return number.ToString();

            if (number > 9)
                return ((char)(55 + number)).ToString();

            return "0";
        }

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Regex reg_numbers = new Regex(@"(\d+)");
            bool failed = false;
            bool end = false;

            while (args.Length < 1 || !end)
            {
                failed = false;
                Console.WriteLine("Give rbga or rbg value:");
                string input = Console.ReadLine();
                if(input.ToLower() == "end")
                {
                    end = true;
                    break;
                }
                int[] numbers = new int[3];
                Match match_number = reg_numbers.Match(input);
                for(int i = 0; i < 3; i++)
                {
                    if(!match_number.Success)
                    {
                        Console.WriteLine("Invalid input!");
                        failed = true;
                        break;
                    }
                    if(!Int32.TryParse(match_number.Value, out numbers[i]))
                    {
                        failed = true;
                        break;
                    }
                    match_number = match_number.NextMatch();
                }

                if (!failed)
                {
                    string hex = Convert(numbers);
                    Clipboard.SetText(hex);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("Copied " + hex + " to clipboard.");
                    Console.WriteLine("------------------------------");
                }
            }
        }
    }
}
