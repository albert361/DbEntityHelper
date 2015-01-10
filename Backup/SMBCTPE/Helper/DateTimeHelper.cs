using System;
using System.Collections.Generic;
using System.Text;

namespace SMBCTPE.Helper
{
    /// <summary>
    /// A helper class with several static functions for converting and validating datetime
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// Convert to MingGuo Date string in yyyMMdd or yyyMMddHHmmss format
        /// </summary>
        /// <param name="inDate">A.D. date string in yyyyMMdd or yyyyMMddHHmmss format</param>
        /// <returns>yyyMMdd string</returns>
        public static string ToMingGuoDate(string inDate)
        {
            int year = 0;
            // check format
            if (inDate.Length == 8)
            {
                // check yyyyMMdd
                if (!ValidationHelper.IsAnnoDominiString(inDate))
                    throw new ArgumentException("The input string is not a valid A.D. date!");
            }
            else if (inDate.Length == 14)
            {
                // check yyyyMMddHHmmss
                if (!ValidationHelper.IsAnnoDominiString(inDate.Substring(0, 8)))
                    throw new ArgumentException("The input string is not a valid A.D. date!");
                if (!ValidationHelper.IsTimeString(inDate.Substring(8, 6)))
                    throw new ArgumentException("The input string is not a valid time!");
            }
            else
            {
                throw new ArgumentException("The input string is not a valid A.D. date!");
            }
            // parse year
            if (!int.TryParse(inDate.Substring(0, 4), out year))
                throw new ArgumentException("The input string is not a valid A.D. date!");
            // check year
            if (year <= 1911)
                throw new ArgumentException("The input string is not a valid A.D. date!");
            
            return ((int)(year - 1911)).ToString("000") + inDate.Substring(4);
        }

        /// <summary>
        /// Convert to MingGuo Date string in yyyMMdd or yyyMMddHHmmss format
        /// </summary>
        /// <param name="inDate">date time object</param>
        /// <param name="withTime">with/without the time string</param>
        /// <returns>yyyMMdd or yyyMMddHHmmss string</returns>
        public static string ToMingGuoDate(DateTime inDate, bool withTime)
        {
            return ToMingGuoDate(ToDateString(inDate, withTime));
        }

        /// <summary>
        /// Convert to A.D. date string in yyyyMMdd or yyyyMMddHHmmss format
        /// </summary>
        /// <param name="inDate">date time object</param>
        /// <param name="withTime">with/without the time string</param>
        /// <returns>yyyyMMdd or yyyyMMddHHmmss string</returns>
        public static string ToDateString(DateTime inDate, bool withTime)
        {
            return withTime ? inDate.ToString("yyyyMMddHHmmss") : inDate.ToString("yyyyMMdd");
        }
        
        /// <summary>
        /// Parse an A.D. or MingGuo date string to DateTime object
        /// <para>A.D. yyyyMMdd</para>
        /// <para>A.D. yyyyMMddHHmmss</para>
        /// <para>MingGuo yyyMMdd</para>
        /// <para>MingGuo yyyMMddHHmmss</para>
        /// </summary>
        /// <param name="inDate">the A.D. or MingGuo date string</param>
        /// <returns>date time object</returns>
        public static DateTime ParseDateTimeString(string inDate)
        {
            if (inDate.Length == 8)
            {
                // check A.D.'s validity
                if (!ValidationHelper.IsAnnoDominiString(inDate))
                    throw new ArgumentException("The input string is not a valid A.D. date!");
                return new DateTime(int.Parse(inDate.Substring(0, 4)),
                                    int.Parse(inDate.Substring(4, 2)),
                                    int.Parse(inDate.Substring(6, 2))
                                    );
            }
            else if (inDate.Length == 7)
            {
                // check MingGuo's validity
                if (!ValidationHelper.IsMingGuoString(inDate))
                    throw new ArgumentException("The input string is not a valid MingGuo date!");
                return new DateTime(int.Parse(inDate.Substring(0, 3) + 1911),
                                    int.Parse(inDate.Substring(3, 2)),
                                    int.Parse(inDate.Substring(5, 2))
                                    );
            }
            else if (inDate.Length == (8 + 6))
            {
                // check the validity of A.D. with time
                if (!ValidationHelper.IsAnnoDominiString(inDate.Substring(0, 8)))
                    throw new ArgumentException("The input string is not a valid A.D. date!");
                if (!ValidationHelper.IsTimeString(inDate.Substring(8, 6)))
                    throw new ArgumentException("The input string is not a valid time!");
                return new DateTime(int.Parse(inDate.Substring(0, 4)),
                                    int.Parse(inDate.Substring(4, 2)),
                                    int.Parse(inDate.Substring(6, 2)),
                                    int.Parse(inDate.Substring(8, 2)),
                                    int.Parse(inDate.Substring(10, 2)),
                                    int.Parse(inDate.Substring(12, 2))
                                    );
            }
            else if (inDate.Length == (7 + 6))
            {
                // check the validity of MingGuo with time
                if (!ValidationHelper.IsMingGuoString(inDate.Substring(0, 7)))
                    throw new ArgumentException("The input string is not a valid MingGuo date!");
                if (!ValidationHelper.IsTimeString(inDate.Substring(7, 6)))
                    throw new ArgumentException("The input string is not a valid time!");
                return new DateTime(int.Parse(inDate.Substring(0, 3)),
                                    int.Parse(inDate.Substring(3, 2)),
                                    int.Parse(inDate.Substring(5, 2)),
                                    int.Parse(inDate.Substring(7, 2)),
                                    int.Parse(inDate.Substring(9, 2)),
                                    int.Parse(inDate.Substring(11, 2))
                                    );
            }
            else
            {
                // error!
                throw new ArgumentException("The input string is not an A.D. or MingGuo date!");
            }
        }
    }
}
