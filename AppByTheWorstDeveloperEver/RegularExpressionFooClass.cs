using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppByTheWorstDeveloperEver
{
    public class RegularExpressionFooClass
    {
        public bool Foo(
            RegexOptions options,
            TimeSpan matchTimeout,
            string input,
            string replacement,
            MatchEvaluator evaluator)
        {
            // All the following instantiations are Sensitive.
            Regex a1 = new Regex("(a+)+");
            Regex a2 = new Regex("(a+)+", options);
            Regex a3 = new Regex("(a+)+", options, matchTimeout);

            // All the following static methods are Sensitive.
            bool r1 = Regex.IsMatch(input, "(a+)+");
            bool r2 = Regex.IsMatch(input, "(a+)+", options);
            bool r3 = Regex.IsMatch(input, "(a+)+", options, matchTimeout);

            Match r4 = Regex.Match(input, "(a+)+");
            Match r5 = Regex.Match(input, "(a+)+", options);
            Match r6 = Regex.Match(input, "(a+)+", options, matchTimeout);

            MatchCollection r7 = Regex.Matches(input, "(a+)+");
            MatchCollection r8 = Regex.Matches(input, "(a+)+", options);
            MatchCollection r9 = Regex.Matches(input, "(a+)+", options, matchTimeout);

            string r10 = Regex.Replace(input, "(a+)+", evaluator);
            string r11 = Regex.Replace(input, "(a+)+", evaluator, options);
            string r12 = Regex.Replace(input, "(a+)+", evaluator, options, matchTimeout);
            string r13 = Regex.Replace(input, "(a+)+", replacement);
            string r14 = Regex.Replace(input, "(a+)+", replacement, options);
            string r15 = Regex.Replace(input, "(a+)+", replacement, options, matchTimeout);

            string[] r16 = Regex.Split(input, "(a+)+");
            string[] r17 = Regex.Split(input, "(a+)+", options);
            string[] r18 = Regex.Split(input, "(a+)+", options, matchTimeout);

            return (a1.ToString() == a2.ToString()) &&
                   (a3.ToString() == r1.ToString()) &&
                   (r2.ToString() == r3.ToString()) &&
                   (r4.ToString() == r5.ToString()) &&
                   (r6.ToString() == r7.ToString()) &&
                   (r8.ToString() == r9.ToString()) &&
                   (r10.ToString() == r11.ToString()) &&
                   (r12.ToString() == r13.ToString()) &&
                   (r14.ToString() == r15.ToString()) &&
                   (r16.ToString() == r17.ToString()) &&
                   (r18.ToString() == a1.ToString());
        }

        public override string ToString()
        {
            return null; // bad practice
        }
    }
}