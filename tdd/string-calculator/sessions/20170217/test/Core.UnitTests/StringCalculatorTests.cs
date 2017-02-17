﻿using System;
using Xunit;

namespace GusztavVargadDr.Katas.Tdd.UnitTests
{
    public class StringCalculatorTests
    {
        public class Add : StringCalculatorTests
        {
            // ReSharper disable once UnusedParameter.Local
            private static void AssertResultEquals(string numbers, int expectedResult)
            {
                var stringCalculator = new StringCalculator();

                var actualResult = stringCalculator.Add(numbers);

                Assert.Equal(expectedResult, actualResult);
            }

            public class EmptyString : Add
            {
                [Fact]
                public void ReturnsZero()
                {
                    AssertResultEquals(string.Empty, 0);
                }
            }

            public class OneNumber : Add
            {
                [Theory]
                [InlineData("0", 0)]
                [InlineData("1", 1)]
                [InlineData("2", 2)]
                public void ReturnsNumber(string numbers, int number)
                {
                    AssertResultEquals(numbers, number);
                }
            }


            public class TwoNumbers : Add
            {
                [Theory]
                [InlineData("0,1", 1)]
                [InlineData("1,2", 3)]
                [InlineData("2,3", 5)]
                public void ReturnsSum(string numbers, int sum)
                {
                    AssertResultEquals(numbers, sum);
                }
            }

            public class MultipleNumbers : Add
            {
                [Theory]
                [InlineData("1,2,3", 6)]
                [InlineData("2,3,4,5", 14)]
                [InlineData("3,4,5,6,7", 25)]
                public void ReturnsSum(string numbers, int sum)
                {
                    AssertResultEquals(numbers, sum);
                }
            }

            public class NewLineDelimiter : Add
            {
                [Theory]
                [InlineData("0,1\n2", 3)]
                [InlineData("1\n2,3", 6)]
                [InlineData("2\n3\n4", 9)]
                public void ReturnsSum(string numbers, int sum)
                {
                    AssertResultEquals(numbers, sum);
                }
            }

            public class CustomDelimiter : Add
            {
                [Theory]
                [InlineData("//:\n0:1", 1)]
                [InlineData("//;\n1;2", 3)]
                [InlineData("//a\n2a3", 5)]
                public void ReturnsSum(string numbers, int sum)
                {
                    AssertResultEquals(numbers, sum);
                }
            }


            public class NegativeNumbers : Add
            {
                [Theory]
                [InlineData("-1")]
                [InlineData("1,-2")]
                [InlineData("-3,-4")]
                public void ThrowsArgumentOutOfRange(string numbers)
                {
                    var stringCalculator = new StringCalculator();

                    Assert.Throws<ArgumentOutOfRangeException>(() => stringCalculator.Add(numbers));
                }

                [Theory]
                [InlineData("-1")]
                [InlineData("1,-2")]
                [InlineData("-3,-4")]
                public void ThrowsAndMessageContainsNegativesNotAllowed(string numbers)
                {
                    var stringCalculator = new StringCalculator();

                    var ex = Assert.ThrowsAny<Exception>(() => stringCalculator.Add(numbers));

                    Assert.Contains("negatives not allowed", ex.Message);
                }

                [Theory]
                [InlineData("-1", new[] {-1})]
                [InlineData("1,-2", new[] {-2})]
                [InlineData("-3,-4", new[] {-3, -4})]
                public void ThrowsAndMessageContainsNegativeNumbers(string numbers, int[] negativeNumbers)
                {
                    var stringCalculator = new StringCalculator();

                    var ex = Assert.ThrowsAny<Exception>(() => stringCalculator.Add(numbers));

                    Assert.All(negativeNumbers, item => Assert.Contains(item.ToString(), ex.Message));
                }
            }
        }
    }
}