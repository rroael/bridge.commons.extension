using System.Globalization;

namespace Bridge.Commons.Extension
{
    public static class DecimalExtension
    {
        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="culture">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this decimal value, string culture = "pt-BR")
        {
            return value.ToCurrencyString(CultureInfo.CreateSpecificCulture(culture));
        }

        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="cultureInfo">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this decimal value, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "{0:C}", value);
        }
    }
}