# CreditCardValidator
First pass on a simple C# implementation to sanity-check credit card numbers.

On a recent project, I thought I was going to need a server-side credit card validator.  I didn't, as it turns out, but I put in a fair amount of thought to how I wanted it to work that it was easy enough to put together in case it's useful in the future.

The intent is to have a module that is more or less defined declaratively and eliminates the risk of comparisons that need to be performed in a specific order.  For any input number, then, the class should be able to identify the credit card issuer (subject to the supplied rules) and---if necessary---verify the checksum.


