using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Hstd.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace
namespace Hstd.Security.Tests
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class SecureStringExtensionTest
    {
        const string strToken = "kokoJambo8čřž   =ýě žčáýě bbbdxší~˘ˇ°˘`˛˛°";


        [TestMethod]
        public void ToUnsecureStringTest()
        {
            // Make secureString instance
            SecureString secureTestStr;
            unsafe
            {
                fixed (char* passwordChars = strToken)
                {
                    secureTestStr = new SecureString(passwordChars, strToken.Length);
                    secureTestStr.MakeReadOnly();
                }
            }

            // empty test
            "".ToSecureString();

            // Value test
            string str = secureTestStr.ToUnsecureString();
            Assert.AreEqual(strToken, str);




        }

        [TestMethod]
        public void ToSecureString()
        {
            // Test instance
            SecureString secureTestStr;
            unsafe
            {
                fixed (char* passwordChars = strToken)
                {
                    secureTestStr = new SecureString(passwordChars, strToken.Length);
                    secureTestStr.MakeReadOnly();
                }
            }

            // empty test
            var secureTestEmptyStr = new SecureString();
            Assert.AreEqual(ToString(secureTestEmptyStr), secureTestEmptyStr.ToUnsecureString());



            // Value test
            var secureStr = strToken.ToSecureString();
            string resolvedStr = ToString(secureStr);
            string resolvedTestStr = ToString(secureTestStr);
            Assert.AreEqual(resolvedTestStr, resolvedStr);

        }





        // Helper
        // ================================
        private string ToString(SecureString secureStr)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            string resolvedStr;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureStr);
                resolvedStr = Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }

            return resolvedStr;
        }
    }
}
