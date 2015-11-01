﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.BuildingBlocks.Messaging
{
    internal static class MessageErrorInfoExtensions
    {
        internal static void AssertNoErrors(this MessageErrorInfo errorInfo)
        {
            Assert.IsNotNull(errorInfo);
            Assert.AreEqual(0, errorInfo.ErrorCount);
        }        

        internal static MessageErrorInfo AssertErrorCountIs(this MessageErrorInfo errorInfo, int count)
        {
            Assert.IsNotNull(errorInfo);
            Assert.AreEqual(count, errorInfo.ErrorCount);

            return errorInfo;
        }

        internal static MessageErrorInfo AssertError(this MessageErrorInfo errorInfo, string errorMessage)
        {
            Assert.IsNotNull(errorInfo);
            Assert.AreEqual(errorMessage, errorInfo.MessageError);

            return errorInfo;
        }

        internal static MessageErrorInfo AssertMemberError(this MessageErrorInfo errorInfo, string errorMessage)
        {
            return AssertMemberError(errorInfo, errorMessage, "Member");
        }

        internal static MessageErrorInfo AssertMemberError(this MessageErrorInfo errorInfo, string errorMessage, params string[] memberNames)
        {            
            Assert.IsNotNull(errorInfo);            

            foreach (var memberName in memberNames)
            {                
                Assert.IsTrue(errorInfo.MemberErrors.ContainsKey(memberName), "No error for member '{0}' was present.", memberName);
                Assert.AreEqual(errorMessage, errorInfo.MemberErrors[memberName]);    
            }
            return errorInfo;
        }               
    }
}