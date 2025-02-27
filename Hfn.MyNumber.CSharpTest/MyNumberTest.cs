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
            var calculateCheckDigit = MyNumberModule.CalculateCheckDigit;

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
        [TestCategory("isValidCheckDigit")]
        public void IsValidCheckDigitTests()
        {
            var isValidCheckDigit = MyNumberModule.IsValidCheckDigit;

            Assert.IsTrue(isValidCheckDigit("123456789018"));
            Assert.IsFalse(isValidCheckDigit("123456789010"));
            Assert.IsFalse(isValidCheckDigit("123456789011"));
            Assert.IsFalse(isValidCheckDigit("123456789012"));
            Assert.IsFalse(isValidCheckDigit("123456789013"));
            Assert.IsFalse(isValidCheckDigit("123456789014"));
            Assert.IsFalse(isValidCheckDigit("123456789015"));
            Assert.IsFalse(isValidCheckDigit("123456789016"));
            Assert.IsFalse(isValidCheckDigit("123456789017"));
            Assert.IsFalse(isValidCheckDigit("123456789019"));

            Assert.IsTrue(isValidCheckDigit("234567890121"));
            Assert.IsFalse(isValidCheckDigit("234567890120"));
            Assert.IsFalse(isValidCheckDigit("234567890122"));
            Assert.IsFalse(isValidCheckDigit("234567890123"));
            Assert.IsFalse(isValidCheckDigit("234567890124"));
            Assert.IsFalse(isValidCheckDigit("234567890125"));
            Assert.IsFalse(isValidCheckDigit("234567890126"));
            Assert.IsFalse(isValidCheckDigit("234567890127"));
            Assert.IsFalse(isValidCheckDigit("234567890128"));
            Assert.IsFalse(isValidCheckDigit("234567890129"));

            Assert.IsTrue(isValidCheckDigit("012345678903"));
            Assert.IsFalse(isValidCheckDigit("012345678900"));
            Assert.IsFalse(isValidCheckDigit("012345678901"));
            Assert.IsFalse(isValidCheckDigit("012345678902"));
            Assert.IsFalse(isValidCheckDigit("012345678904"));
            Assert.IsFalse(isValidCheckDigit("012345678905"));
            Assert.IsFalse(isValidCheckDigit("012345678906"));
            Assert.IsFalse(isValidCheckDigit("012345678907"));
            Assert.IsFalse(isValidCheckDigit("012345678908"));
            Assert.IsFalse(isValidCheckDigit("012345678909"));

            Assert.IsFalse(isValidCheckDigit("-12345678901"));
            Assert.IsFalse(isValidCheckDigit("12345678901."));
            Assert.IsFalse(isValidCheckDigit("12345678901"));

            Assert.IsFalse(isValidCheckDigit(string.Empty));
            Assert.IsFalse(isValidCheckDigit(null));
        }
    }
}

