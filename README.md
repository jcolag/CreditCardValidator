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


