using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FileLibraryPosition
/// </summary>

namespace Developer.Position
{
    public class FileLibraryPosition
    {
        private string[] values;
        private string[] text;

        public FileLibraryPosition()
        {
            text = new string[]{"Nhóm dữ liệu 1 - không dùng", "Nhóm dữ liệu 2- không dùng", "Nhóm dữ liệu trang thư viện"};
            values = new string[] {"0","1","2"};
        }
        public string[] Text
        {
            get { return text; }
        }
        public string[] Values
        {
            get { return values; }
        }
    }
}
