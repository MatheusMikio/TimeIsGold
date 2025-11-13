

namespace Application.Services
{
    public class UserValidator
    {
        public static bool ValidateCpf(string cpf)
        {
            ReadOnlySpan<char> cpfSpan = cpf;
            Span<int> digits = stackalloc int[11];
            int count = 0;

            foreach (char cpfNum in cpfSpan)
            {
                if (char.IsDigit(cpfNum))
                {
                    if (count >= 11) return false;
                    digits[count++] = cpfNum - '0';
                }

            }

            if (count != 11) return false;

            bool allSame = true;
            for (int i = 1; i < 11; i++)
            {
                if (digits[i] != digits[0])
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame) return false;

            static int CalculateCheckDigit(ReadOnlySpan<int> digits)
            {
                int sum = 0;
                int multiplication = digits.Length + 1;
                for (int i = 0; i < digits.Length; i++)
                {
                    sum += digits[i] * (multiplication - i);
                }
                int rest = sum % 11;
                return (rest < 2) ? 0 : 11 - rest;
            }

            int digitCheck1 = CalculateCheckDigit(digits.Slice(0, 9));

            if (digitCheck1 != digits[9]) return false;

            int digitCheck2 = CalculateCheckDigit(digits.Slice(0, 10));
            return digits[10] == digitCheck2;
        }

        private static string RemoveCpfMask(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            email = email.Trim();

            int atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex != email.LastIndexOf('@')) return false;

            string local = email.Substring(0, atIndex);
            string domain = email.Substring(atIndex + 1);

            if (local.Length == 0 || domain.Length == 0) return false;

            int dotIndex = domain.IndexOf('.');
            if (dotIndex <= 0 || dotIndex == domain.Length - 1) return false;

            foreach (char character in domain)
            {
                if (char.IsWhiteSpace(character))
                    return false;
            }

            foreach (char character in local)
            {
                if (!(char.IsLetterOrDigit(character) || character == '.' || character == '_' || character == '-'))
                    return false;
            }

            foreach (char character in domain)
            {
                if (!(char.IsLetterOrDigit(character) || character == '.' || character == '-'))
                    return false;
            }

            return true;
        }

    }
}
