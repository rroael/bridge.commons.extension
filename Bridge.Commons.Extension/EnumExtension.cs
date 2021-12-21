using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de enumeradores
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        ///     Buscar descrição
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var fieldInfo = enumType.GetField(enumValue.ToString());

            return !(Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute
                descAttr)
                ? enumValue.ToString()
                : descAttr.Description;
        }

        /// <summary>
        ///     Busca descrição por atributo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionByAttribute(this Enum value)
        {
            if (value == null)
                return null;
            var desc = value.GetAttribute<DescriptionAttribute>();
            if (desc != null)
                return desc.Description;
            var nome = value.ToString();
            return nome;
        }

        /// <summary>
        ///     Buscar descrições
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> GetDescriptions(this Enum value)
        {
            var values = Enum.GetValues(value.GetType());
            var descriptions = new List<string>();

            foreach (var item in values)
            {
                var description = ((Enum)item).GetDescription();
                descriptions.Add(description);
            }

            return descriptions;
        }

        /// <summary>
        ///     Buscar atributo
        /// </summary>
        /// <param name="enumValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            if (enumValue == null) return null;

            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo == null) return null;

            var attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();

            return attribute;
        }

        /// <summary>
        ///     Buscar enumerador de objeto genérico
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetEnumFrom<T>(this string value) where T : IComparable, IFormattable, IConvertible
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        ///     Buscar enumerador de objeto genérico
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetEnumFrom<T>(this int value) where T : IComparable, IFormattable, IConvertible
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        ///     Buscar descrição de objeto genérico
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDescriptionFrom<T>(this int value) where T : IComparable, IFormattable, IConvertible
        {
            if (!Enum.IsDefined(typeof(T), value)) return string.Empty;

            var enumValue = (Enum)Enum.ToObject(typeof(T), value);

            return GetDescription(enumValue);
        }

        /// <summary>
        ///     Verifica se é definido em um enumerador
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsDefinedInEnum<T>(this object value) where T : IComparable, IFormattable, IConvertible
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        ///     Realiza a conversão para int
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int ToInt<T>(this T value) where T : IComparable, IFormattable, IConvertible
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        ///     Realiza a conversão para long
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static long ToLong<T>(this T value) where T : IComparable, IFormattable, IConvertible
        {
            return Convert.ToInt64(value);
        }

        /// <summary>
        ///     Verifica se esta dentro de alguns parâmetros
        /// </summary>
        /// <param name="value"></param>
        /// <param name="listOfValues"></param>
        /// <returns></returns>
        public static bool IsInto(this Enum value, params Enum[] listOfValues)
        {
            return listOfValues.Contains(value);
        }

        /// <summary>
        ///     Verifica se não esta dentro de alguns parâmetros
        /// </summary>
        /// <param name="value"></param>
        /// <param name="listOfValues"></param>
        /// <returns></returns>
        public static bool IsNotInto(this Enum value, params Enum[] listOfValues)
        {
            return !IsInto(value, listOfValues);
        }
    }
}