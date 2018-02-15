using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace WTS.BL.Extensions
{

    public static class ObjectExtensions
    {
        public static bool EqualsAny<T>(this T obj, params T [] values)
        {
            return Array.IndexOf<T>(values, obj) != -1;
        }

        public static bool EqualsNone<T>(this T obj, params T [] values)
        {
            return !obj.EqualsAny<T>(values);
        }

        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo<T>(default(T));
        }

        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                Type type = typeof(T);
                if (value.GetType() == type)
                    return (T)value;
                TypeConverter converter1 = TypeDescriptor.GetConverter(value);
                if (converter1 != null && converter1.CanConvertTo(type))
                    return (T)converter1.ConvertTo(value, type);
                TypeConverter converter2 = TypeDescriptor.GetConverter(type);
                if (converter2 != null && converter2.CanConvertFrom(value.GetType()))
                    return (T)converter2.ConvertFrom(value);
            }
            return defaultValue;
        }

        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (!ignoreException)
                return value.ConvertTo<T>();
            try
            {
                return value.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                Type type = typeof(T);
                TypeConverter converter1 = TypeDescriptor.GetConverter(value);
                if (converter1 != null && converter1.CanConvertTo(type))
                    return true;
                TypeConverter converter2 = TypeDescriptor.GetConverter(type);
                if (converter2 != null && converter2.CanConvertFrom(value.GetType()))
                    return true;
            }
            return false;
        }

        public static object InvokeMethod(this object obj, string methodName, params object [] parameters)
        {
            return obj.InvokeMethod<object>(methodName, parameters);
        }

        public static T InvokeMethod<T>(this object obj, string methodName, params object [] parameters)
        {
            MethodInfo method = obj.GetType().GetMethod(methodName);
            if (method == null)
                throw new ArgumentException(string.Format("Method '{0}' not found.", (object)methodName), methodName);
            object obj1 = method.Invoke(obj, parameters);
            if (!(obj1 is T))
                return default(T);
            return (T)obj1;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetPropertyValue<object>(propertyName, (object)null);
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return obj.GetPropertyValue<T>(propertyName, default(T));
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException(string.Format("Property '{0}' not found.", (object)propertyName), propertyName);
            object obj1 = property.GetValue(obj, (object [])null);
            if (!(obj1 is T))
                return defaultValue;
            return (T)obj1;
        }

        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException(string.Format("Property '{0}' not found.", (object)propertyName), propertyName);
            if (!property.CanWrite)
                throw new ArgumentException(string.Format("Property '{0}' does not allow writes.", (object)propertyName), propertyName);
            property.SetValue(obj, value, (object [])null);
        }

        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetAttribute<T>(true);
        }

        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            object [] customAttributes = (obj as Type ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited);
            if (customAttributes.Length > 0)
                return customAttributes [0] as T;
            return default(T);
        }

        public static IEnumerable<T> GetAttributes<T>(this object obj) where T : Attribute
        {
            return obj.GetAttributes<T>();
        }

        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited).OfType<T>().Select<T, T>((Func<T, T>)(attribute => attribute));
        }

        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }

        public static bool IsOfType(this object obj, Type type)
        {
            return obj.GetType().Equals(type);
        }

        public static bool IsOfTypeOrInherits<T>(this object obj)
        {
            return obj.IsOfTypeOrInherits(typeof(T));
        }

        public static bool IsOfTypeOrInherits(this object obj, Type type)
        {
            for (Type type1 = obj.GetType(); !type1.Equals(type); type1 = type1.BaseType)
            {
                if (type1 == type1.BaseType || type1.BaseType == null)
                    return false;
            }
            return true;
        }

        public static bool IsAssignableTo<T>(this object obj)
        {
            return obj.IsAssignableTo(typeof(T));
        }

        public static bool IsAssignableTo(this object obj, Type type)
        {
            Type type1 = obj.GetType();
            return type.IsAssignableFrom(type1);
        }

        public static T GetTypeDefaultValue<T>(this T value)
        {
            return default(T);
        }

        public static object ToDatabaseValue<T>(this T value)
        {
            if (!value.Equals((object)value.GetTypeDefaultValue<T>()))
                return (object)value;
            return (object)DBNull.Value;
        }

        public static T CastTo<T>(this object value)
        {
            return (T)value;
        }

        public static bool IsNull(this object target)
        {
            return target.IsNull<object>();
        }

        public static bool IsNull<T>(this T target)
        {
            return object.ReferenceEquals((object)target, (object)null);
        }

        public static bool IsNotNull(this object target)
        {
            return target.IsNotNull<object>();
        }

        public static bool IsNotNull<T>(this T target)
        {
            return !object.ReferenceEquals((object)target, (object)null);
        }

        public static string AsString(this object target)
        {
            if (!object.ReferenceEquals(target, (object)null))
                return string.Format("{0}", target);
            return (string)null;
        }

        public static string AsString(this object target, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0}", new object [1]
            {
      target
            });
        }

        public static string AsInvariantString(this object target)
        {
            return string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0}", new object [1]
            {
      target
            });
        }

        public static T NotNull<T>(this T target, T notNullValue)
        {
            if (!object.ReferenceEquals((object)target, (object)null))
                return target;
            return notNullValue;
        }

        public static T NotNull<T>(this T target, Func<T> notNullValueProvider)
        {
            if (!object.ReferenceEquals((object)target, (object)null))
                return target;
            return notNullValueProvider();
        }

        public static string ToStringDump(this object o, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, int maxArrayElements = 5)
        {
            return ObjectExtensions.ToStringDumpInternal((XContainer)o.ToXElement(flags, maxArrayElements)).Aggregate<string, string>(string.Empty, (Func<string, string, string>)((str, el) => str + el));
        }

        private static IEnumerable<string> ToStringDumpInternal(XContainer toXElement)
        {
            foreach (XElement xelement in (IEnumerable<XElement>)toXElement.Elements().OrderBy<XElement, string>((Func<XElement, string>)(o => o.Name.ToString())))
            {
                if (xelement.HasElements)
                {
                    foreach (string str in ObjectExtensions.ToStringDumpInternal((XContainer)xelement))
                        yield return "{" + string.Format("{0}={1}", (object)xelement.Name, (object)str) + "}";
                }
                else
                    yield return "{" + string.Format("{0}={1}", (object)xelement.Name, (object)xelement.Value) + "}";
            }
        }

        public static string ToHTMLTable(this object o, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, int maxArrayElements = 5)
        {
            return ObjectExtensions.ToHTMLTableInternal((XContainer)o.ToXElement(flags, maxArrayElements), 0).Aggregate<string, string>(string.Empty, (Func<string, string, string>)((str, el) => str + el));
        }

        private static IEnumerable<string> ToHTMLTableInternal(XContainer xel, int padding)
        {
            yield return ObjectExtensions.FormatHTMLLine("<table>", padding);
            yield return ObjectExtensions.FormatHTMLLine("<tr><th>Attribute</th><th>Value</th></tr>", padding + 1);
            foreach (XElement xelement in (IEnumerable<XElement>)xel.Elements().OrderBy<XElement, string>((Func<XElement, string>)(o => o.Name.ToString())))
            {
                if (xelement.HasElements)
                {
                    yield return ObjectExtensions.FormatHTMLLine(string.Format("<tr><td>{0}</td><td>", (object)xelement.Name), padding + 1);
                    foreach (string str in ObjectExtensions.ToHTMLTableInternal((XContainer)xelement, padding + 2))
                        yield return str;
                    yield return ObjectExtensions.FormatHTMLLine("</td></tr>", padding + 1);
                }
                else
                    yield return ObjectExtensions.FormatHTMLLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", (object)xelement.Name, (object)HttpUtility.HtmlEncode(xelement.Value)), padding + 1);
            }
            yield return ObjectExtensions.FormatHTMLLine("</table>", padding);
        }

        private static string FormatHTMLLine(string tag, int padding)
        {
            return string.Format("{0}{1}{2}", (object)string.Empty.PadRight(padding, '\t'), (object)tag, (object)Environment.NewLine);
        }

        public static XElement ToXElement(this object o, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, int maxArrayElements = 5)
        {
            try
            {
                return ObjectExtensions.ToXElementInternal(o, (ICollection<object>)new HashSet<object>(), flags, maxArrayElements);
            }
            catch
            {
                return new XElement((XName)o.GetType().Name);
            }
        }

        private static XElement ToXElementInternal(object o, ICollection<object> visited, BindingFlags flags, int maxArrayElements)
        {
            if (o == null)
                return new XElement((XName)"null");
            if (visited.Contains(o))
                return new XElement((XName)"cyclicreference");
            if (!o.GetType().IsValueType)
                visited.Add(o);
            Type type1 = o.GetType();
            XElement xelement = new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)type1.Name, type1.IsArray));
            if (!ObjectExtensions.NeedRecursion(type1, o))
            {
                xelement.Add((object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)type1.Name, type1.IsArray), (object)(string.Empty + o)));
                return xelement;
            }
            if (o is IEnumerable)
            {
                int num = 0;
                foreach (object obj in o as IEnumerable)
                {
                    Type type2 = obj.GetType();
                    xelement.Add(ObjectExtensions.NeedRecursion(type2, obj) ? (object)ObjectExtensions.ToXElementInternal(obj, visited, flags, maxArrayElements) : (object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)type2.Name, type2.IsArray), obj));
                    if (num++ >= maxArrayElements)
                        break;
                }
                return xelement;
            }
            foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>)type1.GetProperties(flags)).Where<PropertyInfo>((Func<PropertyInfo, bool>)(propertyInfo => propertyInfo.CanRead)))
            {
                object o1 = ObjectExtensions.GetValue(o, propertyInfo);
                xelement.Add(ObjectExtensions.NeedRecursion(propertyInfo.PropertyType, o1) ? (object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)propertyInfo.Name, propertyInfo.PropertyType.IsArray), (object)ObjectExtensions.ToXElementInternal(o1, visited, flags, maxArrayElements)) : (object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)propertyInfo.Name, propertyInfo.PropertyType.IsArray), (object)(string.Empty + o1)));
            }
            foreach (FieldInfo field in type1.GetFields())
            {
                object o1 = field.GetValue(o);
                xelement.Add(ObjectExtensions.NeedRecursion(field.FieldType, o1) ? (object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)field.Name, field.FieldType.IsArray), (object)ObjectExtensions.ToXElementInternal(o1, visited, flags, maxArrayElements)) : (object)new XElement((XName)ObjectExtensions.CleanName((IEnumerable<char>)field.Name, field.FieldType.IsArray), (object)(string.Empty + o1)));
            }
            return xelement;
        }

        private static bool NeedRecursion(Type type, object o)
        {
            if (o != null && !type.IsPrimitive && (!(o is string) && !(o is DateTime)) && (!(o is DateTimeOffset) && !(o is TimeSpan) && ((object)(o as Delegate) == null && !(o is Enum))) && !(o is Decimal))
                return !(o is Guid);
            return false;
        }

        private static object GetValue(object o, PropertyInfo propertyInfo)
        {
            try
            {
                return propertyInfo.GetValue(o, (object [])null);
            }
            catch
            {
                try
                {
                    return propertyInfo.GetValue(o, new object [1]
                    {
          (object) 0
                    });
                }
                catch
                {
                    return (object)null;
                }
            }
        }

        private static string CleanName(IEnumerable<char> name, bool isArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in name.Where<char>((Func<char, bool>)(c =>
            {
                if (char.IsLetterOrDigit(c))
                    return (int)c != 96;
                return false;
            })).Select<char, char>((Func<char, char>)(c => c)))
                stringBuilder.Append(ch);
            if (isArray)
                stringBuilder.Append("Array");
            return stringBuilder.ToString();
        }

        public static T CastAs<T>(this object obj) where T : class, new()
        {
            return obj as T;
        }

        public static int CountLoopsToNull<T>(this T item, Func<T, T> function) where T : class
        {
            int num = 0;
            while ((object)(item = function(item)) != null)
                ++num;
            return num;
        }

        public static K FindTypeByRecursion<T, K>(this T item, Func<T, T> function) where T : class where K : class, T
        {
            while (!((object)item is K))
            {
                if ((object)(item = function(item)) == null)
                    return default(K);
            }
            return (K)(object)item;
        }

        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable.", nameof(source));
            if (object.ReferenceEquals((object)source, (object)null))
                return default(T);
            IFormatter formatter = (IFormatter)new BinaryFormatter();
            Stream serializationStream = (Stream)new MemoryStream();
            using (serializationStream)
            {
                formatter.Serialize(serializationStream, (object)source);
                serializationStream.Seek(0L, SeekOrigin.Begin);
                return (T)formatter.Deserialize(serializationStream);
            }
        }
    }

}
