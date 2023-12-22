using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

public static class Extensions
{
    /// <summary>
    ///     An Int64 extension method that query if '@this' is multiple of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factor">The factor.</param>
    /// <returns>true if multiple of, false if not.</returns>
    public static bool IsMultipleOf(this Int64 @this, Int64 factor)
    {
        return @this%factor == 0;
    }

    /// <summary>
    /// Calculates if the float is multiple of another float.
    /// </summary>
    /// <param name="n">The number that we want to know if it is multiple of.</param>
    /// <param name="tolerance">The tolerance that we want to set to deal with potential mathematical errors.</param>
    /// <returns></returns>
    public static bool IsMultipleOf(this float v, float n, float tolerance = 0.001f)
    {
        return Math.Abs(v % n) < tolerance;
    }
    /// <summary>
    /// Calculates if the float is multiple of an int.
    /// </summary>
    /// <param name="n">The number that we want to know if it is multiple of.</param>
    /// <param name="tolerance">The tolerance that we want to set to deal with potential mathematical errors.</param>
    /// <returns></returns>
    public static bool IsMultipleOf(this float v, int n, float tolerance = 0.001f)
    {
        return Math.Abs(Math.Abs(v % n)) < tolerance;
    }

    /// <summary>
    /// Invokes the action if it is not null.
    /// </summary>
    public static void InvokeIfNotNull(this Action del)
    {
        if (del != null)
        {
            del.Invoke();
        }
    }

    /// <summary>
    /// Invokes the action if it is not null.
    /// </summary>
    public static void InvokeIfNotNull<T1>(this Action<T1> del, T1 p1)
    {
        if (del != null)
        {
            del.Invoke(p1);
        }
    }

    /// <summary>
    /// Invokes the action if it is not null.
    /// </summary>
    public static void InvokeIfNotNull<T1, T2>(this Action<T1, T2> del, T1 p1, T2 p2)
    {
        if (del != null)
        {
            del.Invoke(p1, p2);
        }
    }

    /// <summary>
    /// Invokes the action if it is not null.
    /// </summary>
    public static void InvokeIfNotNull<T1, T2, T3>(this Action<T1, T2, T3> del, T1 p1, T2 p2, T3 p3)
    {
        if (del != null)
        {
            del.Invoke(p1, p2, p3);
        }
    }

