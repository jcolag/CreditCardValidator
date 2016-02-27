//-----------------------------------------------------------------------
// <copyright file="CreditCardType.cs" company="Colagioia Industries">
//     Provided under the terms of the AGPL v3.
// </copyright>
//-----------------------------------------------------------------------

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
        /// American Express cards.
        /// </summary>
        [Description("American Express")]
        AmEx,

        /// <summary>
        /// Discover Card cards.
        /// </summary>
        [Description("Discover Card")]
        Discover,

        /// <summary>
        /// MasterCard cards.
        /// </summary>
        [Description("MasterCard")]
        MasterCard,

        /// <summary>
        /// Visa cards.
        /// </summary>
        [Description("Visa")]
        Visa,
    }
}