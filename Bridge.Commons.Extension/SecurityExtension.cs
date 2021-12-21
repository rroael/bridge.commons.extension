using System;
using System.Security.Cryptography;
using System.Text;
using Bridge.Commons.Extension.Enums;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de segurança
    /// </summary>
    public static class SecurityExtension
    {
        /// <summary>
        ///     Conversão para hash
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hashType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ToHash(this string value, EHashType hashType)
        {
            string result;

            string BitsToString(byte[] bytes)
            {
                var hash = BitConverter.ToString(bytes);
                hash = hash.Replace("-", string.Empty).ToLower();

                return hash;
            }

            switch (hashType)
            {
                case EHashType.MD5:
                    using (var ha = MD5.Create())
                    {
                        ha.ComputeHash(Encoding.UTF8.GetBytes(value));
                        result = BitsToString(ha.Hash);
                    }

                    break;
                case EHashType.SHA256:
                    using (var ha = SHA256.Create())
                    {
                        ha.ComputeHash(Encoding.UTF8.GetBytes(value));
                        result = BitsToString(ha.Hash);
                    }

                    break;
                case EHashType.SHA384:
                    using (var ha = SHA384.Create())
                    {
                        ha.ComputeHash(Encoding.UTF8.GetBytes(value));
                        result = BitsToString(ha.Hash);
                    }

                    break;
                case EHashType.SHA512:
                    using (var ha = SHA512.Create())
                    {
                        ha.ComputeHash(Encoding.UTF8.GetBytes(value));
                        result = BitsToString(ha.Hash);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(hashType), hashType, null);
            }

            return result;
        }
    }
}