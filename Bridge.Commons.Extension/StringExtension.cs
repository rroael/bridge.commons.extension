using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de string
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///     Formato de CPF e CNPJ
        /// </summary>
        /// <param name="cpfCnpj"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string FormatCpfCnpj(this string cpfCnpj)
        {
            string formatted;

            switch (cpfCnpj.Length)
            {
                case 11:
                    formatted = Convert.ToUInt64(cpfCnpj).ToString(@"000\.000\.000\-00");
                    break;
                case 14:
                    formatted = Convert.ToUInt64(cpfCnpj).ToString(@"00\.000\.000\/0000\-00");
                    break;
                default:
                    throw new FormatException("CPF or CNPJ must have 11 or 14 characters, respectively.");
            }

            return formatted;
        }

        /// <summary>
        ///     Converão para int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            return int.TryParse(value, out var result) ? result : default;
        }

        /// <summary>
        ///     Remove caractéres especiais do documento
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveDocumentSpecialCharacters(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Replace("-", string.Empty).Replace(".", string.Empty).Replace("/", string.Empty);
        }

        /// <summary>
        ///     Verifica se o e-mail é valido
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string value)
        {
            var valid = false;

            try
            {
                var mailAddress = new MailAddress(value);
                valid = true;
            }
            catch (Exception)
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        ///     Verifica se é digital
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDigit(this string value)
        {
            return value.All(char.IsDigit);
        }

        /// <summary>
        ///     Verifica se o CPF ou CNPJ é valido
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsValidCpfCnpj(this string document)
        {
            var cpfcnpj = document.RemoveDocumentSpecialCharacters();

            switch (cpfcnpj.Length)
            {
                case 11:
                    return IsValidCpf(cpfcnpj);
                case 14:
                    return IsValidCnpj(cpfcnpj);
                default:
                    return false;
            }
        }

        /// <summary>
        ///     Verifica se o CNPJ é valido
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool IsValidCnpj(this string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }

        /// <summary>
        ///     Faz conversão para camel case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str[0].ToString().ToLower() + str.Substring(1, str.Length - 1);
        }

        /// <summary>
        ///     Verifica se é um CPF válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidCpf(this string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;
            return cpf.EndsWith(digito);
        }

        /// <summary>
        ///     Conversão para enumerador
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <param name="culture"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this string text, char separator = ',',
            CultureInfo culture = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<T>().AsEnumerable();

            var list = text.Split(separator);

            if (list == null || list.Length == 0)
                return new List<T>().AsEnumerable();

            if (culture == null)
                culture = new CultureInfo("en-US");

            return list.Select(x => (T)Convert.ChangeType(x, typeof(T), culture));
        }

        /// <summary>
        ///     Remove diacríticos (acentos, tremas, etc)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in
                     from c in normalizedString
                     let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)
                     where unicodeCategory != UnicodeCategory.NonSpacingMark
                     select c)
                stringBuilder.Append(c);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}