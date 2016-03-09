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
        /// The ranges.
        /// </summary>
        private static List<CreditCardRange> ranges = new List<CreditCardRange>();

        /// <summary>
        /// Flag on whether to use Luhn checksums.
        /// </summary>
        private static bool useLuhn = false;

        /// <summary>
        /// The issuer.
        /// </summary>
        private CreditCardType issuer;

        /// <summary>
        /// The range numbers.
        /// </summary>
        private List<string> rangeNumbers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardProcessing.CreditCardRange"/> class.
        /// </summary>
        public CreditCardRange()
        {
            this.Issuer = CreditCardType.Unknown;
            this.RangeActive = true;
            this.UsesLuhn = true;
            this.Lengths = new List<int>();
            this.IssuerAccepted = true;
            ranges.Add(this);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> uses Luhn checksums.
        /// </summary>
        /// <value><c>true</c> if it uses Luhn; otherwise, <c>false</c>.</value>
        public static bool UseLuhn
        {
            get
            {
                return useLuhn;
            }

            set
            {
                useLuhn = value;
            }
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
                if (this.rangeNumbers == null)
                {
                    this.rangeNumbers = new List<string>();
                }

                this.rangeNumbers.Clear();
                foreach (var range in value.Split(','))
                {
                    var extremes = range.Trim().Split('-');
                    if (extremes.Length == 1)
                    {
                        // Not a range
                        this.rangeNumbers.Add(extremes[0]);
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
                        this.rangeNumbers.Add(i.ToString());
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
        /// Gets or sets a value indicating whether this instance issuer is accepted.
        /// </summary>
        /// <value><c>true</c> if this instance issuer is accepted by the vendor; otherwise, <c>false</c>.</value>
        public bool IssuerAccepted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> uses Luhn.
        /// </summary>
        /// <value><c>true</c> if the issuer uses the Luhn checksum; otherwise, <c>false</c>.</value>
        public bool UsesLuhn { get; set; }

        /// <summary>
        /// Clear this instance's number ranges.
        /// </summary>
        public static void Clear()
        {
            ranges.Clear();
        }

        /// <summary>
        /// Creates all credit card types known as of 2016 March 06 as defaults.
        /// </summary>
        /// <param name="acceptedTypes">Accepted types.</param>
        public static void CreateDefaults(List<CreditCardType> acceptedTypes = null)
        {
            new CreditCardRange
            {
                Issuer = CreditCardType.AmEx,
                Numbers = "34,37",
                Lengths = { 15 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.AmEx),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Bankcard,
                Numbers = "5610,560221-560225",
                Lengths = { 16 },
                RangeActive = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Bankcard),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.ChinaUnionPay,
                Numbers = "62",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.ChinaUnionPay),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.DCCarteBlanche,
                Numbers = "300-305",
                Lengths = { 14 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.DCCarteBlanche),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.DCEnRoute,
                Numbers = "2014,2149",
                Lengths = { 15 },
                RangeActive = false,
                UsesLuhn = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.DCEnRoute),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.DCInternational,
                Numbers = "309,36,38,39",
                Lengths = { 14 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.DCInternational),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.DCUSCan,
                Numbers = "54,55",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.DCUSCan),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Discover,
                Numbers = "6011,622126-622925,644-649,65",
                Lengths =
                {
                    16,
                    19,
                },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Discover),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.InterPayment,
                Numbers = "636",
                Lengths = { 16, 17, 18, 19 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.InterPayment),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.InstaPayment,
                Numbers = "637-639",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.InstaPayment),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.JCB,
                Numbers = "3528-3589",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.JCB),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Laser,
                Numbers = "6304,6706,6771,6709",
                Lengths = { 16, 17, 18, 19 },
                RangeActive = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Laser),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Maestro,
                Numbers = "56-69",
                Lengths =
                {
                    12,
                    13,
                    14,
                    15,
                    16,
                    17,
                    18,
                    19,
                },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Maestro),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Dankort,
                Numbers = "4175,4571,5019",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Dankort),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.MasterCard,
                Numbers = "2221-2720",
                Lengths = { 16 },
                RangeActive = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.MasterCard),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.MasterCard,
                Numbers = "51-55",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.MasterCard),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Solo,
                Numbers = "6334,6767",
                Lengths = { 16, 18, 19 },
                RangeActive = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Solo),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Switch,
                Numbers = "4903,4905,4911,4936,564182,633110,6333,6759",
                Lengths =
                {
                    16,
                    18,
                    19,
                },
                RangeActive = false,
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Switch),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Visa,
                Numbers = "4",
                Lengths = { 13, 16, 19 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Visa),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.UATP,
                Numbers = "1",
                Lengths = { 15 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.UATP),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Verve,
                Numbers = "506099-506198,650002-650027",
                Lengths = { 16, 19 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Verve),
            };
            new CreditCardRange
            {
                Issuer = CreditCardType.Cardguard,
                Numbers = "5392",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Cardguard),
            };
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
                bool accepted = range.LengthIdentify(creditCardNumber, out length);

                if (accepted && length > maxLength)
                {
                    maxLength = length;
                    type = range.Issuer;
                }
            }

            return type;
        }

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
            foreach (string num in this.rangeNumbers)
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
			return this.IssuerAccepted && this.Issuer != CreditCardType.Unknown;
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
