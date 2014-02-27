using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeathermanServiceLayer.Utility
{

    /// <summary>
    /// Unique code generator.  Stolen from http://stackoverflow.com/questions/1546472/generate-random-strings-in-vb-net
    /// </summary>
    public class TypingMonkey
    {
        private const string LegalCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static Random random = new Random();

        /// <summary>
        /// The Typing Monkey Generates a random string with the given length.
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <returns>Random string</returns>
        public string TypeAway(int size)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < size; i++)
            {
                var ch = LegalCharacters[random.Next(0, LegalCharacters.Length)];
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
