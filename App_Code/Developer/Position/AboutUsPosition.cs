﻿namespace Developer.Position
{
    public class AboutUsPosition
    {
        private string[] values;
        private string[] text;

        public AboutUsPosition()
        {
            text = new string[]
            {
                "Nhóm 1"
            };
            values = new string[]
            {
                "0"
            };
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
