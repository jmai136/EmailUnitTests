using Email;

namespace EmailTests
{
    [TestClass]
    public class EmailUnitTest : EmailTestBase
    {
        /****************** INVALID ADDRESSES **********/
        [TestMethod]
        public void IsValidEmail_NoAtSymbol_False()
        {
            // Arrange
            string email = GetTestSetting<string>("NoAtSymbol", "plainaddress.com");

            // Assert
            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No @ symbol: '{email}'.");
        }

        [TestMethod]
        public void IsValidEmail_DuplicateAtSymbol_False()
        {
            // Arrange
            string email = GetTestSetting<string>("DuplicateAtSymbolCharacter", "a@b@c@example.com");

            // Assert
            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Duplicate @ symbol: '{email}'.");
        }

        [TestMethod]
        public void IsValidEmail_NoDotSymbolAfterAt_False()
        {
            // Arrange
            string email = GetTestSetting<string>("NoDotSymbolAfterAt", "plainaddress@com");

            // Assert
            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No . symbol: '{email}'.");
        }

        [TestMethod]
        public void IsValidEmail_NoAlphaBeginningCharacter_Digit_False()
        {
            // Arrange
            string email = GetTestSetting<string>("NoAlphaBeginningCharacter_Digit", "1mail@something.com");

            // Assert
            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No alpha beginning digit character: '{email}'.");
        }

        [TestMethod]
        public void IsValidEmail_NoAlphaEndCharacter_Digit_False()
        {
            string email = GetTestSetting<string>("NoAlphaEndCharacter_Digit", "email@123.123.123.123");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No alpha end digit character: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NoAlphaEndCharacter_InvalidCharactersInRange_False()
        {
            string email = GetTestSetting<string>("NoAlphaEndCharacter_InvalidCharactersAboveRange", "email@domain.org[");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No alpha end invalid character above range: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NoAlphaEndCharacter_InvalidCharactersAboveRange_False()
        {
            string email = GetTestSetting<string>("NoAlphaEndCharacter_InvalidCharactersAboveRange", "email@domain.org©");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"No alpha end invalid character above range: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_SpacesInName_False()
        {
            string email = GetTestSetting<string>("SpacesInName", "Joe Smith email@example.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Spaces in name: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_SpacesInDomain_False()
        {
            string email = GetTestSetting<string>("SpacesInDomain", "email@e xa mple .c o m ");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Spaces in domain: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NameLengthLessThan1_False()
        {
            string email = GetTestSetting<string>("NameLengthLessThan1", "@example.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Name length less than 1: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NameLengthMoreThan100_False()
        {
            string email = GetTestSetting<string>("NameLengthMoreThan100", "Loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua@example.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Name length more than 100: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_DomainLengthLessThan1_False()
        {
            string email = GetTestSetting<string>("DomainLengthLessThan1", "abc@.");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Domain length less than 1: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_DomainLengthMoreThan100_False()
        {
            string email = GetTestSetting<string>("DomainLengthMoreThan100", "abc@loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua@example.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Domain length more than 100: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InvalidCharactersInName_False()
        {
            string email = GetTestSetting<string>("InvalidCharactersInName", "©µ@example.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Invalid characters in name: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InvalidSecondCharacterInNameOnward_False()
        {
            string email = GetTestSetting<string>("InvalidSecondCharacterInNameOnward", "A©µ@domain.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Invalid second character in name onward: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InvalidCharactersInDomain_False()
        {
            string email = GetTestSetting<string>("InvalidCharactersInDomain", "email@eðñòóôõö÷.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Invalid characters in domain: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NullOrEmpty_False()
        {
            string email = GetTestSetting<string>("NullOrEmpty", "");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Null or empty: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InRangeSpecialCharacterInNameButInvalid_False()
        {
            string email = GetTestSetting<string>("InRangeSpecialCharacterInNameButInvalid", "name)@domain.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"In range special character in name but invalid: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InRangeSpecialCharacterInNameButDomain_False()
        {
            string email = GetTestSetting<string>("InRangeSpecialCharacterInNameButDomain", "name@domain(.com");

            Assert.IsFalse(EmailValidator.IsValidEmail(email), $"In range special character in domain but invalid: '{email}'");
        }







        /****************** VALID ADDRESSES **********/
        [TestMethod]
        public void IsValidEmail_MultipleHigherDomains_True()
        {
            string email = GetTestSetting<string>("MultipleHigherDomains", "tippugrautrepra-3693@kili.org.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Multiple higher domains: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_InRangeLength_True()
        {
            string email = GetTestSetting<string>("InRangeLength", "email@subdomain.example.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"In range length: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_Digits_True()
        {
            string email = GetTestSetting<string>("Digits", "t532953@564613.36m");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Digits: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_Standard_True()
        {
            string email = GetTestSetting<string>("Standard", "zhaid@example.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Standard: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_DotBeforeAt_True()
        {
            string email = GetTestSetting<string>("DotBeforeAt", "firstname.lastname@example.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Dot before @: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_AcceptableSpecialCharactersInName_True()
        {
            string email = GetTestSetting<string>("AcceptableSpecialCharactersInName", "A~!$%^&amp;*_=+}{'?-@domain.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Acceptable special characters in name: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_AcceptableSpecialCharactersInDomain_True()
        {
            string email = GetTestSetting<string>("AcceptableSpecialCharactersInDomain", "abc@~!$%^&amp;*_=+}{'?-.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Acceptable special characters in domain: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_UppercaseAlphaInDomain_True()
        {
            string email = GetTestSetting<string>("UppercaseAlphaInDomain", "name@UPPERCASE.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Uppercase alpha in domain: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_AcceptableSpecialCharacters_DashSign_True()
        {
            string email = GetTestSetting<string>("AcceptableSpecialCharacters_DashSign", "waffaprubouye-2869@yopmail.com");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Acceptable special characters - dash sign: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_NonDotComDomain_True()
        {
            string email = GetTestSetting<string>("NonDotComDomain", "abc@example.jp");

            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Non .com domain: '{email}'");
        }

        [TestMethod]
        public void IsValidEmail_UppercaseAlphaInLastCharacter_True()
        {
            // Arrange
            string email = GetTestSetting<string>("UppercaseAlphaInLastCharacter", "name@domain.coM");

            // Assert
            Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Uppercase alpha in last character: '{email}'.");
        }
    }
}