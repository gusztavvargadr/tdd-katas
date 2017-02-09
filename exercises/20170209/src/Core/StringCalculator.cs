﻿using System.Linq;

namespace TddKata
{
    public class StringCalculator
    {
        private const string CustomNumberDelimiterMark = "//";
        private const char CustomNumberDelimiterDelimiter = '\n';
        private static readonly char[] DefaultNumberDelimiters = {',', '\n'};

        public StringCalculator()
        {
            NumberDelimiters = DefaultNumberDelimiters;
        }

        private char[] NumberDelimiters { get; set; }

        public int Add(string numbers)
        {
            if (numbers == string.Empty)
                return 0;

            if (numbers.StartsWith(CustomNumberDelimiterMark) && numbers[3] == CustomNumberDelimiterDelimiter)
            {
                NumberDelimiters = new[] {numbers[2]};
                numbers = numbers.Substring(4);
            }

            return numbers.Split(NumberDelimiters).Select(int.Parse).Sum();
        }
    }
}