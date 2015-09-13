﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.BuildingBlocks.Messaging.Constraints
{
    [TestClass]
    public sealed class StringConstraintTest : ConstraintTest
    {                
        #region [====== IsNotNullOrEmpty ======]

        [TestMethod]
        public void ValidateIsNotNullOrEmpty_ReturnsNoErrors_IfStringIsNotNullOrEmpty()
        {            
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrEmpty(RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();            
        }

        [TestMethod]
        public void ValidateIsNotNullOrEmpty_ReturnsExpectedError_IfStringIsNull()
        {            
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrEmpty(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);            
        }

        [TestMethod]
        public void ValidateIsNotNullOrEmpty_ReturnsExpectedError_IfStringIsEmpty()
        {            
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrEmpty(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);            
        }

        [TestMethod]
        public void ValidateIsNotNullOrEmpty_ReturnsDefaultError_IfStringIsEmpty_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrEmpty();

            validator.Validate(message).AssertOneError("Member () is not allowed to be null or empty.");
        }

        #endregion

        #region [====== IsNullOrEmpty ======]

        [TestMethod]
        public void ValidateIsNullOrEmpty_ReturnsNoErrors_IfStringIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrEmpty();

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNullOrEmpty_ReturnsNoErrors_IfStringIsEmpty()
        {
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrEmpty();

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNullOrEmpty_ReturnsExpectedError_IfStringHasNonEmptyValue()
        {
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrEmpty(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsNullOrEmpty_ReturnsDefaultError_IfStringHasNonEmptyValue_And_NoErrorMessageIsSpecified()
        {
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrEmpty();

            validator.Validate(message).AssertOneError(string.Format("Member ({0}) must be either null or empty.", message.Member));
        }

        #endregion

        #region [====== IsNotNullOrWhiteSpace ======]

        [TestMethod]
        public void ValidateIsNotNullOrWhiteSpace_ReturnsNoErrors_IfStringIsNotNullOrWhiteSpace()
        {            
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrWhiteSpace(RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();           
        }

        [TestMethod]
        public void ValidateIsNotNullOrWhiteSpace_ReturnsExpectedError_IfStringIsNull()
        {            
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrWhiteSpace(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);            
        }

        [TestMethod]
        public void ValidateIsNotNullOrWhiteSpace_ReturnsExpectedError_IfStringIsEmpty()
        {            
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrWhiteSpace(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);            
        }

        [TestMethod]
        public void ValidateIsNotNullOrWhiteSpace_ReturnsExpectedError_IfStringIsWhiteSpace()
        {            
            var message = new ValidatedMessage<string>("     ");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrWhiteSpace(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);            
        }

        [TestMethod]
        public void ValidateIsNotNullOrWhiteSpace_ReturnsDefaultError_IfStringIsWhiteSpace_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("     ");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotNullOrWhiteSpace();

            validator.Validate(message).AssertOneError("Member (     ) is not allowed to be null or contain only white space.");
        }

        #endregion

        #region [====== IsNullOrWhiteSpace ======]

        [TestMethod]
        public void ValidateIsNullOrWhiteSpace_ReturnsNoErrors_IfStringIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrWhiteSpace();

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNullOrWhiteSpace_ReturnsNoErrors_IfStringIsEmpty()
        {
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrWhiteSpace();

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNullOrWhiteSpace_ReturnsNoErrors_IfStringIsWhiteSpace()
        {
            var message = new ValidatedMessage<string>("     ");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrWhiteSpace();

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNullOrWhiteSpace_ReturnsExpectedError_IfStringHasNonWhiteSpaceValue()
        {
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrWhiteSpace(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsNullOrWhiteSpace_ReturnsDefaultError_IfStringHasNonWhiteSpaceValue_And_NoErrorMessageIsSpecified()
        {
            var message = NewValidatedMessage();
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNullOrWhiteSpace();

            validator.Validate(message).AssertOneError(string.Format("Member ({0}) must be either null or contain only white space.", message.Member));
        }

        #endregion

        #region [====== IsNotEqualTo ======]

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsExpectedError_IfMemberIsEqualToValue_Ordinal()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some value", StringComparison.Ordinal, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsDefaultError_IfMemberIsEqualToValue_Ordinal_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some value", StringComparison.Ordinal);

            validator.Validate(message).AssertOneError("Member (Some value) must not be equal to 'Some value'.");
        }

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsNoErrors_IfMemberIsNotEqualToValue_Ordinal()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some VALUE", StringComparison.Ordinal, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsExpectedError_IfMemberIsEqualToValue_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsExpectedError_IfMemberIsEqualToValueExceptForCasing_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some VALUE", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsNotEqualTo_ReturnsNoErrors_IfMemberIsNotEqualToValue_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsNotEqualTo("Some other value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        #endregion

        #region [====== IsEqualTo ======]

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsNoErrors_IfMemberIsEqualToValue_Ordinal()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some value", StringComparison.Ordinal, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsExpectedError_IfMemberIsNotEqualToValue_Ordinal()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some VALUE", StringComparison.Ordinal, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsDefaultError_IfMemberIsNotEqualToValue_Ordinal_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some VALUE", StringComparison.Ordinal);

            validator.Validate(message).AssertOneError("Member (Some value) must be equal to 'Some VALUE'.");
        }

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsNoErrors_IfMemberIsEqualToValue_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsNoErrors_IfMemberIsEqualToValueExceptForCasing_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some VALUE", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsEqualTo_ReturnsExpectedError_IfMemberIsNotEqualToValue_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsEqualTo("Some other value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== DoesNotStartWith ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateDoesNotStartWith_Throws_IfPrefixIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotStartWith(null);
        }        

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateDoesNotStartWith_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotStartWith("SOME");

            validator.Validate(message);
        }               

        [TestMethod]
        public void ValidateDoesNotStartWith_ReturnsExpectedError_IfValueStartsWithSpecifiedPrefix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotStartWith("SOME", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateDoesNotStartWith_ReturnsDefaultError_IfValueStartsWithSpecifiedPrefix_OrdinalIgnoreCase_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotStartWith("SOME", StringComparison.OrdinalIgnoreCase);

            validator.Validate(message).AssertOneError("Member (Some value) must not start with 'SOME'.");
        }

        [TestMethod]
        public void ValidateDoesNotStartWith_ReturnsNoErrors_IfValueDoesNotStartWithSpecifiedPrefix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotStartWith("value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        #endregion

        #region [====== StartsWith ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateStartsWith_Throws_IfPrefixIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith(null);            
        }

        [TestMethod]
        public void ValidateStartsWith_ReturnsNoErrors_IfValueStartsWithSpecifiedPrefix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("Some", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateStartsWith_Throws_IfValueIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("SOME");

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateStartsWith_ReturnsExpectedError_IfValueDoesNotStartWithSpecifiedPrefix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("SOME", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateStartsWith_ReturnsExpectedError_IfValueDoesNotStartWithSpecifiedPrefix_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("SOME");

            validator.Validate(message).AssertOneError("Member (Some value) must start with 'SOME'.");
        }

        [TestMethod]
        public void ValidateStartsWith_ReturnsNoErrors_IfValueStartsWithSpecifiedPrefix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("SOME", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateStartsWith_ReturnsNoErrors_IfValueDoesNotStartWithSpecifiedPrefix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).StartsWith("value", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== DoesNotEndWith ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateDoesNotEndWith_Throws_IfPostfixIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith(null);

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateDoesNotEndWith_ReturnsExpectedError_IfValueEndsWithSpecifiedPostfix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith("value", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateDoesNotEndWith_ReturnsDefaultError_IfValueEndsWithSpecifiedPostfix_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith("value");

            validator.Validate(message).AssertOneError("Member (Some value) must not end with 'value'.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateDoesNotEndWith_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith("value");

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateDoesNotEndWith_ReturnsNoErrors_IfValueDoesNotEndWithSpecifiedPostfix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith("VALUE");

            validator.Validate(message).AssertNoErrors();
        }        

        [TestMethod]
        public void ValidateDoesNotEndWith_ReturnsExpectedErrors_IfValueEndsWithSpecifiedPostfix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotEndWith("VALUE", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }        

        #endregion

        #region [====== EndsWith ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateEndsWith_Throws_IfPostfixIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith(null);

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateEndsWith_ReturnsNoErrors_IfValueEndsWithSpecifiedPostfix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("value", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateEndsWith_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("value");

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateEndsWith_ReturnsExpectedError_IfValueDoesNotEndWithSpecifiedPostfix()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("VALUE", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateEndsWith_ReturnsExpectedError_IfValueDoesNotEndWithSpecifiedPostfix_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("VALUE");

            validator.Validate(message).AssertOneError("Member (Some value) must end with 'VALUE'.");
        }

        [TestMethod]
        public void ValidateEndsWith_ReturnsNoErrors_IfValueEndsWithSpecifiedPostfix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("VALUE", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateEndsWith_ReturnsNoErrors_IfValueDoesNotEndWithSpecifiedPostfix_OrdinalIgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).EndsWith("Some", StringComparison.OrdinalIgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== DoesNotContain ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateDoesNotContain_Throws_IfSpecifiedValueIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotContain(null, RandomErrorMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateDoesNotContain_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotContain("x");

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateDoesNotContain_ReturnsNoErrors_IfValueDoesNotContainSpecifiedValue()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotContain("xyz", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateDoesNotContain_ReturnsExpectedError_IfValueContainsValue()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotContain("e va", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateDoesNotContain_ReturnsExpectedError_IfValueContainsValue_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotContain("e va");

            validator.Validate(message).AssertOneError("Member (Some value) must not contain 'e va'.");
        }

        #endregion

        #region [====== Contains ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateContains_Throws_IfSpecifiedValueIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Contains(null, RandomErrorMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateContains_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Contains("x");

            validator.Validate(message);
        }

        [TestMethod]       
        public void ValidateContains_ReturnsExpectedError_IfValueDoesNotContainSpecifiedValue()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Contains("xyz", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateContains_ReturnsExpectedError_IfValueDoesNotContainSpecifiedValue_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Contains("xyz");

            validator.Validate(message).AssertOneError("Member (Some value) must contain 'xyz'.");
        }

        [TestMethod]
        public void ValidateContains_ReturnsNoErrors_IfValueContainsSpecifiedValue()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Contains("e va", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        #endregion

        #region [====== DoesNotMatch ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateDoesNotMatch_Throws_IfPatternIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateDoesNotMatch_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("x");

            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateDoesNotMatch_ReturnsExpectedError_IfValueMatchesSpecifiedPattern()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("v.l", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateDoesNotMatch_ReturnsDefaultError_IfValueMatchesSpecifiedPattern_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("v.l");

            validator.Validate(message).AssertOneError("Member (Some value) must not match pattern 'v.l'.");
        }

        [TestMethod]
        public void ValidateDoesNotMatch_ReturnsNoErrors_IfValueDoesNotMatchSpecifiedPattern()
        {
            var message = new ValidatedMessage<string>("Some VALUE");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("v.l", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateDoesNotMatch_ReturnsExpectedError_IfValueMatchesSpecifiedPattern_IgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some VALUE");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("v.l", RegexOptions.IgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateDoesNotMatch_ReturnsNoErrors_IfValueDoesNotMatchSpecifiedPattern_IgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).DoesNotMatch("valeu", RegexOptions.IgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        #endregion

        #region [====== Matches ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateMatches_Throws_IfPatternIsNull()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateMatches_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("x");

            validator.Validate(message);
        }

        [TestMethod]        
        public void ValidateMatches_ReturnsNoErrors_IfValueMatchesSpecifiedPattern()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("v.l", RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateMatches_ReturnsExpectedError_IfValueDoesNotMatchSpecifiedPattern()
        {
            var message = new ValidatedMessage<string>("Some VALUE");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("v.l", RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateMatches_ReturnsDefaultError_IfValueDoesNotMatchSpecifiedPattern()
        {
            var message = new ValidatedMessage<string>("Some VALUE");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("v.l");

            validator.Validate(message).AssertOneError("Member (Some VALUE) must match pattern 'v.l'.");
        }

        [TestMethod]
        public void ValidateMatches_ReturnsNoErrors_IfValueMatchesSpecifiedPattern_IgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some VALUE");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("v.l", RegexOptions.IgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateMatches_ReturnsExpectedError_IfValueDoesNotMatchSpecifiedPattern_IgnoreCase()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).Matches("valeu", RegexOptions.IgnoreCase, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== Length ======]

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateHasLengthOf_Throws_IfLengthIsNegative()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthOf(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateHasLengthOf_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthOf(10);

            validator.Validate(message);
        }

        [TestMethod]        
        public void ValidateHasLengthOf_ReturnsExpectedError_IfValueDoesNotHaveSpecifiedLength()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthOf(9, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateHasLengthOf_ReturnsDefaultError_IfValueDoesNotHaveSpecifiedLength_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthOf(9);

            validator.Validate(message).AssertOneError("Member (Some value) must have a length of 9 character(s).");
        }

        [TestMethod]
        public void ValidateHasLengthOf_ReturnsNoErrors_IfValueHasSpecifiedLength()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthOf(10, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateHasLengthBetween_Throws_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(0, 10);

            validator.Validate(message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateHasLengthBetween_Throws_IfMaximumIsNegative()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(-2, -1, RandomErrorMessage);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateHasLengthBetween_Throws_IfMaximumIsSmallerThanMinimum()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(8, 7, RandomErrorMessage);
        }

        [TestMethod]        
        public void ValidateHasLengthBetween_ReturnsExpectedError_IfLengthOfValueIsNotInSpecifiedRange()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(0, 9, RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateHasLengthBetween_ReturnsDefaultError_IfLengthOfValueIsNotInSpecifiedRange_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(0, 9);

            validator.Validate(message).AssertOneError("Member's Length (Some value) must be within range [0, 9].");
        }

        [TestMethod]
        public void ValidateHasLengthBetween_ReturnsNoErrors_IfLengthOfValueIsInSpecifiedRange()
        {
            var message = new ValidatedMessage<string>("Some value");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).HasLengthBetween(9, 11, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        #endregion

        #region [====== IsByte ======]

        [TestMethod]        
        public void ValidateIsByte_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsByte_ReturnsNoErrors_IfValueIsByte()
        {
            const byte value = 255;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsByte(RandomErrorMessage)                
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsByte_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsByte_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsByte();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a byte.");
        }

        [TestMethod]
        public void ValidateIsByte_ReturnsExpectedError_IfValueIsTooSmallForByte()
        {
            var message = new ValidatedMessage<string>("-1");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsByte(RandomErrorMessage);                

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsByte_ReturnsExpectedErrors_IfValueIsTooLargeForByte()
        {
            var message = new ValidatedMessage<string>("256");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== IsSByte ======]

        [TestMethod]        
        public void ValidateIsSByte_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsNoErrors_IfValueIsPositiveSByte()
        {
            const sbyte value = 127;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsSByte(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsNoErrors_IfValueIsNegativeSByte()
        {
            const sbyte value = -128;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsSByte(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSByte();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a signed byte.");
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsExpectedError_IfValueIsTooSmallForByte()
        {
            var message = new ValidatedMessage<string>("-129");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSByte_ReturnsExpectedError_IfValueIsTooLargeForByte()
        {
            var message = new ValidatedMessage<string>("128");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSByte(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== IsChar ======]

        [TestMethod]        
        public void ValidateIsChar_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsChar(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsChar_ReturnsNoErrors_IfStringConsistsOfOnlyASingleCharacter()
        {
            const char value = 'a';
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsChar(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsChar_ReturnsExpectedError_IfValueCannotBeConvertedToCharacter()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsChar(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsChar_ReturnsDefaultError_IfValueCannotBeConvertedToCharacter_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsChar();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a single character.");
        }

        [TestMethod]
        public void ValidateIsChar_ReturnsExpectedError_IfStringIsEmpty()
        {
            var message = new ValidatedMessage<string>(string.Empty);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsChar(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsChar_ReturnsExpectedError_IfStringHasMoreThanOneCharacter()
        {
            var message = new ValidatedMessage<string>(Guid.NewGuid().ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsChar(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== IsInt16 ======]

        [TestMethod]        
        public void ValidateIsInt16_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt16(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsNoErrors_IfValueIsPositiveInt16()
        {
            const short value = 32767;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt16(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsNoErrors_IfValueIsNegativeInt16()
        {
            const short value = -32768;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt16(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt16(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt16();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 16-bit integer.");
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsExpectedError_IfValueIsTooSmallForInt16()
        {
            var message = new ValidatedMessage<string>("-32769");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt16(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt16_ReturnsExpectedError_IfValueIsTooLargeForInt16()
        {
            var message = new ValidatedMessage<string>("32768");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt16(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion        

        #region [====== IsInt32 ======]

        [TestMethod]        
        public void ValidateIsInt32_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt32(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsNoErrors_IfValueIsPositiveInt32()
        {
            const int value = 2147483647;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt32(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsNoErrors_IfValueIsNegativeInt32()
        {
            const int value = -2147483648;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt32(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt32(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt32();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 32-bit integer.");
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsExpectedError_IfValueIsTooSmallForInt32()
        {
            var message = new ValidatedMessage<string>("-2147483649");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt32(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt32_ReturnsExpectedError_IfValueIsTooLargeForInt32()
        {
            var message = new ValidatedMessage<string>("2147483648");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt32(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion        

        #region [====== IsInt64 ======]

        [TestMethod]        
        public void ValidateIsInt64_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt64(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsNoErrors_IfValueIsPositiveInt64()
        {
            const long value = 9223372036854775807;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt64(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsNoErrors_IfValueIsNegativeInt64()
        {
            const long value = -9223372036854775808;
            var message = new ValidatedMessage<string>(value.ToString());
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsInt64(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt64(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt64();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 64-bit integer.");
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsExpectedError_IfValueIsTooSmallForInt64()
        {
            var message = new ValidatedMessage<string>("-9223372036854775809");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt64(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsInt64_ReturnsExpectedError_IfValueIsTooLargeForInt64()
        {
            var message = new ValidatedMessage<string>("9223372036854775808");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsInt64(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion        

        #region [====== IsSingle ======]

        [TestMethod]        
        public void ValidateIsSingle_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSingle(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsNoErrors_IfValueIsPositiveSingle()
        {
            const float value = 3.4E+38F;
            var message = new ValidatedMessage<string>("3.4E+37");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsSingle(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsNoErrors_IfValueIsNegativeSingle()
        {
            const float value = -3.4E+38F;
            var message = new ValidatedMessage<string>("-3.4E+37");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsSingle(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSingle(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSingle();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 32-bit floating point number.");
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsExpectedError_IfValueIsTooSmallForSingle()
        {
            var message = new ValidatedMessage<string>("-3.4E+38");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSingle(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsSingle_ReturnsExpectedError_IfValueIsTooLargeForSingle()
        {
            var message = new ValidatedMessage<string>("3.4E+38");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsSingle(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== IsDouble ======]

        [TestMethod]        
        public void ValidateIsDouble_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDouble(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsNoErrors_IfValueIsPositiveDouble()
        {
            const double value = 1.7E+308;
            var message = new ValidatedMessage<string>("1.7E+307");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsDouble(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsNoErrors_IfValueIsNegativeDouble()
        {
            const double value = -1.7E+308;
            var message = new ValidatedMessage<string>("-1.7E+307");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsDouble(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDouble(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDouble();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 64-bit floating point number.");
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsExpectedError_IfValueIsTooSmallForDouble()
        {
            var message = new ValidatedMessage<string>("-1.7E+308");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDouble(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDouble_ReturnsExpectedError_IfValueIsTooLargeForDouble()
        {
            var message = new ValidatedMessage<string>("1.7E+308");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDouble(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        #region [====== IsDecimal ======]

        [TestMethod]        
        public void ValidateIsDecimal_ReturnsExpectedError_IfMemberIsNull()
        {
            var message = new ValidatedMessage<string>(null);
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDecimal(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsNoErrors_IfValueIsPositiveDecimal()
        {
            const decimal value = 79228162514264337593543950335M;
            var message = new ValidatedMessage<string>(value.ToString(CultureInfo.InvariantCulture));
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsDecimal(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsNoErrors_IfValueIsNegativeDecimal()
        {
            const decimal value = -79228162514264337593543950335M;
            var message = new ValidatedMessage<string>(value.ToString(CultureInfo.InvariantCulture));
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member)
                .IsDecimal(RandomErrorMessage)
                .IsEqualTo(value, RandomErrorMessage);

            validator.Validate(message).AssertNoErrors();
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsExpectedError_IfValueCannotBeConvertedToNumber()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDecimal(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsDefaultError_IfValueCannotBeConvertedToNumber_And_NoErrorMessageIsSpecified()
        {
            var message = new ValidatedMessage<string>("xyz");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDecimal();

            validator.Validate(message).AssertOneError("Member (xyz) could not be converted to a 96-bit floating point number.");
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsExpectedError_IfValueIsTooSmallForDecimal()
        {
            var message = new ValidatedMessage<string>("-79228162514264337593543950336");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDecimal(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        [TestMethod]
        public void ValidateIsDecimal_ReturnsExpectedError_IfValueIsTooLargeForDecimal()
        {
            var message = new ValidatedMessage<string>("79228162514264337593543950336");
            var validator = message.CreateConstraintValidator();

            validator.VerifyThat(m => m.Member).IsDecimal(RandomErrorMessage);

            validator.Validate(message).AssertOneError(RandomErrorMessage);
        }

        #endregion

        private static ValidatedMessage<string> NewValidatedMessage()
        {
            return new ValidatedMessage<string>(Guid.NewGuid().ToString("N"));
        }
    }
}