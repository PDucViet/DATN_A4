using System.Security.Cryptography;

namespace DATN.Api.Helpers
{
    public class PasswordHepler
    {
        #region Private Constants
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DigitChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()_+[]{}|;:,.<>?";
        #endregion

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateAccountPassword(int length)
        {
            if (length < 7)
            {
                throw new ArgumentException("Độ dài mật khẩu phải ít nhất là 7 ký tự.");
            }

            // First Character is Upercase
            var passwordChars = new char[] { UppercaseChars[RandomNumberGenerator.GetInt32(UppercaseChars.Length)] };

            // Character include normalcase, upercase , digit and special char
            passwordChars = passwordChars.Concat(GenerateRandomChars(LowercaseChars, 2))
                                         .Concat(GenerateRandomChars(DigitChars, 2))
                                         .Concat(GenerateRandomChars(SpecialChars, 2))
                                         .ToArray();

            // complete password with characters
            var remainingLength = length - passwordChars.Length;
            var randomChars = GenerateRandomChars(LowercaseChars + UppercaseChars + DigitChars + SpecialChars, remainingLength);
            passwordChars = passwordChars.Concat(randomChars).ToArray();

            // suffle password
            passwordChars = passwordChars.OrderBy(c => Guid.NewGuid()).ToArray();

            return new string(passwordChars);
        }

        /// <summary>
        /// Generate random character
        /// </summary>
        /// <param name="charSet"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static char[] GenerateRandomChars(string charSet, int count)
        {
            var random = new Random();
            return Enumerable.Range(0, count)
                             .Select(_ => charSet[random.Next(charSet.Length)])
                             .ToArray();
        }
    }
}
