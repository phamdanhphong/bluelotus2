namespace Developer.Position
{
    public class HotelPosition
    {
        private string[] values;
        private string[] text;

        public HotelPosition()
        {
            text = new string[]
            {
                "Nhóm hotel trang chủ"
                , "Nhóm hotel bên phải web"
            };
            values = new string[]
            {
                "0"
                , "1"
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
