using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BigNumber : MonoBehaviour
{
    private readonly string[] abbreviations = { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz" };

    public string ConvertIntoString(BigInteger value)
    {
        int index = 0;
        BigInteger formattedValue = value;
        BigInteger decimalPart;

        while (formattedValue >= BigInteger.Pow(10, 3) && index < abbreviations.Length - 1)
        {
            decimalPart = formattedValue % BigInteger.Pow(10, 3);
            formattedValue /= BigInteger.Pow(10, 3);
            index++;
        }
        string integerPart = formattedValue.ToString();
        double decimalFraction = (double)decimalPart / (double)BigInteger.Pow(10, 3);
        // Format the decimal part with two decimal places.
        string decimalPartFormatted = Mathf.Floor((float)(decimalFraction * 100)) / 100 + "";
        // If the decimal part is 0.0, don't include it.
        if (decimalPartFormatted == "0")
        {
            return integerPart + abbreviations[index];
        }

        // Combine the integer and decimal parts and append the appropriate abbreviation.
        string result = integerPart + "." + decimalPartFormatted.Substring(2) + abbreviations[index];

        return result;
    }

      private string FormatValue(BigInteger value)
    {
        int suffixIndex = 0;
        while (value >= BigInteger.Pow(10, 3) && suffixIndex < abbreviations.Length - 1)
        {
            value /= BigInteger.Pow(10, 3);
            suffixIndex++;
        }

        // Format value with two decimal places
        string formattedValue = $"{(double)value:0.00}" + abbreviations[suffixIndex];
        return formattedValue;
    }
}