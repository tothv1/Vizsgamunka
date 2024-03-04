using AuthAPI.Services.IServices;
using System.Text;

namespace AuthAPI.Services.PasswordStrengthChecker
{
    public class PasswordManager : IPasswordManager
    {
        public bool CheckPassword(string password)
        {
            string specialCharacters = "!@#$%^&*()-_+={}[]|\\:;\"'<>,.?/";

            if (password.Length == 0 || password.Length < 8)
                return false;

            int countUpperLetters = 0;
            int countSpecialCharacters = 0;
            int countNumbers = 0;

            foreach (char c in password)
            {
                if (Char.IsUpper(c))
                {
                    countUpperLetters++;
                }
                if (specialCharacters.Contains(c))
                {
                    countSpecialCharacters++;
                }
                if (Char.IsDigit(c))
                {
                    countNumbers++;
                }
            }
            if(countUpperLetters < 1 || countSpecialCharacters < 1 || countNumbers < 1)
            {
                return false;
            }
           

            return true;
        }

        public string generateNewPassword(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] symbols = "<>#&@{};>.:-_'+!%/=()$÷".ToCharArray();
            char[] numbers = "0123456789".ToCharArray();

            List<char[]> arrrays =
            [
                alpha, symbols, numbers
            ];

            for (int i = 0; i < length; i++)
            {
                char[] selectedAray = arrrays[random.Next(arrrays.Count)];
                sb.Append(selectedAray[random.Next(selectedAray.Length)]);
            }
            return sb.ToString();
        }

        public bool PasswordMatch(string password, string passwordRepeate)
        {
            return password.Equals(passwordRepeate);
        }
    }
}
