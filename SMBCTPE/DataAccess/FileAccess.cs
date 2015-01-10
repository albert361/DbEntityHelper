using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DbEntityHelper.DataAccess
{
    /// <summary>
    /// File Access class with several static functions for opening and saving files
    /// </summary>
    public class FileAccess
    {
        /// <summary>
        /// Create a file and return its stream, then you can user BinaryReader/BinaryWriter for read/write.
        /// </summary>
        /// <param name="filename">filename with path</param>
        /// <returns>file stream</returns>
        public static Stream CreateFile(string filename)
        {
            return File.Open(filename, FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
        }

        /// <summary>
        /// Open a file and return its stream, then you can user BinaryReader/BinaryWriter for read/write.
        /// </summary>
        /// <param name="filename">filename with path</param>
        /// <returns>file stream</returns>
        public static Stream OpenFile(string filename)
        {
            return File.Open(filename, FileMode.Open, System.IO.FileAccess.ReadWrite);
        }
    }
}
