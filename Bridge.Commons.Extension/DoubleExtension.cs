using System;
using System.Globalization;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de double
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        ///     Verifica se a diferença entre os valores está dentro do valor aceitável
        /// </summary>
        /// <param name="value1">Valor 1</param>
        /// <param name="value2">Valor 2</param>
        /// <param name="acceptableDifference">Valor aceitável da diferença</param>
        /// <returns></returns>
        public static bool CloseEnoughForMe(this double value1, double value2, double acceptableDifference)
        {
            return Math.Abs(value1 - value2) <= acceptableDifference;
        }

        /// <summary>
        ///     Verifica se os valores são iguais com variação fuzzy
        /// </summary>
        /// <param name="val1">Valor 1</param>
        /// <param name="val2">Valor 2</param>
        /// <param name="pVariation">Porcentagem de variação (de 0 até 1)</param>
        /// <returns></returns>
        public static bool IsFuzzyEqual(this double val1, double val2, float pVariation)
        {
            return Math.Abs(val1 - val2) / val1 <= pVariation;
        }

        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="culture">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this double value, string culture = "pt-BR")
        {
            return value.ToCurrencyString(CultureInfo.CreateSpecificCulture(culture));
        }

        /// <summary>
        ///     Converte o valor para moeda em string na culture informada
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="cultureInfo">CultureInfo</param>
        /// <returns></returns>
        public static string ToCurrencyString(this double value, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "{0:C}", value);
        }
    }
}