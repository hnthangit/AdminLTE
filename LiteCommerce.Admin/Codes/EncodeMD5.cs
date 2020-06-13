using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Encrypt MD5
    /// </summary>
    public static class EncodeMD5
    {
        /// <summary>
        /// Encrypt một chuỗi ký tự MD5
        /// </summary>
        /// <param name="str">Chuỗi ký tự</param>
        /// <returns>Chuỗi đã mã hóa MD5</returns>
        public static string GetMD5(string str)
        {
            try
            {
                MD5 mD5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = mD5.ComputeHash(Encoding.Default.GetBytes(str));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    stringBuilder.AppendFormat("{0:x2}", dataMd5[i]);
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kiểm tra một chuỗi đã mã hóa MD5 hay không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMD5(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }
    }
}