using NUnit.Framework;
using Ninject;

namespace Tests.Day11
{
    [TestFixture]
    public sealed class CorporatePolicyTests : TestBase
    {
        // hijklmmn meets the first requirement (because it contains the straight hij)
        // but fails the second requirement requirement (because it contains i and l).
        [TestCase("hijklmmn", ExpectedResult = false)]
        // abbceffg meets the third requirement (because it repeats bb and ff)
        // but fails the first requirement.
        [TestCase("abbceffg", ExpectedResult = false)]
        // abbcegjk fails the third requirement, because it only has one double letter (bb).
        [TestCase("abbcegjk", ExpectedResult = false)]
        // The next password after abcdefgh is abcdffaa.
        [TestCase("abcdffaa", ExpectedResult = true)]
        // The next password after ghijklmn is ghjaabcc, because you eventually skip
        // all the passwords that start with ghi..., since i is not allowed.
        [TestCase("ghjaabcc", ExpectedResult = true)]
        public bool Provided_passwords_are_incorrect(string password)
        {
            IPasswordChecker passwordChecker = Kernel.Get<IPasswordChecker>();
            bool result = passwordChecker.IsCorrect(password);
            return result;
        }

        // The next password after abcdefgh is abcdffaa.
        [TestCase("abcdefgh", ExpectedResult = "abcdffaa")]
        // The next password after ghijklmn is ghjaabcc, because you eventually skip
        // all the passwords that start with ghi..., since i is not allowed.
        [TestCase("ghijklmn", ExpectedResult = "ghjaabcc")]
        public string Next_password_is_calculated_correctly(string password)
        {
            IPasswordGenerator passwordGenerator = Kernel.Get<IPasswordGenerator>();
            string result = passwordGenerator.GenerateNextPassword(password);
            return result;
        }
    }
}
