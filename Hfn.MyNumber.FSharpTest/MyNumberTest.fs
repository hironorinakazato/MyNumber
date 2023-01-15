namespace Hfn.MyNumber.FSharpTest

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type MyNumberTest() =
    [<TestInitialize>]
    member _.Initialize() = ()

    [<TestCleanup>]
    member _.Cleanup() = ()

    [<TestMethod>]
    [<TestCategory("calculateCheckDigit")>]
    member _.CalculateCheckDigitTests() =
        let calculateCheckDigit = Hfn.MyNumber.calculateCheckDigit

        let mutable result = calculateCheckDigit "12345678901"
        Assert.IsTrue(result.IsSome)
        Assert.AreEqual(8, result.Value)

        result <- calculateCheckDigit "23456789012"
        Assert.IsTrue(result.IsSome)
        Assert.AreEqual(1, result.Value)

        result <- calculateCheckDigit "01234567890"
        Assert.IsTrue(result.IsSome)
        Assert.AreEqual(3, result.Value)

        result <- calculateCheckDigit "-12345678901"
        Assert.IsFalse(result.IsSome)

        result <- calculateCheckDigit "12345678901."
        Assert.IsFalse(result.IsSome)

        result <- calculateCheckDigit ""
        Assert.IsFalse(result.IsSome)

        result <- calculateCheckDigit null
        Assert.IsFalse(result.IsSome)

    [<TestMethod>]
    [<TestCategory("isCheckDigitValid")>]
    member _.IsCheckDigitValidTests() =
        let isCheckDigitValid = Hfn.MyNumber.isCheckDigitValid

        Assert.IsTrue(isCheckDigitValid "123456789018");
        Assert.IsFalse(isCheckDigitValid "123456789010");
        Assert.IsFalse(isCheckDigitValid "123456789011");
        Assert.IsFalse(isCheckDigitValid "123456789012");
        Assert.IsFalse(isCheckDigitValid "123456789013");
        Assert.IsFalse(isCheckDigitValid "123456789014");
        Assert.IsFalse(isCheckDigitValid "123456789015");
        Assert.IsFalse(isCheckDigitValid "123456789016");
        Assert.IsFalse(isCheckDigitValid "123456789017");
        Assert.IsFalse(isCheckDigitValid "123456789019");

        Assert.IsTrue(isCheckDigitValid "234567890121");
        Assert.IsFalse(isCheckDigitValid "234567890120");
        Assert.IsFalse(isCheckDigitValid "234567890122");
        Assert.IsFalse(isCheckDigitValid "234567890123");
        Assert.IsFalse(isCheckDigitValid "234567890124");
        Assert.IsFalse(isCheckDigitValid "234567890125");
        Assert.IsFalse(isCheckDigitValid "234567890126");
        Assert.IsFalse(isCheckDigitValid "234567890127");
        Assert.IsFalse(isCheckDigitValid "234567890128");
        Assert.IsFalse(isCheckDigitValid "234567890129");

        Assert.IsTrue(isCheckDigitValid "012345678903");
        Assert.IsFalse(isCheckDigitValid "012345678900");
        Assert.IsFalse(isCheckDigitValid "012345678901");
        Assert.IsFalse(isCheckDigitValid "012345678902");
        Assert.IsFalse(isCheckDigitValid "012345678904");
        Assert.IsFalse(isCheckDigitValid "012345678905");
        Assert.IsFalse(isCheckDigitValid "012345678906");
        Assert.IsFalse(isCheckDigitValid "012345678907");
        Assert.IsFalse(isCheckDigitValid "012345678908");
        Assert.IsFalse(isCheckDigitValid "012345678909");

        Assert.IsFalse(isCheckDigitValid "-12345678901");
        Assert.IsFalse(isCheckDigitValid "12345678901.");
        Assert.IsFalse(isCheckDigitValid "12345678901");

        Assert.IsFalse(isCheckDigitValid "");
        Assert.IsFalse(isCheckDigitValid null);

