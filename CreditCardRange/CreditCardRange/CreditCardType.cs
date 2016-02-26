/// <summary>
/// Credit card type.
/// </summary>
namespace CreditCardProcessing
{
	using System.ComponentModel;

	/// <summary>
	/// Credit card type.
	/// </summary>
    public enum CreditCardType
    {
		/// <summary>
		/// Unknown issuers.
		/// </summary>
		[Description("Unknown Issuer")]
        Unknown,

		/// <summary>
		/// American Express.
		/// </summary>
		[Description("American Express")]
		AmEx,

		/// <summary>
		/// Discover Card.
		/// </summary>
		[Description("Discover Card")]
		Discover,

		/// <summary>
		/// Master Card.
		/// </summary>
		[Description("MasterCard")]
		MasterCard,

		/// <summary>
		/// Visa.
		/// </summary>
		[Description("Visa")]
		Visa,
    }
}