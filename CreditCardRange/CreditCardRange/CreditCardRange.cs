//-----------------------------------------------------------------------
// <copyright file="CreditCardRange.cs" company="Colagioia Industries">
//     Provided under the terms of the AGPL v3.
// </copyright>
//-----------------------------------------------------------------------
namespace CreditCardProcessing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Credit card range.
    /// </summary>
    public class CreditCardRange
    {
        /// <summary>
        /// The issuer card number ranges.
        /// </summary>
        private static List<CreditCardRange> ranges = new List<CreditCardRange>();

        /// <summary>
        /// Whether the system uses the Luhn checksum for any card numbers.
        /// </summary>
        public static bool UseLuhn = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardProcessing.CreditCardRange"/> class.
        /// </summary>
        public CreditCardRange()
        {
            this.Issuer = CreditCardType.Unknown;
            this.RangeActive = true;
            this.UsesLuhn = true;
            this.Lengths = new List<int>();
            ranges.Add(this);
        }

        /// <summary>
        /// Gets or sets this instance's issuer name.
        /// </summary>
        /// <value>The name of the card's issuer.</value>
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets this instance's issuer.
        /// </summary>
        /// <value>One of an enumeration of issuers found in the <see cref="CreditCardProcessing.CreditCardType"/> class.</value>
        public CreditCardType Issuer
        {
            get
            {
                return this.issuer;
            }

            set
            {
                this.issuer = value;
                if (value != CreditCardType.Unknown && string.IsNullOrWhiteSpace(this.IssuerName))
                {
                    this.IssuerName = this.issuer.ToString();
                }
            }
        }

        /// <summary>
        /// Sets the number prefix ranges.
        /// </summary>
        /// <value>The number prefix ranges, as a comma-delimited list.</value>
        public string Numbers
        {
            set
            {
                if (this.RangeNumbers == null)
                {
                    this.RangeNumbers = new List<string>();
                }

                this.RangeNumbers.Clear();
                foreach (var range in value.Split(','))
                {
                    var extremes = range.Trim().Split('-');
                    if (extremes.Length == 1)
                    {
                        // Not a range
                        this.RangeNumbers.Add(extremes[0]);
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
                        this.RangeNumbers.Add(i.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the card number lengths.
        /// </summary>
        /// <value>The lengths.</value>
        public List<int> Lengths { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> range is active.
        /// </summary>
        /// <value><c>true</c> if range is in use; otherwise, <c>false</c>.</value>
        public bool RangeActive { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance issuer is accepted.
        /// </summary>
        /// <value><c>true</c> if this instance issuer is accepted by the vendor; otherwise, <c>false</c>.</value>
        public bool IssuerAccepted
        {
            get
            {
                return this.Issuer != CreditCardType.Unknown;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> uses Luhn.
        /// </summary>
        /// <value><c>true</c> if the issuer uses the Luhn checksum; otherwise, <c>false</c>.</value>
        public bool UsesLuhn { get; set; }

        /// <summary>
        /// The issuer.
        /// </summary>
        private CreditCardType issuer;

        /// <summary>
        /// The range numbers.
        /// </summary>
        private List<string> RangeNumbers;

        /// <summary>
        /// Validate the card number's structure, with information on the best match.
        /// </summary>
        /// <returns><c>true</c>, if the number is valid, <c>false</c> otherwise.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        /// <param name="length">The length of the longest prefix matched. (Output)</param>
        public bool LengthIdentify(string creditCardNumber, out int length)
        {
            int maxLength = 0;

            // Skip if nobody cares
            if (!this.RangeActive)
            {
                length = 0;
                return false;
            }

            // Check the possibilities
            foreach (string num in this.RangeNumbers)
            {
                if (creditCardNumber.StartsWith(num) && num.Length > maxLength)
                {
                    maxLength = num.Length;
                }
            }

            // Validate number structure
            if (!this.Lengths.Contains(creditCardNumber.Length) || (UseLuhn && !VerifyCreditCardNumberByLuhn(creditCardNumber)))
            {
                maxLength = 0;
            }

            length = maxLength;
            return this.IssuerAccepted;
        }

        /// <summary>
        /// Clear this instance's number ranges.
        /// </summary>
        public static void Clear()
        {
            ranges.Clear();
        }

        /// <summary>
        /// Validates the card number.
        /// </summary>
        /// <returns>The card issuer.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        public static CreditCardType ValidateCardNumber(string creditCardNumber)
        {
            CreditCardType type = CreditCardType.Unknown;
            int maxLength = 0;

            foreach (CreditCardRange range in ranges)
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

        /// <summary>
        /// Verifies the credit card number by Luhn.
        /// </summary>
        /// <returns><c>true</c>, if credit card number by the Luhn checksum was verified, <c>false</c> otherwise.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        private static bool VerifyCreditCardNumberByLuhn(string creditCardNumber)
        {
            int total = 0;

            for (int i = creditCardNumber.Length - 2; i > -1; i--)
            {
                char c = creditCardNumber[i];
                int val = (int)char.GetNumericValue(c);
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
