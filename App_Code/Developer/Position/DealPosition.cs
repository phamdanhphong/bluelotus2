using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DealPosition
/// </summary>

namespace Developer.Position
{
    public class DealPosition
    {
        private string[] values;
        private string[] text;

        public DealPosition()
        {
            text = new string[]{"Nhóm deal 1", "Nhóm deal 2", "Nhóm deal 3"};
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
