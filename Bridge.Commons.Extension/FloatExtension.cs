using System;
using System.Globalization;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de float
    /// </summary>
    public static class FloatExtension
    {
        /// <summary>
        ///     Round
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static float Round(this float value, int precision = 6)
        {
            return (float)Math.Round(value, precision);
        }

        /// <summary>
        ///     Round
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static float Round(this float value, int precision, MidpointRounding mode)
        {
            return (float)Math.Round(value, precision, mode);
        }

        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="culture">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this float value, string culture = "pt-BR")
        {
            return value.ToCurrencyString(CultureInfo.CreateSpecificCulture(culture));
        }

        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="cultureInfo">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this float value, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "{0:C}", value);
        }
    }
}