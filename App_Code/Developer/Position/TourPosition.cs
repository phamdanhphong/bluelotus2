namespace Developer.Position
{
    public class TourPosition
    {
        private string[] values;
        private string[] text;

        public TourPosition()
        {
            text = new string[]
            {
                "Nhóm tour trang chủ" //0
                , "Nhóm tour bên phải web" //1
                , "Nhóm tour trang chủ - bên dưới" //2
            };
            values = new string[]
            {
                "0"
                , "1"
                , "2"
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
