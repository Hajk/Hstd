using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Hstd.Security
{
    /// <summary>
    ///     SecureString extension methods
    /// </summary>
    public static class SecureStringExtension
    {
        /// <summary>
        ///     Convert to String
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToUnsecureString(this SecureString secureString)
        {
            // defend
            if (secureString == null)
                throw new ArgumentNullException("secureString");

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }


        /// <summary>
        ///     Convert to SecureString
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SecureString ToSecureString(this string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var secureString = new SecureString();

            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
    }
}