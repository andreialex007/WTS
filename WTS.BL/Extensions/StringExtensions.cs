using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WTS.BL.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            if (value != null)
                return value.Length == 0;
            return true;
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        public static string IfEmpty(this string value, string defaultValue)
        {
            if (!value.IsNotEmpty())
                return defaultValue;
            return value;
        }

        public static string FormatWith(this string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }

        public static string TrimToMaxLength(this string value, int maxLength)
        {
            if (value != null && value.Length > maxLength)
                return value.Substring(0, maxLength);
            return value;
        }

        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            if (value != null && value.Length > maxLength)
                return value.Substring(0, maxLength) + suffix;
            return value;
        }

        public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            return inputValue.IndexOf(comparisonValue, comparisonType) != -1;
        }

        public static XDocument ToXDocument(this string xml)
        {
            return XDocument.Parse(xml);
        }

        public static XmlDocument ToXmlDOM(this string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }

        public static XPathNavigator ToXPath(this string xml)
        {
            return new XPathDocument(new StringReader(xml)).CreateNavigator();
        }

        public static string Reverse(this string value)
        {
            if (value.IsEmpty() || value.Length == 1)
                return value;
            var charArray = value.ToCharArray();
            Array.Reverse((Array) charArray);
            return new string(charArray);
        }

        public static string EnsureStartsWith(this string value, string prefix)
        {
            if (!value.StartsWith(prefix))
                return prefix + value;
            return value;
        }

        public static string EnsureEndsWith(this string value, string suffix)
        {
            if (!value.EndsWith(suffix))
                return value + suffix;
            return value;
        }

        public static bool IsNumeric(this string value)
        {
            float result;
            return float.TryParse(value, out result);
        }

        public static string ExtractDigits(this string value)
        {
            return string.Join(null, Regex.Split(value, "[^\\d]"));
        }

        public static string ConcatWith(this string value, params string[] values)
        {
            return value + string.Concat(values);
        }

        public static Guid ToGuid(this string value)
        {
            return new Guid(value);
        }

        public static Guid ToGuidSave(this string value)
        {
            return value.ToGuidSave(Guid.Empty);
        }

        public static Guid ToGuidSave(this string value, Guid defaultValue)
        {
            if (value.IsEmpty())
                return defaultValue;
            try
            {
                return value.ToGuid();
            }
            catch
            {
            }
            return defaultValue;
        }

        public static string GetBefore(this string value, string x)
        {
            var length = value.IndexOf(x);
            if (length != -1)
                return value.Substring(0, length);
            return string.Empty;
        }

        public static string GetBetween(this string value, string x, string y)
        {
            var num1 = value.IndexOf(x);
            var num2 = value.LastIndexOf(y);
            if (num1 == -1 || num1 == -1)
                return string.Empty;
            var startIndex = num1 + x.Length;
            if (startIndex < num2)
                return value.Substring(startIndex, num2 - startIndex).Trim();
            return string.Empty;
        }

        public static string GetAfter(this string value, string x)
        {
            var num = value.LastIndexOf(x);
            if (num == -1)
                return string.Empty;
            var startIndex = num + x.Length;
            if (startIndex < value.Length)
                return value.Substring(startIndex).Trim();
            return string.Empty;
        }

        public static string Join<T>(string separator, T[] value)
        {
            if (value == null || value.Length == 0)
                return string.Empty;
            if (separator == null)
                separator = string.Empty;
            Converter<T, string> converter = o => o.ToString();
            return string.Join(separator, Array.ConvertAll(value, converter));
        }

        public static string Remove(this string value, params string[] strings)
        {
            return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
        }

        public static bool IsEmptyOrWhiteSpace(this string value)
        {
            if (!value.IsEmpty())
                return value.All(t => char.IsWhiteSpace(t));
            return true;
        }

        public static bool IsNotEmptyOrWhiteSpace(this string value)
        {
            return !value.IsEmptyOrWhiteSpace();
        }

        public static string IfEmptyOrWhiteSpace(this string value, string defaultValue)
        {
            if (!value.IsEmptyOrWhiteSpace())
                return value;
            return defaultValue;
        }

        public static string ToUpperFirstLetter(this string value)
        {
            if (value.IsEmptyOrWhiteSpace())
                return string.Empty;
            var charArray = value.ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            return new string(charArray);
        }

        public static byte[] GetBytes(this string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        public static byte[] GetBytes(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);
        }

        public static string ToPlural(this string singular)
        {
            var num = singular.LastIndexOf(" of ");
            if (num > 0)
                return singular.Substring(0, num) + singular.Remove(0, num).ToPlural();
            if (singular.EndsWith("sh") || singular.EndsWith("ch") || singular.EndsWith("us") || singular.EndsWith("ss"))
                return singular + "es";
            if (singular.EndsWith("y"))
                return singular.Remove(singular.Length - 1, 1) + "ies";
            if (singular.EndsWith("o"))
                return singular.Remove(singular.Length - 1, 1) + "oes";
            return singular + "s";
        }

        public static string ToHtmlSafe(this string s)
        {
            return s.ToHtmlSafe(false, false);
        }

        public static string ToHtmlSafe(this string s, bool all)
        {
            return s.ToHtmlSafe(all, false);
        }

        public static string ToHtmlSafe(this string s, bool all, bool replace)
        {
            if (s.IsEmptyOrWhiteSpace())
                return string.Empty;
            var numArray = new int [280]
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                28,
                29,
                30,
                31,
                34,
                39,
                38,
                60,
                62,
                123,
                124,
                125,
                126,
                sbyte.MaxValue,
                160,
                161,
                162,
                163,
                164,
                165,
                166,
                167,
                168,
                169,
                170,
                171,
                172,
                173,
                174,
                175,
                176,
                177,
                178,
                179,
                180,
                181,
                182,
                183,
                184,
                185,
                186,
                187,
                188,
                189,
                190,
                191,
                215,
                247,
                192,
                193,
                194,
                195,
                196,
                197,
                198,
                199,
                200,
                201,
                202,
                203,
                204,
                205,
                206,
                207,
                208,
                209,
                210,
                211,
                212,
                213,
                214,
                215,
                216,
                217,
                218,
                219,
                220,
                221,
                222,
                223,
                224,
                225,
                226,
                227,
                228,
                229,
                230,
                231,
                232,
                233,
                234,
                235,
                236,
                237,
                238,
                239,
                240,
                241,
                242,
                243,
                244,
                245,
                246,
                247,
                248,
                249,
                250,
                251,
                252,
                253,
                254,
                byte.MaxValue,
                256,
                8704,
                8706,
                8707,
                8709,
                8711,
                8712,
                8713,
                8715,
                8719,
                8721,
                8722,
                8727,
                8730,
                8733,
                8734,
                8736,
                8743,
                8744,
                8745,
                8746,
                8747,
                8756,
                8764,
                8773,
                8776,
                8800,
                8801,
                8804,
                8805,
                8834,
                8835,
                8836,
                8838,
                8839,
                8853,
                8855,
                8869,
                8901,
                913,
                914,
                915,
                916,
                917,
                918,
                919,
                920,
                921,
                922,
                923,
                924,
                925,
                926,
                927,
                928,
                929,
                931,
                932,
                933,
                934,
                935,
                936,
                937,
                945,
                946,
                947,
                948,
                949,
                950,
                951,
                952,
                953,
                954,
                955,
                956,
                957,
                958,
                959,
                960,
                961,
                962,
                963,
                964,
                965,
                966,
                967,
                968,
                969,
                977,
                978,
                982,
                338,
                339,
                352,
                353,
                376,
                402,
                710,
                732,
                8194,
                8195,
                8201,
                8204,
                8205,
                8206,
                8207,
                8211,
                8212,
                8216,
                8217,
                8218,
                8220,
                8221,
                8222,
                8224,
                8225,
                8226,
                8230,
                8240,
                8242,
                8243,
                8249,
                8250,
                8254,
                8364,
                8482,
                8592,
                8593,
                8594,
                8595,
                8596,
                8629,
                8968,
                8969,
                8970,
                8971,
                9674,
                9824,
                9827,
                9829,
                9830
            };
            var stringBuilder = new StringBuilder();
            foreach (var ch in s)
                if (all || numArray.Contains(ch))
                    stringBuilder.Append("&#" + (int) ch + ";");
                else
                    stringBuilder.Append(ch);
            if (!replace)
                return stringBuilder.ToString();
            return stringBuilder.Replace("", "<br />").Replace("\n", "<br />").Replace(" ", "&nbsp;").ToString();
        }

        public static bool IsMatchingTo(this string value, string regexPattern)
        {
            return value.IsMatchingTo(regexPattern, RegexOptions.None);
        }

        public static bool IsMatchingTo(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.IsMatch(value, regexPattern, options);
        }

        public static string ReplaceWith(this string value, string regexPattern, string replaceValue)
        {
            return value.ReplaceWith(regexPattern, replaceValue, RegexOptions.None);
        }

        public static string ReplaceWith(this string value, string regexPattern, string replaceValue, RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }

        public static string ReplaceWith(this string value, string regexPattern, MatchEvaluator evaluator)
        {
            return value.ReplaceWith(regexPattern, RegexOptions.None, evaluator);
        }

        public static string ReplaceWith(this string value, string regexPattern, RegexOptions options, MatchEvaluator evaluator)
        {
            return Regex.Replace(value, regexPattern, evaluator, options);
        }

        public static MatchCollection GetMatches(this string value, string regexPattern)
        {
            return value.GetMatches(regexPattern, RegexOptions.None);
        }

        public static MatchCollection GetMatches(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }

        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern)
        {
            return value.GetMatchingValues(regexPattern, RegexOptions.None);
        }

        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern, RegexOptions options)
        {
            return value.GetMatches(regexPattern, options).Cast<Match>().Where(match => match.Success).Select(match => match.Value);
        }

        public static string[] Split(this string value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }

        public static string[] Split(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }

        public static string[] GetWords(this string value)
        {
            return value.Split("\\W");
        }

        public static string GetWordByIndex(this string value, int index)
        {
            var words = value.GetWords();
            if (index < 0 || index > words.Length - 1)
                throw new IndexOutOfRangeException("The word number is out of range.");
            return words[index];
        }

        [Obsolete("Please use RemoveAllSpecialCharacters instead")]
        public static string AdjustInput(this string value)
        {
            return string.Join(null, Regex.Split(value, "[^a-zA-Z0-9]"));
        }

        public static string RemoveAllSpecialCharacters(this string value)
        {
            var stringBuilder = new StringBuilder();
            foreach (var ch in value.Where(c =>
            {
                if (c >= 48 && c <= 57 || c >= 65 && c <= 90)
                    return true;
                if (c >= 97)
                    return (int) c <= 122;
                return false;
            }))
                stringBuilder.Append(ch);
            return stringBuilder.ToString();
        }

        public static string SpaceOnUpper(this string value)
        {
            return Regex.Replace(value, "([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)", " $1$2").TrimStart();
        }

        public static byte[] ToBytes(this string value)
        {
            return value.ToBytes(null);
        }

        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.Default;
            return encoding.GetBytes(value);
        }

        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(null);
        }

        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        public static string DecodeBase64(this string encodedValue)
        {
            return encodedValue.DecodeBase64((Encoding) null);
        }

        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }
    }
}