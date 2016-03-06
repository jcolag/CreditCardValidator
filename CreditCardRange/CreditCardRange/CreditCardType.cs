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

        /// <summary>
        /// Bankcard cards.
        /// </summary>
        [Description("Bankcard")]
        Bankcard,

        /// <summary>
        /// China Union Pay cards.
        /// </summary>
        [Description("China Union Pay")]
        ChinaUnionPay,

        /// <summary>
        /// Diners Club Carte Blanche cards.
        /// </summary>
        [Description("Diners Club Carte Blanche")]
        DCCarteBlanche,

        /// <summary>
        /// Diners Club enRoute cards.
        /// </summary>
        [Description("Diners Club enRoute")]
        DCEnRoute,

        /// <summary>
        /// Diners Club International cards.
        /// </summary>
        [Description("Diners Club International")]
        DCInternational,

        /// <summary>
        /// Diners Club US and Canada cards.
        /// </summary>
        [Description("Diners Club US and Canada")]
        DCUSCan,

        /// <summary>
        /// InterPayment cards.
        /// </summary>
        [Description("InterPayment")]
        InterPayment,

        /// <summary>
        /// InstaPayment cards.
        /// </summary>
        [Description("InstaPayment")]
        InstaPayment,

        /// <summary>
        /// JCB cards.
        /// </summary>
        [Description("JCB")]
        JCB,

        /// <summary>
        /// Laser cards.
        /// </summary>
        [Description("Laser")]
        Laser,

        /// <summary>
        /// Maestro cards.
        /// </summary>
        [Description("Maestro")]
        Maestro,

        /// <summary>
        /// Dankort cards.
        /// </summary>
        [Description("Dankort")]
        Dankort,

        /// <summary>
        /// Solo cards.
        /// </summary>
        [Description("Solo")]
        Solo,

        /// <summary>
        /// Switch cards.
        /// </summary>
        [Description("Switch")]
        Switch,

        /// <summary>
        /// UATP cards.
        /// </summary>
        [Description("UATP")]
        UATP,

        /// <summary>
        /// Verve cards.
        /// </summary>
        [Description("Verve")]
        Verve,

        /// <summary>
        /// Cardguard cards.
        /// </summary>
        [Description("Cardguard EAD BG ILS")]
        Cardguard,
    }
}