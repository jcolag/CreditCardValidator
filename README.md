# CreditCardValidator
First pass on a simple C# implementation to sanity-check credit card numbers.

On a recent project, I thought I was going to need a server-side credit card validator.  I didn't, as it turns out, but I put in a fair amount of thought to how I wanted it to work that it was easy enough to put together in case it's useful in the future.

The intent is to have a module that is more or less defined declaratively and eliminates the risk of comparisons that need to be performed in a specific order.  For any input number, then, the class should be able to identify the credit card issuer (subject to the supplied rules) and---if necessary---verify the checksum.

## Declaration

As mentioned, credit cards types are mostly declarative.  For example, old (obsolete) Bankcard would be represented by adding an entry to CreditCardType (if desired; the "Unknown" tag could be sufficient or it may be added in a future version) and simply describing the type.

    new CreditCardRange() { "Bankcard", CreditCardType.Bankcard,
        "5610,560221-560225", { 16 }, false, true, true };

That's an extreme case, of course, where all the work needs to be done by the programmer.  If the card name can be derived from the enumerated type and the flags' defaults are correct:

    new CreditCardRange() { Issuer = CreditCardType.Visa, Numbers = "4",
        Lengths = { 16 } }

Note that they're not assigned anywhere.  Each range object is stored and managed internally as part of the class.

## Usage

At least for expected cases, all anyone should need to know is...

    CreditCardType CreditCardRange.ValidateCardNumber(String creditCardNumber)

Give it the credit card number and it returns a member of the enumerated type representing the issuer, making sure the number conforms to the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm), if specified.

Note one critical point:  The class automatically orders the Issuer Identification Number Ranges (IINs) from most specific to least.  So, for example, [Visa](https://usa.visa.com/) has the range of numbers starting with a `4-`.  If, some day, through the magic of mergers, acquisitions, and trades, some obscure company (we can call them "Potlatch") ends up with all cards starting with `498765-`, the _CreditCardRange_ class will check the Potlatch cards first, so that Visa doesn't prevent them from getting recognized.


