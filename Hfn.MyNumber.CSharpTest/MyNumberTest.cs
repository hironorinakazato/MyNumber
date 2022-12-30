namespace Hfn.MyNumber.CSharpTest
{
    using Hfn.Interoperation.Extensions;

    [TestClass]
    public class MyNumberTest
    {
        [TestInitialize]
        public void Initialize()
        { }

        [TestCleanup]
        public void Cleanup()
        { }

        [TestMethod]
        [TestCategory("CalculateCheckDigit")]
        public void CalculateCheckDigitTests()
        {
            var calculateCheckDigit = Hfn.MyNumberModule.CalculateCheckDigit;

            var result = calculateCheckDigit("12345678901");
            Assert.IsTrue(result.IsSome());
            Assert.AreEqual(8, result.Value);

            result = calculateCheckDigit("23456789012");
            Assert.IsTrue(result.IsSome());
            Assert.AreEqual(1, result.Value);

            result = calculateCheckDigit("01234567890");
            Assert.IsTrue(result.IsSome());
            Assert.AreEqual(3, result.Value);

            result = calculateCheckDigit("-12345678901");
            Assert.IsTrue(result.IsNone());

            result = calculateCheckDigit("12345678901.");
            Assert.IsTrue(result.IsNone());

            result = calculateCheckDigit(string.Empty);
            Assert.IsTrue(result.IsNone());

            result = calculateCheckDigit(null);
            Assert.IsTrue(result.IsNone());
        }

        [TestMethod]
        [TestCategory("IsCheckDigitValid")]
        public void IsCheckDigitValidTests()
        {
            var isCheckDigitValid = Hfn.MyNumberModule.IsCheckDigitValid;

            Assert.IsTrue(isCheckDigitValid("123456789018"));
            Assert.IsFalse(isCheckDigitValid("123456789010"));
            Assert.IsFalse(isCheckDigitValid("123456789011"));
            Assert.IsFalse(isCheckDigitValid("123456789012"));
            Assert.IsFalse(isCheckDigitValid("123456789013"));
            Assert.IsFalse(isCheckDigitValid("123456789014"));
            Assert.IsFalse(isCheckDigitValid("123456789015"));
            Assert.IsFalse(isCheckDigitValid("123456789016"));
            Assert.IsFalse(isCheckDigitValid("123456789017"));
            Assert.IsFalse(isCheckDigitValid("123456789019"));

            Assert.IsTrue(isCheckDigitValid("234567890121"));
            Assert.IsFalse(isCheckDigitValid("234567890120"));
            Assert.IsFalse(isCheckDigitValid("234567890122"));
            Assert.IsFalse(isCheckDigitValid("234567890123"));
            Assert.IsFalse(isCheckDigitValid("234567890124"));
            Assert.IsFalse(isCheckDigitValid("234567890125"));
            Assert.IsFalse(isCheckDigitValid("234567890126"));
            Assert.IsFalse(isCheckDigitValid("234567890127"));
            Assert.IsFalse(isCheckDigitValid("234567890128"));
            Assert.IsFalse(isCheckDigitValid("234567890129"));

            Assert.IsTrue(isCheckDigitValid("012345678903"));
            Assert.IsFalse(isCheckDigitValid("012345678900"));
            Assert.IsFalse(isCheckDigitValid("012345678901"));
            Assert.IsFalse(isCheckDigitValid("012345678902"));
            Assert.IsFalse(isCheckDigitValid("012345678904"));
            Assert.IsFalse(isCheckDigitValid("012345678905"));
            Assert.IsFalse(isCheckDigitValid("012345678906"));
            Assert.IsFalse(isCheckDigitValid("012345678907"));
            Assert.IsFalse(isCheckDigitValid("012345678908"));
            Assert.IsFalse(isCheckDigitValid("012345678909"));

            Assert.IsFalse(isCheckDigitValid("-12345678901"));
            Assert.IsFalse(isCheckDigitValid("12345678901."));
            Assert.IsFalse(isCheckDigitValid("12345678901"));

            Assert.IsFalse(isCheckDigitValid(string.Empty));
            Assert.IsFalse(isCheckDigitValid(null));
        }
    }
}

