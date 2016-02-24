using System.ComponentModel;

namespace CreditCardProcessing
{
    public enum CreditCardType
    {
		[Description("Unknown Issuer")]
        Unknown,

		[Description("American Express")]
		AmEx,

		[Description("Discover Card")]
		Discover,

		[Description("MasterCard")]
		MasterCard,

		[Description("Visa")]
		Visa,
    }
}

