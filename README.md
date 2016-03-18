# Credit Card Validator

First pass on a simple C# implementation to sanity-check credit card numbers.

On a recent project, I thought I was going to need a server-side credit card validator.  I didn't, as it turns out, but I put in a fair amount of thought to how I wanted it to work that it was easy enough to put together in case it's useful in the future.

The intent is to have a module that is more or less defined declaratively and eliminates the risk of comparisons that need to be performed in a specific order.  For any input number, then, the class should be able to identify the credit card issuer (subject to the supplied rules) and---if necessary---verify the checksum.

A further intent is to avoid the usual unmaintainable mess of fixed regular expressions that have never been investigated beyond working for the required cases.

## Declaring Issuers

As mentioned, credit cards types are mostly declarative.  For example, old (obsolete) Bankcard would be represented by adding an entry to CreditCardType (if desired; the "Unknown" tag could be sufficient or it may be added in a future version) and simply describing the type.

    new CreditCardRange() { "Bankcard", CreditCardType.Bankcard,
        "5610,560221-560225", { 16 }, false, true, true };

That's an extreme case, where all the work needs to be done by the programmer.  If the card name can be derived from the enumerated type and the flags' defaults are correct.  For example, the four major carriers in the United States:

    new CreditCardRange { Issuer = CreditCardType.AmEx, Numbers = "34,37", Lengths = { 15 } };
    new CreditCardRange { Issuer = CreditCardType.Visa, Numbers = "4", Lengths = { 16 } };
    new CreditCardRange {
        Issuer = CreditCardType.MasterCard,
        Numbers = "51-55",
        Lengths = { 16 }
    };
    new CreditCardRange {
        Issuer = CreditCardType.Discover,
        Numbers = "6011,622126-622925,644-649,65",
        Lengths = { 16 }
    };

Note that the range objects aren't _assigned_ anywhere.  Each object is stored and managed internally as part of the class.

### Sources

Apart from the issuers themselves, an obvious place to mine for updates to credit card numbers is the Wikipedia page on [Bank Card Numbers](https://en.wikipedia.org/wiki/Bank_card_number), which is the source of all samples and defaults.  Knowing the "new" (effective 2008!) Discover Card issuer ranges is like wizardry, in most circles.

### Quick and Dirty

Want to just load every known (at this writing) issuer and deal with everything later?  Make a call to `CreditCardRange.CreateDefaults()`.  It takes an (optional) list of issuers (of enumeration class `CreditCardType`) to enumerate those that you plan to accept.  Note that this means relying on the code to be up to date, which may not be the case at any given time, even now; the most recent update was 06 March 2016.

## Usage

At least for expected cases, all anyone should need to know is...

    CreditCardType CreditCardRange.ValidateCardNumber(String creditCardNumber)

Give it the credit card number and it returns a member of the enumerated type representing the issuer, making sure the number conforms to the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm), if specified.

Note one critical point:  The class automatically orders the Issuer Identification Number Ranges (IINs) from most specific to least.  So, for example, [Visa](https://usa.visa.com/) has the range of numbers starting with a `4-`.  If, some day, through the magic of mergers, acquisitions, and trades, some obscure company (we can call them "Potlatch") ends up with all cards starting with `498765-`, the _CreditCardRange_ class will check the Potlatch cards first, so that Visa doesn't prevent them from getting recognized.


