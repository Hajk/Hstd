using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;


namespace Hstd.Security
{
    /// <summary>
    /// Class for providing hash functions
    /// </summary>
    public class Hash
    {
        #region Singleton 

        private static Hash _instance;

        private Hash() {}

        /// <summary>
        /// Instance of Hash singleton
        /// </summary>
        public static Hash Get
        {
            get
            {
                return _instance ?? (_instance = new Hash()); 
            }
        }

        #endregion

        private readonly MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();





        // Private Methods
        //=====================================
        /// <summary>
        /// Generate Hash by using MD5 agorithm from SecureString
        /// </summary>
        /// <param name="thisPassword">password for hash</param>
        /// <param name="salt">Salt for secure hash</param>
        /// <returns>string contains generated hash value</returns>
        public string GenerateHash(SecureString thisPassword, byte[] salt)
        {
            // for security reason do not return hash of pure salt
            if (thisPassword.Length == 0) return string.Empty;

            // Turn password into byte array
            byte[] tmpSource = ConvertToCharArray(thisPassword);

            // Combine with salt
            var combinedSource = new byte[tmpSource.Length + salt.Length];
            combinedSource = Combine(tmpSource, salt);

            // generate hash
            byte[] tmpHash = _md5.ComputeHash(combinedSource);

            // generate string 
            var sb = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Count<byte>(); i++)
            {
                sb.Append(tmpHash[i].ToString("X2"));  // X2 formats to hexadecimal
            }

            return sb.ToString();
        }

        private byte[] ConvertToCharArray(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);

                string strPsw = Marshal.PtrToStringUni(unmanagedString);
                var array = new byte[strPsw.Length];

                for (int i = 0; i < strPsw.Length; i++)
                {
                    array[i] = (byte)strPsw[i];
                }

                return array;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}