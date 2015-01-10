using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace DbEntityHelper.Helper
{
    /// <summary>
    /// A helper class with several static functions for validating value
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Check if a string is a numeric
        /// </summary>
        /// <param name="inStr">input string</param>
        /// <returns>true if it is a numeric</returns>
        public static Boolean IsNumeric(string inStr)
        {
            return Information.IsNumeric(inStr);
        }

        /// <summary>
        /// Check if a string is a date
        /// </summary>
        /// <param name="inStr">input string</param>
        /// <returns>true if it is a date</returns>
        public static Boolean IsDate(string inStr)
        {
            return Information.IsDate(inStr);
        }

        /// <summary>
        /// Check the A.D. date string in yyyyMMdd format
        /// </summary>
        /// <param name="inDate">date string</param>
        /// <returns>true if it's valid</returns>
        public static bool IsAnnoDominiString(string inDate)
        {
            if (inDate.Length != 8)
                return false;
            int year = int.Parse(inDate.Substring(0, 4));
            int month = int.Parse(inDate.Substring(4, 2));
            int day = int.Parse(inDate.Substring(6, 2));
            try
            {
                DateTime date = new DateTime(year, month, day);
                return (date != null);
            }
            catch (Exception) { return false; };
        }

        /// <summary>
        /// Check the MingGuo date string in yyyMMdd format
        /// </summary>
        /// <param name="inDate">date string</param>
        /// <returns>true if it's valid</returns>
        public static bool IsMingGuoString(string inDate)
        {
            if (inDate.Length != 7)
                return false;
            int year = int.Parse(inDate.Substring(0, 3)) + 1911;
            int month = int.Parse(inDate.Substring(3, 2));
            int day = int.Parse(inDate.Substring(5, 2));
            try
            {
                DateTime date = new DateTime(year, month, day);
                return (date != null);
            }
            catch (Exception) { return false; };
        }

        /// <summary>
        /// Check the time string in HHmmss format
        /// </summary>
        /// <param name="inTime">time string</param>
        /// <returns>true if it's valid</returns>
        public static bool IsTimeString(string inTime)
        {
            if (inTime.Length != 6)
                return false;
            uint hour = uint.Parse(inTime.Substring(0, 2));
            uint minute = uint.Parse(inTime.Substring(2, 2));
            uint second = uint.Parse(inTime.Substring(4, 2));
            if ((hour > 23) || (minute > 59) || (second > 59))
                return false;
            return true;
        }

        /// <summary>
        /// Verify a string is not empty
        /// </summary>
        /// <param name="inStr">The input string</param>
        /// <returns>true if it's not empty</returns>
        public static bool NotEmpty(string inStr)
        {
            if (inStr.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