    /// <summary>
    /// Invokes the action if it is not null.
    /// </summary>
    public static void InvokeIfNotNull<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> del, T1 p1, T2 p2, T3 p3, T4 p4)
    {
        if (del != null)
        {
            del.Invoke(p1, p2, p3, p4);
        }
    }

    /// <summary>
    /// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
    /// </summary>
    public static TResult InvokeIfNotNull<TResult>(this Func<TResult> func)
    {
        return (func != null) ? func() : default(TResult);
    }

    /// <summary>
    /// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
    /// </summary>
    public static TResult InvokeIfNotNull<T1, TResult>(this Func<T1, TResult> func, T1 p1)
    {
        return (func != null) ? func(p1) : default(TResult);
    }

    /// <summary>
    /// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
    /// </summary>
    public static TResult InvokeIfNotNull<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2)
    {
        return (func != null) ? func(p1, p2) : default(TResult);
    }

    /// <summary>
    /// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
    /// </summary>
    public static TResult InvokeIfNotNull<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3)
    {
        return (func != null) ? func(p1, p2, p3) : default(TResult);
    }

    /// <summary>
    /// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
    /// </summary>
    public static TResult InvokeIfNotNull<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4)
    {
        return (func != null) ? func(p1, p2, p3, p4) : default(TResult);
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe(this Action action)
    {
        if (action != null) action.Invoke();
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T>(this Action<T> action, T param)
    {
        if (action != null) action.Invoke(param);
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
    {
        if (action != null) action.Invoke(param1, param2);
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T1, T2, T3>(this Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
    {
        if (action != null) action.Invoke(param1, param2, param3);
    }

    /// <summary>An Environment.SpecialFolder extension method that gets folder path.</summary>
    /// <param name="this">this.</param>
    /// <returns>The folder path.</returns>
    public static string GetFolderPath(this Environment.SpecialFolder @this)
    {
        return Environment.GetFolderPath(@this);
    }

    /// <summary>An Environment.SpecialFolder extension method that gets folder path.</summary>
    /// <param name="this">this.</param>
    /// <param name="option">The option.</param>
    /// <returns>The folder path.</returns>
    public static string GetFolderPath(this Environment.SpecialFolder @this, Environment.SpecialFolderOption option)
    {
        return Environment.GetFolderPath(@this, option);
    }

    /// <summary>
    ///     Adds an offset to the value of an unsigned pointer.
    /// </summary>
    /// <param name="pointer">The unsigned pointer to add the offset to.</param>
    /// <param name="offset">The offset to add.</param>
    /// <returns>A new unsigned pointer that reflects the addition of  to .</returns>
    public static UIntPtr Add(this UIntPtr pointer, Int32 offset)
    {
        return UIntPtr.Add(pointer, offset);
    }

    /// <summary>
    ///     Subtracts an offset from the value of an unsigned pointer.
    /// </summary>
    /// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
    /// <param name="offset">The offset to subtract.</param>
    /// <returns>A new unsigned pointer that reflects the subtraction of  from .</returns>
    public static UIntPtr Subtract(this UIntPtr pointer, Int32 offset)
    {
        return UIntPtr.Subtract(pointer, offset);
    }

    /// <summary>
    ///     Removes all occurrences of the invocation list of a delegate from the invocation list of another delegate.
    /// </summary>
    /// <param name="source">The delegate from which to remove the invocation list of .</param>
    /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of .</param>
    /// ###
    /// <returns>
    ///     A new delegate with an invocation list formed by taking the invocation list of  and removing all occurrences
    ///     of the invocation list of , if the invocation list of  is found within the invocation list of . Returns  if
    ///     is null or if the invocation list of  is not found within the invocation list of . Returns a null reference
    ///     if the invocation list of  is equal to the invocation list of , if  contains only a series of invocation
    ///     lists that are equal to the invocation list of , or if  is a null reference.
    /// </returns>
    public static Delegate RemoveAll(this Delegate source, Delegate value)
    {
        return Delegate.RemoveAll(source, value);
    }

    /// <summary>
    ///     Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.
    /// </summary>
    /// <param name="source">The delegate from which to remove the invocation list of .</param>
    /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of .</param>
    /// ###
    /// <returns>
    ///     A new delegate with an invocation list formed by taking the invocation list of  and removing the last
    ///     occurrence of the invocation list of , if the invocation list of  is found within the invocation list of .
    ///     Returns  if  is null or if the invocation list of  is not found within the invocation list of . Returns a
    ///     null reference if the invocation list of  is equal to the invocation list of  or if  is a null reference.
    /// </returns>
    public static Delegate Remove(this Delegate source, Delegate value)
    {
        return Delegate.Remove(source, value);
    }

    /// <summary>
    ///     Concatenates the invocation lists of two delegates.
    /// </summary>
    /// <param name="a">The delegate whose invocation list comes first.</param>
    /// <param name="b">The delegate whose invocation list comes last.</param>
    /// ###
    /// <returns>
    ///     A new delegate with an invocation list that concatenates the invocation lists of  and  in that order. Returns
    ///     if  is null, returns  if  is a null reference, and returns a null reference if both  and  are null references.
    /// </returns>
    public static Delegate Combine(this Delegate a, Delegate b)
    {
        return Delegate.Combine(a, b);
    }

    /// <summary>
    ///     Returns the smaller of two 16-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static UInt16 Min(this UInt16 val1, UInt16 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns the larger of two 16-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static UInt16 Max(this UInt16 val1, UInt16 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this UInt16 @this, params UInt16[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this UInt16 @this, UInt16 minValue, UInt16 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this UInt16 @this, UInt16 minValue, UInt16 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this UInt16 @this, params UInt16[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A Random extension method that flip a coin toss.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true 50% of time, otherwise false.</returns>
    public static bool CoinToss(this Random @this)
    {
        return @this.Next(2) == 0;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this UInt32 @this, UInt32 minValue, UInt32 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A Random extension method that return a random value from the specified values.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing arguments.</param>
    /// <returns>One of the specified value.</returns>
    public static T OneOf<T>(this Random @this, params T[] values)
    {
        return values[@this.Next(values.Length)];
    }

    public static SqlDbType SqlSystemTypeToSqlDbType(this byte @this)
    {
        switch (@this)
        {
            case 34: // 34 | "image" | SqlDbType.Image
                return SqlDbType.Image;

            case 35: // 35 | "text" | SqlDbType.Text
                return SqlDbType.Text;

            case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                return SqlDbType.UniqueIdentifier;

            case 40: // 40 | "date" | SqlDbType.Date
                return SqlDbType.Date;

            case 41: // 41 | "time" | SqlDbType.Time
                return SqlDbType.Time;

            case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                return SqlDbType.DateTime2;

            case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                return SqlDbType.DateTimeOffset;

            case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                return SqlDbType.TinyInt;

            case 52: // 52 | "smallint" | SqlDbType.SmallInt
                return SqlDbType.SmallInt;

            case 56: // 56 | "int" | SqlDbType.Int
                return SqlDbType.Int;

            case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                return SqlDbType.SmallDateTime;

            case 59: // 59 | "real" | SqlDbType.Real
                return SqlDbType.Real;

            case 60: // 60 | "money" | SqlDbType.Money
                return SqlDbType.Money;

            case 61: // 61 | "datetime" | SqlDbType.DateTime
                return SqlDbType.DateTime;

            case 62: // 62 | "float" | SqlDbType.Float
                return SqlDbType.Float;

            case 98: // 98 | "sql_variant" | SqlDbType.Variant
                return SqlDbType.Variant;

            case 99: // 99 | "ntext" | SqlDbType.NText
                return SqlDbType.NText;

            case 104: // 104 | "bit" | SqlDbType.Bit
                return SqlDbType.Bit;

            case 106: // 106 | "decimal" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 108: // 108 | "numeric" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                return SqlDbType.SmallMoney;

            case 127: // 127 | "bigint" | SqlDbType.BigInt
                return SqlDbType.BigInt;

            case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                return SqlDbType.VarBinary;

            case 167: // 167 | "varchar" | SqlDbType.VarChar
                return SqlDbType.VarChar;

            case 173: // 173 | "binary" | SqlDbType.Binary
                return SqlDbType.Binary;

            case 175: // 175 | "char" | SqlDbType.Char
                return SqlDbType.Char;

            case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                return SqlDbType.Timestamp;

            case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                return SqlDbType.NVarChar;

            case 239: // 239 | "nchar" | SqlDbType.NChar
                return SqlDbType.NChar;

            case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;

            case 241: // 241 | "xml" | SqlDbType.Xml
                return SqlDbType.Xml;

            default:
                throw new Exception(string.Format("Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com", @this));
        }
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this UInt32 @this, UInt32 minValue, UInt32 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this UInt32 @this, params UInt32[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this UInt32 @this, params UInt32[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     Returns the larger of two 32-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static UInt32 Max(this UInt32 val1, UInt32 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the smaller of two 32-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static UInt32 Min(this UInt32 val1, UInt32 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns the larger of two 8-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static Byte Max(this Byte val1, Byte val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the smaller of two 8-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static Byte Min(this Byte val1, Byte val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     A bool extension method that convert this object into a binary representation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A binary represenation of this object.</returns>
    public static byte ToBinary(this bool @this)
    {
        return Convert.ToByte(@this);
    }

    /// <summary>
    ///     A bool extension method that execute an Action if the value is false.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="action">The action to execute.</param>
    public static void IfFalse(this bool @this, Action action)
    {
        if (!@this)
        {
            action();
        }
    }

    /// <summary>
    ///     A bool extension method that execute an Action if the value is true.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="action">The action to execute.</param>
    public static void IfTrue(this bool @this, Action action)
    {
        if (@this)
        {
            action();
        }
    }

    /// <summary>
    ///     Returns the larger of two 64-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static UInt64 Max(this UInt64 val1, UInt64 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the smaller of two 64-bit unsigned integers.
    /// </summary>
    /// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
    /// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static UInt64 Min(this UInt64 val1, UInt64 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this UInt64 @this, UInt64 minValue, UInt64 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this UInt64 @this, UInt64 minValue, UInt64 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this UInt64 @this, params UInt64[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Guid @this, params Guid[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this UInt64 @this, params UInt64[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>A GUID extension method that queries if a not is empty.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if a not is empty, false if not.</returns>
    public static bool IsNotEmpty(this Guid @this)
    {
        return @this != Guid.Empty;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Guid @this, params Guid[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>A GUID extension method that query if '@this' is empty.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if empty, false if not.</returns>
    public static bool IsEmpty(this Guid @this)
    {
        return @this == Guid.Empty;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this SByte @this, params SByte[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this SByte @this, params SByte[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     Returns the absolute value of an 8-bit signed integer.
    /// </summary>
    /// <param name="value">A number that is greater than , but less than or equal to .</param>
    /// <returns>An 8-bit signed integer, x, such that 0 ? x ?.</returns>
    public static SByte Abs(this SByte value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns a value indicating the sign of an 8-bit signed integer.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this SByte value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     An Array extension method that clears the array.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    public static void ClearAll(this Array @this)
    {
        Array.Clear(@this, 0, @this.Length);
    }

    /// <summary>
    ///     Sorts the elements in an entire one-dimensional  using the  implementation of each element of the .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    public static void Sort(this Array array)
    {
        Array.Sort(array);
    }

    /// <summary>
    ///     Sorts a pair of one-dimensional  objects (one contains the keys and the other contains the corresponding
    ///     items) based on the keys in the first  using the  implementation of each key.
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items)
    {
        Array.Sort(array, items);
    }

    /// <summary>
    ///     Sorts the elements in a range of elements in a one-dimensional  using the  implementation of each element of
    ///     the .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    public static void Sort(this Array array, Int32 index, Int32 length)
    {
        Array.Sort(array, index, length);
    }

    /// <summary>
    ///     Sorts a range of elements in a pair of one-dimensional  objects (one contains the keys and the other contains
    ///     the corresponding items) based on the keys in the first  using the  implementation of each key.
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, Int32 index, Int32 length)
    {
        Array.Sort(array, items, index, length);
    }

    /// <summary>
    ///     Sorts the elements in a one-dimensional  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    public static void Sort(this Array array, IComparer comparer)
    {
        Array.Sort(array, comparer);
    }

    /// <summary>
    ///     Sorts a pair of one-dimensional  objects (one contains the keys and the other contains the corresponding
    ///     items) based on the keys in the first  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, IComparer comparer)
    {
        Array.Sort(array, items, comparer);
    }

    /// <summary>
    ///     Sorts the elements in a range of elements in a one-dimensional  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    public static void Sort(this Array array, Int32 index, Int32 length, IComparer comparer)
    {
        Array.Sort(array, index, length, comparer);
    }

    /// <summary>
    ///     Sorts a range of elements in a pair of one-dimensional  objects (one contains the keys and the other contains
    ///     the corresponding items) based on the keys in the first  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, Int32 index, Int32 length, IComparer comparer)
    {
        Array.Sort(array, items, index, length, comparer);
    }

    /// <summary>
    ///     Returns the smaller of two 8-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 8-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 8-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static SByte Min(this SByte val1, SByte val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns the larger of two 8-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 8-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 8-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static SByte Max(this SByte val1, SByte val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the number of bytes in the specified array.
    /// </summary>
    /// <param name="array">An array.</param>
    /// <returns>The number of bytes in the array.</returns>
    public static Int32 ByteLength(this Array array)
    {
        return Buffer.ByteLength(array);
    }

    /// <summary>
    ///     Gets the types of the objects in the specified array.
    /// </summary>
    /// <param name="args">An array of objects whose types to determine.</param>
    /// <returns>An array of  objects representing the types of the corresponding elements in .</returns>
    public static Type[] GetTypeArray(this Object[] args)
    {
        return Type.GetTypeArray(args);
    }

    /// <summary>
    ///     An Array extension method that check if the array is lower then the specified index.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="index">Zero-based index of the.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool WithinIndex(this Array @this, int index)
    {
        return index >= 0 && index < @this.Length;
    }

    /// <summary>
    ///     An Array extension method that check if the array is lower then the specified index.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="index">Zero-based index of the.</param>
    /// <param name="dimension">(Optional) the dimension.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool WithinIndex(this Array @this, int index, int dimension = 0)
    {
        return index >= @this.GetLowerBound(dimension) && index <= @this.GetUpperBound(dimension);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the last occurrence within the entire one-
    ///     dimensional .
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <returns>
    ///     The index of the last occurrence of  within the entire , if found; otherwise, the lower bound of the array
    ///     minus 1.
    /// </returns>
    public static Int32 LastIndexOf(this Array array, Object value)
    {
        return Array.LastIndexOf(array, value);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the last occurrence within the range of elements
    ///     in the one-dimensional  that extends from the first element to the specified index.
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <returns>
    ///     The index of the last occurrence of  within the range of elements in  that extends from the first element to ,
    ///     if found; otherwise, the lower bound of the array minus 1.
    /// </returns>
    public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex)
    {
        return Array.LastIndexOf(array, value, startIndex);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the last occurrence within the range of elements
    ///     in the one-dimensional  that contains the specified number of elements and ends at the specified index.
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <param name="startIndex">The starting index of the backward search.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <returns>
    ///     The index of the last occurrence of  within the range of elements in  that contains the number of elements
    ///     specified in  and ends at , if found; otherwise, the lower bound of the array minus 1.
    /// </returns>
    public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
    {
        return Array.LastIndexOf(array, value, startIndex, count);
    }

    /// <summary>
    ///     Retrieves the byte at a specified location in a specified array.
    /// </summary>
    /// <param name="array">An array.</param>
    /// <param name="index">A location in the array.</param>
    /// <returns>Returns the  byte in the array.</returns>
    public static Byte GetByte(this Array array, Int32 index)
    {
        return Buffer.GetByte(array, index);
    }

    /// <summary>
    ///     Copies a range of elements from an  starting at the first element and pastes them into another  starting at
    ///     the first element. The length is specified as a 32-bit integer.
    /// </summary>
    /// <param name="sourceArray">The  that contains the data to copy.</param>
    /// <param name="destinationArray">The  that receives the data.</param>
    /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
    public static void Copy(this Array sourceArray, Array destinationArray, Int32 length)
    {
        Array.Copy(sourceArray, destinationArray, length);
    }

    /// <summary>
    ///     Copies a range of elements from an  starting at the specified source index and pastes them to another
    ///     starting at the specified destination index. The length and the indexes are specified as 32-bit integers.
    /// </summary>
    /// <param name="sourceArray">The  that contains the data to copy.</param>
    /// <param name="sourceIndex">A 32-bit integer that represents the index in the  at which copying begins.</param>
    /// <param name="destinationArray">The  that receives the data.</param>
    /// <param name="destinationIndex">A 32-bit integer that represents the index in the  at which storing begins.</param>
    /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
    public static void Copy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
    {
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
    }

    /// <summary>
    ///     Copies a range of elements from an  starting at the first element and pastes them into another  starting at
    ///     the first element. The length is specified as a 64-bit integer.
    /// </summary>
    /// <param name="sourceArray">The  that contains the data to copy.</param>
    /// <param name="destinationArray">The  that receives the data.</param>
    /// <param name="length">
    ///     A 64-bit integer that represents the number of elements to copy. The integer must be between
    ///     zero and , inclusive.
    /// </param>
    public static void Copy(this Array sourceArray, Array destinationArray, Int64 length)
    {
        Array.Copy(sourceArray, destinationArray, length);
    }

    /// <summary>
    ///     Copies a range of elements from an  starting at the specified source index and pastes them to another
    ///     starting at the specified destination index. The length and the indexes are specified as 64-bit integers.
    /// </summary>
    /// <param name="sourceArray">The  that contains the data to copy.</param>
    /// <param name="sourceIndex">A 64-bit integer that represents the index in the  at which copying begins.</param>
    /// <param name="destinationArray">The  that receives the data.</param>
    /// <param name="destinationIndex">A 64-bit integer that represents the index in the  at which storing begins.</param>
    /// <param name="length">
    ///     A 64-bit integer that represents the number of elements to copy. The integer must be between
    ///     zero and , inclusive.
    /// </param>
    public static void Copy(this Array sourceArray, Int64 sourceIndex, Array destinationArray, Int64 destinationIndex, Int64 length)
    {
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the first occurrence within the entire one-
    ///     dimensional .
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <returns>
    ///     The index of the first occurrence of  within the entire , if found; otherwise, the lower bound of the array
    ///     minus 1.
    /// </returns>
    public static Int32 IndexOf(this Array array, Object value)
    {
        return Array.IndexOf(array, value);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the first occurrence within the range of elements
    ///     in the one-dimensional  that extends from the specified index to the last element.
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
    /// <returns>
    ///     The index of the first occurrence of  within the range of elements in  that extends from  to the last element,
    ///     if found; otherwise, the lower bound of the array minus 1.
    /// </returns>
    public static Int32 IndexOf(this Array array, Object value, Int32 startIndex)
    {
        return Array.IndexOf(array, value, startIndex);
    }

    /// <summary>
    ///     Searches for the specified object and returns the index of the first occurrence within the range of elements
    ///     in the one-dimensional  that starts at the specified index and contains the specified number of elements.
    /// </summary>
    /// <param name="array">The one-dimensional  to search.</param>
    /// <param name="value">The object to locate in .</param>
    /// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <returns>
    ///     The index of the first occurrence of  within the range of elements in  that starts at  and contains the
    ///     number of elements specified in , if found; otherwise, the lower bound of the array minus 1.
    /// </returns>
    public static Int32 IndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
    {
        return Array.IndexOf(array, value, startIndex, count);
    }

    /// <summary>
    ///     Assigns a specified value to a byte at a particular location in a specified array.
    /// </summary>
    /// <param name="array">An array.</param>
    /// <param name="index">A location in the array.</param>
    /// <param name="value">A value to assign.</param>
    public static void SetByte(this Array array, Int32 index, Byte value)
    {
        Buffer.SetByte(array, index, value);
    }

    /// <summary>
    ///     Copies a specified number of bytes from a source array starting at a particular offset to a destination array
    ///     starting at a particular offset.
    /// </summary>
    /// <param name="src">The source buffer.</param>
    /// <param name="srcOffset">The zero-based byte offset into .</param>
    /// <param name="dst">The destination buffer.</param>
    /// <param name="dstOffset">The zero-based byte offset into .</param>
    /// <param name="count">The number of bytes to copy.</param>
    public static void BlockCopy(this Array src, Int32 srcOffset, Array dst, Int32 dstOffset, Int32 count)
    {
        Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
    }

    /// <summary>
    ///     Searches an entire one-dimensional sorted  for a specific element, using the  interface implemented by each
    ///     element of the  and by the specified object.
    /// </summary>
    /// <param name="array">The sorted one-dimensional  to search.</param>
    /// <param name="value">The object to search for.</param>
    /// <returns>
    ///     The index of the specified  in the specified , if  is found. If  is not found and  is less than one or more
    ///     elements in , a negative number which is the bitwise complement of the index of the first element that is
    ///     larger than . If  is not found and  is greater than any of the elements in , a negative number which is the
    ///     bitwise complement of (the index of the last element plus 1).
    /// </returns>
    public static Int32 BinarySearch(this Array array, Object value)
    {
        return Array.BinarySearch(array, value);
    }

    /// <summary>
    ///     Searches a range of elements in a one-dimensional sorted  for a value, using the  interface implemented by
    ///     each element of the  and by the specified value.
    /// </summary>
    /// <param name="array">The sorted one-dimensional  to search.</param>
    /// <param name="index">The starting index of the range to search.</param>
    /// <param name="length">The length of the range to search.</param>
    /// <param name="value">The object to search for.</param>
    /// <returns>
    ///     The index of the specified  in the specified , if  is found. If  is not found and  is less than one or more
    ///     elements in , a negative number which is the bitwise complement of the index of the first element that is
    ///     larger than . If  is not found and  is greater than any of the elements in , a negative number which is the
    ///     bitwise complement of (the index of the last element plus 1).
    /// </returns>
    public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, Object value)
    {
        return Array.BinarySearch(array, index, length, value);
    }

    /// <summary>
    ///     Searches an entire one-dimensional sorted  for a value using the specified  interface.
    /// </summary>
    /// <param name="array">The sorted one-dimensional  to search.</param>
    /// <param name="value">The object to search for.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or- null to use the  implementation
    ///     of each element.
    /// </param>
    /// <returns>
    ///     The index of the specified  in the specified , if  is found. If  is not found and  is less than one or more
    ///     elements in , a negative number which is the bitwise complement of the index of the first element that is
    ///     larger than . If  is not found and  is greater than any of the elements in , a negative number which is the
    ///     bitwise complement of (the index of the last element plus 1).
    /// </returns>
    public static Int32 BinarySearch(this Array array, Object value, IComparer comparer)
    {
        return Array.BinarySearch(array, value, comparer);
    }

    /// <summary>
    ///     Searches a range of elements in a one-dimensional sorted  for a value, using the specified  interface.
    /// </summary>
    /// <param name="array">The sorted one-dimensional  to search.</param>
    /// <param name="index">The starting index of the range to search.</param>
    /// <param name="length">The length of the range to search.</param>
    /// <param name="value">The object to search for.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or- null to use the  implementation
    ///     of each element.
    /// </param>
    /// <returns>
    ///     The index of the specified  in the specified , if  is found. If  is not found and  is less than one or more
    ///     elements in , a negative number which is the bitwise complement of the index of the first element that is
    ///     larger than . If  is not found and  is greater than any of the elements in , a negative number which is the
    ///     bitwise complement of (the index of the last element plus 1).
    /// </returns>
    public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, Object value, IComparer comparer)
    {
        return Array.BinarySearch(array, index, length, value, comparer);
    }

    /// <summary>
    ///     Copies a range of elements from an  starting at the specified source index and pastes them to another
    ///     starting at the specified destination index.  Guarantees that all changes are undone if the copy does not
    ///     succeed completely.
    /// </summary>
    /// <param name="sourceArray">The  that contains the data to copy.</param>
    /// <param name="sourceIndex">A 32-bit integer that represents the index in the  at which copying begins.</param>
    /// <param name="destinationArray">The  that receives the data.</param>
    /// <param name="destinationIndex">A 32-bit integer that represents the index in the  at which storing begins.</param>
    /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
    public static void ConstrainedCopy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
    {
        Array.ConstrainedCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
    }

    /// <summary>A StringBuilder extension method that appends a join.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
    {
        @this.Append(string.Join(separator, values));

        return @this;
    }

    /// <summary>A StringBuilder extension method that appends a join.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, params T[] values)
    {
        @this.Append(string.Join(separator, values));

        return @this;
    }

    /// <summary>
    ///     Sets a range of elements in the  to zero, to false, or to null, depending on the element type.
    /// </summary>
    /// <param name="array">The  whose elements need to be cleared.</param>
    /// <param name="index">The starting index of the range of elements to clear.</param>
    /// <param name="length">The number of elements to clear.</param>
    public static void Clear(this Array array, Int32 index, Int32 length)
    {
        Array.Clear(array, index, length);
    }

    /// <summary>A StringBuilder extension method that substrings.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>A string.</returns>
    public static string Substring(this StringBuilder @this, int startIndex)
    {
        return @this.ToString(startIndex, @this.Length - startIndex);
    }

    /// <summary>A StringBuilder extension method that substrings.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string Substring(this StringBuilder @this, int startIndex, int length)
    {
        return @this.ToString(startIndex, length);
    }

    /// <summary>A StringBuilder extension method that gets index after next double quote.</summary>
    /// <param name="this">The path to act on.</param>
    /// <returns>The index after next double quote.</returns>
    public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this)
    {
        return @this.GetIndexAfterNextDoubleQuote(0, false);
    }

    /// <summary>A StringBuilder extension method that gets index after next double quote.</summary>
    /// <param name="this">The path to act on.</param>
    /// <param name="allowEscape">true to allow, false to deny escape.</param>
    /// <returns>The index after next double quote.</returns>
    public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, bool allowEscape)
    {
        return @this.GetIndexAfterNextDoubleQuote(0, allowEscape);
    }

    /// <summary>A StringBuilder extension method that gets index after next double quote.</summary>
    /// <param name="this">The path to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The index after next double quote.</returns>
    public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, int startIndex)
    {
        return @this.GetIndexAfterNextDoubleQuote(startIndex, false);
    }

    /// <summary>A StringBuilder extension method that gets index after next double quote.</summary>
    /// <param name="this">The path to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="allowEscape">true to allow, false to deny escape.</param>
    /// <returns>The index after next double quote.</returns>
    public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, int startIndex, bool allowEscape)
    {
        while (startIndex < @this.Length)
        {
            char ch = @this[startIndex];
            startIndex++;

            char nextChar;
            if (allowEscape && ch == '\\' && startIndex < @this.Length && ((nextChar = @this[startIndex]) == '\\' || nextChar == '"'))
            {
                startIndex++; // Treat as escape character for \\ or \"
            }
            else if (ch == '"')
            {
                return startIndex;
            }
        }

        return startIndex;
    }

    /// <summary>A StringBuilder extension method that appends a line join.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    public static StringBuilder AppendLineJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
    {
        @this.AppendLine(string.Join(separator, values));

        return @this;
    }

    /// <summary>A StringBuilder extension method that appends a line join.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    public static StringBuilder AppendLineJoin(this StringBuilder @this, string separator, params object[] values)
    {
        @this.AppendLine(string.Join(separator, values));

        return @this;
    }

    /// <summary>A StringBuilder extension method that extracts the directive described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted directive.</returns>
    public static StringBuilder ExtractToken(this StringBuilder @this)
    {
        return @this.ExtractToken(0);
    }

    /// <summary>A StringBuilder extension method that extracts the directive described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted directive.</returns>
    public static StringBuilder ExtractToken(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractToken(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the directive described by @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted directive.</returns>
    public static StringBuilder ExtractToken(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractToken(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the directive described by @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted directive.</returns>
    public static StringBuilder ExtractToken(this StringBuilder @this, int startIndex, out int endIndex)
    {
        /* A token can be:
         * - Keyword / Literal
         * - Operator
         * - String
         * - Integer
         * - Real
         */

        // CHECK first which type is the token
        var ch1 = @this[startIndex];
        var pos = startIndex + 1;

        switch (ch1)
        {
            case '@':
                if (pos < @this.Length && @this[pos] == '"')
                {
                    return @this.ExtractStringArobasDoubleQuote(startIndex, out endIndex);
                }
                if (pos < @this.Length && @this[pos] == '\'')
                {
                    return @this.ExtractStringArobasSingleQuote(startIndex, out endIndex);
                }

                break;
            case '"':
                return @this.ExtractStringDoubleQuote(startIndex, out endIndex);
            case '\'':
                return @this.ExtractStringSingleQuote(startIndex, out endIndex);
            case '`':
            case '~':
            case '!':
            case '#':
            case '$':
            case '%':
            case '^':
            case '&':
            case '*':
            case '(':
            case ')':
            case '-':
            case '_':
            case '=':
            case '+':
            case '[':
            case ']':
            case '{':
            case '}':
            case '|':
            case ':':
            case ';':
            case ',':
            case '.':
            case '<':
            case '>':
            case '?':
            case '/':
                return @this.ExtractOperator(startIndex, out endIndex);
            case '0':
                if (pos < @this.Length && (@this[pos] == 'x' || @this[pos] == 'X'))
                {
                    return @this.ExtractHexadecimal(startIndex, out endIndex);
                }

                return @this.ExtractNumber(startIndex, out endIndex);
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return @this.ExtractNumber(startIndex, out endIndex);
            default:
                if ((ch1 >= 'a' && ch1 <= 'z') || (ch1 >= 'A' && ch1 <= 'Z'))
                {
                    return @this.ExtractKeyword(startIndex, out endIndex);
                }

                endIndex = -1;
                return null;
        }

        throw new Exception("Invalid token");
    }

    /// <summary>Gets index after next single quote.</summary>
    /// <param name="this">Full pathname of the file.</param>
    /// <returns>The index after next single quote.</returns>
    public static int GetIndexAfterNextSingleQuote(this StringBuilder @this)
    {
        return @this.GetIndexAfterNextSingleQuote(0, false);
    }

    /// <summary>Gets index after next single quote.</summary>
    /// <param name="this">Full pathname of the file.</param>
    /// <param name="allowEscape">true to allow, false to deny escape.</param>
    /// <returns>The index after next single quote.</returns>
    public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, bool allowEscape)
    {
        return @this.GetIndexAfterNextSingleQuote(0, allowEscape);
    }

    /// <summary>Gets index after next single quote.</summary>
    /// <param name="this">Full pathname of the file.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The index after next single quote.</returns>
    public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, int startIndex)
    {
        return @this.GetIndexAfterNextSingleQuote(startIndex, false);
    }

    /// <summary>Gets index after next single quote.</summary>
    /// <param name="this">Full pathname of the file.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="allowEscape">true to allow, false to deny escape.</param>
    /// <returns>The index after next single quote.</returns>
    public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, int startIndex, bool allowEscape)
    {
        while (startIndex < @this.Length)
        {
            char ch = @this[startIndex];
            startIndex++;

            char nextChar;
            if (allowEscape && ch == '\\' && startIndex < @this.Length && ((nextChar = @this[startIndex]) == '\\' || nextChar == '\''))
            {
                startIndex++; // Treat as escape character for \\ or \'
            }
            else if (ch == '\'')
            {
                return startIndex;
            }
        }

        return startIndex;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas single quote
    ///     described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted string arobas single quote.</returns>
    public static StringBuilder ExtractStringArobasSingleQuote(this StringBuilder @this)
    {
        return @this.ExtractStringArobasSingleQuote(0);
    }
    /// <summary>A StringBuilder extension method that extracts the string arobas single quote
    /// described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string arobas single quote.</returns>
    public static StringBuilder ExtractStringArobasSingleQuote(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractStringArobasSingleQuote(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas single quote
    ///     described by @this.
    /// </summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted string arobas single quote.</returns>
    public static StringBuilder ExtractStringArobasSingleQuote(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractStringArobasSingleQuote(startIndex, out endIndex);
    }
    /// <summary>A StringBuilder extension method that extracts the string arobas single quote
    /// described by @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string arobas single quote.</returns>
    public static StringBuilder ExtractStringArobasSingleQuote(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '@' && ch2 == '\'')
            {
                // WARNING: This is not a valid string, however single quote is often used to make it more readable in text templating
                // @'my string'

                var pos = startIndex + 2;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    if (ch == '\'' && pos < @this.Length && @this[pos] == '\'')
                    {
                        sb.Append(ch);
                        pos++; // Treat as escape character for @'abc''def'
                    }
                    else if (ch == '\'')
                    {
                        endIndex = pos;
                        return sb;
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }

                throw new Exception("Unclosed string starting at position: " + startIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that appends a when.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>A StringBuilder.</returns>
    public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var value in values)
        {
            if (predicate(value))
            {
                @this.Append(value);
            }
        }

        return @this;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the trivia tokens described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted trivia tokens.</returns>
    public static StringBuilder ExtractTriviaToken(this StringBuilder @this)
    {
        return @this.ExtractTriviaToken(0);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the trivia tokens described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted trivia tokens.</returns>
    public static StringBuilder ExtractTriviaToken(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractTriviaToken(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the trivia tokens described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted trivia tokens.</returns>
    public static StringBuilder ExtractTriviaToken(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractTriviaToken(startIndex, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the trivia tokens described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted trivia tokens.</returns>
    public static StringBuilder ExtractTriviaToken(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();
        var pos = startIndex;

        var isSpace = false;

        while (pos < @this.Length)
        {
            var ch = @this[pos];
            pos++;

            if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t')
            {
                isSpace = true;
                sb.Append(ch);
            }
            else if (ch == '/' && !isSpace)
            {
                if (pos < @this.Length)
                {
                    ch = @this[pos];
                    if (ch == '/')
                    {
                        return @this.ExtractCommentSingleLine(startIndex, out endIndex);
                    }
                    if (ch == '*')
                    {
                        return @this.ExtractCommentMultiLine(startIndex, out endIndex);
                    }

                    // otherwise is probably the divide operator
                    pos--;
                    break;
                }
            }
            else
            {
                pos -= 2;
                break;
            }
        }

        if (isSpace)
        {
            endIndex = pos;
            return sb;
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that appends a line when.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>A StringBuilder.</returns>
    public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var value in values)
        {
            if (predicate(value))
            {
                @this.AppendLine(value.ToString());
            }
        }

        return @this;
    }

    /// <summary>
    ///     A StringBuilder extension method that appends a line format.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="format">Describes the format to use.</param>
    /// <param name="args">A variable-length parameters list containing arguments.</param>
    public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
    {
        @this.AppendLine(string.Format(format, args));

        return @this;
    }

    /// <summary>
    ///     A StringBuilder extension method that appends a line format.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="format">Describes the format to use.</param>
    /// <param name="args">A variable-length parameters list containing arguments.</param>
    public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
    {
        @this.AppendLine(string.Format(format, args));

        return @this;
    }

    /// <summary>A StringBuilder extension method that extracts the operator described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted operator.</returns>
    public static StringBuilder ExtractOperator(this StringBuilder @this)
    {
        return @this.ExtractOperator(0);
    }

    /// <summary>A StringBuilder extension method that extracts the operator described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted operator.</returns>
    public static StringBuilder ExtractOperator(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractOperator(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the operator described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted operator.</returns>
    public static StringBuilder ExtractOperator(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractOperator(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the operator described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted operator.</returns>
    public static StringBuilder ExtractOperator(this StringBuilder @this, int startIndex, out int endIndex)
    {
        // WARNING: This method support custom operator for .NET Runtime Compiler
        // An operator can be any sequence of supported operator character
        var sb = new StringBuilder();

        var pos = startIndex;

        while (pos < @this.Length)
        {
            var ch = @this[pos];
            pos++;

            switch (ch)
            {
                case '`':
                case '~':
                case '!':
                case '#':
                case '$':
                case '%':
                case '^':
                case '&':
                case '*':
                case '(':
                case ')':
                case '-':
                case '_':
                case '=':
                case '+':
                case '[':
                case ']':
                case '{':
                case '}':
                case '|':
                case ':':
                case ';':
                case ',':
                case '.':
                case '<':
                case '>':
                case '?':
                case '/':
                    sb.Append(ch);
                    break;
                default:
                    if (sb.Length > 0)
                    {
                        endIndex = pos - 2;
                        return sb;
                    }

                    endIndex = -1;
                    return null;
            }
        }

        if (sb.Length > 0)
        {
            endIndex = pos;
            return sb;
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that extracts the character described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted character.</returns>
    public static char ExtractChar(this StringBuilder @this)
    {
        return @this.ExtractChar(0);
    }

    /// <summary>A StringBuilder extension method that extracts the character described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted character.</returns>
    public static char ExtractChar(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractChar(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the character described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted character.</returns>
    public static char ExtractChar(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractChar(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the character described by @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted character.</returns>
    public static char ExtractChar(this StringBuilder @this, int startIndex, out int endIndex)
    {
        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];
            var ch3 = @this[startIndex + 2];

            if (ch1 == '\'' && ch3 == '\'')
            {
                endIndex = startIndex + 2;
                return ch2;
            }
        }

        throw new Exception("Invalid char at position: " + startIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the comment described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted comment.</returns>
    public static StringBuilder ExtractComment(this StringBuilder @this)
    {
        return @this.ExtractComment(0);
    }

    /// <summary>A StringBuilder extension method that extracts the comment described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment.</returns>
    public static StringBuilder ExtractComment(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractComment(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the comment described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted comment.</returns>
    public static StringBuilder ExtractComment(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractComment(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the comment described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment.</returns>
    public static StringBuilder ExtractComment(this StringBuilder @this, int startIndex, out int endIndex)
    {
        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '/' && ch2 == '/')
            {
                // Single line comment

                return @this.ExtractCommentSingleLine(startIndex, out endIndex);
            }

            if (ch1 == '/' && ch2 == '*')
            {
                /*
                 * Multi-line comment
                 */

                return @this.ExtractCommentMultiLine(startIndex, out endIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string single quote described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted string single quote.</returns>
    public static StringBuilder ExtractStringSingleQuote(this StringBuilder @this)
    {
        return @this.ExtractStringSingleQuote(0);
    }
    /// <summary>A StringBuilder extension method that extracts the string single quote described by
    /// @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string single quote.</returns>
    public static StringBuilder ExtractStringSingleQuote(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractStringSingleQuote(0, out endIndex);
    }


    /// <summary>
    ///     A StringBuilder extension method that extracts the string single quote described by
    ///     @this.
    /// </summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted string single quote.</returns>
    public static StringBuilder ExtractStringSingleQuote(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractStringSingleQuote(startIndex, out endIndex);
    }
    /// <summary>A StringBuilder extension method that extracts the string single quote described by
    /// @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string single quote.</returns>
    public static StringBuilder ExtractStringSingleQuote(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];

            if (ch1 == '\'')
            {
                // WARNING: This is not a valid string, however single quote is often used to make it more readable in text templating
                // 'my string'

                var pos = startIndex + 1;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    char nextChar;
                    if (ch == '\\' && pos < @this.Length && ((nextChar = @this[pos]) == '\\' || nextChar == '\''))
                    {
                        sb.Append(nextChar);
                        pos++; // Treat as escape character for \\ or \"
                    }
                    else if (ch == '\'')
                    {
                        endIndex = pos;
                        return sb;
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }

                throw new Exception("Unclosed string starting at position: " + startIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted keyword.</returns>
    public static StringBuilder ExtractKeyword(this StringBuilder @this)
    {
        return @this.ExtractKeyword(0);
    }

    /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted keyword.</returns>
    public static StringBuilder ExtractKeyword(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractKeyword(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted keyword.</returns>
    public static StringBuilder ExtractKeyword(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractKeyword(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the keyword described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted keyword.</returns>
    public static StringBuilder ExtractKeyword(this StringBuilder @this, int startIndex, out int endIndex)
    {
        // WARNING: This method support custom operator for .NET Runtime Compiler
        // An operator can be any sequence of supported operator character
        var sb = new StringBuilder();

        var pos = startIndex;
        var hasCharacter = false;

        while (pos < @this.Length)
        {
            var ch = @this[pos];
            pos++;

            if ((ch >= 'a' && ch <= 'z') || (ch>= 'A' && ch <= 'Z'))
            {
                hasCharacter = true;
                sb.Append(ch);
            }
            else if (ch == '@')
            {
                sb.Append(ch);
            }
            else if (ch >= '0' && ch <= '9' && hasCharacter)
            {
                sb.Append(ch);
            }
            else
            {
                pos-= 2;
                break;
            }
        }

        if (hasCharacter)
        {
            endIndex = pos;
            return sb;
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that extracts the string described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted string.</returns>
    public static StringBuilder ExtractString(this StringBuilder @this)
    {
        return @this.ExtractString(0);
    }

    /// <summary>A StringBuilder extension method that extracts the string described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string.</returns>
    public static StringBuilder ExtractString(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractString(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the string described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted string.</returns>
    public static StringBuilder ExtractString(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractString(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the string described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string.</returns>
    public static StringBuilder ExtractString(this StringBuilder @this, int startIndex, out int endIndex)
    {
        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '@' && ch2 == '"')
            {
                // @"my string"

                return @this.ExtractStringArobasDoubleQuote(startIndex, out endIndex);
            }

            if (ch1 == '@' && ch2 == '\'')
            {
                // WARNING: This is not a valid string, however single quote is often used to make it more readable in text templating
                // @'my string'

                return @this.ExtractStringArobasSingleQuote(startIndex, out endIndex);
            }

            if (ch1 == '"')
            {
                // "my string"

                return @this.ExtractStringDoubleQuote(startIndex, out endIndex);
            }

            if (ch1 == '\'')
            {
                // WARNING: This is not a valid string, however single quote is often used to make it more readable in text templating
                // 'my string'

                return @this.ExtractStringSingleQuote(startIndex, out endIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that extracts the number described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted number.</returns>
    public static StringBuilder ExtractNumber(this StringBuilder @this)
    {
        return @this.ExtractNumber(0);
    }

    /// <summary>A StringBuilder extension method that extracts the number described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted number.</returns>
    public static StringBuilder ExtractNumber(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractNumber(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the number described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted number.</returns>
    public static StringBuilder ExtractNumber(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractNumber(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the number described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted number.</returns>
    public static StringBuilder ExtractNumber(this StringBuilder @this, int startIndex, out int endIndex)
    {
        // WARNING: This method support all kind of suffix for .NET Runtime Compiler
        // An operator can be any sequence of supported operator character
        var sb = new StringBuilder();

        var hasNumber = false;
        var hasDot = false;
        var hasSuffix = false;

        var pos = startIndex;

        while (pos < @this.Length)
        {
            var ch = @this[pos];
            pos++;

            if (ch >= '0' && ch <= '9' && !hasSuffix)
            {
                hasNumber = true;
                sb.Append(ch);
            }
            else if (ch == '.' && !hasSuffix && !hasDot)
            {
                hasDot = true;
                sb.Append(ch);
            }
            else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
            {
                hasSuffix = true;
                sb.Append(ch);
            }
            else
            {
                pos-= 2;
                break;
            }
        }

        if (hasNumber)
        {
            endIndex = pos;
            return sb;
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas double quote
    ///     described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted string arobas double quote.</returns>
    public static StringBuilder ExtractStringArobasDoubleQuote(this StringBuilder @this)
    {
        return @this.ExtractStringArobasDoubleQuote(0);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas double quote
    ///     described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string arobas double quote.</returns>
    public static StringBuilder ExtractStringArobasDoubleQuote(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractStringArobasDoubleQuote(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas double quote
    ///     described by @this.
    /// </summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted string arobas double quote.</returns>
    public static StringBuilder ExtractStringArobasDoubleQuote(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractStringArobasDoubleQuote(startIndex, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string arobas double quote
    ///     described by @this.
    /// </summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string arobas double quote.</returns>
    public static StringBuilder ExtractStringArobasDoubleQuote(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '@' && ch2 == '"')
            {
                // @"my string"

                var pos = startIndex + 2;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    if (ch == '"' && pos < @this.Length && @this[pos] == '"')
                    {
                        sb.Append(ch);
                        pos++; // Treat as escape character for @"abc""def"
                    }
                    else if (ch == '"')
                    {
                        endIndex = pos;
                        return sb;
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }

                throw new Exception("Unclosed string starting at position: " + startIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment multi line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted comment multi line.</returns>
    public static StringBuilder ExtractCommentMultiLine(this StringBuilder @this)
    {
        return @this.ExtractCommentMultiLine(0);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment multi line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment multi line.</returns>
    public static StringBuilder ExtractCommentMultiLine(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractCommentMultiLine(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment multi line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted comment multi line.</returns>
    public static StringBuilder ExtractCommentMultiLine(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractCommentMultiLine(startIndex, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment multi line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment multi line.</returns>
    public static StringBuilder ExtractCommentMultiLine(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '/' && ch2 == '*')
            {
                /*
                 * Multi-line comment
                 */

                sb.Append(ch1);
                sb.Append(ch2);
                var pos = startIndex + 2;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    if (ch == '*' && pos < @this.Length && @this[pos] == '/')
                    {
                        sb.Append(ch);
                        sb.Append(@this[pos]);
                        endIndex = pos;
                        return sb;
                    }

                    sb.Append(ch);
                }

                endIndex = pos;
                return sb;
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment single line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted comment single line.</returns>
    public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this)
    {
        return @this.ExtractCommentSingleLine(0);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment single line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment single line.</returns>
    public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractCommentSingleLine(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment single line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted comment single line.</returns>
    public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractCommentSingleLine(startIndex, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the comment single line described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted comment single line.</returns>
    public static StringBuilder ExtractCommentSingleLine(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];
            var ch2 = @this[startIndex + 1];

            if (ch1 == '/' && ch2 == '/')
            {
                // Single line comment

                sb.Append(ch1);
                sb.Append(ch2);
                var pos = startIndex + 2;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    if (ch == '\r' && pos < @this.Length && @this[pos] == '\n')
                    {
                        endIndex = pos - 1;
                        return sb;
                    }

                    sb.Append(ch);
                }

                endIndex = pos;
                return sb;
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string double quote described by
    ///     @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted string double quote.</returns>
    public static StringBuilder ExtractStringDoubleQuote(this StringBuilder @this)
    {
        return @this.ExtractStringDoubleQuote(0);
    }
    /// <summary>A StringBuilder extension method that extracts the string double quote described by
    /// @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string double quote.</returns>
    public static StringBuilder ExtractStringDoubleQuote(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractStringDoubleQuote(0, out endIndex);
    }

    /// <summary>
    ///     A StringBuilder extension method that extracts the string double quote described by
    ///     @this.
    /// </summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted string double quote.</returns>
    public static StringBuilder ExtractStringDoubleQuote(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractStringDoubleQuote(startIndex, out endIndex);
    }
    /// <summary>A StringBuilder extension method that extracts the string double quote described by
    /// @this.</summary>
    /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted string double quote.</returns>
    public static StringBuilder ExtractStringDoubleQuote(this StringBuilder @this, int startIndex, out int endIndex)
    {
        var sb = new StringBuilder();

        if (@this.Length > startIndex + 1)
        {
            var ch1 = @this[startIndex];

            if (ch1 == '"')
            {
                // "my string"

                var pos = startIndex + 1;

                while (pos < @this.Length)
                {
                    var ch = @this[pos];
                    pos++;

                    char nextChar;
                    if (ch == '\\' && pos < @this.Length && ((nextChar = @this[pos]) == '\\' || nextChar == '"'))
                    {
                        sb.Append(nextChar);
                        pos++; // Treat as escape character for \\ or \"
                    }
                    else if (ch == '"')
                    {
                        endIndex = pos;
                        return sb;
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }

                throw new Exception("Unclosed string starting at position: " + startIndex);
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>A StringBuilder extension method that extracts the hexadecimal described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted hexadecimal.</returns>
    public static StringBuilder ExtractHexadecimal(this StringBuilder @this)
    {
        return @this.ExtractHexadecimal(0);
    }

    /// <summary>A StringBuilder extension method that extracts the hexadecimal described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted hexadecimal.</returns>
    public static StringBuilder ExtractHexadecimal(this StringBuilder @this, out int endIndex)
    {
        return @this.ExtractHexadecimal(0, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the hexadecimal described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns>The extracted hexadecimal.</returns>
    public static StringBuilder ExtractHexadecimal(this StringBuilder @this, int startIndex)
    {
        int endIndex;
        return @this.ExtractHexadecimal(startIndex, out endIndex);
    }

    /// <summary>A StringBuilder extension method that extracts the hexadecimal described by @this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">[out] The end index.</param>
    /// <returns>The extracted hexadecimal.</returns>
    public static StringBuilder ExtractHexadecimal(this StringBuilder @this, int startIndex, out int endIndex)
    {
        // WARNING: This method support all kind of suffix for .NET Runtime Compiler
        // An operator can be any sequence of supported operator character

        if (startIndex + 1 < @this.Length && @this[startIndex] == '0'
                                          && (@this[startIndex + 1] == 'x' || @this[startIndex + 1] == 'X'))
        {
            var sb = new StringBuilder();

            var hasNumber = false;
            var hasSuffix = false;

            sb.Append(@this[startIndex]);
            sb.Append(@this[startIndex + 1]);

            var pos = startIndex + 2;

            while (pos < @this.Length)
            {
                var ch = @this[pos];
                pos++;

                if (((ch >= '0' && ch <= '9')
                     || (ch >= 'a' && ch <= 'f')
                     || (ch >= 'A' && ch <= 'F'))
                    && !hasSuffix)
                {
                    hasNumber = true;
                    sb.Append(ch);
                }
                else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    hasSuffix = true;
                    sb.Append(ch);
                }
                else
                {
                    pos -= 2;
                    break;
                }
            }

            if (hasNumber)
            {
                endIndex = pos;
                return sb;
            }
        }

        endIndex = -1;
        return null;
    }

    /// <summary>
    ///     A TimeSpan extension method that add the specified TimeSpan to the current UTC (Coordinated Universal Time)
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The current UTC (Coordinated Universal Time) with the specified TimeSpan added to it.</returns>
    public static DateTime UtcFromNow(this TimeSpan @this)
    {
        return DateTime.UtcNow.Add(@this);
    }

    /// <summary>
    ///     A TimeSpan extension method that add the specified TimeSpan to the current DateTime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The current DateTime with the specified TimeSpan added to it.</returns>
    public static DateTime FromNow(this TimeSpan @this)
    {
        return DateTime.Now.Add(@this);
    }

    /// <summary>
    ///     A TimeSpan extension method that substract the specified TimeSpan to the current UTC (Coordinated Universal
    ///     Time)
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The current UTC (Coordinated Universal Time) with the specified TimeSpan substracted from it.</returns>
    public static DateTime UtcAgo(this TimeSpan @this)
    {
        return DateTime.UtcNow.Subtract(@this);
    }

    /// <summary>
    ///     A TimeSpan extension method that substract the specified TimeSpan to the current DateTime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The current DateTime with the specified TimeSpan substracted from it.</returns>
    public static DateTime Ago(this TimeSpan @this)
    {
        return DateTime.Now.Subtract(@this);
    }

    /// <summary>
    ///     A System.DateTime extension method that ends of week.
    /// </summary>
    /// <param name="dt">Date/Time of the dt.</param>
    /// <param name="startDayOfWeek">(Optional) the start day of week.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
    {
        DateTime end = dt;
        DayOfWeek endDayOfWeek = startDayOfWeek - 1;
        if (endDayOfWeek < 0)
        {
            endDayOfWeek = DayOfWeek.Saturday;
        }

        if (end.DayOfWeek != endDayOfWeek)
        {
            if (endDayOfWeek < end.DayOfWeek)
            {
                end = end.AddDays(7 - (end.DayOfWeek - endDayOfWeek));
            }
            else
            {
                end = end.AddDays(endDayOfWeek - end.DayOfWeek);
            }
        }

        return new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is a week day.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if '@this' is a week day, false if not.</returns>
    public static bool IsWeekDay(this DateTime @this)
    {
        return !(@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
    }

    /// <summary>
    ///     A DateTime extension method that query if 'time' is time equal.
    /// </summary>
    /// <param name="time">The time to act on.</param>
    /// <param name="timeToCompare">Date/Time of the time to compare.</param>
    /// <returns>true if time equal, false if not.</returns>
    public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
    {
        return (time.TimeOfDay == timeToCompare.TimeOfDay);
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is in the future.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if the value is in the future, false if not.</returns>
    public static bool IsFuture(this DateTime @this)
    {
        return @this > DateTime.Now;
    }

    /// <summary>
    ///     A DateTime extension method that return a DateTime with the time set to "23:59:59:999". The last moment of
    ///     the day. Use "DateTime2" column type in sql to keep the precision.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime of the day with the time set to "23:59:59:999".</returns>
    public static DateTime EndOfDay(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is now.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if now, false if not.</returns>
    public static bool IsNow(this DateTime @this)
    {
        return @this == DateTime.Now;
    }

    /// <summary>
    ///     A DateTime extension method that return a DateTime of the first day of the month with the time set to
    ///     "00:00:00:000". The first moment of the first day of the month.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime of the first day of the month with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfMonth(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, 1);
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is morning.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if morning, false if not.</returns>
    public static bool IsMorning(this DateTime @this)
    {
        return @this.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }

    /// <summary>
    ///     A DateTime extension method that first day of week.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime FirstDayOfWeek(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-(int) @this.DayOfWeek);
    }

    /// <summary>
    ///     A DateTime extension method that return a DateTime with the time set to "00:00:00:000". The first moment of
    ///     the day.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime of the day with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfDay(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, @this.Day);
    }

    /// <summary>
    ///     A DateTime extension method that ages the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>An int.</returns>
    public static int Age(this DateTime @this)
    {
        if (DateTime.Today.Month < @this.Month ||
            DateTime.Today.Month == @this.Month &&
            DateTime.Today.Day < @this.Day)
        {
            return DateTime.Today.Year - @this.Year - 1;
        }
        return DateTime.Today.Year - @this.Year;
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is today.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if today, false if not.</returns>
    public static bool IsToday(this DateTime @this)
    {
        return @this.Date == DateTime.Today;
    }

    /// <summary>
    ///     A DateTime extension method that converts the @this to an epoch time span.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a TimeSpan.</returns>
    public static TimeSpan ToEpochTimeSpan(this DateTime @this)
    {
        return @this.Subtract(new DateTime(1970, 1, 1));
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is afternoon.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if afternoon, false if not.</returns>
    public static bool IsAfternoon(this DateTime @this)
    {
        return @this.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }

    /// <summary>
    ///     A DateTime extension method that starts of week.
    /// </summary>
    /// <param name="dt">The dt to act on.</param>
    /// <param name="startDayOfWeek">(Optional) the start day of week.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
    {
        var start = new DateTime(dt.Year, dt.Month, dt.Day);

        if (start.DayOfWeek != startDayOfWeek)
        {
            int d = startDayOfWeek - start.DayOfWeek;
            if (startDayOfWeek <= start.DayOfWeek)
            {
                return start.AddDays(d);
            }
            return start.AddDays(-7 + d);
        }

        return start;
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is in the past.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if the value is in the past, false if not.</returns>
    public static bool IsPast(this DateTime @this)
    {
        return @this < DateTime.Now;
    }

    /// <summary>
    ///     Sets the time of the current date with minute precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime SetTime(this DateTime current, int hour)
    {
        return SetTime(current, hour, 0, 0, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with minute precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute)
    {
        return SetTime(current, hour, minute, 0, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with second precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <param name="second">The second.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
    {
        return SetTime(current, hour, minute, second, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with millisecond precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <param name="second">The second.</param>
    /// <param name="millisecond">The millisecond.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
    {
        return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
    }

    /// <summary>
    ///     A DateTime extension method that elapsed the given datetime.
    /// </summary>
    /// <param name="datetime">The datetime to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Elapsed(this DateTime datetime)
    {
        return DateTime.Now - datetime;
    }

    /// <summary>
    ///     A DateTime extension method that query if 'date' is date equal.
    /// </summary>
    /// <param name="date">The date to act on.</param>
    /// <param name="dateToCompare">Date/Time of the date to compare.</param>
    /// <returns>true if date equal, false if not.</returns>
    public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
    {
        return (date.Date == dateToCompare.Date);
    }

    /// <summary>
    ///     A DateTime extension method that query if '@this' is a weekend day.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if '@this' is a weekend day, false if not.</returns>
    public static bool IsWeekendDay(this DateTime @this)
    {
        return (@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
    }

    /// <summary>
    ///     A DateTime extension method that return a DateTime of the first day of the year with the time set to
    ///     "00:00:00:000". The first moment of the first day of the year.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime of the first day of the year with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfYear(this DateTime @this)
    {
        return new DateTime(@this.Year, 1, 1);
    }

    /// <summary>
    ///     A DateTime extension method that last day of week.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime.</returns>
    public static DateTime LastDayOfWeek(this DateTime @this)
    {
        return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(6 - (int) @this.DayOfWeek);
    }

    /// <summary>
    ///     A DateTime extension method that return a DateTime of the last day of the year with the time set to
    ///     "23:59:59:999". The last moment of the last day of the year.  Use "DateTime2" column type in sql to keep the
    ///     precision.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DateTime of the last day of the year with the time set to "23:59:59:999".</returns>
    public static DateTime EndOfYear(this DateTime @this)
    {
        return new DateTime(@this.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    ///     Converts a time to the time in a particular time zone.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="destinationTimeZone">The time zone to convert  to.</param>
    /// <returns>The date and time in the destination time zone.</returns>
    public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
        return TimeZoneInfo.ConvertTime(dateTime, destinationTimeZone);
    }

    /// <summary>
    ///     Converts a time from one time zone to another.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="sourceTimeZone">The time zone of .</param>
    /// <param name="destinationTimeZone">The time zone to convert  to.</param>
    /// <returns>
    ///     The date and time in the destination time zone that corresponds to the  parameter in the source time zone.
    /// </returns>
    public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
    {
        return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
    }

    /// <summary>
    ///     Converts a time to the time in another time zone based on the time zone&#39;s identifier.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
    /// <returns>The date and time in the destination time zone.</returns>
    public static DateTime ConvertTimeBySystemTimeZoneId(this DateTime dateTime, String destinationTimeZoneId)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, destinationTimeZoneId);
    }

    /// <summary>
    ///     Converts a time from one time zone to another based on time zone identifiers.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="sourceTimeZoneId">The identifier of the source time zone.</param>
    /// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
    /// <returns>
    ///     The date and time in the destination time zone that corresponds to the  parameter in the source time zone.
    /// </returns>
    public static DateTime ConvertTimeBySystemTimeZoneId(this DateTime dateTime, String sourceTimeZoneId, String destinationTimeZoneId)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, sourceTimeZoneId, destinationTimeZoneId);
    }

    /// <summary>
    ///     Converts the current date and time to Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>
    ///     The Coordinated Universal Time (UTC) that corresponds to the  parameter. The  value&#39;s  property is always
    ///     set to .
    /// </returns>
    public static DateTime ConvertTimeToUtc(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeToUtc(dateTime);
    }

    /// <summary>
    ///     Converts the time in a specified time zone to Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="sourceTimeZone">The time zone of .</param>
    /// <returns>
    ///     The Coordinated Universal Time (UTC) that corresponds to the  parameter. The  object&#39;s  property is
    ///     always set to .
    /// </returns>
    public static DateTime ConvertTimeToUtc(this DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);
    }

    /// <summary>
    ///     Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
    /// </summary>
    /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
    /// <param name="destinationTimeZone">The time zone to convert  to.</param>
    /// <returns>
    ///     The date and time in the destination time zone. Its  property is  if  is ; otherwise, its  property is .
    /// </returns>
    public static DateTime ConvertTimeFromUtc(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this DateTime @this, params DateTime[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this DateTime @this, params DateTime[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a month day string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToMonthDayString(this DateTime @this)
    {
        return @this.ToString("m", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a month day string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToMonthDayString(this DateTime @this, string culture)
    {
        return @this.ToString("m", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a month day string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToMonthDayString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("m", culture);
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateTimeString(this DateTime @this)
    {
        return @this.ToString("g", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("g", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("g", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToSortableDateTimeString(this DateTime @this)
    {
        return @this.ToString("s", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToSortableDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("s", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToSortableDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("s", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortTimeString(this DateTime @this)
    {
        return @this.ToString("t", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("t", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("t", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableDateTimeString(this DateTime @this)
    {
        return @this.ToString("u", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("u", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("u", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a full date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToFullDateTimeString(this DateTime @this)
    {
        return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a full date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToFullDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("F", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a full date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToFullDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("F", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a year month string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToYearMonthString(this DateTime @this)
    {
        return @this.ToString("y", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a year month string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToYearMonthString(this DateTime @this, string culture)
    {
        return @this.ToString("y", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a year month string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToYearMonthString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("y", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableLongDateTimeString(this DateTime @this)
    {
        return @this.ToString("U", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableLongDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("U", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to an universal sortable long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToUniversalSortableLongDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("U", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongTimeString(this DateTime @this)
    {
        return @this.ToString("T", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("T", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("T", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateShortTimeString(this DateTime @this)
    {
        return @this.ToString("f", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateShortTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("f", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date short time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateShortTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("f", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateString(this DateTime @this)
    {
        return @this.ToString("D", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateString(this DateTime @this, string culture)
    {
        return @this.ToString("D", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("D", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateString(this DateTime @this)
    {
        return @this.ToString("d", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateString(this DateTime @this, string culture)
    {
        return @this.ToString("d", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("d", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateTimeString(this DateTime @this)
    {
        return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("F", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a long date time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToLongDateTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("F", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a rfc 1123 string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToRFC1123String(this DateTime @this)
    {
        return @this.ToString("r", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a rfc 1123 string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToRFC1123String(this DateTime @this, string culture)
    {
        return @this.ToString("r", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a rfc 1123 string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToRFC1123String(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("r", culture);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateLongTimeString(this DateTime @this)
    {
        return @this.ToString("G", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateLongTimeString(this DateTime @this, string culture)
    {
        return @this.ToString("G", new CultureInfo(culture));
    }

    /// <summary>
    ///     A DateTime extension method that converts this object to a short date long time string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToShortDateLongTimeString(this DateTime @this, CultureInfo culture)
    {
        return @this.ToString("G", culture);
    }

    /// <summary>
    ///     An EventHandler extension method that raises the event event.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="sender">Source of the event.</param>
    public static void RaiseEvent(this System.EventHandler @this, object sender)
    {
        if (@this != null)
        {
            @this(sender, null);
        }
    }

    /// <summary>
    ///     An EventHandler extension method that raises.
    /// </summary>
    /// <param name="handler">The handler to act on.</param>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event information.</param>
    public static void Raise(this System.EventHandler handler, object sender, EventArgs e)
    {
        if (handler != null)
            handler(sender, e);
    }

    /// <summary>
    ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
    /// </summary>
    /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="sender">Source of the event.</param>
    public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender) where TEventArgs : EventArgs
    {
        if (@this != null)
        {
            @this(sender, Activator.CreateInstance<TEventArgs>());
        }
    }

    /// <summary>
    ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
    /// </summary>
    /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event information to send to registered event handlers.</param>
    public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e) where TEventArgs : EventArgs
    {
        if (@this != null)
        {
            @this(sender, e);
        }
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Single @this, Single minValue, Single maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Single @this, Single minValue, Single maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Single @this, params Single[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Single @this, params Single[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to positive infinity.
    /// </summary>
    /// <param name="f">A single-precision floating-point number.</param>
    /// <returns>true if  evaluates to ; otherwise, false.</returns>
    public static Boolean IsPositiveInfinity(this Single f)
    {
        return Single.IsPositiveInfinity(f);
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to negative or positive infinity.
    /// </summary>
    /// <param name="f">A single-precision floating-point number.</param>
    /// <returns>true if  evaluates to  or ; otherwise, false.</returns>
    public static Boolean IsInfinity(this Single f)
    {
        return Single.IsInfinity(f);
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified value is not a number ().
    /// </summary>
    /// <param name="f">A single-precision floating-point number.</param>
    /// <returns>true if  evaluates to not a number (); otherwise, false.</returns>
    public static Boolean IsNaN(this Single f)
    {
        return Single.IsNaN(f);
    }

    /// <summary>
    ///     Returns the absolute value of a single-precision floating-point number.
    /// </summary>
    /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
    /// <returns>A single-precision floating-point number, x, such that 0 ? x ?.</returns>
    public static Single Abs(this Single value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to negative infinity.
    /// </summary>
    /// <param name="f">A single-precision floating-point number.</param>
    /// <returns>true if  evaluates to ; otherwise, false.</returns>
    public static Boolean IsNegativeInfinity(this Single f)
    {
        return Single.IsNegativeInfinity(f);
    }

    /// <summary>
    ///     Sets the time of the current date with minute precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <returns>A DateTimeOffset.</returns>
    public static DateTimeOffset SetTime(this DateTimeOffset current, int hour)
    {
        return SetTime(current, hour, 0, 0, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with minute precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <returns>A DateTimeOffset.</returns>
    public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute)
    {
        return SetTime(current, hour, minute, 0, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with second precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <param name="second">The second.</param>
    /// <returns>A DateTimeOffset.</returns>
    public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute, int second)
    {
        return SetTime(current, hour, minute, second, 0);
    }

    /// <summary>
    ///     Sets the time of the current date with millisecond precision.
    /// </summary>
    /// <param name="current">The current date.</param>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    /// <param name="second">The second.</param>
    /// <param name="millisecond">The millisecond.</param>
    /// <returns>A DateTimeOffset.</returns>
    public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute, int second, int millisecond)
    {
        return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
    }

    /// <summary>
    ///     Returns the larger of two single-precision floating-point numbers.
    /// </summary>
    /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
    /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
    /// <returns>Parameter  or , whichever is larger. If , or , or both  and  are equal to ,  is returned.</returns>
    public static Single Max(this Single val1, Single val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a single-precision floating-point number.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Single value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the smaller of two single-precision floating-point numbers.
    /// </summary>
    /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
    /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller. If , , or both  and  are equal to ,  is returned.</returns>
    public static Single Min(this Single val1, Single val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this DateTimeOffset @this, params DateTimeOffset[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this DateTimeOffset @this, params DateTimeOffset[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     Converts a time to the time in another time zone based on the time zone&#39;s identifier.
    /// </summary>
    /// <param name="dateTimeOffset">The date and time to convert.</param>
    /// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
    /// <returns>The date and time in the destination time zone.</returns>
    public static DateTimeOffset ConvertTimeBySystemTimeZoneId(this DateTimeOffset dateTimeOffset, String destinationTimeZoneId)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, destinationTimeZoneId);
    }

    /// <summary>
    ///     Converts a time to the time in a particular time zone.
    /// </summary>
    /// <param name="dateTimeOffset">The date and time to convert.</param>
    /// <param name="destinationTimeZone">The time zone to convert  to.</param>
    /// <returns>The date and time in the destination time zone.</returns>
    public static DateTimeOffset ConvertTime(this DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
    {
        return TimeZoneInfo.ConvertTime(dateTimeOffset, destinationTimeZone);
    }

    /// <summary>
    ///     A char extension method that repeats a character the specified number of times.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="repeatCount">Number of repeats.</param>
    /// <returns>The repeated char.</returns>
    public static string Repeat(this char @this, int repeatCount)
    {
        return new string(@this, repeatCount);
    }

    /// <summary>
    ///     Enumerates from @this to toCharacter.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="toCharacter">to character.</param>
    /// <returns>An enumerator that allows foreach to be used to process @this to toCharacter.</returns>
    public static IEnumerable<char> To(this char @this, char toCharacter)
    {
        bool reverseRequired = (@this > toCharacter);

        char first = reverseRequired ? toCharacter : @this;
        char last = reverseRequired ? @this : toCharacter;

        IEnumerable<char> result = Enumerable.Range(first, last - first + 1).Select(charCode => (char) charCode);

        if (reverseRequired)
        {
            result = result.Reverse();
        }


        return result;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Char @this, params Char[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Char @this, params Char[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     Indicates whether the two specified  objects form a surrogate pair.
    /// </summary>
    /// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
    /// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
    /// <returns>
    ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF, and the numeric value of the
    ///     parameter ranges from U+DC00 through U+DFFF; otherwise, false.
    /// </returns>
    public static Boolean IsSurrogatePair(this Char highSurrogate, Char lowSurrogate)
    {
        return Char.IsSurrogatePair(highSurrogate, lowSurrogate);
    }

    /// <summary>
    ///     Indicates whether the specified character has a surrogate code unit.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is either a high surrogate or a low surrogate; otherwise, false.</returns>
    public static Boolean IsSurrogate(this Char c)
    {
        return Char.IsSurrogate(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a number.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a number; otherwise, false.</returns>
    public static Boolean IsNumber(this Char c)
    {
        return Char.IsNumber(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a Unicode letter.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a letter; otherwise, false.</returns>
    public static Boolean IsLetter(this Char c)
    {
        return Char.IsLetter(c);
    }

    /// <summary>
    ///     Indicates whether the specified  object is a high surrogate.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>
    ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF; otherwise, false.
    /// </returns>
    public static Boolean IsHighSurrogate(this Char c)
    {
        return Char.IsHighSurrogate(c);
    }

    /// <summary>
    ///     Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant
    ///     culture.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>
    ///     The uppercase equivalent of the  parameter, or the unchanged value of , if  is already uppercase or not
    ///     alphabetic.
    /// </returns>
    public static Char ToUpperInvariant(this Char c)
    {
        return Char.ToUpperInvariant(c);
    }

    /// <summary>
    ///     Converts the specified numeric Unicode character to a double-precision floating point number.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>The numeric value of  if that character represents a number; otherwise, -1.0.</returns>
    public static Double GetNumericValue(this Char c)
    {
        return Char.GetNumericValue(c);
    }

    /// <summary>
    ///     Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-
    ///     specific formatting information.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <param name="culture">An object that supplies culture-specific casing rules.</param>
    /// <returns>
    ///     The uppercase equivalent of , modified according to , or the unchanged value of  if  is already uppercase,
    ///     has no uppercase equivalent, or is not alphabetic.
    /// </returns>
    public static Char ToUpper(this Char c, CultureInfo culture)
    {
        return Char.ToUpper(c, culture);
    }

    /// <summary>
    ///     Converts the value of a Unicode character to its uppercase equivalent.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>
    ///     The uppercase equivalent of , or the unchanged value of  if  is already uppercase, has no uppercase
    ///     equivalent, or is not alphabetic.
    /// </returns>
    public static Char ToUpper(this Char c)
    {
        return Char.ToUpper(c);
    }

    /// <summary>
    ///     Indicates whether the specified  object is a low surrogate.
    /// </summary>
    /// <param name="c">The character to evaluate.</param>
    /// <returns>
    ///     true if the numeric value of the  parameter ranges from U+DC00 through U+DFFF; otherwise, false.
    /// </returns>
    public static Boolean IsLowSurrogate(this Char c)
    {
        return Char.IsLowSurrogate(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a punctuation mark.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a punctuation mark; otherwise, false.</returns>
    public static Boolean IsPunctuation(this Char c)
    {
        return Char.IsPunctuation(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as an uppercase letter.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is an uppercase letter; otherwise, false.</returns>
    public static Boolean IsUpper(this Char c)
    {
        return Char.IsUpper(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a lowercase letter.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a lowercase letter; otherwise, false.</returns>
    public static Boolean IsLower(this Char c)
    {
        return Char.IsLower(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a control character.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a control character; otherwise, false.</returns>
    public static Boolean IsControl(this Char c)
    {
        return Char.IsControl(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as white space.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is white space; otherwise, false.</returns>
    public static Boolean IsWhiteSpace(this Char c)
    {
        return Char.IsWhiteSpace(c);
    }

    /// <summary>
    ///     Categorizes a specified Unicode character into a group identified by one of the  values.
    /// </summary>
    /// <param name="c">The Unicode character to categorize.</param>
    /// <returns>A  value that identifies the group that contains .</returns>
    public static UnicodeCategory GetUnicodeCategory(this Char c)
    {
        return Char.GetUnicodeCategory(c);
    }

    /// <summary>
    ///     Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant
    ///     culture.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>
    ///     The lowercase equivalent of the  parameter, or the unchanged value of , if  is already lowercase or not
    ///     alphabetic.
    /// </returns>
    public static Char ToLowerInvariant(this Char c)
    {
        return Char.ToLowerInvariant(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a separator character.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a separator character; otherwise, false.</returns>
    public static Boolean IsSeparator(this Char c)
    {
        return Char.IsSeparator(c);
    }

    /// <summary>
    ///     Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.
    /// </summary>
    /// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
    /// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
    /// <returns>The 21-bit Unicode code point represented by the  and  parameters.</returns>
    public static Int32 ConvertToUtf32(this Char highSurrogate, Char lowSurrogate)
    {
        return Char.ConvertToUtf32(highSurrogate, lowSurrogate);
    }

    /// <summary>
    ///     Converts the specified Unicode character to its equivalent string representation.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>The string representation of the value of .</returns>
    public static String ToString(this Char c)
    {
        return Char.ToString(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a symbol character.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a symbol character; otherwise, false.</returns>
    public static Boolean IsSymbol(this Char c)
    {
        return Char.IsSymbol(c);
    }

    /// <summary>
    ///     Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-
    ///     specific formatting information.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <param name="culture">An object that supplies culture-specific casing rules.</param>
    /// <returns>
    ///     The lowercase equivalent of , modified according to , or the unchanged value of , if  is already lowercase or
    ///     not alphabetic.
    /// </returns>
    public static Char ToLower(this Char c, CultureInfo culture)
    {
        return Char.ToLower(c, culture);
    }

    /// <summary>
    ///     Converts the value of a Unicode character to its lowercase equivalent.
    /// </summary>
    /// <param name="c">The Unicode character to convert.</param>
    /// <returns>
    ///     The lowercase equivalent of , or the unchanged value of , if  is already lowercase or not alphabetic.
    /// </returns>
    public static Char ToLower(this Char c)
    {
        return Char.ToLower(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a decimal digit.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a decimal digit; otherwise, false.</returns>
    public static Boolean IsDigit(this Char c)
    {
        return Char.IsDigit(c);
    }

    /// <summary>
    ///     Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.
    /// </summary>
    /// <param name="c">The Unicode character to evaluate.</param>
    /// <returns>true if  is a letter or a decimal digit; otherwise, false.</returns>
    public static Boolean IsLetterOrDigit(this Char c)
    {
        return Char.IsLetterOrDigit(c);
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Double @this, Double minValue, Double maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A Double extension method that converts the @this to a money.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a Double.</returns>
    public static Double ToMoney(this Double @this)
    {
        return Math.Round(@this, 2);
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Double @this, Double minValue, Double maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Double @this, params Double[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Double @this, params Double[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     Returns a  that represents a specified number of days, where the specification is accurate to the nearest
    ///     millisecond.
    /// </summary>
    /// <param name="value">A number of days, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromDays(this Double value)
    {
        return TimeSpan.FromDays(value);
    }

    /// <summary>
    ///     Returns a  that represents a specified number of hours, where the specification is accurate to the nearest
    ///     millisecond.
    /// </summary>
    /// <param name="value">A number of hours accurate to the nearest millisecond.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromHours(this Double value)
    {
        return TimeSpan.FromHours(value);
    }

    /// <summary>
    ///     Returns a  that represents a specified number of seconds, where the specification is accurate to the nearest
    ///     millisecond.
    /// </summary>
    /// <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromSeconds(this Double value)
    {
        return TimeSpan.FromSeconds(value);
    }

    /// <summary>
    ///     Returns a  that represents a specified number of minutes, where the specification is accurate to the nearest
    ///     millisecond.
    /// </summary>
    /// <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromMinutes(this Double value)
    {
        return TimeSpan.FromMinutes(value);
    }

    /// <summary>
    ///     Returns a  that represents a specified number of milliseconds.
    /// </summary>
    /// <param name="value">A number of milliseconds.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromMilliseconds(this Double value)
    {
        return TimeSpan.FromMilliseconds(value);
    }

    /// <summary>
    ///     Returns a  equivalent to the specified OLE Automation Date.
    /// </summary>
    /// <param name="d">An OLE Automation Date value.</param>
    /// <returns>An object that represents the same date and time as .</returns>
    public static DateTime FromOADate(this Double d)
    {
        return DateTime.FromOADate(d);
    }

    /// <summary>
    ///     Returns the cosine of the specified angle.
    /// </summary>
    /// <param name="d">An angle, measured in radians.</param>
    /// <returns>The cosine of . If  is equal to , , or , this method returns .</returns>
    public static Double Cos(this Double d)
    {
        return Math.Cos(d);
    }

    /// <summary>
    ///     Returns the tangent of the specified angle.
    /// </summary>
    /// <param name="a">An angle, measured in radians.</param>
    /// <returns>The tangent of . If  is equal to , , or , this method returns .</returns>
    public static Double Tan(this Double a)
    {
        return Math.Tan(a);
    }

    /// <summary>
    ///     Returns the hyperbolic tangent of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>
    ///     The hyperbolic tangent of . If  is equal to , this method returns -1. If value is equal to , this method
    ///     returns 1. If  is equal to , this method returns .
    /// </returns>
    public static Double Tanh(this Double value)
    {
        return Math.Tanh(value);
    }

    /// <summary>
    ///     Returns the absolute value of a double-precision floating-point number.
    /// </summary>
    /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
    /// <returns>A double-precision floating-point number, x, such that 0 ? x ?.</returns>
    public static Double Abs(this Double value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <param name="d">The number whose logarithm is to be found.</param>
    /// <returns>
    ///     One of the values in the following table.  parameterReturn value Positive The natural logarithm of ; that is,
    ///     ln , or log eZero Negative Equal to Equal to.
    /// </returns>
    public static Double Log(this Double d)
    {
        return Math.Log(d);
    }

    /// <summary>
    ///     Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="d">The number whose logarithm is to be found.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>
    ///     One of the values in the following table. (+Infinity denotes , -Infinity denotes , and NaN denotes .)Return
    ///     value&gt; 0(0 &lt;&lt; 1) -or-(&gt; 1)lognewBase(a)&lt; 0(any value)NaN(any value)&lt; 0NaN != 1 = 0NaN != 1
    ///     = +InfinityNaN = NaN(any value)NaN(any value) = NaNNaN(any value) = 1NaN = 00 &lt;&lt; 1 +Infinity = 0&gt; 1-
    ///     Infinity =  +Infinity0 &lt;&lt; 1-Infinity =  +Infinity&gt; 1+Infinity = 1 = 00 = 1 = +Infinity0.
    /// </returns>
    /// ###
    /// <param name="a">The number whose logarithm is to be found.</param>
    public static Double Log(this Double d, Double newBase)
    {
        return Math.Log(d, newBase);
    }

    /// <summary>
    ///     Returns the hyperbolic cosine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The hyperbolic cosine of . If  is equal to  or ,  is returned. If  is equal to ,  is returned.</returns>
    public static Double Cosh(this Double value)
    {
        return Math.Cosh(value);
    }

    /// <summary>
    ///     Returns the sine of the specified angle.
    /// </summary>
    /// <param name="a">An angle, measured in radians.</param>
    /// <returns>The sine of . If  is equal to , , or , this method returns .</returns>
    public static Double Sin(this Double a)
    {
        return Math.Sin(a);
    }

    /// <summary>
    ///     Returns the smaller of two double-precision floating-point numbers.
    /// </summary>
    /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
    /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller. If , , or both  and  are equal to ,  is returned.</returns>
    public static Double Min(this Double val1, Double val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns e raised to the specified power.
    /// </summary>
    /// <param name="d">A number specifying a power.</param>
    /// <returns>
    ///     The number e raised to the power . If  equals  or , that value is returned. If  equals , 0 is returned.
    /// </returns>
    public static Double Exp(this Double d)
    {
        return Math.Exp(d);
    }

    /// <summary>
    ///     Rounds a double-precision floating-point value to the nearest integral value.
    /// </summary>
    /// <param name="a">A double-precision floating-point number to be rounded.</param>
    /// <returns>
    ///     The integer nearest . If the fractional component of  is halfway between two integers, one of which is even
    ///     and the other odd, then the even number is returned. Note that this method returns a  instead of an integral
    ///     type.
    /// </returns>
    public static Double Round(this Double a)
    {
        return Math.Round(a);
    }

    /// <summary>
    ///     Rounds a double-precision floating-point value to a specified number of fractional digits.
    /// </summary>
    /// <param name="a">A double-precision floating-point number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to  that contains a number of fractional digits equal to .</returns>
    /// ###
    /// <param name="value">A double-precision floating-point number to be rounded.</param>
    public static Double Round(this Double a, Int32 digits)
    {
        return Math.Round(a, digits);
    }

    /// <summary>
    ///     Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the
    ///     value if it is midway between two numbers.
    /// </summary>
    /// <param name="a">A double-precision floating-point number to be rounded.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The integer nearest . If  is halfway between two integers, one of which is even and the other odd, then
    ///     determines which of the two is returned.
    /// </returns>
    /// ###
    /// <param name="value">A double-precision floating-point number to be rounded.</param>
    public static Double Round(this Double a, MidpointRounding mode)
    {
        return Math.Round(a, mode);
    }

    /// <summary>
    ///     Rounds a double-precision floating-point value to a specified number of fractional digits. A parameter
    ///     specifies how to round the value if it is midway between two numbers.
    /// </summary>
    /// <param name="a">A double-precision floating-point number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The number nearest to  that has a number of fractional digits equal to . If  has fewer fractional digits than
    ///     ,  is returned unchanged.
    /// </returns>
    /// ###
    /// <param name="value">A double-precision floating-point number to be rounded.</param>
    public static Double Round(this Double a, Int32 digits, MidpointRounding mode)
    {
        return Math.Round(a, digits, mode);
    }

    /// <summary>
    ///     Returns the angle whose cosine is the specified number.
    /// </summary>
    /// <param name="d">
    ///     A number representing a cosine, where  must be greater than or equal to -1, but less than or
    ///     equal to 1.
    /// </param>
    /// <returns>An angle, ?, measured in radians, such that 0 ????-or-  if  &lt; -1 or  &gt; 1 or  equals .</returns>
    public static Double Acos(this Double d)
    {
        return Math.Acos(d);
    }

    /// <summary>
    ///     Returns a specified number raised to the specified power.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="y">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number  raised to the power .</returns>
    public static Double Pow(this Double x, Double y)
    {
        return Math.Pow(x, y);
    }

    /// <summary>
    ///     Returns the larger of two double-precision floating-point numbers.
    /// </summary>
    /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
    /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
    /// <returns>Parameter  or , whichever is larger. If , , or both  and  are equal to ,  is returned.</returns>
    public static Double Max(this Double val1, Double val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the hyperbolic sine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The hyperbolic sine of . If  is equal to , , or , this method returns a  equal to .</returns>
    public static Double Sinh(this Double value)
    {
        return Math.Sinh(value);
    }

    /// <summary>
    ///     Returns the remainder resulting from the division of a specified number by another specified number.
    /// </summary>
    /// <param name="x">A dividend.</param>
    /// <param name="y">A divisor.</param>
    /// <returns>
    ///     A number equal to  - ( Q), where Q is the quotient of  /  rounded to the nearest integer (if  /  falls
    ///     halfway between two integers, the even integer is returned).If  - ( Q) is zero, the value +0 is returned if
    ///     is positive, or -0 if  is negative.If  = 0,  is returned.
    /// </returns>
    public static Double IEEERemainder(this Double x, Double y)
    {
        return Math.IEEERemainder(x, y);
    }

    /// <summary>
    ///     Returns the angle whose sine is the specified number.
    /// </summary>
    /// <param name="d">
    ///     A number representing a sine, where  must be greater than or equal to -1, but less than or equal
    ///     to 1.
    /// </param>
    /// <returns>
    ///     An angle, ?, measured in radians, such that -?/2 ????/2 -or-  if  &lt; -1 or  &gt; 1 or  equals .
    /// </returns>
    public static Double Asin(this Double d)
    {
        return Math.Asin(d);
    }

    /// <summary>
    ///     Returns the angle whose tangent is the quotient of two specified numbers.
    /// </summary>
    /// <param name="y">The y coordinate of a point.</param>
    /// <param name="x">The x coordinate of a point.</param>
    /// <returns>
    ///     An angle, ?, measured in radians, such that -?????, and tan(?) =  / , where (, ) is a point in the Cartesian
    ///     plane. Observe the following: For (, ) in quadrant 1, 0 &lt; ? &lt; ?/2.For (, ) in quadrant 2, ?/2 &lt;
    ///     ???.For (, ) in quadrant 3, -? &lt; ? &lt; -?/2.For (, ) in quadrant 4, -?/2 &lt; ? &lt; 0.For points on the
    ///     boundaries of the quadrants, the return value is the following:If y is 0 and x is not negative, ? = 0.If y is
    ///     0 and x is negative, ? = ?.If y is positive and x is 0, ? = ?/2.If y is negative and x is 0, ? = -?/2.If  or
    ///     is , or if  and  are either  or , the method returns .
    /// </returns>
    public static Double Atan2(this Double y, Double x)
    {
        return Math.Atan2(y, x);
    }

    /// <summary>
    ///     Returns the square root of a specified number.
    /// </summary>
    /// <param name="d">The number whose square root is to be found.</param>
    /// <returns>
    ///     One of the values in the following table.  parameter Return value Zero or positive The positive square root
    ///     of . Negative Equals Equals.
    /// </returns>
    public static Double Sqrt(this Double d)
    {
        return Math.Sqrt(d);
    }

    /// <summary>
    ///     Calculates the integral part of a specified double-precision floating-point number.
    /// </summary>
    /// <param name="d">A number to truncate.</param>
    /// <returns>
    ///     The integral part of ; that is, the number that remains after any fractional digits have been discarded, or
    ///     one of the values listed in the following table. Return value.
    /// </returns>
    public static Double Truncate(this Double d)
    {
        return Math.Truncate(d);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a double-precision floating-point number.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Double value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the smallest integral value that is greater than or equal to the specified double-precision floating-
    ///     point number.
    /// </summary>
    /// <param name="a">A double-precision floating-point number.</param>
    /// <returns>
    ///     The smallest integral value that is greater than or equal to . If  is equal to , , or , that value is
    ///     returned. Note that this method returns a  instead of an integral type.
    /// </returns>
    public static Double Ceiling(this Double a)
    {
        return Math.Ceiling(a);
    }

    /// <summary>
    ///     Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <param name="d">A number whose logarithm is to be found.</param>
    /// <returns>
    ///     One of the values in the following table.  parameter Return value Positive The base 10 log of ; that is, log
    ///     10. Zero Negative Equal to Equal to.
    /// </returns>
    public static Double Log10(this Double d)
    {
        return Math.Log10(d);
    }

    /// <summary>
    ///     Returns the largest integer less than or equal to the specified double-precision floating-point number.
    /// </summary>
    /// <param name="d">A double-precision floating-point number.</param>
    /// <returns>The largest integer less than or equal to . If  is equal to , , or , that value is returned.</returns>
    public static Double Floor(this Double d)
    {
        return Math.Floor(d);
    }

    /// <summary>
    ///     Returns the angle whose tangent is the specified number.
    /// </summary>
    /// <param name="d">A number representing a tangent.</param>
    /// <returns>
    ///     An angle, ?, measured in radians, such that -?/2 ????/2.-or-  if  equals , -?/2 rounded to double precision (-
    ///     1.5707963267949) if  equals , or ?/2 rounded to double precision (1.5707963267949) if  equals .
    /// </returns>
    public static Double Atan(this Double d)
    {
        return Math.Atan(d);
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to negative infinity.
    /// </summary>
    /// <param name="d">A double-precision floating-point number.</param>
    /// <returns>true if  evaluates to ; otherwise, false.</returns>
    public static Boolean IsNegativeInfinity(this Double d)
    {
        return Double.IsNegativeInfinity(d);
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified value is not a number ().
    /// </summary>
    /// <param name="d">A double-precision floating-point number.</param>
    /// <returns>true if  evaluates to ; otherwise, false.</returns>
    public static Boolean IsNaN(this Double d)
    {
        return Double.IsNaN(d);
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to negative or positive infinity.
    /// </summary>
    /// <param name="d">A double-precision floating-point number.</param>
    /// <returns>true if  evaluates to  or ; otherwise, false.</returns>
    public static Boolean IsInfinity(this Double d)
    {
        return Double.IsInfinity(d);
    }

    /// <summary>
    ///     Returns a value indicating whether the specified number evaluates to positive infinity.
    /// </summary>
    /// <param name="d">A double-precision floating-point number.</param>
    /// <returns>true if  evaluates to ; otherwise, false.</returns>
    public static Boolean IsPositiveInfinity(this Double d)
    {
        return Double.IsPositiveInfinity(d);
    }

    /// <summary>
    ///     A T[] extension method that converts the @this to a data table.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DataTable.</returns>
    public static DataTable ToDataTable<T>(this T[] @this)
    {
        Type type = typeof (T);

        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        var dt = new DataTable();

        foreach (PropertyInfo property in properties)
        {
            dt.Columns.Add(property.Name, property.PropertyType);
        }

        foreach (FieldInfo field in fields)
        {
            dt.Columns.Add(field.Name, field.FieldType);
        }

        foreach (T item in @this)
        {
            DataRow dr = dt.NewRow();

            foreach (PropertyInfo property in properties)
            {
                dr[property.Name] = property.GetValue(item, null);
            }

            foreach (FieldInfo field in fields)
            {
                dr[field.Name] = field.GetValue(item);
            }

            dt.Rows.Add(dr);
        }

        return dt;
    }

    /// <summary>
    ///     A T[] extension method that clears at.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The arrayToClear to act on.</param>
    /// <param name="at">at.</param>
    public static void ClearAt<T>(this T[] @this, int at)
    {
        Array.Clear(@this, at, 1);
    }

    /// <summary>
    ///     A T[] extension method that clears all described by @this.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// ###
    /// <returns>.</returns>
    public static void ClearAll<T>(this T[] @this)
    {
        Array.Clear(@this, 0, @this.Length);
    }

    /// <summary>
    ///     A T[] extension method that true for all.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static Boolean TrueForAll<T>(this T[] array, Predicate<T> match)
    {
        return Array.TrueForAll(array, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the last index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindLastIndex<T>(this T[] array, Predicate<T> match)
    {
        return Array.FindLastIndex(array, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the last index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
    {
        return Array.FindLastIndex(array, startIndex, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the last index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="count">Number of.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
    {
        return Array.FindLastIndex(array, startIndex, count, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first all.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found all.</returns>
    public static T[] FindAll<T>(this T[] array, Predicate<T> match)
    {
        return Array.FindAll(array, match);
    }

    /// <summary>
    ///     A T[] extension method that exists.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static Boolean Exists<T>(this T[] array, Predicate<T> match)
    {
        return Array.Exists(array, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindIndex<T>(this T[] array, Predicate<T> match)
    {
        return Array.FindIndex(array, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
    {
        return Array.FindIndex(array, startIndex, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first index.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="count">Number of.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found index.</returns>
    public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
    {
        return Array.FindIndex(array, startIndex, count, match);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first last.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>The found last.</returns>
    public static T FindLast<T>(this T[] array, Predicate<T> match)
    {
        return Array.FindLast(array, match);
    }

    /// <summary>
    ///     A T[] extension method that converts an array to a read only.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <returns>A list of.</returns>
    public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
    {
        return Array.AsReadOnly(array);
    }

    /// <summary>
    ///     A T[] extension method that applies an operation to all items in this collection.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="action">The action.</param>
    public static void ForEach<T>(this T[] array, Action<T> action)
    {
        Array.ForEach(array, action);
    }

    /// <summary>
    ///     A T[] extension method that searches for the first match.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="array">The array to act on.</param>
    /// <param name="match">Specifies the match.</param>
    /// <returns>A T.</returns>
    public static T Find<T>(this T[] array, Predicate<T> match)
    {
        return Array.Find(array, match);
    }

    /// <summary>
    ///     An object extension method that gets description attribute.
    /// </summary>
    /// <param name="value">The value to act on.</param>
    /// <returns>The description attribute.</returns>
    public static string GetCustomAttributeDescription(this Enum value)
    {
        var attr = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        return attr.Description;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    public static bool In(this Enum @this, params Enum[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    public static bool NotIn(this Enum @this, params Enum[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    /// Returns an array containing all values of <typeparamref name="T"/>.
    /// </summary>
    public static T[] GetValues<T>() where T : struct, IComparable, IConvertible, IFormattable
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    /// <summary>
    ///     Converts the specified Windows file time to an equivalent local time.
    /// </summary>
    /// <param name="fileTime">A Windows file time expressed in ticks.</param>
    /// <returns>
    ///     An object that represents the local time equivalent of the date and time represented by the  parameter.
    /// </returns>
    public static DateTime FromFileTime(this Int64 fileTime)
    {
        return DateTime.FromFileTime(fileTime);
    }

    /// <summary>
    ///     Converts the specified Windows file time to an equivalent UTC time.
    /// </summary>
    /// <param name="fileTime">A Windows file time expressed in ticks.</param>
    /// <returns>
    ///     An object that represents the UTC time equivalent of the date and time represented by the  parameter.
    /// </returns>
    public static DateTime FromFileTimeUtc(this Int64 fileTime)
    {
        return DateTime.FromFileTimeUtc(fileTime);
    }

    /// <summary>
    ///     Deserializes a 64-bit binary value and recreates an original serialized  object.
    /// </summary>
    /// <param name="dateData">
    ///     A 64-bit signed integer that encodes the  property in a 2-bit field and the  property in
    ///     a 62-bit field.
    /// </param>
    /// <returns>An object that is equivalent to the  object that was serialized by the  method.</returns>
    public static DateTime FromBinary(this Int64 dateData)
    {
        return DateTime.FromBinary(dateData);
    }

    /// <summary>
    ///     Converts a long value from host byte order to network byte order.
    /// </summary>
    /// <param name="host">The number to convert, expressed in host byte order.</param>
    /// <returns>A long value, expressed in network byte order.</returns>
    public static Int64 HostToNetworkOrder(this Int64 host)
    {
        return IPAddress.HostToNetworkOrder(host);
    }

    /// <summary>
    ///     Converts a long value from network byte order to host byte order.
    /// </summary>
    /// <param name="network">The number to convert, expressed in network byte order.</param>
    /// <returns>A long value, expressed in host byte order.</returns>
    public static Int64 NetworkToHostOrder(this Int64 network)
    {
        return IPAddress.NetworkToHostOrder(network);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Int64 @this, params Int64[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Int64 @this, Int64 minValue, Int64 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Int64 @this, params Int64[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Int64 @this, Int64 minValue, Int64 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     Converts the specified 64-bit signed integer, which contains an OLE Automation Currency value, to the
    ///     equivalent  value.
    /// </summary>
    /// <param name="cy">An OLE Automation Currency value.</param>
    /// <returns>A  that contains the equivalent of .</returns>
    public static Decimal FromOACurrency(this Int64 cy)
    {
        return Decimal.FromOACurrency(cy);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a 64-bit signed integer.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Int64 value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the absolute value of a 64-bit signed integer.
    /// </summary>
    /// <param name="value">A number that is greater than , but less than or equal to .</param>
    /// <returns>A 64-bit signed integer, x, such that 0 ? x ?.</returns>
    public static Int64 Abs(this Int64 value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns the smaller of two 64-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static Int64 Min(this Int64 val1, Int64 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     An Int64 extension method that div rem.
    /// </summary>
    /// <param name="a">a to act on.</param>
    /// <param name="b">The Int64 to process.</param>
    /// <param name="result">[out] The result.</param>
    /// <returns>An Int64.</returns>
    public static Int64 DivRem(this Int64 a, Int64 b, out Int64 result)
    {
        return Math.DivRem(a, b, out result);
    }

    /// <summary>
    ///     Returns the larger of two 64-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static Int64 Max(this Int64 val1, Int64 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns the specified 64-bit signed integer value as an array of bytes.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>An array of bytes with length 8.</returns>
    public static Byte[] GetBytes(this Int64 value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    ///     Converts the specified 64-bit signed integer to a double-precision floating point number.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>A double-precision floating point number whose value is equivalent to .</returns>
    public static Double Int64BitsToDouble(this Int64 value)
    {
        return BitConverter.Int64BitsToDouble(value);
    }

    /// <summary>
    ///     Returns a  that represents a specified time, where the specification is in units of ticks.
    /// </summary>
    /// <param name="value">A number of ticks that represent a time.</param>
    /// <returns>An object that represents .</returns>
    public static TimeSpan FromTicks(this Int64 value)
    {
        return TimeSpan.FromTicks(value);
    }

    /// <summary>
    ///     An Int64 extension method that query if '@this' is odd.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if odd, false if not.</returns>
    public static bool IsOdd(this Int64 @this)
    {
        return @this%2 != 0;
    }

    /// <summary>
    ///     An Int64 extension method that weeks the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Weeks(this Int64 @this)
    {
        return TimeSpan.FromDays(@this*7);
    }

    /// <summary>
    ///     An Int64 extension method that query if '@this' is prime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if prime, false if not.</returns>
    public static bool IsPrime(this Int64 @this)
    {
        if (@this == 1 || @this == 2)
        {
            return true;
        }

        if (@this%2 == 0)
        {
            return false;
        }

        var sqrt = (Int64) Math.Sqrt(@this);
        for (Int64 t = 3; t <= sqrt; t = t + 2)
        {
            if (@this%t == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     An Int64 extension method that hours the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Hours(this Int64 @this)
    {
        return TimeSpan.FromHours(@this);
    }

    /// <summary>
    ///     An Int64 extension method that days the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Days(this Int64 @this)
    {
        return TimeSpan.FromDays(@this);
    }

    /// <summary>
    ///     An Int64 extension method that query if '@this' is even.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if even, false if not.</returns>
    public static bool IsEven(this Int64 @this)
    {
        return @this%2 == 0;
    }

    /// <summary>
    ///     An Int64 extension method that seconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Seconds(this Int64 @this)
    {
        return TimeSpan.FromSeconds(@this);
    }

    /// <summary>
    ///     An Int64 extension method that minutes the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Minutes(this Int64 @this)
    {
        return TimeSpan.FromMinutes(@this);
    }

    /// <summary>
    ///     An Int64 extension method that factor of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factorNumer">The factor numer.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool FactorOf(this Int64 @this, Int64 factorNumer)
    {
        return factorNumer%@this == 0;
    }

    /// <summary>
    ///     An Int64 extension method that milliseconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Milliseconds(this Int64 @this)
    {
        return TimeSpan.FromMilliseconds(@this);
    }

    /// <summary>
    ///     A string extension method that right safe.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string RightSafe(this string @this, int length)
    {
        return @this.Substring(Math.Max(0, @this.Length - length));
    }

    /// <summary>
    ///     A string extension method that get the string after the specified string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value to search.</param>
    /// <returns>The string after the specified value.</returns>
    public static string GetAfter(this string @this, string value)
    {
        if (@this.IndexOf(value) == -1)
        {
            return "";
        }
        return @this.Substring(@this.IndexOf(value) + value.Length);
    }

    /// <summary>
    ///     A string extension method that decrypt a string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <returns>The decrypted string.</returns>
    public static string DecryptRSA(this string @this, string key)
    {
        var cspp = new CspParameters {KeyContainerName = key};
        var rsa = new RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};
        string[] decryptArray = @this.Split(new[] {"-"}, StringSplitOptions.None);
        byte[] decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));
        byte[] bytes = rsa.Decrypt(decryptByteArray, true);

        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>A string extension method that strip HTML.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string StripHtml(this string @this)
    {
        var path = new StringBuilder(@this);
        var sb = new StringBuilder();

        int pos = 0;

        while (pos < path.Length)
        {
            char ch = path[pos];
            pos++;

            if (ch == '<')
            {
                // LOOP until we close the html tag
                while (pos < path.Length)
                {
                    ch = path[pos];
                    pos++;

                    if (ch == '>')
                    {
                        break;
                    }

                    if (ch == '\'')
                    {
                        // SKIP attribute starting with single quote
                        pos = GetIndexAfterNextSingleQuote(path, pos, true);
                    }
                    else if (ch == '"')
                    {
                        // SKIP attribute starting with double quote
                        pos = GetIndexAfterNextDoubleQuote(path, pos, true);
                    }
                }
            }
            else
            {
                sb.Append(ch);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    ///     A string extension method that converts the @this to a byte array.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a byte[].</returns>
    public static byte[] ToByteArray(this string @this)
    {
        Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
        return encoding.GetBytes(@this);
    }

    /// <summary>
    ///     A string extension method that replace first occurence.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the first occurence of old value replace by new value.</returns>
    public static string ReplaceFirst(this string @this, string oldValue, string newValue)
    {
        int startindex = @this.IndexOf(oldValue);

        if (startindex == -1)
        {
            return @this;
        }

        return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
    }

    /// <summary>
    ///     A string extension method that replace first number of occurences.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="number">Number of.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the numbers of occurences of old value replace by new value.</returns>
    public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        int old = number + 1;
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(newValue, listStart) +
               (listEnd.Any() ? oldValue : "") +
               string.Join(oldValue, listEnd);
    }

    /// <summary>
    ///     A string extension method that newline 2 line break.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string Nl2Br(this string @this)
    {
        return @this.Replace("\r\n", "<br />").Replace("\n", "<br />");
    }

    /// <summary>
    ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
    ///     Object instance in a specified array.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="arg0">The argument 0.</param>
    /// <returns>
    ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
    ///     instances of Object in args.
    /// </returns>
    public static String FormatWith(this String @this, Object arg0)
    {
        return String.Format(@this, arg0);
    }

    /// <summary>
    ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
    ///     Object instance in a specified array.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="arg0">The argument 0.</param>
    /// <param name="arg1">The first argument.</param>
    /// <returns>
    ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
    ///     instances of Object in args.
    /// </returns>
    public static String FormatWith(this String @this, Object arg0, Object arg1)
    {
        return String.Format(@this, arg0, arg1);
    }

    /// <summary>
    ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
    ///     Object instance in a specified array.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="arg0">The argument 0.</param>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <returns>
    ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
    ///     instances of Object in args.
    /// </returns>
    public static String FormatWith(this String @this, Object arg0, Object arg1, Object arg2)
    {
        return String.Format(@this, arg0, arg1, arg2);
    }

    /// <summary>
    ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
    ///     Object instance in a specified array.
    /// </summary>
    /// <param name="this">A String containing zero or more format items.</param>
    /// <param name="values">An Object array containing zero or more objects to format.</param>
    /// <returns>
    ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
    ///     instances of Object in args.
    /// </returns>
    public static string FormatWith(this string @this, params object[] values)
    {
        return String.Format(@this, values);
    }

    /// <summary>
    ///     A string extension method that get the string before the specified string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value to search.</param>
    /// <returns>The string before the specified value.</returns>
    public static string GetBefore(this string @this, string value)
    {
        if (@this.IndexOf(value) == -1)
        {
            return "";
        }
        return @this.Substring(0, @this.IndexOf(value));
    }

    public static SqlDbType SqlTypeNameToSqlDbType(this string @this)
    {
        switch (@this.ToLower())
        {
            case "image": // 34 | "image" | SqlDbType.Image
                return SqlDbType.Image;

            case "text": // 35 | "text" | SqlDbType.Text
                return SqlDbType.Text;

            case "uniqueidentifier": // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                return SqlDbType.UniqueIdentifier;

            case "date": // 40 | "date" | SqlDbType.Date
                return SqlDbType.Date;

            case "time": // 41 | "time" | SqlDbType.Time
                return SqlDbType.Time;

            case "datetime2": // 42 | "datetime2" | SqlDbType.DateTime2
                return SqlDbType.DateTime2;

            case "datetimeoffset": // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                return SqlDbType.DateTimeOffset;

            case "tinyint": // 48 | "tinyint" | SqlDbType.TinyInt
                return SqlDbType.TinyInt;

            case "smallint": // 52 | "smallint" | SqlDbType.SmallInt
                return SqlDbType.SmallInt;

            case "int": // 56 | "int" | SqlDbType.Int
                return SqlDbType.Int;

            case "smalldatetime": // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                return SqlDbType.SmallDateTime;

            case "real": // 59 | "real" | SqlDbType.Real
                return SqlDbType.Real;

            case "money": // 60 | "money" | SqlDbType.Money
                return SqlDbType.Money;

            case "datetime": // 61 | "datetime" | SqlDbType.DateTime
                return SqlDbType.DateTime;

            case "float": // 62 | "float" | SqlDbType.Float
                return SqlDbType.Float;

            case "sql_variant": // 98 | "sql_variant" | SqlDbType.Variant
                return SqlDbType.Variant;

            case "ntext": // 99 | "ntext" | SqlDbType.NText
                return SqlDbType.NText;

            case "bit": // 104 | "bit" | SqlDbType.Bit
                return SqlDbType.Bit;

            case "decimal": // 106 | "decimal" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case "numeric": // 108 | "numeric" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case "smallmoney": // 122 | "smallmoney" | SqlDbType.SmallMoney
                return SqlDbType.SmallMoney;

            case "bigint": // 127 | "bigint" | SqlDbType.BigInt
                return SqlDbType.BigInt;

            case "varbinary": // 165 | "varbinary" | SqlDbType.VarBinary
                return SqlDbType.VarBinary;

            case "varchar": // 167 | "varchar" | SqlDbType.VarChar
                return SqlDbType.VarChar;

            case "binary": // 173 | "binary" | SqlDbType.Binary
                return SqlDbType.Binary;

            case "char": // 175 | "char" | SqlDbType.Char
                return SqlDbType.Char;

            case "timestamp": // 189 | "timestamp" | SqlDbType.Timestamp
                return SqlDbType.Timestamp;

            case "nvarchar": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                return SqlDbType.NVarChar;
            case "sysname": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                return SqlDbType.NVarChar;

            case "nchar": // 239 | "nchar" | SqlDbType.NChar
                return SqlDbType.NChar;

            case "hierarchyid": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;
            case "geometry": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;
            case "geography": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;

            case "xml": // 241 | "xml" | SqlDbType.Xml
                return SqlDbType.Xml;

            default:
                throw new Exception(string.Format("Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com", @this));
        }
    }

    /// <summary>
    ///     A string extension method that query if '@this' is anagram of other String.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="otherString">The other string</param>
    /// <returns>true if the @this is anagram of the otherString, false if not.</returns>
    public static bool IsAnagram(this string @this, string otherString)
    {
        return @this
            .OrderBy(c => c)
            .SequenceEqual(otherString.OrderBy(c => c));
    }

    /// <summary>A string extension method that replaces.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="length">The length.</param>
    /// <param name="value">The value.</param>
    /// <returns>A string.</returns>
    public static string Replace(this string @this, int startIndex, int length, string value)
    {
        @this = @this.Remove(startIndex, length).Insert(startIndex, value);

        return @this;
    }

    /// <summary>
    ///     A string extension method that queries if '@this' is not (null or empty).
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if '@this' is not (null or empty), false if not.</returns>
    public static bool IsNotNullOrEmpty(this string @this)
    {
        return !string.IsNullOrEmpty(@this);
    }

    /// <summary>
    ///     A string extension method that removes the diacritics character from the strings.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The string without diacritics character.</returns>
    public static string RemoveDiacritics(this string @this)
    {
        string normalizedString = @this.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (char t in normalizedString)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    ///     A string extension method that replace all values specified by an empty string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>A string with all specified values replaced by an empty string.</returns>
    public static string ReplaceByEmpty(this string @this, params string[] values)
    {
        foreach (string value in values)
        {
            @this = @this.Replace(value, "");
        }

        return @this;
    }

    /// <summary>
    ///     A string extension method that get the string between the two specified string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="before">The string before to search.</param>
    /// <param name="after">The string after to search.</param>
    /// <returns>The string between the two specified string.</returns>
    public static string GetBetween(this string @this, string before, string after)
    {
        int beforeStartIndex = @this.IndexOf(before);
        int startIndex = beforeStartIndex + before.Length;
        int afterStartIndex = @this.IndexOf(after, startIndex);

        if (beforeStartIndex == -1 || afterStartIndex == -1)
        {
            return "";
        }

        return @this.Substring(startIndex, afterStartIndex - startIndex);
    }

    /// <summary>
    ///     A string extension method that converts the @this to an enum.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a T.</returns>
    public static T ToEnum<T>(this string @this)
    {
        Type enumType = typeof (T);
        return (T) Enum.Parse(enumType, @this);
    }

    /// <summary>
    ///     A string extension method that converts the @this to a XDocument.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an XDocument.</returns>
    public static XDocument ToXDocument(this string @this)
    {
        Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
        using (var ms = new MemoryStream(encoding.GetBytes(@this)))
        {
            return XDocument.Load(ms);
        }
    }

    /// <summary>
    ///     A string extension method that converts the @this to a title case.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a string.</returns>
    public static string ToTitleCase(this string @this)
    {
        return new CultureInfo("en-US").TextInfo.ToTitleCase(@this);
    }

    /// <summary>
    ///     A string extension method that converts the @this to a title case.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cultureInfo">Information describing the culture.</param>
    /// <returns>@this as a string.</returns>
    public static string ToTitleCase(this string @this, CultureInfo cultureInfo)
    {
        return cultureInfo.TextInfo.ToTitleCase(@this);
    }

    /// <summary>
    ///     A string extension method that replace when equals.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The new value if the string equal old value; Otherwise old value.</returns>
    public static string ReplaceWhenEquals(this string @this, string oldValue, string newValue)
    {
        return @this == oldValue ? newValue : @this;
    }

    /// <summary>
    ///     A string extension method that extracts the number described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted number.</returns>
    public static string ExtractNumber(this string @this)
    {
        return new string(@this.ToCharArray().Where(x => Char.IsNumber(x)).ToArray());
    }

    /// <summary>
    ///     A string extension method that removes the letter.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>A string.</returns>
    public static string RemoveWhere(this string @this, Func<char, bool> predicate)
    {
        return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
    }

    /// <summary>
    ///     A string extension method that extracts this object.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>A string.</returns>
    public static string Extract(this string @this, Func<char, bool> predicate)
    {
        return new string(@this.ToCharArray().Where(predicate).ToArray());
    }

    /// <summary>
    ///     A string extension method that queries if a not is empty.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if a not is empty, false if not.</returns>
    public static bool IsNotEmpty(this string @this)
    {
        return @this != "";
    }

    /// <summary>
    ///     A string extension method that truncates.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <returns>A string.</returns>
    public static string Truncate(this string @this, int maxLength)
    {
        const string suffix = "...";

        if (@this == null || @this.Length <= maxLength)
        {
            return @this;
        }

        int strLength = maxLength - suffix.Length;
        return @this.Substring(0, strLength) + suffix;
    }

    /// <summary>
    ///     A string extension method that truncates.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix.</param>
    /// <returns>A string.</returns>
    public static string Truncate(this string @this, int maxLength, string suffix)
    {
        if (@this == null || @this.Length <= maxLength)
        {
            return @this;
        }

        int strLength = maxLength - suffix.Length;
        return @this.Substring(0, strLength) + suffix;
    }

    /// <summary>
    ///     A string extension method that query if '@this' is Alphanumeric.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if Alphanumeric, false if not.</returns>
    public static bool IsAlphaNumeric(this string @this)
    {
        return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
    }

    /// <summary>
    ///     A string extension method that query if '@this' is empty.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if empty, false if not.</returns>
    public static bool IsEmpty(this string @this)
    {
        return @this == "";
    }

    /// <summary>
    ///     A string extension method that removes the letter described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string RemoveLetter(this string @this)
    {
        return new string(@this.ToCharArray().Where(x => !Char.IsLetter(x)).ToArray());
    }

    /// <summary>
    ///     A string extension method that concatenate with.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>A string.</returns>
    public static string ConcatWith(this string @this, params string[] values)
    {
        return string.Concat(@this, string.Concat(values));
    }

    /// <summary>
    ///     A string extension method that converts the @this to a directory information.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DirectoryInfo.</returns>
    public static DirectoryInfo ToDirectoryInfo(this string @this)
    {
        return new DirectoryInfo(@this);
    }

    /// <summary>
    ///     A string extension method that converts the @this to a MemoryStream.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a MemoryStream.</returns>
    public static Stream ToMemoryStream(this string @this)
    {
        Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
        return new MemoryStream(encoding.GetBytes(@this));
    }

    /// <summary>
    ///     Returns a String array containing the substrings in this string that are delimited by elements of a specified
    ///     String array. A parameter specifies whether to return empty array elements.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">A string that delimit the substrings in this string.</param>
    /// <param name="option">
    ///     (Optional) Specify RemoveEmptyEntries to omit empty array elements from the array returned,
    ///     or None to include empty array elements in the array returned.
    /// </param>
    /// <returns>
    ///     An array whose elements contain the substrings in this string that are delimited by the separator.
    /// </returns>
    public static string[] Split(this string @this, string separator, StringSplitOptions option = StringSplitOptions.None)
    {
        return @this.Split(new[] {separator}, option);
    }

    /// <summary>
    ///     A string extension method that line break 2 newline.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string Br2Nl(this string @this)
    {
        return @this.Replace("<br />", "\r\n").Replace("<br>", "\r\n");
    }

    /// <summary>
    ///     A string extension method that return the left part of the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>The left part.</returns>
    public static string Left(this string @this, int length)
    {
        return @this.Substring(0, length);
    }

    /// <summary>
    ///     A string extension method that query if '@this' contains any values.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it contains any values, otherwise false.</returns>
    public static bool ContainsAny(this string @this, params string[] values)
    {
        foreach (string value in values)
        {
            if (@this.IndexOf(value) != -1)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    ///     A string extension method that query if '@this' contains any values.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it contains any values, otherwise false.</returns>
    public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
    {
        foreach (string value in values)
        {
            if (@this.IndexOf(value, comparisonType) != -1)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// A string extension method that converts the @this to a valid date time or null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DateTime?</returns>
    public static DateTime? ToValidDateTimeOrNull(this string @this)
    {
        DateTime date;

        if (DateTime.TryParse(@this, out date))
        {
            return date;
        }

        return null;
    }

    /// <summary>
    ///     A string extension method that if empty.
    /// </summary>
    /// <param name="value">The value to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>A string.</returns>
    public static string IfEmpty(this string value, string defaultValue)
    {
        return (value.IsNotEmpty() ? value : defaultValue);
    }

    /// <summary>
    ///     A String extension method that converts the @this to a secure string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a SecureString.</returns>
    public static SecureString ToSecureString(this string @this)
    {
        var secureString = new SecureString();
        foreach (Char c in @this)
            secureString.AppendChar(c);

        return secureString;
    }

    /// <summary>
    ///     Indicates whether a specified string is not null, not empty, or not consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
    public static Boolean IsNotNullOrWhiteSpace(this string value)
    {
        return !String.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     A string extension method that removes the number described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string RemoveNumber(this string @this)
    {
        return new string(@this.ToCharArray().Where(x => !Char.IsNumber(x)).ToArray());
    }

    /// <summary>
    ///     A string extension method that converts the @this to an XmlDocument.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an XmlDocument.</returns>
    public static XmlDocument ToXmlDocument(this string @this)
    {
        var doc = new XmlDocument();
        doc.LoadXml(@this);
        return doc;
    }

    /// <summary>
    ///     A string extension method that save the string into a file.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="fileName">Filename of the file.</param>
    /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
    public static void SaveAs(this string @this, string fileName, bool append = false)
    {
        using (TextWriter tw = new StreamWriter(fileName, append))
        {
            tw.Write(@this);
        }
    }

    /// <summary>
    ///     A string extension method that save the string into a file.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="file">The FileInfo.</param>
    /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
    public static void SaveAs(this string @this, FileInfo file, bool append = false)
    {
        using (TextWriter tw = new StreamWriter(file.FullName, append))
        {
            tw.Write(@this);
        }
    }

    /// <summary>
    ///     A string extension method that converts the @this to a file information.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a FileInfo.</returns>
    public static FileInfo ToFileInfo(this string @this)
    {
        return new FileInfo(@this);
    }

    /// <summary>
    ///     A string extension method that query if '@this' satisfy the specified pattern.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="pattern">The pattern to use. Use '*' as wildcard string.</param>
    /// <returns>true if '@this' satisfy the specified pattern, false if not.</returns>
    public static bool IsLike(this string @this, string pattern)
    {
        // Turn the pattern into regex pattern, and match the whole string with ^$
        string regexPattern = "^" + Regex.Escape(pattern) + "$";

        // Escape special character ?, #, *, [], and [!]
        regexPattern = regexPattern.Replace(@"\[!", "[^")
            .Replace(@"\[", "[")
            .Replace(@"\]", "]")
            .Replace(@"\?", ".")
            .Replace(@"\*", ".*")
            .Replace(@"\#", @"\d");

        return Regex.IsMatch(@this, regexPattern);
    }

    /// <summary>An IEnumerable&lt;string&gt; extension method that concatenates the given this.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string Concatenate(this IEnumerable<string> @this)
    {
        var sb = new StringBuilder();

        foreach (var s in @this)
        {
            sb.Append(s);
        }

        return sb.ToString();
    }

    /// <summary>An IEnumerable&lt;T&gt; extension method that concatenates.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="source">The source to act on.</param>
    /// <param name="func">The function.</param>
    /// <returns>A string.</returns>
    public static string Concatenate<T>(this IEnumerable<T> source, Func<T, string> func)
    {
        var sb = new StringBuilder();
        foreach (var item in source)
        {
            sb.Append(func(item));
        }

        return sb.ToString();
    }

    /// <summary>
    ///     A string extension method that return null if the value is empty else the value.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>null if the value is empty, otherwise the value.</returns>
    public static string NullIfEmpty(this string @this)
    {
        return @this == "" ? null : @this;
    }

    /// <summary>
    ///     A string extension method that query if '@this' contains all values.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it contains all values, otherwise false.</returns>
    public static bool ContainsAll(this string @this, params string[] values)
    {
        foreach (string value in values)
        {
            if (@this.IndexOf(value) == -1)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    ///     A string extension method that query if this object contains the given @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it contains all values, otherwise false.</returns>
    public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
    {
        foreach (string value in values)
        {
            if (@this.IndexOf(value, comparisonType) == -1)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    ///     A string extension method that query if this object contains the given value.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if the value is in the string, false if not.</returns>
    public static bool Contains(this string @this, string value)
    {
        return @this.IndexOf(value) != -1;
    }

    /// <summary>
    ///     A string extension method that query if this object contains the given value.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    /// <returns>true if the value is in the string, false if not.</returns>
    public static bool Contains(this string @this, string value, StringComparison comparisonType)
    {
        return @this.IndexOf(value, comparisonType) != -1;
    }

    /// <summary>
    ///     A string extension method that left safe.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string LeftSafe(this string @this, int length)
    {
        return @this.Substring(0, Math.Min(length, @this.Length));
    }

    /// <summary>
    ///     A string extension method that decode a Base64 String.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The Base64 String decoded.</returns>
    public static string DecodeBase64(this string @this)
    {
        return Encoding.ASCII.GetString(Convert.FromBase64String(@this));
    }

    /// <summary>
    ///     A string extension method that encrypts the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <returns>The encrypted string.</returns>
    public static string EncryptRSA(this string @this, string key)
    {
        var cspp = new CspParameters {KeyContainerName = key};
        var rsa = new RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};
        byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(@this), true);

        return BitConverter.ToString(bytes);
    }

    /// <summary>
    ///     A string extension method that extracts the letter described by @this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted letter.</returns>
    public static string ExtractLetter(this string @this)
    {
        return new string(@this.ToCharArray().Where(x => Char.IsLetter(x)).ToArray());
    }

    /// <summary>
    /// A string extension method that checks if '@this' is equal to another string regardless of it's case.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="comparedString">The compared string.</param>
    /// <returns>
    /// true if it contains all values, otherwise false.
    /// </returns>
    public static bool EqualsIgnoreCase(this string @this, string comparedString)
    {
        return @this.Equals(comparedString, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Combines multiples string into a path.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="paths">A variable-length parameters list containing paths.</param>
    /// <returns>
    ///     The combined paths. If one of the specified paths is a zero-length string, this method returns the other path.
    /// </returns>
    public static string PathCombine(this string @this, params string[] paths)
    {
        List<string> list = paths.ToList();
        list.Insert(0, @this);
        return Path.Combine(list.ToArray());
    }

    /// <summary>
    ///     A string extension method that replace last occurence.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the last occurence of old value replace by new value.</returns>
    public static string ReplaceLast(this string @this, string oldValue, string newValue)
    {
        int startindex = @this.LastIndexOf(oldValue);

        if (startindex == -1)
        {
            return @this;
        }

        return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
    }

    /// <summary>
    ///     A string extension method that replace last numbers occurences.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="number">Number of.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the last numbers occurences of old value replace by new value.</returns>
    public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        int old = Math.Max(0, list.Count - number - 1);
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(oldValue, listStart) +
               (old > 0 ? oldValue : "") +
               string.Join(newValue, listEnd);
    }

    /// <summary>
    ///     A string extension method that query if '@this' is Alpha.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if Alpha, false if not.</returns>
    public static bool IsAlpha(this string @this)
    {
        return !Regex.IsMatch(@this, "[^a-zA-Z]");
    }

    /// <summary>
    ///     A string extension method that return the right part of the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="length">The length.</param>
    /// <returns>The right part.</returns>
    public static string Right(this string @this, int length)
    {
        return @this.Substring(@this.Length - length);
    }

    /// <summary>
    ///     A string extension method that encode the string to Base64.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The encoded string to Base64.</returns>
    public static string EncodeBase64(this string @this)
    {
        return Convert.ToBase64String(Activator.CreateInstance<ASCIIEncoding>().GetBytes(@this));
    }

    /// <summary>
    ///     A string extension method that query if '@this' is numeric.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if numeric, false if not.</returns>
    public static bool IsNumeric(this string @this)
    {
        return !Regex.IsMatch(@this, "[^0-9]");
    }

    /// <summary>
    ///     A string extension method that repeats the string a specified number of times.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="repeatCount">Number of repeats.</param>
    /// <returns>The repeated string.</returns>
    public static string Repeat(this string @this, int repeatCount)
    {
        if (@this.Length == 1)
        {
            return new string(@this[0], repeatCount);
        }

        var sb = new StringBuilder(repeatCount*@this.Length);
        while (repeatCount-- > 0)
        {
            sb.Append(@this);
        }

        return sb.ToString();
    }

    /// <summary>
    ///     A string extension method that escape XML.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string.</returns>
    public static string EscapeXml(this string @this)
    {
        return @this.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
    }

    /// <summary>
    ///     A string extension method that query if 'obj' is valid IP.
    /// </summary>
    /// <param name="obj">The obj to act on.</param>
    /// <returns>true if valid ip, false if not.</returns>
    public static bool IsValidIP(this string obj)
    {
        return Regex.IsMatch(obj, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
    }

    /// <summary>
    ///     Searches the specified input string for the first occurrence of the specified regular expression.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <returns>An object that contains information about the match.</returns>
    public static Match Match(this String input, String pattern)
    {
        return Regex.Match(input, pattern);
    }

    /// <summary>
    ///     Searches the input string for the first occurrence of the specified regular expression, using the specified
    ///     matching options.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
    /// <returns>An object that contains information about the match.</returns>
    public static Match Match(this String input, String pattern, RegexOptions options)
    {
        return Regex.Match(input, pattern, options);
    }

    /// <summary>
    ///     A string extension method that query if 'obj' is valid email.
    /// </summary>
    /// <param name="obj">The obj to act on.</param>
    /// <returns>true if valid email, false if not.</returns>
    public static bool IsValidEmail(this string obj)
    {
        return Regex.IsMatch(obj, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z0-9]{1,30})(\]?)$");
    }

    /// <summary>
    ///     Indicates whether the specified regular expression finds a match in the specified input string.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
    public static Boolean IsMatch(this String input, String pattern)
    {
        return Regex.IsMatch(input, pattern);
    }

    /// <summary>
    ///     Indicates whether the specified regular expression finds a match in the specified input string, using the
    ///     specified matching options.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
    /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
    public static Boolean IsMatch(this String input, String pattern, RegexOptions options)
    {
        return Regex.IsMatch(input, pattern, options);
    }

    /// <summary>
    ///     Searches the specified input string for all occurrences of a specified regular expression.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <returns>
    ///     A collection of the  objects found by the search. If no matches are found, the method returns an empty
    ///     collection object.
    /// </returns>
    public static MatchCollection Matches(this String input, String pattern)
    {
        return Regex.Matches(input, pattern);
    }

    /// <summary>
    ///     Searches the specified input string for all occurrences of a specified regular expression, using the
    ///     specified matching options.
    /// </summary>
    /// <param name="input">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
    /// <returns>
    ///     A collection of the  objects found by the search. If no matches are found, the method returns an empty
    ///     collection object.
    /// </returns>
    public static MatchCollection Matches(this String input, String pattern, RegexOptions options)
    {
        return Regex.Matches(input, pattern, options);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a
    ///     punctuation mark.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a punctuation mark; otherwise, false.</returns>
    public static Boolean IsPunctuation(this String s, Int32 index)
    {
        return Char.IsPunctuation(s, index);
    }

    /// <summary>
    ///     Indicates whether the  object at the specified position in a string is a high surrogate.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>
    ///     true if the numeric value of the specified character in the  parameter ranges from U+D800 through U+DBFF;
    ///     otherwise, false.
    /// </returns>
    public static Boolean IsHighSurrogate(this String s, Int32 index)
    {
        return Char.IsHighSurrogate(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a separator
    ///     character.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a separator character; otherwise, false.</returns>
    public static Boolean IsSeparator(this String s, Int32 index)
    {
        return Char.IsSeparator(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string has a surrogate code unit.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>
    ///     true if the character at position  in  is a either a high surrogate or a low surrogate; otherwise, false.
    /// </returns>
    public static Boolean IsSurrogate(this String s, Int32 index)
    {
        return Char.IsSurrogate(s, index);
    }

    /// <summary>
    ///     Indicates whether two adjacent  objects at a specified position in a string form a surrogate pair.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The starting position of the pair of characters to evaluate within .</param>
    /// <returns>
    ///     true if the  parameter includes adjacent characters at positions  and  + 1, and the numeric value of the
    ///     character at position  ranges from U+D800 through U+DBFF, and the numeric value of the character at position
    ///     +1 ranges from U+DC00 through U+DFFF; otherwise, false.
    /// </returns>
    public static Boolean IsSurrogatePair(this String s, Int32 index)
    {
        return Char.IsSurrogatePair(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a number.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a number; otherwise, false.</returns>
    public static Boolean IsNumber(this String s, Int32 index)
    {
        return Char.IsNumber(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a letter or
    ///     a decimal digit.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a letter or a decimal digit; otherwise, false.</returns>
    public static Boolean IsLetterOrDigit(this String s, Int32 index)
    {
        return Char.IsLetterOrDigit(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as an
    ///     uppercase letter.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is an uppercase letter; otherwise, false.</returns>
    public static Boolean IsUpper(this String s, Int32 index)
    {
        return Char.IsUpper(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a lowercase
    ///     letter.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a lowercase letter; otherwise, false.</returns>
    public static Boolean IsLower(this String s, Int32 index)
    {
        return Char.IsLower(s, index);
    }

    /// <summary>
    ///     Converts the numeric Unicode character at the specified position in a specified string to a double-precision
    ///     floating point number.
    /// </summary>
    /// <param name="s">A .</param>
    /// <param name="index">The character position in .</param>
    /// <returns>
    ///     The numeric value of the character at position  in  if that character represents a number; otherwise, -1.
    /// </returns>
    public static Double GetNumericValue(this String s, Int32 index)
    {
        return Char.GetNumericValue(s, index);
    }

    /// <summary>
    ///     Indicates whether the  object at the specified position in a string is a low surrogate.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>
    ///     true if the numeric value of the specified character in the  parameter ranges from U+DC00 through U+DFFF;
    ///     otherwise, false.
    /// </returns>
    public static Boolean IsLowSurrogate(this String s, Int32 index)
    {
        return Char.IsLowSurrogate(s, index);
    }

    /// <summary>
    ///     Categorizes the character at the specified position in a specified string into a group identified by one of
    ///     the  values.
    /// </summary>
    /// <param name="s">A .</param>
    /// <param name="index">The character position in .</param>
    /// <returns>A  enumerated constant that identifies the group that contains the character at position  in .</returns>
    public static UnicodeCategory GetUnicodeCategory(this String s, Int32 index)
    {
        return Char.GetUnicodeCategory(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a symbol
    ///     character.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a symbol character; otherwise, false.</returns>
    public static Boolean IsSymbol(this String s, Int32 index)
    {
        return Char.IsSymbol(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as white space.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is white space; otherwise, false.</returns>
    public static Boolean IsWhiteSpace(this String s, Int32 index)
    {
        return Char.IsWhiteSpace(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a decimal
    ///     digit.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a decimal digit; otherwise, false.</returns>
    public static Boolean IsDigit(this String s, Int32 index)
    {
        return Char.IsDigit(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a Unicode
    ///     letter.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a letter; otherwise, false.</returns>
    public static Boolean IsLetter(this String s, Int32 index)
    {
        return Char.IsLetter(s, index);
    }

    /// <summary>
    ///     Indicates whether the character at the specified position in a specified string is categorized as a control
    ///     character.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="index">The position of the character to evaluate in .</param>
    /// <returns>true if the character at position  in  is a control character; otherwise, false.</returns>
    public static Boolean IsControl(this String s, Int32 index)
    {
        return Char.IsControl(s, index);
    }

    /// <summary>
    ///     Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a
    ///     Unicode code point.
    /// </summary>
    /// <param name="s">A string that contains a character or surrogate pair.</param>
    /// <param name="index">The index position of the character or surrogate pair in .</param>
    /// <returns>
    ///     The 21-bit Unicode code point represented by the character or surrogate pair at the position in the parameter
    ///     specified by the  parameter.
    /// </returns>
    public static Int32 ConvertToUtf32(this String s, Int32 index)
    {
        return Char.ConvertToUtf32(s, index);
    }

    /// <summary>
    ///     A T extension method that query if '@this' is null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if null, false if not.</returns>
    public static bool IsNull(this String @this)
    {
        return @this == null;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    public static bool NotIn(this String @this, params String[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg0">The object to format.</param>
    /// <returns>A copy of  in which any format items are replaced by the string representation of .</returns>
    public static String Format(this String format, Object arg0)
    {
        return String.Format(format, arg0);
    }

    /// <summary>
    ///     Replaces the format items in a specified string with the string representation of two specified objects.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg0">The first object to format.</param>
    /// <param name="arg1">The second object to format.</param>
    /// <returns>A copy of  in which format items are replaced by the string representations of  and .</returns>
    public static String Format(this String format, Object arg0, Object arg1)
    {
        return String.Format(format, arg0, arg1);
    }

    /// <summary>
    ///     Replaces the format items in a specified string with the string representation of three specified objects.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg0">The first object to format.</param>
    /// <param name="arg1">The second object to format.</param>
    /// <param name="arg2">The third object to format.</param>
    /// <returns>
    ///     A copy of  in which the format items have been replaced by the string representations of , , and .
    /// </returns>
    public static String Format(this String format, Object arg0, Object arg1, Object arg2)
    {
        return String.Format(format, arg0, arg1, arg2);
    }

    /// <summary>
    ///     Replaces the format item in a specified string with the string representation of a corresponding object in a
    ///     specified array.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    /// <returns>
    ///     A copy of  in which the format items have been replaced by the string representation of the corresponding
    ///     objects in .
    /// </returns>
    public static String Format(this String format, Object[] args)
    {
        return String.Format(format, args);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    public static bool In(this String @this, params String[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that query if '@this' is not null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if not null, false if not.</returns>
    public static bool IsNotNull(this String @this)
    {
        return @this != null;
    }

    /// <summary>
    ///     Retrieves the system&#39;s reference to the specified .
    /// </summary>
    /// <param name="str">A string to search for in the intern pool.</param>
    /// <returns>
    ///     The system&#39;s reference to , if it is interned; otherwise, a new reference to a string with the value of .
    /// </returns>
    public static String Intern(this String str)
    {
        return String.Intern(str);
    }

    /// <summary>
    ///     Concatenates all the elements of a string array, using the specified separator between each element.
    /// </summary>
    /// <param name="separator">
    ///     The string to use as a separator.  is included in the returned string only if  has more
    ///     than one element.
    /// </param>
    /// <param name="value">An array that contains the elements to concatenate.</param>
    /// <returns>
    ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
    ///     returns .
    /// </returns>
    public static String Join(this String separator, String[] value)
    {
        return String.Join(separator, value);
    }

    /// <summary>
    ///     Concatenates the elements of an object array, using the specified separator between each element.
    /// </summary>
    /// <param name="separator">
    ///     The string to use as a separator.  is included in the returned string only if  has more
    ///     than one element.
    /// </param>
    /// <param name="values">An array that contains the elements to concatenate.</param>
    /// <returns>
    ///     A string that consists of the elements of  delimited by the  string. If  is an empty array, the method
    ///     returns .
    /// </returns>
    public static String Join(this String separator, Object[] values)
    {
        return String.Join(separator, values);
    }

    /// <summary>
    ///     A String extension method that joins.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="separator">
    ///     The string to use as a separator.  is included in the returned string only if  has more
    ///     than one element.
    /// </param>
    /// <param name="values">An array that contains the elements to concatenate.</param>
    /// <returns>A String.</returns>
    public static String Join<T>(this String separator, IEnumerable<T> values)
    {
        return String.Join(separator, values);
    }

    /// <summary>
    ///     Concatenates all the elements of a string array, using the specified separator between each element.
    /// </summary>
    /// <param name="separator">
    ///     The string to use as a separator.  is included in the returned string only if  has more
    ///     than one element.
    /// </param>
    /// <param name="values">An array that contains the elements to concatenate.</param>
    /// <returns>
    ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
    ///     returns .
    /// </returns>
    public static String Join(this String separator, IEnumerable<String> values)
    {
        return String.Join(separator, values);
    }

    /// <summary>
    ///     Concatenates the specified elements of a string array, using the specified separator between each element.
    /// </summary>
    /// <param name="separator">
    ///     The string to use as a separator.  is included in the returned string only if  has more
    ///     than one element.
    /// </param>
    /// <param name="value">An array that contains the elements to concatenate.</param>
    /// <param name="startIndex">The first element in  to use.</param>
    /// <param name="count">The number of elements of  to use.</param>
    /// <returns>
    ///     A string that consists of the strings in  delimited by the  string. -or- if  is zero,  has no elements, or
    ///     and all the elements of  are .
    /// </returns>
    public static String Join(this String separator, String[] value, Int32 startIndex, Int32 count)
    {
        return String.Join(separator, value, startIndex, count);
    }

    /// <summary>
    ///     Concatenates two specified instances of .
    /// </summary>
    /// <param name="str0">The first string to concatenate.</param>
    /// <param name="str1">The second string to concatenate.</param>
    /// <returns>The concatenation of  and .</returns>
    public static String Concat(this String str0, String str1)
    {
        return String.Concat(str0, str1);
    }

    /// <summary>
    ///     Concatenates three specified instances of .
    /// </summary>
    /// <param name="str0">The first string to concatenate.</param>
    /// <param name="str1">The second string to concatenate.</param>
    /// <param name="str2">The third string to concatenate.</param>
    /// <returns>The concatenation of , , and .</returns>
    public static String Concat(this String str0, String str1, String str2)
    {
        return String.Concat(str0, str1, str2);
    }

    /// <summary>
    ///     Concatenates four specified instances of .
    /// </summary>
    /// <param name="str0">The first string to concatenate.</param>
    /// <param name="str1">The second string to concatenate.</param>
    /// <param name="str2">The third string to concatenate.</param>
    /// <param name="str3">The fourth string to concatenate.</param>
    /// <returns>The concatenation of , , , and .</returns>
    public static String Concat(this String str0, String str1, String str2, String str3)
    {
        return String.Concat(str0, str1, str2, str3);
    }

    /// <summary>
    ///     Retrieves a reference to a specified .
    /// </summary>
    /// <param name="str">The string to search for in the intern pool.</param>
    /// <returns>A reference to  if it is in the common language runtime intern pool; otherwise, null.</returns>
    public static String IsInterned(this String str)
    {
        return String.IsInterned(str);
    }

    /// <summary>
    ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
    public static Boolean IsNullOrWhiteSpace(this String value)
    {
        return String.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Compares two specified  objects by evaluating the numeric values of the corresponding  objects in each string.
    /// </summary>
    /// <param name="strA">The first string to compare.</param>
    /// <param name="strB">The second string to compare.</param>
    /// <returns>
    ///     An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero
    ///     is less than . Zero  and  are equal. Greater than zero  is greater than .
    /// </returns>
    public static Int32 CompareOrdinal(this String strA, String strB)
    {
        return String.CompareOrdinal(strA, strB);
    }

    /// <summary>
    ///     Compares substrings of two specified  objects by evaluating the numeric values of the corresponding  objects
    ///     in each substring.
    /// </summary>
    /// <param name="strA">The first string to use in the comparison.</param>
    /// <param name="indexA">The starting index of the substring in .</param>
    /// <param name="strB">The second string to use in the comparison.</param>
    /// <param name="indexB">The starting index of the substring in .</param>
    /// <param name="length">The maximum number of characters in the substrings to compare.</param>
    /// <returns>
    ///     A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition
    ///     Less than zero The substring in  is less than the substring in . Zero The substrings are equal, or  is zero.
    ///     Greater than zero The substring in  is greater than the substring in .
    /// </returns>
    public static Int32 CompareOrdinal(this String strA, Int32 indexA, String strB, Int32 indexB, Int32 length)
    {
        return String.CompareOrdinal(strA, indexA, strB, indexB, length);
    }

    /// <summary>
    ///     Creates a new instance of  with the same value as a specified .
    /// </summary>
    /// <param name="str">The string to copy.</param>
    /// <returns>A new string with the same value as .</returns>
    public static String Copy(this String str)
    {
        return String.Copy(str);
    }

    /// <summary>
    ///     A string extension method that extracts the UInt32 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted UInt32.</returns>
    public static uint ExtractUInt32(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                sb.Append(@this[i]);
            }
        }

        return Convert.ToUInt32(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts the UInt16 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted UInt16.</returns>
    public static ushort ExtractUInt16(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                sb.Append(@this[i]);
            }
        }

        return Convert.ToUInt16(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts all UInt16 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted UInt16.</returns>
    public static ushort[] ExtractManyUInt16(this string @this)
    {
        return Regex.Matches(@this, @"\d+")
            .Cast<Match>()
            .Select(x => Convert.ToUInt16(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts the Int64 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted Int64.</returns>
    public static long ExtractInt64(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                {
                    sb.Append('-');
                }
                sb.Append(@this[i]);
            }
        }

        return Convert.ToInt64(sb.ToString());
    }

    /// <summary>
    ///     An object extension method that converts the @this to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A T.</returns>
    public static T AsOrDefault<T>(this object @this)
    {
        try
        {
            return (T) @this;
        }
        catch (Exception)
        {
            return default(T);
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>A T.</returns>
    public static T AsOrDefault<T>(this object @this, T defaultValue)
    {
        try
        {
            return (T) @this;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>A T.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_AsOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void AsOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = 1;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_AsOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void AsOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = 1;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_AsOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void AsOrDefault()
    ///                   {
    ///                       // Type
    ///                       object intValue = 1;
    ///                       object invalidValue = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(1, result1);
    ///                       Assert.AreEqual(0, result2);
    ///                       Assert.AreEqual(3, result3);
    ///                       Assert.AreEqual(4, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T AsOrDefault<T>(this object @this, Func<T> defaultValueFactory)
    {
        try
        {
            return (T) @this;
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>A T.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_AsOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void AsOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = 1;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_AsOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void AsOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = 1;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_AsOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void AsOrDefault()
    ///                   {
    ///                       // Type
    ///                       object intValue = 1;
    ///                       object invalidValue = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       var result1 = intValue.AsOrDefault&lt;int&gt;(); // return 1;
    ///                       var result2 = invalidValue.AsOrDefault&lt;int&gt;(); // return 0;
    ///                       int result3 = invalidValue.AsOrDefault(3); // return 3;
    ///                       int result4 = invalidValue.AsOrDefault(() =&gt; 4); // return 4;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(1, result1);
    ///                       Assert.AreEqual(0, result2);
    ///                       Assert.AreEqual(3, result3);
    ///                       Assert.AreEqual(4, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T AsOrDefault<T>(this object @this, Func<object, T> defaultValueFactory)
    {
        try
        {
            return (T) @this;
        }
        catch (Exception)
        {
            return defaultValueFactory(@this);
        }
    }

    /// <summary>
    ///     A string extension method that extracts the Int16 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted Int16.</returns>
    public static short ExtractInt16(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                {
                    sb.Append('-');
                }
                sb.Append(@this[i]);
            }
        }

        return Convert.ToInt16(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts all Int16 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted Int16.</returns>
    public static short[] ExtractManyInt16(this string @this)
    {
        return Regex.Matches(@this, @"[-]?\d+")
            .Cast<Match>()
            .Select(x => Convert.ToInt16(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts all Decimal from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted Decimal.</returns>
    public static decimal[] ExtractManyDecimal(this string @this)
    {
        return Regex.Matches(@this, @"[-]?\d+(\.\d+)?")
            .Cast<Match>()
            .Select(x => Convert.ToDecimal(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts the UInt64 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted UInt64.</returns>
    public static ulong ExtractUInt64(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                sb.Append(@this[i]);
            }
        }

        return Convert.ToUInt64(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts the Int32 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted Int32.</returns>
    public static int ExtractInt32(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]))
            {
                if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                {
                    sb.Append('-');
                }
                sb.Append(@this[i]);
            }
        }

        return Convert.ToInt32(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts all Int64 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted Int64.</returns>
    public static long[] ExtractManyInt64(this string @this)
    {
        return Regex.Matches(@this, @"[-]?\d+")
            .Cast<Match>()
            .Select(x => Convert.ToInt64(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts all UInt32 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted UInt32.</returns>
    public static uint[] ExtractManyUInt32(this string @this)
    {
        return Regex.Matches(@this, @"\d+")
            .Cast<Match>()
            .Select(x => Convert.ToUInt32(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts all Int32 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted Int32.</returns>
    public static int[] ExtractManyInt32(this string @this)
    {
        return Regex.Matches(@this, @"[-]?\d+")
            .Cast<Match>()
            .Select(x => Convert.ToInt32(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts all UInt64 from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted UInt64.</returns>
    public static ulong[] ExtractManyUInt64(this string @this)
    {
        return Regex.Matches(@this, @"\d+")
            .Cast<Match>()
            .Select(x => Convert.ToUInt64(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts all Double from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>All extracted Double.</returns>
    public static double[] ExtractManyDouble(this string @this)
    {
        return Regex.Matches(@this, @"[-]?\d+(\.\d+)?")
            .Cast<Match>()
            .Select(x => Convert.ToDouble(x.Value))
            .ToArray();
    }

    /// <summary>
    ///     A string extension method that extracts the Double from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted Double.</returns>
    public static double ExtractDouble(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]) || @this[i] == '.')
            {
                if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                {
                    sb.Append('-');
                }
                sb.Append(@this[i]);
            }
        }

        return Convert.ToDouble(sb.ToString());
    }

    /// <summary>
    ///     A string extension method that extracts the Decimal from the string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The extracted Decimal.</returns>
    public static decimal ExtractDecimal(this string @this)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            if (Char.IsDigit(@this[i]) || @this[i] == '.')
            {
                if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                {
                    sb.Append('-');
                }
                sb.Append(@this[i]);
            }
        }

        return Convert.ToDecimal(sb.ToString());
    }

    /// <summary>
    ///     A T extension method that chains actions.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="action">The action.</param>
    /// <returns>The @this acted on.</returns>
    public static T Chain<T>(this T @this, Action<T> action)
    {
        action(@this);

        return @this;
    }

    /// <summary>
    ///     A T extension method that that return the first not null value (including the @this) or a default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>The first not null value or a default value.</returns>
    public static T CoalesceOrDefault<T>(this T @this, params T[] values) where T : class
    {
        if (@this != null)
        {
            return @this;
        }

        foreach (T value in values)
        {
            if (value != null)
            {
                return value;
            }
        }

        return default(T);
    }

    /// <summary>
    ///     A T extension method that that return the first not null value (including the @this) or a default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>The first not null value or a default value.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_CoalesceOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void CoalesceOrDefault()
    ///               {
    ///                   // Varable
    ///                   object nullObject = null;
    ///
    ///                   // Type
    ///                   object @thisNull = null;
    ///                   object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_CoalesceOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void CoalesceOrDefault()
    ///               {
    ///                   // Varable
    ///                   object nullObject = null;
    ///
    ///                   // Type
    ///                   object @thisNull = null;
    ///                   object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_CoalesceOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void CoalesceOrDefault()
    ///                   {
    ///                       // Varable
    ///                       object nullObject = null;
    ///
    ///                       // Type
    ///                       object @thisNull = null;
    ///                       object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T CoalesceOrDefault<T>(this T @this, Func<T> defaultValueFactory, params T[] values) where T : class
    {
        if (@this != null)
        {
            return @this;
        }

        foreach (T value in values)
        {
            if (value != null)
            {
                return value;
            }
        }

        return defaultValueFactory();
    }

    /// <summary>
    ///     A T extension method that that return the first not null value (including the @this) or a default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>The first not null value or a default value.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_CoalesceOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void CoalesceOrDefault()
    ///               {
    ///                   // Varable
    ///                   object nullObject = null;
    ///
    ///                   // Type
    ///                   object @thisNull = null;
    ///                   object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result3 = @thisNull.CoalesceOrDefault((x) =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_CoalesceOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void CoalesceOrDefault()
    ///               {
    ///                   // Varable
    ///                   object nullObject = null;
    ///
    ///                   // Type
    ///                   object @thisNull = null;
    ///                   object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                   object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                   object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                   Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                   Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_CoalesceOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void CoalesceOrDefault()
    ///                   {
    ///                       // Varable
    ///                       object nullObject = null;
    ///
    ///                       // Type
    ///                       object @thisNull = null;
    ///                       object @thisNotNull = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       object result1 = @thisNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Buzz&quot;;
    ///                       object result2 = @thisNull.CoalesceOrDefault(() =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                       object result3 = @thisNull.CoalesceOrDefault(x =&gt; &quot;Buzz&quot;, null, null); // return &quot;Buzz&quot;;
    ///                       object result4 = @thisNotNull.CoalesceOrDefault(nullObject, nullObject, &quot;Buzz&quot;); // return &quot;Fizz&quot;;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result1);
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result2);
    ///                       Assert.AreEqual(&quot;Buzz&quot;, result3);
    ///                       Assert.AreEqual(&quot;Fizz&quot;, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T CoalesceOrDefault<T>(this T @this, Func<T, T> defaultValueFactory, params T[] values) where T : class
    {
        if (@this != null)
        {
            return @this;
        }

        foreach (T value in values)
        {
            if (value != null)
            {
                return value;
            }
        }

        return defaultValueFactory(@this);
    }

    /// <summary>
    ///     A T extension method that gets value or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <returns>The value or default.</returns>
    public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func)
    {
        try
        {
            return func(@this);
        }
        catch (Exception)
        {
            return default(TResult);
        }
    }

    /// <summary>
    ///     A T extension method that gets value or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The value or default.</returns>
    public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue)
    {
        try
        {
            return func(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     A T extension method that gets value or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The value or default.</returns>
    /// <example>
    ///     <code>
    ///       using System.Xml;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_GetValueOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void GetValueOrDefault()
    ///               {
    ///                   // Type
    ///                   var @this = new XmlDocument();
    ///
    ///                   // Exemples
    ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using System.Xml;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_GetValueOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void GetValueOrDefault()
    ///               {
    ///                   // Type
    ///                   var @this = new XmlDocument();
    ///
    ///                   // Exemples
    ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using System.Xml;
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_GetValueOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void GetValueOrDefault()
    ///                   {
    ///                       // Type
    ///                       var @this = new XmlDocument();
    ///
    ///                       // Exemples
    ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory)
    {
        try
        {
            return func(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     A T extension method that gets value or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The value or default.</returns>
    /// <example>
    ///     <code>
    ///       using System.Xml;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_GetValueOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void GetValueOrDefault()
    ///               {
    ///                   // Type
    ///                   var @this = new XmlDocument();
    ///
    ///                   // Exemples
    ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using System.Xml;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_GetValueOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void GetValueOrDefault()
    ///               {
    ///                   // Type
    ///                   var @this = new XmlDocument();
    ///
    ///                   // Exemples
    ///                   string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                   string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                   Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using System.Xml;
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_GetValueOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void GetValueOrDefault()
    ///                   {
    ///                       // Type
    ///                       var @this = new XmlDocument();
    ///
    ///                       // Exemples
    ///                       string result1 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;;
    ///                       string result2 = @this.GetValueOrDefault(x =&gt; x.FirstChild.InnerXml, () =&gt; &quot;FizzBuzz&quot;); // return &quot;FizzBuzz&quot;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result1);
    ///                       Assert.AreEqual(&quot;FizzBuzz&quot;, result2);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<T, TResult> defaultValueFactory)
    {
        try
        {
            return func(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory(@this);
        }
    }

    /// <summary>
    ///     An object extension method that query if '@this' is assignable from.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if assignable from, false if not.</returns>
    public static bool IsAssignableFrom<T>(this object @this)
    {
        Type type = @this.GetType();
        return type.IsAssignableFrom(typeof (T));
    }

    /// <summary>
    ///     An object extension method that query if '@this' is assignable from.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>true if assignable from, false if not.</returns>
    public static bool IsAssignableFrom(this object @this, Type targetType)
    {
        Type type = @this.GetType();
        return type.IsAssignableFrom(targetType);
    }

    /// <summary>
    ///     A T extension method that makes a deep copy of '@this' object.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>the copied object.</returns>
    public static T DeepClone<T>(this T @this)
    {
        IFormatter formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, @this);
            stream.Seek(0, SeekOrigin.Begin);
            return (T) formatter.Deserialize(stream);
        }
    }

    /// <summary>
    ///     An object extension method that cast anonymous type to the specified type.
    /// </summary>
    /// <typeparam name="T">Generic type parameter. The specified type.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The object as the specified type.</returns>
    public static T As<T>(this object @this)
    {
        return (T) @this;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    public static bool InRange<T>(this T @this, T minValue, T maxValue) where T : IComparable<T>
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method that shallow copy.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A T.</returns>
    public static T ShallowCopy<T>(this T @this)
    {
        MethodInfo method = @this.GetType().GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
        return (T) method.Invoke(@this, null);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    public static bool In<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     Returns an indication whether the specified object is of type .
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="value">An object.</param>
    /// <returns>true if  is of type ; otherwise, false.</returns>
    public static Boolean IsDBNull<T>(this T value) where T : class
    {
        return Convert.IsDBNull(value);
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    public static bool Between<T>(this T @this, T minValue, T maxValue) where T : IComparable<T>
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>A T extension method that execute an action when the value is not null.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="action">The action.</param>
    public static void IfNotNull<T>(this T @this, Action<T> action) where T : class
    {
        if (@this != null)
        {
            action(@this);
        }
    }

    /// <summary>
    ///     A T extension method that the function result if not null otherwise default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <returns>The function result if @this is not null otherwise default value.</returns>
    public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func) where T : class
    {
        return @this != null ? func(@this) : default(TResult);
    }

    /// <summary>
    ///     A T extension method that the function result if not null otherwise default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The function result if @this is not null otherwise default value.</returns>
    public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue) where T : class
    {
        return @this != null ? func(@this) : defaultValue;
    }

    /// <summary>
    ///     A T extension method that the function result if not null otherwise default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="func">The function.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The function result if @this is not null otherwise default value.</returns>
    public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory) where T : class
    {
        return @this != null ? func(@this) : defaultValueFactory();
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    public static bool NotIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <returns>A TResult.</returns>
    public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction)
    {
        try
        {
            return tryFunction(@this);
        }
        catch
        {
            return default(TResult);
        }
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <param name="catchValue">The catch value.</param>
    /// <returns>A TResult.</returns>
    public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue)
    {
        try
        {
            return tryFunction(@this);
        }
        catch
        {
            return catchValue;
        }
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <param name="catchValueFactory">The catch value factory.</param>
    /// <returns>A TResult.</returns>
    public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory)
    {
        try
        {
            return tryFunction(@this);
        }
        catch
        {
            return catchValueFactory(@this);
        }
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <param name="result">[out] The result.</param>
    /// <returns>A TResult.</returns>
    public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, out TResult result)
    {
        try
        {
            result = tryFunction(@this);
            return true;
        }
        catch
        {
            result = default(TResult);
            return false;
        }
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <param name="catchValue">The catch value.</param>
    /// <param name="result">[out] The result.</param>
    /// <returns>A TResult.</returns>
    public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue, out TResult result)
    {
        try
        {
            result = tryFunction(@this);
            return true;
        }
        catch
        {
            result = catchValue;
            return false;
        }
    }

    /// <summary>A TType extension method that tries.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryFunction">The try function.</param>
    /// <param name="catchValueFactory">The catch value factory.</param>
    /// <param name="result">[out] The result.</param>
    /// <returns>A TResult.</returns>
    public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory, out TResult result)
    {
        try
        {
            result = tryFunction(@this);
            return true;
        }
        catch
        {
            result = catchValueFactory(@this);
            return false;
        }
    }

    /// <summary>A TType extension method that attempts to action from the given data.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryAction">The try action.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool Try<TType>(this TType @this, Action<TType> tryAction)
    {
        try
        {
            tryAction(@this);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>A TType extension method that attempts to action from the given data.</summary>
    /// <typeparam name="TType">Type of the type.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="tryAction">The try action.</param>
    /// <param name="catchAction">The catch action.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool Try<TType>(this TType @this, Action<TType> tryAction, Action<TType> catchAction)
    {
        try
        {
            tryAction(@this);
            return true;
        }
        catch
        {
            catchAction(@this);
            return false;
        }
    }

    /// <summary>
    ///     A T extension method that query if 'source' is the default value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="source">The source to act on.</param>
    /// <returns>true if default, false if not.</returns>
    public static bool IsDefault<T>(this T source)
    {
        return source.Equals(default(T));
    }

    /// <summary>
    ///     A T extension method that null if equals.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <returns>A T.</returns>
    public static T NullIfEquals<T>(this T @this, T value) where T : class
    {
        if (@this.Equals(value))
        {
            return null;
        }
        return @this;
    }

    /// <summary>
    ///     A System.Object extension method that converts this object to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">this.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a T.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_ToOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void ToOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = &quot;1&quot;;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_ToOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void ToOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = &quot;1&quot;;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_ToOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void ToOrDefault()
    ///                   {
    ///                       // Type
    ///                       object intValue = &quot;1&quot;;
    ///                       object invalidValue = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                       var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                       int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                       int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(1, result1);
    ///                       Assert.AreEqual(0, result2);
    ///                       Assert.AreEqual(3, result3);
    ///                       Assert.AreEqual(4, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T ToOrDefault<T>(this Object @this, Func<object, T> defaultValueFactory)
    {
        try
        {
            if (@this != null)
            {
                Type targetType = typeof (T);

                if (@this.GetType() == targetType)
                {
                    return (T) @this;
                }

                TypeConverter converter = TypeDescriptor.GetConverter(@this);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return (T) converter.ConvertTo(@this, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(@this.GetType()))
                    {
                        return (T) converter.ConvertFrom(@this);
                    }
                }

                if (@this == DBNull.Value)
                {
                    return (T) (object) null;
                }
            }

            return (T) @this;
        }
        catch (Exception)
        {
            return defaultValueFactory(@this);
        }
    }

    /// <summary>
    ///     A System.Object extension method that converts this object to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">this.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a T.</returns>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_ToOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void ToOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = &quot;1&quot;;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_ToOrDefault
    ///           {
    ///               [TestMethod]
    ///               public void ToOrDefault()
    ///               {
    ///                   // Type
    ///                   object intValue = &quot;1&quot;;
    ///                   object invalidValue = &quot;Fizz&quot;;
    ///
    ///                   // Exemples
    ///                   var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                   var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                   int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                   int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(0, result2);
    ///                   Assert.AreEqual(3, result3);
    ///                   Assert.AreEqual(4, result4);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///           using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///           using Z.ExtensionMethods.Object;
    ///
    ///           namespace ExtensionMethods.Examples
    ///           {
    ///               [TestClass]
    ///               public class System_Object_ToOrDefault
    ///               {
    ///                   [TestMethod]
    ///                   public void ToOrDefault()
    ///                   {
    ///                       // Type
    ///                       object intValue = &quot;1&quot;;
    ///                       object invalidValue = &quot;Fizz&quot;;
    ///
    ///                       // Exemples
    ///                       var result1 = intValue.ToOrDefault&lt;int&gt;(); // return 1;
    ///                       var result2 = invalidValue.ToOrDefault&lt;int&gt;(); // return 0;
    ///                       int result3 = invalidValue.ToOrDefault(3); // return 3;
    ///                       int result4 = invalidValue.ToOrDefault(() =&gt; 4); // return 4;
    ///
    ///                       // Unit Test
    ///                       Assert.AreEqual(1, result1);
    ///                       Assert.AreEqual(0, result2);
    ///                       Assert.AreEqual(3, result3);
    ///                       Assert.AreEqual(4, result4);
    ///                   }
    ///               }
    ///           }
    ///     </code>
    /// </example>
    public static T ToOrDefault<T>(this Object @this, Func<T> defaultValueFactory)
    {
        return @this.ToOrDefault(x => defaultValueFactory());
    }

    /// <summary>
    ///     A System.Object extension method that converts this object to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">this.</param>
    /// <returns>The given data converted to a T.</returns>
    public static T ToOrDefault<T>(this Object @this)
    {
        return @this.ToOrDefault(x => default(T));
    }

    /// <summary>
    ///     A System.Object extension method that converts this object to an or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">this.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a T.</returns>
    public static T ToOrDefault<T>(this Object @this, T defaultValue)
    {
        return @this.ToOrDefault(x => defaultValue);
    }

    /// <summary>
    ///     A T extension method that that return the first not null value (including the @this).
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>The first not null value.</returns>
    public static T Coalesce<T>(this T @this, params T[] values) where T : class
    {
        if (@this != null)
        {
            return @this;
        }

        foreach (T value in values)
        {
            if (value != null)
            {
                return value;
            }
        }

        return null;
    }

    /// <summary>
    ///     A System.Object extension method that toes the given this.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">this.</param>
    /// <returns>A T.</returns>
    /// <example>
    ///     <code>
    ///       using System;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_To
    ///           {
    ///               [TestMethod]
    ///               public void To()
    ///               {
    ///                   string nullValue = null;
    ///                   string value = &quot;1&quot;;
    ///                   object dbNullValue = DBNull.Value;
    ///
    ///                   // Exemples
    ///                   var result1 = value.To&lt;int&gt;(); // return 1;
    ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
    ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
    ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(1, result2.Value);
    ///                   Assert.IsFalse(result3.HasValue);
    ///                   Assert.IsFalse(result4.HasValue);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using System;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_To
    ///           {
    ///               [TestMethod]
    ///               public void To()
    ///               {
    ///                   string nullValue = null;
    ///                   string value = &quot;1&quot;;
    ///                   object dbNullValue = DBNull.Value;
    ///
    ///                   // Exemples
    ///                   var result1 = value.To&lt;int&gt;(); // return 1;
    ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
    ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
    ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(1, result2.Value);
    ///                   Assert.IsFalse(result3.HasValue);
    ///                   Assert.IsFalse(result4.HasValue);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    public static T To<T>(this Object @this)
    {
        if (@this != null)
        {
            Type targetType = typeof (T);

            if (@this.GetType() == targetType)
            {
                return (T) @this;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(@this);
            if (converter != null)
            {
                if (converter.CanConvertTo(targetType))
                {
                    return (T) converter.ConvertTo(@this, targetType);
                }
            }

            converter = TypeDescriptor.GetConverter(targetType);
            if (converter != null)
            {
                if (converter.CanConvertFrom(@this.GetType()))
                {
                    return (T) converter.ConvertFrom(@this);
                }
            }

            if (@this == DBNull.Value)
            {
                return (T) (object) null;
            }
        }

        return (T) @this;
    }

    /// <summary>
    ///     A System.Object extension method that toes the given this.
    /// </summary>
    /// <param name="this">this.</param>
    /// <param name="type">The type.</param>
    /// <returns>An object.</returns>
    /// <example>
    ///     <code>
    ///       using System;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_To
    ///           {
    ///               [TestMethod]
    ///               public void To()
    ///               {
    ///                   string nullValue = null;
    ///                   string value = &quot;1&quot;;
    ///                   object dbNullValue = DBNull.Value;
    ///
    ///                   // Exemples
    ///                   var result1 = value.To&lt;int&gt;(); // return 1;
    ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
    ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
    ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(1, result2.Value);
    ///                   Assert.IsFalse(result3.HasValue);
    ///                   Assert.IsFalse(result4.HasValue);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// <example>
    ///     <code>
    ///       using System;
    ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
    ///       using Z.ExtensionMethods.Object;
    ///
    ///       namespace ExtensionMethods.Examples
    ///       {
    ///           [TestClass]
    ///           public class System_Object_To
    ///           {
    ///               [TestMethod]
    ///               public void To()
    ///               {
    ///                   string nullValue = null;
    ///                   string value = &quot;1&quot;;
    ///                   object dbNullValue = DBNull.Value;
    ///
    ///                   // Exemples
    ///                   var result1 = value.To&lt;int&gt;(); // return 1;
    ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
    ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
    ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
    ///
    ///                   // Unit Test
    ///                   Assert.AreEqual(1, result1);
    ///                   Assert.AreEqual(1, result2.Value);
    ///                   Assert.IsFalse(result3.HasValue);
    ///                   Assert.IsFalse(result4.HasValue);
    ///               }
    ///           }
    ///       }
    /// </code>
    /// </example>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static object To(this Object @this, Type type)
    {
        if (@this != null)
        {
            Type targetType = type;

            if (@this.GetType() == targetType)
            {
                return @this;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(@this);
            if (converter != null)
            {
                if (converter.CanConvertTo(targetType))
                {
                    return converter.ConvertTo(@this, targetType);
                }
            }

            converter = TypeDescriptor.GetConverter(targetType);
            if (converter != null)
            {
                if (converter.CanConvertFrom(@this.GetType()))
                {
                    return converter.ConvertFrom(@this);
                }
            }

            if (@this == DBNull.Value)
            {
                return null;
            }
        }

        return @this;
    }

    /// <summary>
    ///     Returns an object of the specified type whose value is equivalent to the specified object.
    /// </summary>
    /// <param name="value">An object that implements the  interface.</param>
    /// <param name="typeCode">The type of object to return.</param>
    /// <returns>
    ///     An object whose underlying type is  and whose value is equivalent to .-or-A null reference (Nothing in Visual
    ///     Basic), if  is null and  is , , or .
    /// </returns>
    public static Object ChangeType(this Object value, TypeCode typeCode)
    {
        return Convert.ChangeType(value, typeCode);
    }

    /// <summary>
    ///     Returns an object of the specified type whose value is equivalent to the specified object. A parameter
    ///     supplies culture-specific formatting information.
    /// </summary>
    /// <param name="value">An object that implements the  interface.</param>
    /// <param name="typeCode">The type of object to return.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    ///     An object whose underlying type is  and whose value is equivalent to .-or- A null reference (Nothing in
    ///     Visual Basic), if  is null and  is , , or .
    /// </returns>
    public static Object ChangeType(this Object value, TypeCode typeCode, IFormatProvider provider)
    {
        return Convert.ChangeType(value, typeCode, provider);
    }

    /// <summary>
    ///     Returns an object of the specified type and whose value is equivalent to the specified object.
    /// </summary>
    /// <param name="value">An object that implements the  interface.</param>
    /// <param name="conversionType">The type of object to return.</param>
    /// <returns>
    ///     An object whose type is  and whose value is equivalent to .-or-A null reference (Nothing in Visual Basic), if
    ///     is null and  is not a value type.
    /// </returns>
    public static Object ChangeType(this Object value, Type conversionType)
    {
        return Convert.ChangeType(value, conversionType);
    }

    /// <summary>
    ///     Returns an object of the specified type whose value is equivalent to the specified object. A parameter
    ///     supplies culture-specific formatting information.
    /// </summary>
    /// <param name="value">An object that implements the  interface.</param>
    /// <param name="conversionType">The type of object to return.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    ///     An object whose type is  and whose value is equivalent to .-or- , if the  of  and  are equal.-or- A null
    ///     reference (Nothing in Visual Basic), if  is null and  is not a value type.
    /// </returns>
    public static Object ChangeType(this Object value, Type conversionType, IFormatProvider provider)
    {
        return Convert.ChangeType(value, conversionType, provider);
    }

    /// <summary>
    ///     Returns an object of the specified type and whose value is equivalent to the specified object.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="value">An object that implements the  interface.</param>
    /// <returns>
    ///     An object whose type is  and whose value is equivalent to .-or-A null reference (Nothing in Visual Basic), if
    ///     is null and  is not a value type.
    /// </returns>
    public static Object ChangeType<T>(this Object value)
    {
        return (T) Convert.ChangeType(value, typeof (T));
    }

    /// <summary>
    ///     Returns an object of the specified type whose value is equivalent to the specified object. A parameter
    ///     supplies culture-specific formatting information.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="value">An object that implements the  interface.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    ///     An object whose type is  and whose value is equivalent to .-or- , if the  of  and  are equal.-or- A null
    ///     reference (Nothing in Visual Basic), if  is null and  is not a value type.
    /// </returns>
    public static Object ChangeType<T>(this Object value, IFormatProvider provider)
    {
        return (T) Convert.ChangeType(value, typeof (T), provider);
    }

    /// <summary>
    ///     A T extension method that null if equals any.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>A T.</returns>
    public static T NullIfEqualsAny<T>(this T @this, params T[] values) where T : class
    {
        if (Array.IndexOf(values, @this) != -1)
        {
            return null;
        }
        return @this;
    }

    /// <summary>
    ///     An object extension method that converts the @this to string or return an empty string if the value is null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a string or empty if the value is null.</returns>
    public static string ToStringSafe(this object @this)
    {
        return @this == null ? "" : @this.ToString();
    }

    /// <summary>
    ///     A T extension method that null if.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>A T.</returns>
    public static T NullIf<T>(this T @this, Func<T, bool> predicate) where T : class
    {
        if (predicate(@this))
        {
            return null;
        }
        return @this;
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid short, false if not.</returns>
    public static bool IsValidInt16(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        short result;
        return short.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     Returns the  for the specified object.
    /// </summary>
    /// <param name="value">An object that implements the  interface.</param>
    /// <returns>The  for , or  if  is null.</returns>
    public static TypeCode GetTypeCode(this Object value)
    {
        return Convert.GetTypeCode(value);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid ulong.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid ulong, false if not.</returns>
    public static bool IsValidUInt64(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        ulong result;
        return ulong.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid long, false if not.</returns>
    public static bool IsValidInt64(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        long result;
        return long.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid float.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid float, false if not.</returns>
    public static bool IsValidFloat(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        float result;
        return float.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid string.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid string, false if not.</returns>
    public static bool IsValidString(this object @this)
    {
        return true;
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid ushort.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid ushort, false if not.</returns>
    public static bool IsValidUInt16(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        ushort result;
        return ushort.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid sbyte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid sbyte, false if not.</returns>
    public static bool IsValidSByte(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        sbyte result;
        return sbyte.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid ushort.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid ushort, false if not.</returns>
    public static bool IsValidUShort(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        ushort result;
        return ushort.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid char.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid char, false if not.</returns>
    public static bool IsValidChar(this object @this)
    {
        char result;
        return char.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid uint.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid uint, false if not.</returns>
    public static bool IsValidUInt32(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        uint result;
        return uint.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid float.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid float, false if not.</returns>
    public static bool IsValidSingle(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        float result;
        return float.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid decimal.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid decimal, false if not.</returns>
    public static bool IsValidDecimal(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        decimal result;
        return decimal.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid byte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid byte, false if not.</returns>
    public static bool IsValidByte(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        byte result;
        return byte.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid long, false if not.</returns>
    public static bool IsValidLong(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        long result;
        return long.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid ulong.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid ulong, false if not.</returns>
    public static bool IsValidULong(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        ulong result;
        return ulong.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid System.DateTime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid System.DateTime, false if not.</returns>
    public static bool IsValidDateTime(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        DateTime result;
        return DateTime.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid double.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid double, false if not.</returns>
    public static bool IsValidDouble(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        double result;
        return double.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid System.Guid.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid System.Guid, false if not.</returns>
    public static bool IsValidGuid(this object @this)
    {
        Guid result;
        return Guid.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid int.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid int, false if not.</returns>
    public static bool IsValidInt32(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        int result;
        return int.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an uint?</returns>
    public static uint? ToNullableUInt32OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return default(uint);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an uint?</returns>
    public static uint? ToNullableUInt32OrDefault(this object @this, uint? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an uint?</returns>
    public static uint? ToNullableUInt32OrDefault(this object @this, Func<uint?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid System.DateTimeOffset.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid System.DateTimeOffset, false if not.</returns>
    public static bool IsValidDateTimeOffSet(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        DateTimeOffset result;
        return DateTimeOffset.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid bool.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid bool, false if not.</returns>
    public static bool IsValidBoolean(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        bool result;
        return bool.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a DateTime?</returns>
    public static DateTime? ToNullableDateTimeOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return default(DateTime);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a DateTime?</returns>
    public static DateTime? ToNullableDateTimeOrDefault(this object @this, DateTime? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a DateTime?</returns>
    public static DateTime? ToNullableDateTimeOrDefault(this object @this, Func<DateTime?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that query if '@this' is valid short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid short, false if not.</returns>
    public static bool IsValidShort(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        short result;
        return short.TryParse(@this.ToString(), out result);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableFloatOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return default(float);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableFloatOrDefault(this object @this, float? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableFloatOrDefault(this object @this, Func<float?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableLongOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return default(long);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableLongOrDefault(this object @this, long? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableLongOrDefault(this object @this, Func<long?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToSingleOrDefault(this object @this)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return default(float);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToSingleOrDefault(this object @this, float defaultValue)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToSingleOrDefault(this object @this, float defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToSingleOrDefault(this object @this, Func<float> defaultValueFactory)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToSingleOrDefault(this object @this, Func<float> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableUInt64OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return default(ulong);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableUInt64OrDefault(this object @this, ulong? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableUInt64OrDefault(this object @this, Func<ulong?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToInt64OrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return default(long);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToInt64OrDefault(this object @this, long defaultValue)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToInt64OrDefault(this object @this, long defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToInt64OrDefault(this object @this, Func<long> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToInt64OrDefault(this object @this, Func<long> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a long.</returns>
    public static long ToLong(this object @this)
    {
        return Convert.ToInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableShortOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return default(short);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableShortOrDefault(this object @this, short? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableShortOrDefault(this object @this, Func<short?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a double.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a double.</returns>
    public static double ToDouble(this object @this)
    {
        return Convert.ToDouble(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable s byte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a sbyte?</returns>
    public static sbyte? ToNullableSByte(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToSByte(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable u int 16.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ushort?</returns>
    public static ushort? ToNullableUInt16(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToUInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a byte.</returns>
    public static byte ToByteOrDefault(this object @this)
    {
        try
        {
            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return default(byte);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a byte.</returns>
    public static byte ToByteOrDefault(this object @this, byte defaultValue)
    {
        try
        {
            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>An object extension method that converts this object to a byte or default.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a byte.</returns>
    public static byte ToByteOrDefault(this object @this, byte defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a byte.</returns>
    public static byte ToByteOrDefault(this object @this, Func<byte> defaultValueFactory)
    {
        try
        {
            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>An object extension method that converts this object to a byte or default.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a byte.</returns>
    public static byte ToByteOrDefault(this object @this, Func<byte> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToFloatOrDefault(this object @this)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return default(float);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToFloatOrDefault(this object @this, float defaultValue)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToFloatOrDefault(this object @this, float defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToFloatOrDefault(this object @this, Func<float> defaultValueFactory)
    {
        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a float or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a float.</returns>
    public static float ToFloatOrDefault(this object @this, Func<float> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable character.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a char?</returns>
    public static char? ToNullableChar(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToChar(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable u int 32.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an uint?</returns>
    public static uint? ToNullableUInt32(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToUInt32(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to the s byte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a sbyte.</returns>
    public static sbyte ToSByte(this object @this)
    {
        return Convert.ToSByte(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a GUID.</returns>
    public static Guid ToGuidOrDefault(this object @this)
    {
        try
        {
            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a GUID.</returns>
    public static Guid ToGuidOrDefault(this object @this, Guid defaultValue)
    {
        try
        {
            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a GUID.</returns>
    public static Guid ToGuidOrDefault(this object @this, Guid defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a GUID.</returns>
    public static Guid ToGuidOrDefault(this object @this, Func<Guid> defaultValueFactory)
    {
        try
        {
            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a GUID.</returns>
    public static Guid ToGuidOrDefault(this object @this, Func<Guid> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable double.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a double?</returns>
    public static double? ToNullableDouble(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDouble(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a string or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToStringOrDefault(this object @this)
    {
        try
        {
            return Convert.ToString(@this);
        }
        catch (Exception)
        {
            return default(string);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a string or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToStringOrDefault(this object @this, string defaultValue)
    {
        try
        {
            return Convert.ToString(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a string or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToStringOrDefault(this object @this, string defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToString(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a string or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToStringOrDefault(this object @this, Func<string> defaultValueFactory)
    {
        try
        {
            return Convert.ToString(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a string or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a string.</returns>
    public static string ToStringOrDefault(this object @this, Func<string> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToString(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUInt16OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return default(ushort);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUInt16OrDefault(this object @this, ushort? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUInt16OrDefault(this object @this, Func<ushort?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a decimal.</returns>
    public static decimal ToDecimalOrDefault(this object @this)
    {
        try
        {
            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return default(decimal);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a decimal.</returns>
    public static decimal ToDecimalOrDefault(this object @this, decimal defaultValue)
    {
        try
        {
            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a decimal.</returns>
    public static decimal ToDecimalOrDefault(this object @this, decimal defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a decimal.</returns>
    public static decimal ToDecimalOrDefault(this object @this, Func<decimal> defaultValueFactory)
    {
        try
        {
            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a decimal.</returns>
    public static decimal ToDecimalOrDefault(this object @this, Func<decimal> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an int 64.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a long.</returns>
    public static long ToInt64(this object @this)
    {
        return Convert.ToInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToUInt64OrDefault(this object @this)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return default(ulong);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToUInt64OrDefault(this object @this, ulong defaultValue)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToUInt64OrDefault(this object @this, ulong defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToUInt64OrDefault(this object @this, Func<ulong> defaultValueFactory)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToUInt64OrDefault(this object @this, Func<ulong> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUShortOrDefault(this object @this)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return default(ushort);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUShortOrDefault(this object @this, ushort defaultValue)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUShortOrDefault(this object @this, ushort defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUShortOrDefault(this object @this, Func<ushort> defaultValueFactory)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUShortOrDefault(this object @this, Func<ushort> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a bool?</returns>
    public static bool? ToNullableBooleanOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return default(bool);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a bool?</returns>
    public static bool? ToNullableBooleanOrDefault(this object @this, bool? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a bool?</returns>
    public static bool? ToNullableBooleanOrDefault(this object @this, Func<bool?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a float.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a float.</returns>
    public static float ToFloat(this object @this)
    {
        return Convert.ToSingle(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable int 16.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a short?</returns>
    public static short? ToNullableInt16(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an int.</returns>
    public static int ToInt32OrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return default(int);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an int.</returns>
    public static int ToInt32OrDefault(this object @this, int defaultValue)
    {
        try
        {
            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an int.</returns>
    public static int ToInt32OrDefault(this object @this, int defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an int.</returns>
    public static int ToInt32OrDefault(this object @this, Func<int> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an int.</returns>
    public static int ToInt32OrDefault(this object @this, Func<int> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this)
    {
        try
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return default(DateTimeOffset);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, DateTimeOffset defaultValue)
    {
        try
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, DateTimeOffset defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, Func<DateTimeOffset> defaultValueFactory)
    {
        try
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSetOrDefault(this object @this, Func<DateTimeOffset> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an u long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ulong.</returns>
    public static ulong ToULong(this object @this)
    {
        return Convert.ToUInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable int 32.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an int?</returns>
    public static int? ToNullableInt32(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt32(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a byte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a byte.</returns>
    public static byte ToByte(this object @this)
    {
        return Convert.ToByte(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a byte?</returns>
    public static byte? ToNullableByteOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return default(byte);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a byte?</returns>
    public static byte? ToNullableByteOrDefault(this object @this, byte? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a byte?</returns>
    public static byte? ToNullableByteOrDefault(this object @this, Func<byte?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a char?</returns>
    public static char? ToNullableCharOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return default(char);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a char?</returns>
    public static char? ToNullableCharOrDefault(this object @this, char? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a char?</returns>
    public static char? ToNullableCharOrDefault(this object @this, Func<char?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an u int 32.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an uint.</returns>
    public static uint ToUInt32(this object @this)
    {
        return Convert.ToUInt32(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an int?</returns>
    public static int? ToNullableInt32OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return default(int);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an int?</returns>
    public static int? ToNullableInt32OrDefault(this object @this, int? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an int?</returns>
    public static int? ToNullableInt32OrDefault(this object @this, Func<int?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable u short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ushort?</returns>
    public static ushort? ToNullableUShort(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToUInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a short?</returns>
    public static short? ToNullableShort(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable date time.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DateTime?</returns>
    public static DateTime? ToNullableDateTime(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDateTime(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable single.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a float?</returns>
    public static float? ToNullableSingle(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToSingle(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable date time off set.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DateTimeOffset?</returns>
    public static DateTimeOffset? ToNullableDateTimeOffSet(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable boolean.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a bool?</returns>
    public static bool? ToNullableBoolean(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToBoolean(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToInt16OrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return default(short);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToInt16OrDefault(this object @this, short defaultValue)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToInt16OrDefault(this object @this, short defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToInt16OrDefault(this object @this, Func<short> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToInt16OrDefault(this object @this, Func<short> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable int 64.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a long?</returns>
    public static long? ToNullableInt64(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a double?</returns>
    public static double? ToNullableDoubleOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return default(double);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a double?</returns>
    public static double? ToNullableDoubleOrDefault(this object @this, double? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a double?</returns>
    public static double? ToNullableDoubleOrDefault(this object @this, Func<double?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable unique identifier.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a Guid?</returns>
    public static Guid? ToNullableGuid(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return new Guid(@this.ToString());
    }

    /// <summary>
    ///     An object extension method that convert this object into a string representation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A string that represents this object.</returns>
    public static string ToString(this object @this)
    {
        return Convert.ToString(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a decimal?</returns>
    public static decimal? ToNullableDecimalOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return default(decimal);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a decimal?</returns>
    public static decimal? ToNullableDecimalOrDefault(this object @this, decimal? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable decimal or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a decimal?</returns>
    public static decimal? ToNullableDecimalOrDefault(this object @this, Func<decimal?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable float.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a float?</returns>
    public static float? ToNullableFloat(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToSingle(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUShortOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return default(ushort);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUShortOrDefault(this object @this, ushort? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ushort?</returns>
    public static ushort? ToNullableUShortOrDefault(this object @this, Func<ushort?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a bool.</returns>
    public static bool ToBooleanOrDefault(this object @this)
    {
        try
        {
            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return default(bool);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">true to default value.</param>
    /// <returns>The given data converted to a bool.</returns>
    public static bool ToBooleanOrDefault(this object @this, bool defaultValue)
    {
        try
        {
            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
    /// <summary>
    /// An object extension method that converts this object to a boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">true to default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a bool.</returns>
    public static bool ToBooleanOrDefault(this object @this, bool defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a bool.</returns>
    public static bool ToBooleanOrDefault(this object @this, Func<bool> defaultValueFactory)
    {
        try
        {
            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a boolean or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a bool.</returns>
    public static bool ToBooleanOrDefault(this object @this, Func<bool> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToBoolean(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a sbyte?</returns>
    public static sbyte? ToNullableSByteOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return default(sbyte);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a sbyte?</returns>
    public static sbyte? ToNullableSByteOrDefault(this object @this, sbyte? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a sbyte?</returns>
    public static sbyte? ToNullableSByteOrDefault(this object @this, Func<sbyte?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an u int 64.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ulong.</returns>
    public static ulong ToUInt64(this object @this)
    {
        return Convert.ToUInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a date time.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DateTime.</returns>
    public static DateTime ToDateTime(this object @this)
    {
        return Convert.ToDateTime(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a Guid?</returns>
    public static Guid? ToNullableGuidOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a Guid?</returns>
    public static Guid? ToNullableGuidOrDefault(this object @this, Guid? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable unique identifier or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a Guid?</returns>
    public static Guid? ToNullableGuidOrDefault(this object @this, Func<Guid?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new Guid(@this.ToString());
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableULongOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return default(ulong);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableULongOrDefault(this object @this, ulong? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ulong?</returns>
    public static ulong? ToNullableULongOrDefault(this object @this, Func<ulong?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to an u int 16.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ushort.</returns>
    public static ushort ToUInt16(this object @this)
    {
        return Convert.ToUInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToULongOrDefault(this object @this)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return default(ulong);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToULongOrDefault(this object @this, ulong defaultValue)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToULongOrDefault(this object @this, ulong defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToULongOrDefault(this object @this, Func<ulong> defaultValueFactory)
    {
        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ulong.</returns>
    public static ulong ToULongOrDefault(this object @this, Func<ulong> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToUInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a unique identifier.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a GUID.</returns>
    public static Guid ToGuid(this object @this)
    {
        return new Guid(@this.ToString());
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableSingleOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return default(float);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableSingleOrDefault(this object @this, float? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable single or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a float?</returns>
    public static float? ToNullableSingleOrDefault(this object @this, Func<float?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToSingle(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable byte.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a byte?</returns>
    public static byte? ToNullableByte(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToByte(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToShortOrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return default(short);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToShortOrDefault(this object @this, short defaultValue)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToShortOrDefault(this object @this, short defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToShortOrDefault(this object @this, Func<short> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a short or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a short.</returns>
    public static short ToShortOrDefault(this object @this, Func<short> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableInt16OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return default(short);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableInt16OrDefault(this object @this, short? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a short?</returns>
    public static short? ToNullableInt16OrDefault(this object @this, Func<short?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable u long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ulong?</returns>
    public static ulong? ToNullableULong(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToUInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to an int 32.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an int.</returns>
    public static int ToInt32(this object @this)
    {
        return Convert.ToInt32(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUInt16OrDefault(this object @this)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return default(ushort);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUInt16OrDefault(this object @this, ushort defaultValue)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUInt16OrDefault(this object @this, ushort defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUInt16OrDefault(this object @this, Func<ushort> defaultValueFactory)
    {
        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 16 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an ushort.</returns>
    public static ushort ToUInt16OrDefault(this object @this, Func<ushort> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToUInt16(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a double.</returns>
    public static double ToDoubleOrDefault(this object @this)
    {
        try
        {
            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return default(double);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a double.</returns>
    public static double ToDoubleOrDefault(this object @this, double defaultValue)
    {
        try
        {
            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a double.</returns>
    public static double ToDoubleOrDefault(this object @this, double defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a double.</returns>
    public static double ToDoubleOrDefault(this object @this, Func<double> defaultValueFactory)
    {
        try
        {
            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a double or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a double.</returns>
    public static double ToDoubleOrDefault(this object @this, Func<double> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToDouble(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a boolean.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a bool.</returns>
    public static bool ToBoolean(this object @this)
    {
        return Convert.ToBoolean(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable u int 64.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ulong?</returns>
    public static ulong? ToNullableUInt64(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToUInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a decimal.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a decimal.</returns>
    public static decimal ToDecimal(this object @this)
    {
        return Convert.ToDecimal(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a short.</returns>
    public static short ToShort(this object @this)
    {
        return Convert.ToInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToLongOrDefault(this object @this)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return default(long);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToLongOrDefault(this object @this, long defaultValue)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>An object extension method that converts this object to a long or default.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToLongOrDefault(this object @this, long defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a long or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToLongOrDefault(this object @this, Func<long> defaultValueFactory)
    {
        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>An object extension method that converts this object to a long or default.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a long.</returns>
    public static long ToLongOrDefault(this object @this, Func<long> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a single.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a float.</returns>
    public static float ToSingle(this object @this)
    {
        return Convert.ToSingle(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable decimal.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a decimal?</returns>
    public static decimal? ToNullableDecimal(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDecimal(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to an uint.</returns>
    public static uint ToUInt32OrDefault(this object @this)
    {
        try
        {
            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return default(uint);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to an uint.</returns>
    public static uint ToUInt32OrDefault(this object @this, uint defaultValue)
    {
        try
        {
            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an uint.</returns>
    public static uint ToUInt32OrDefault(this object @this, uint defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to an u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to an uint.</returns>
    public static uint ToUInt32OrDefault(this object @this, Func<uint> defaultValueFactory)
    {
        try
        {
            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to an u int 32 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to an uint.</returns>
    public static uint ToUInt32OrDefault(this object @this, Func<uint> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToUInt32(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a character.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a char.</returns>
    public static char ToChar(this object @this)
    {
        return Convert.ToChar(@this);
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a DateTimeOffset?</returns>
    public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return default(DateTimeOffset);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a DateTimeOffset?</returns>
    public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this, DateTimeOffset? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable date time off set or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a DateTimeOffset?</returns>
    public static DateTimeOffset? ToNullableDateTimeOffSetOrDefault(this object @this, Func<DateTimeOffset?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableInt64OrDefault(this object @this)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return default(long);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableInt64OrDefault(this object @this, long? defaultValue)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a nullable int 64 or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a long?</returns>
    public static long? ToNullableInt64OrDefault(this object @this, Func<long?> defaultValueFactory)
    {
        try
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a char.</returns>
    public static char ToCharOrDefault(this object @this)
    {
        try
        {
            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return default(char);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a char.</returns>
    public static char ToCharOrDefault(this object @this, char defaultValue)
    {
        try
        {
            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a char.</returns>
    public static char ToCharOrDefault(this object @this, char defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a char.</returns>
    public static char ToCharOrDefault(this object @this, Func<char> defaultValueFactory)
    {
        try
        {
            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a character or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a char.</returns>
    public static char ToCharOrDefault(this object @this, Func<char> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToChar(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a date time off set.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a DateTimeOffset.</returns>
    public static DateTimeOffset ToDateTimeOffSet(this object @this)
    {
        return new DateTimeOffset(Convert.ToDateTime(@this), TimeSpan.Zero);
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a DateTime.</returns>
    public static DateTime ToDateTimeOrDefault(this object @this)
    {
        try
        {
            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return default(DateTime);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a DateTime.</returns>
    public static DateTime ToDateTimeOrDefault(this object @this, DateTime defaultValue)
    {
        try
        {
            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a DateTime.</returns>
    public static DateTime ToDateTimeOrDefault(this object @this, DateTime defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to a date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a DateTime.</returns>
    public static DateTime ToDateTimeOrDefault(this object @this, Func<DateTime> defaultValueFactory)
    {
        try
        {
            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to a date time or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a DateTime.</returns>
    public static DateTime ToDateTimeOrDefault(this object @this, Func<DateTime> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToDateTime(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to the s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The given data converted to a sbyte.</returns>
    public static sbyte ToSByteOrDefault(this object @this)
    {
        try
        {
            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return default(sbyte);
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to the s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The given data converted to a sbyte.</returns>
    public static sbyte ToSByteOrDefault(this object @this, sbyte defaultValue)
    {
        try
        {
            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// An object extension method that converts this object to the s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a sbyte.</returns>
    public static sbyte ToSByteOrDefault(this object @this, sbyte defaultValue, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An object extension method that converts this object to the s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The given data converted to a sbyte.</returns>
    public static sbyte ToSByteOrDefault(this object @this, Func<sbyte> defaultValueFactory)
    {
        try
        {
            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// An object extension method that converts this object to the s byte or default.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <param name="useDefaultIfNull">true to use default if null.</param>
    /// <returns>The given data converted to a sbyte.</returns>
    public static sbyte ToSByteOrDefault(this object @this, Func<sbyte> defaultValueFactory, bool useDefaultIfNull)
    {
        if (useDefaultIfNull && @this == null)
        {
            return defaultValueFactory();
        }

        try
        {
            return Convert.ToSByte(@this);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    ///     An object extension method that converts the @this to a nullable long.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a long?</returns>
    public static long? ToNullableLong(this object @this)
    {
        if (@this == null || @this == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt64(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to an int 16.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a short.</returns>
    public static short ToInt16(this object @this)
    {
        return Convert.ToInt16(@this);
    }

    /// <summary>
    ///     An object extension method that converts the @this to an u short.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as an ushort.</returns>
    public static ushort ToUShort(this object @this)
    {
        return Convert.ToUInt16(@this);
    }

    /// <summary>
    ///     A Decimal extension method that converts the @this to a money.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a Decimal.</returns>
    public static Decimal ToMoney(this Decimal @this)
    {
        return Math.Round(@this, 2);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Decimal @this, params Decimal[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Decimal @this, Decimal minValue, Decimal maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Decimal @this, Decimal minValue, Decimal maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Decimal @this, params Decimal[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     Multiplies two specified  values.
    /// </summary>
    /// <param name="d1">The multiplicand.</param>
    /// <param name="d2">The multiplier.</param>
    /// <returns>The result of multiplying  and .</returns>
    public static Decimal Multiply(this Decimal d1, Decimal d2)
    {
        return Decimal.Multiply(d1, d2);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent single-precision floating-point number.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A single-precision floating-point number equivalent to the value of .</returns>
    public static Single ToSingle(this Decimal d)
    {
        return Decimal.ToSingle(d);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 64-bit signed integer.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A 64-bit signed integer equivalent to the value of .</returns>
    public static Int64 ToInt64(this Decimal d)
    {
        return Decimal.ToInt64(d);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 64-bit unsigned integer.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A 64-bit unsigned integer equivalent to the value of .</returns>
    public static UInt64 ToUInt64(this Decimal d)
    {
        return Decimal.ToUInt64(d);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 16-bit unsigned integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>A 16-bit unsigned integer equivalent to the value of .</returns>
    public static UInt16 ToUInt16(this Decimal value)
    {
        return Decimal.ToUInt16(value);
    }

    /// <summary>
    ///     Computes the remainder after dividing two  values.
    /// </summary>
    /// <param name="d1">The dividend.</param>
    /// <param name="d2">The divisor.</param>
    /// <returns>The remainder after dividing  by .</returns>
    public static Decimal Remainder(this Decimal d1, Decimal d2)
    {
        return Decimal.Remainder(d1, d2);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 32-bit signed integer.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A 32-bit signed integer equivalent to the value of .</returns>
    public static Int32 ToInt32(this Decimal d)
    {
        return Decimal.ToInt32(d);
    }

    /// <summary>
    ///     Converts the value of a specified instance of  to its equivalent binary representation.
    /// </summary>
    /// <param name="d">The value to convert.</param>
    /// <returns>A 32-bit signed integer array with four elements that contain the binary representation of .</returns>
    public static Int32[] GetBits(this Decimal d)
    {
        return Decimal.GetBits(d);
    }

    /// <summary>
    ///     Returns the result of multiplying the specified  value by negative one.
    /// </summary>
    /// <param name="d">The value to negate.</param>
    /// <returns>A decimal number with the value of , but the opposite sign.-or- Zero, if  is zero.</returns>
    public static Decimal Negate(this Decimal d)
    {
        return Decimal.Negate(d);
    }

    /// <summary>
    ///     Converts the specified  value to the equivalent OLE Automation Currency value, which is contained in a 64-bit
    ///     signed integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of .</returns>
    public static Int64 ToOACurrency(this Decimal value)
    {
        return Decimal.ToOACurrency(value);
    }

    /// <summary>
    ///     Subtracts one specified  value from another.
    /// </summary>
    /// <param name="d1">The minuend.</param>
    /// <param name="d2">The subtrahend.</param>
    /// <returns>The result of subtracting  from .</returns>
    public static Decimal Subtract(this Decimal d1, Decimal d2)
    {
        return Decimal.Subtract(d1, d2);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 8-bit unsigned integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>An 8-bit unsigned integer equivalent to .</returns>
    public static Byte ToByte(this Decimal value)
    {
        return Decimal.ToByte(value);
    }

    /// <summary>
    ///     Divides two specified  values.
    /// </summary>
    /// <param name="d1">The dividend.</param>
    /// <param name="d2">The divisor.</param>
    /// <returns>The result of dividing  by .</returns>
    public static Decimal Divide(this Decimal d1, Decimal d2)
    {
        return Decimal.Divide(d1, d2);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 8-bit signed integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>An 8-bit signed integer equivalent to .</returns>
    public static SByte ToSByte(this Decimal value)
    {
        return Decimal.ToSByte(value);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 16-bit signed integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>A 16-bit signed integer equivalent to .</returns>
    public static Int16 ToInt16(this Decimal value)
    {
        return Decimal.ToInt16(value);
    }

    /// <summary>
    ///     Returns the largest integer less than or equal to the specified decimal number.
    /// </summary>
    /// <param name="d">A decimal number.</param>
    /// <returns>
    ///     The largest integer less than or equal to .  Note that the method returns an integral value of type .
    /// </returns>
    public static Decimal Floor(this Decimal d)
    {
        return Math.Floor(d);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent 32-bit unsigned integer.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A 32-bit unsigned integer equivalent to the value of .</returns>
    public static UInt32 ToUInt32(this Decimal d)
    {
        return Decimal.ToUInt32(d);
    }

    /// <summary>
    ///     Converts the value of the specified  to the equivalent double-precision floating-point number.
    /// </summary>
    /// <param name="d">The decimal number to convert.</param>
    /// <returns>A double-precision floating-point number equivalent to .</returns>
    public static Double ToDouble(this Decimal d)
    {
        return Decimal.ToDouble(d);
    }

    /// <summary>
    ///     Rounds a decimal value to the nearest integral value.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <returns>
    ///     The integer nearest parameter . If the fractional component of  is halfway between two integers, one of which
    ///     is even and the other odd, the even number is returned. Note that this method returns a  instead of an
    ///     integral type.
    /// </returns>
    public static Decimal Round(this Decimal d)
    {
        return Math.Round(d);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="decimals">The number of decimal places in the return value.</param>
    /// <returns>The number nearest to  that contains a number of fractional digits equal to .</returns>
    public static Decimal Round(this Decimal d, Int32 decimals)
    {
        return Math.Round(d, decimals);
    }

    /// <summary>
    ///     Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway
    ///     between two numbers.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The integer nearest . If  is halfway between two numbers, one of which is even and the other odd, then
    ///     determines which of the two is returned.
    /// </returns>
    public static Decimal Round(this Decimal d, MidpointRounding mode)
    {
        return Math.Round(d, mode);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits. A parameter specifies how to round the
    ///     value if it is midway between two numbers.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="decimals">The number of decimal places in the return value.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The number nearest to  that contains a number of fractional digits equal to . If  has fewer fractional digits
    ///     than ,  is returned unchanged.
    /// </returns>
    public static Decimal Round(this Decimal d, Int32 decimals, MidpointRounding mode)
    {
        return Math.Round(d, decimals, mode);
    }

    public static SqlDbType SqlSystemTypeToSqlDbType(this short @this)
    {
        switch (@this)
        {
            case 34: // 34 | "image" | SqlDbType.Image
                return SqlDbType.Image;

            case 35: // 35 | "text" | SqlDbType.Text
                return SqlDbType.Text;

            case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                return SqlDbType.UniqueIdentifier;

            case 40: // 40 | "date" | SqlDbType.Date
                return SqlDbType.Date;

            case 41: // 41 | "time" | SqlDbType.Time
                return SqlDbType.Time;

            case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                return SqlDbType.DateTime2;

            case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                return SqlDbType.DateTimeOffset;

            case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                return SqlDbType.TinyInt;

            case 52: // 52 | "smallint" | SqlDbType.SmallInt
                return SqlDbType.SmallInt;

            case 56: // 56 | "int" | SqlDbType.Int
                return SqlDbType.Int;

            case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                return SqlDbType.SmallDateTime;

            case 59: // 59 | "real" | SqlDbType.Real
                return SqlDbType.Real;

            case 60: // 60 | "money" | SqlDbType.Money
                return SqlDbType.Money;

            case 61: // 61 | "datetime" | SqlDbType.DateTime
                return SqlDbType.DateTime;

            case 62: // 62 | "float" | SqlDbType.Float
                return SqlDbType.Float;

            case 98: // 98 | "sql_variant" | SqlDbType.Variant
                return SqlDbType.Variant;

            case 99: // 99 | "ntext" | SqlDbType.NText
                return SqlDbType.NText;

            case 104: // 104 | "bit" | SqlDbType.Bit
                return SqlDbType.Bit;

            case 106: // 106 | "decimal" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 108: // 108 | "numeric" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                return SqlDbType.SmallMoney;

            case 127: // 127 | "bigint" | SqlDbType.BigInt
                return SqlDbType.BigInt;

            case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                return SqlDbType.VarBinary;

            case 167: // 167 | "varchar" | SqlDbType.VarChar
                return SqlDbType.VarChar;

            case 173: // 173 | "binary" | SqlDbType.Binary
                return SqlDbType.Binary;

            case 175: // 175 | "char" | SqlDbType.Char
                return SqlDbType.Char;

            case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                return SqlDbType.Timestamp;

            case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                return SqlDbType.NVarChar;

            case 239: // 239 | "nchar" | SqlDbType.NChar
                return SqlDbType.NChar;

            case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;

            case 241: // 241 | "xml" | SqlDbType.Xml
                return SqlDbType.Xml;

            default:
                throw new Exception(string.Format("Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com", @this));
        }
    }

    /// <summary>
    ///     Returns the absolute value of a  number.
    /// </summary>
    /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
    /// <returns>A decimal number, x, such that 0 ? x ?.</returns>
    public static Decimal Abs(this Decimal value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns the smallest integral value that is greater than or equal to the specified decimal number.
    /// </summary>
    /// <param name="d">A decimal number.</param>
    /// <returns>
    ///     The smallest integral value that is greater than or equal to . Note that this method returns a  instead of an
    ///     integral type.
    /// </returns>
    public static Decimal Ceiling(this Decimal d)
    {
        return Math.Ceiling(d);
    }

    /// <summary>
    ///     Returns the larger of two decimal numbers.
    /// </summary>
    /// <param name="val1">The first of two decimal numbers to compare.</param>
    /// <param name="val2">The second of two decimal numbers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static Decimal Max(this Decimal val1, Decimal val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Calculates the integral part of a specified decimal number.
    /// </summary>
    /// <param name="d">A number to truncate.</param>
    /// <returns>
    ///     The integral part of ; that is, the number that remains after any fractional digits have been discarded.
    /// </returns>
    public static Decimal Truncate(this Decimal d)
    {
        return Math.Truncate(d);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a decimal number.
    /// </summary>
    /// <param name="value">A signed decimal number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Decimal value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the smaller of two decimal numbers.
    /// </summary>
    /// <param name="val1">The first of two decimal numbers to compare.</param>
    /// <param name="val2">The second of two decimal numbers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static Decimal Min(this Decimal val1, Decimal val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns the specified 16-bit signed integer value as an array of bytes.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>An array of bytes with length 2.</returns>
    public static Byte[] GetBytes(this Int16 value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    ///     Returns the larger of two 16-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 16-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 16-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static Int16 Max(this Int16 val1, Int16 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a 16-bit signed integer.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Int16 value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the absolute value of a 16-bit signed integer.
    /// </summary>
    /// <param name="value">A number that is greater than , but less than or equal to .</param>
    /// <returns>A 16-bit signed integer, x, such that 0 ? x ?.</returns>
    public static Int16 Abs(this Int16 value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     Returns the smaller of two 16-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 16-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 16-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static Int16 Min(this Int16 val1, Int16 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     An Int16 extension method that query if '@this' is even.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if even, false if not.</returns>
    public static bool IsEven(this Int16 @this)
    {
        return @this%2 == 0;
    }

    /// <summary>
    ///     An Int16 extension method that milliseconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Milliseconds(this Int16 @this)
    {
        return TimeSpan.FromMilliseconds(@this);
    }

    /// <summary>
    ///     An Int16 extension method that hours the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Hours(this Int16 @this)
    {
        return TimeSpan.FromHours(@this);
    }

    /// <summary>
    ///     An Int16 extension method that minutes the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Minutes(this Int16 @this)
    {
        return TimeSpan.FromMinutes(@this);
    }

    /// <summary>
    ///     An Int16 extension method that query if '@this' is multiple of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factor">The factor.</param>
    /// <returns>true if multiple of, false if not.</returns>
    public static bool IsMultipleOf(this Int16 @this, Int16 factor)
    {
        return @this%factor == 0;
    }

    /// <summary>
    ///     An Int16 extension method that days the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Days(this Int16 @this)
    {
        return TimeSpan.FromDays(@this);
    }

    /// <summary>
    ///     An Int16 extension method that query if '@this' is odd.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if odd, false if not.</returns>
    public static bool IsOdd(this Int16 @this)
    {
        return @this%2 != 0;
    }

    /// <summary>
    ///     An Int16 extension method that query if '@this' is prime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if prime, false if not.</returns>
    public static bool IsPrime(this Int16 @this)
    {
        if (@this == 1 || @this == 2)
        {
            return true;
        }

        if (@this%2 == 0)
        {
            return false;
        }

        var sqrt = (Int16) Math.Sqrt(@this);
        for (Int64 t = 3; t <= sqrt; t = t + 2)
        {
            if (@this%t == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     An Int16 extension method that factor of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factorNumer">The factor numer.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool FactorOf(this Int16 @this, Int16 factorNumer)
    {
        return factorNumer%@this == 0;
    }

    /// <summary>
    ///     An Int16 extension method that weeks the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Weeks(this Int16 @this)
    {
        return TimeSpan.FromDays(@this*7);
    }

    /// <summary>
    ///     An Int16 extension method that seconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Seconds(this Int16 @this)
    {
        return TimeSpan.FromSeconds(@this);
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Int16 @this, params Int16[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Int16 @this, Int16 minValue, Int16 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Int16 @this, params Int16[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    public static SqlDbType SqlSystemTypeToSqlDbType(this int @this)
    {
        switch (@this)
        {
            case 34: // 34 | "image" | SqlDbType.Image
                return SqlDbType.Image;

            case 35: // 35 | "text" | SqlDbType.Text
                return SqlDbType.Text;

            case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                return SqlDbType.UniqueIdentifier;

            case 40: // 40 | "date" | SqlDbType.Date
                return SqlDbType.Date;

            case 41: // 41 | "time" | SqlDbType.Time
                return SqlDbType.Time;

            case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                return SqlDbType.DateTime2;

            case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                return SqlDbType.DateTimeOffset;

            case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                return SqlDbType.TinyInt;

            case 52: // 52 | "smallint" | SqlDbType.SmallInt
                return SqlDbType.SmallInt;

            case 56: // 56 | "int" | SqlDbType.Int
                return SqlDbType.Int;

            case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                return SqlDbType.SmallDateTime;

            case 59: // 59 | "real" | SqlDbType.Real
                return SqlDbType.Real;

            case 60: // 60 | "money" | SqlDbType.Money
                return SqlDbType.Money;

            case 61: // 61 | "datetime" | SqlDbType.DateTime
                return SqlDbType.DateTime;

            case 62: // 62 | "float" | SqlDbType.Float
                return SqlDbType.Float;

            case 98: // 98 | "sql_variant" | SqlDbType.Variant
                return SqlDbType.Variant;

            case 99: // 99 | "ntext" | SqlDbType.NText
                return SqlDbType.NText;

            case 104: // 104 | "bit" | SqlDbType.Bit
                return SqlDbType.Bit;

            case 106: // 106 | "decimal" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 108: // 108 | "numeric" | SqlDbType.Decimal
                return SqlDbType.Decimal;

            case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                return SqlDbType.SmallMoney;

            case 127: // 127 | "bigint" | SqlDbType.BigInt
                return SqlDbType.BigInt;

            case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                return SqlDbType.VarBinary;

            case 167: // 167 | "varchar" | SqlDbType.VarChar
                return SqlDbType.VarChar;

            case 173: // 173 | "binary" | SqlDbType.Binary
                return SqlDbType.Binary;

            case 175: // 175 | "char" | SqlDbType.Char
                return SqlDbType.Char;

            case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                return SqlDbType.Timestamp;

            case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                return SqlDbType.NVarChar;

            case 239: // 239 | "nchar" | SqlDbType.NChar
                return SqlDbType.NChar;

            case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                return SqlDbType.Udt;

            case 241: // 241 | "xml" | SqlDbType.Xml
                return SqlDbType.Xml;

            default:
                throw new Exception(string.Format("Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com", @this));
        }
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Int16 @this, Int16 minValue, Int16 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     Converts a short value from network byte order to host byte order.
    /// </summary>
    /// <param name="network">The number to convert, expressed in network byte order.</param>
    /// <returns>A short value, expressed in host byte order.</returns>
    public static Int16 NetworkToHostOrder(this Int16 network)
    {
        return IPAddress.NetworkToHostOrder(network);
    }

    /// <summary>
    ///     Converts a short value from host byte order to network byte order.
    /// </summary>
    /// <param name="host">The number to convert, expressed in host byte order.</param>
    /// <returns>A short value, expressed in network byte order.</returns>
    public static Int16 HostToNetworkOrder(this Int16 host)
    {
        return IPAddress.HostToNetworkOrder(host);
    }

    /// <summary>
    ///     Converts the specified Unicode code point into a UTF-16 encoded string.
    /// </summary>
    /// <param name="utf32">A 21-bit Unicode code point.</param>
    /// <returns>
    ///     A string consisting of one  object or a surrogate pair of  objects equivalent to the code point specified by
    ///     the  parameter.
    /// </returns>
    public static String ConvertFromUtf32(this Int32 utf32)
    {
        return Char.ConvertFromUtf32(utf32);
    }

    /// <summary>
    ///     An Int32 extension method that div rem.
    /// </summary>
    /// <param name="a">a to act on.</param>
    /// <param name="b">The Int32 to process.</param>
    /// <param name="result">[out] The result.</param>
    /// <returns>An Int32.</returns>
    public static Int32 DivRem(this Int32 a, Int32 b, out Int32 result)
    {
        return Math.DivRem(a, b, out result);
    }

    /// <summary>
    ///     Returns the larger of two 32-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is larger.</returns>
    public static Int32 Max(this Int32 val1, Int32 val2)
    {
        return Math.Max(val1, val2);
    }

    /// <summary>
    ///     Produces the full product of two 32-bit numbers.
    /// </summary>
    /// <param name="a">The first number to multiply.</param>
    /// <param name="b">The second number to multiply.</param>
    /// <returns>The number containing the product of the specified numbers.</returns>
    public static Int64 BigMul(this Int32 a, Int32 b)
    {
        return Math.BigMul(a, b);
    }

    /// <summary>
    ///     Returns a value indicating the sign of a 32-bit signed integer.
    /// </summary>
    /// <param name="value">A signed number.</param>
    /// <returns>
    ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
    ///     zero. 0  is equal to zero. 1  is greater than zero.
    /// </returns>
    public static Int32 Sign(this Int32 value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    ///     Returns the smaller of two 32-bit signed integers.
    /// </summary>
    /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
    /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
    /// <returns>Parameter  or , whichever is smaller.</returns>
    public static Int32 Min(this Int32 val1, Int32 val2)
    {
        return Math.Min(val1, val2);
    }

    /// <summary>
    ///     Returns the absolute value of a 32-bit signed integer.
    /// </summary>
    /// <param name="value">A number that is greater than , but less than or equal to .</param>
    /// <returns>A 32-bit signed integer, x, such that 0 ? x ?.</returns>
    public static Int32 Abs(this Int32 value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///     An Int32 extension method that query if '@this' is odd.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if odd, false if not.</returns>
    public static bool IsOdd(this Int32 @this)
    {
        return @this%2 != 0;
    }

    /// <summary>
    ///     An Int32 extension method that days the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Days(this Int32 @this)
    {
        return TimeSpan.FromDays(@this);
    }

    /// <summary>
    ///     An Int32 extension method that milliseconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Milliseconds(this Int32 @this)
    {
        return TimeSpan.FromMilliseconds(@this);
    }

    /// <summary>
    ///     An Int32 extension method that query if '@this' is even.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if even, false if not.</returns>
    public static bool IsEven(this Int32 @this)
    {
        return @this%2 == 0;
    }

    /// <summary>
    ///     An Int32 extension method that hours the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Hours(this Int32 @this)
    {
        return TimeSpan.FromHours(@this);
    }

    /// <summary>
    ///     An Int32 extension method that minutes the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Minutes(this Int32 @this)
    {
        return TimeSpan.FromMinutes(@this);
    }

    /// <summary>
    ///     An Int32 extension method that factor of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factorNumer">The factor numer.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool FactorOf(this Int32 @this, Int32 factorNumer)
    {
        return factorNumer%@this == 0;
    }

    /// <summary>
    ///     An Int32 extension method that seconds the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Seconds(this Int32 @this)
    {
        return TimeSpan.FromSeconds(@this);
    }

    /// <summary>
    ///     An Int32 extension method that weeks the given this.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Weeks(this Int32 @this)
    {
        return TimeSpan.FromDays(@this*7);
    }

    /// <summary>
    ///     An Int32 extension method that query if '@this' is prime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if prime, false if not.</returns>
    public static bool IsPrime(this Int32 @this)
    {
        if (@this == 1 || @this == 2)
        {
            return true;
        }

        if (@this%2 == 0)
        {
            return false;
        }

        var sqrt = (Int32) Math.Sqrt(@this);
        for (Int64 t = 3; t <= sqrt; t = t + 2)
        {
            if (@this%t == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     An Int32 extension method that query if '@this' is multiple of.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="factor">The factor.</param>
    /// <returns>true if multiple of, false if not.</returns>
    public static bool IsMultipleOf(this Int32 @this, Int32 factor)
    {
        return @this%factor == 0;
    }

    /// <summary>
    ///     Returns the number of days in the specified month and year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (a number ranging from 1 to 12).</param>
    /// <returns>
    ///     The number of days in  for the specified .For example, if  equals 2 for February, the return value is 28 or
    ///     29 depending upon whether  is a leap year.
    /// </returns>
    public static Int32 DaysInMonth(this Int32 year, Int32 month)
    {
        return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    ///     Returns an indication whether the specified year is a leap year.
    /// </summary>
    /// <param name="year">A 4-digit year.</param>
    /// <returns>true if  is a leap year; otherwise, false.</returns>
    public static Boolean IsLeapYear(this Int32 year)
    {
        return DateTime.IsLeapYear(year);
    }

    /// <summary>
    ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool Between(this Int32 @this, Int32 minValue, Int32 maxValue)
    {
        return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
    }

    /// <summary>
    ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool InRange(this Int32 @this, Int32 minValue, Int32 maxValue)
    {
        return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is not equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list doesn't contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool NotIn(this Int32 @this, params Int32[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }

    /// <summary>
    ///     A T extension method to determines whether the object is equal to any of the provided values.
    /// </summary>
    /// <param name="this">The object to be compared.</param>
    /// <param name="values">The value list to compare with the object.</param>
    /// <returns>true if the values list contains the object, else false.</returns>
    /// ###
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public static bool In(this Int32 @this, params Int32[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    /// <summary>
    ///     Returns the specified 32-bit signed integer value as an array of bytes.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>An array of bytes with length 4.</returns>
    public static Byte[] GetBytes(this Int32 value)
    {
        return BitConverter.GetBytes(value);
    }

    /// <summary>
    ///     Converts an integer value from host byte order to network byte order.
    /// </summary>
    /// <param name="host">The number to convert, expressed in host byte order.</param>
    /// <returns>An integer value, expressed in network byte order.</returns>
    public static Int32 HostToNetworkOrder(this Int32 host)
    {
        return IPAddress.HostToNetworkOrder(host);
    }

    /// <summary>
    ///     Converts an integer value from network byte order to host byte order.
    /// </summary>
    /// <param name="network">The number to convert, expressed in network byte order.</param>
    /// <returns>An integer value, expressed in host byte order.</returns>
    public static Int32 NetworkToHostOrder(this Int32 network)
    {
        return IPAddress.NetworkToHostOrder(network);
    }
}