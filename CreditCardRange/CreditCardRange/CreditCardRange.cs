using System;
using System.Collections.Generic;

namespace CreditCardProcessing
{
    public class CreditCardRange
    {
        private static List<CreditCardRange> Ranges = new List<CreditCardRange>();
        public static bool UseLuhn = false;

        public CreditCardRange()
        {
            Issuer = CreditCardType.Unknown;
            RangeActive = true;
            UsesLuhn = true;
            Lengths = new List<int>();
            Ranges.Add(this);
        }

        public String IssuerName { get; set; }
        public CreditCardType Issuer
        {
            get
            {
                return issuer;
            }
            set
            {
                issuer = value;
                if (value != CreditCardType.Unknown && String.IsNullOrWhiteSpace(IssuerName))
                {
                    IssuerName = issuer.ToString();
                }
            }
        }
        public String Numbers
        {
            set
            {
                if (RangeNumbers == null)
                {
                    RangeNumbers = new List<string>();
                }

                RangeNumbers.Clear();
                foreach(var range in value.Split(','))
                {
                    var extremes = range.Trim().Split('-');
                    if (extremes.Length == 1)
                    {
                        // Not a range
                        RangeNumbers.Add(extremes[0]);
                        continue;
                    }

                    int low = -1, high = -1;
                    int.TryParse(extremes[0], out low);
                    int.TryParse(extremes[1], out high);
                    if (low == -1 || high == -1 || low > high || low.ToString().Length != low.ToString().Length)
                    {
                        // Malformed range
                        continue;
                    }

                    for (int i = low; i <= high; i++)
                    {
                        RangeNumbers.Add(i.ToString());
                    }
                }
            }
        }
        public List<int> Lengths { get; set; }
        public bool RangeActive { get; set; }
        public bool IssuerAccepted
        {
            get
            {
                return Issuer != CreditCardType.Unknown;
            }
        }
        public bool UsesLuhn { get; set; }

        private CreditCardType issuer;
        private List<String> RangeNumbers;

        public bool LengthIdentify(String creditCardNumber, out int length)
        {
            int maxLength = 0;

            // Skip if nobody cares
            if (!RangeActive)
            {
                length = 0;
                return false;
            }

            // Check the possibilities
            foreach (String num in RangeNumbers)
            {
                if (creditCardNumber.StartsWith(num) && num.Length > maxLength)
                {
                    maxLength = num.Length;
                }
            }

            // Validate number structure
            if (!Lengths.Contains(creditCardNumber.Length) || (UseLuhn && !VerifyCreditCardNumberByLuhn(creditCardNumber)))
            {
                maxLength = 0;
            }

            length = maxLength;
            return IssuerAccepted;
        }

        public static void Clear()
        {
            Ranges.Clear();
        }

        public static CreditCardType ValidateCardNumber(String creditCardNumber)
        {
            CreditCardType type = CreditCardType.Unknown;
            int maxLength = 0;

            foreach (CreditCardRange range in Ranges)
            {
                int length;
                bool success = range.LengthIdentify(creditCardNumber, out length);

                if (length > maxLength)
                {
                    maxLength = length;
                    type = range.Issuer;
                }
            }

            return type;
        }

        private static bool VerifyCreditCardNumberByLuhn(String creditCardNumber)
        {
            int total = 0;

            for (int i = creditCardNumber.Length - 2; i > -1; i--)
            {
                char c = creditCardNumber[i];
                int val = (int)(char.GetNumericValue(c));
                if ((creditCardNumber.Length - i) % 2 == 0)
                {
                    // Double every other digit
                    val *= 2;
                    if (val > 9)
                    {
                        // Pretend to add the digits
                        val -= 9;
                    }
                }

                total += val;
            }

            return total % 10 == (10 - char.GetNumericValue(creditCardNumber[creditCardNumber.Length - 1])) % 10;
        }
    }
}
