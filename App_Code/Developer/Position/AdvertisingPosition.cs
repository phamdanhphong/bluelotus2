/// <summary>
/// Summary description for ContentPosition
/// </summary>

namespace Developer.Position
{
    public class AdvertisingPosition
    {
        private string[] values;
        private string[] text;

        public AdvertisingPosition()
        {
            text = new string[]
            {
                "Logo đầu trang","Logo cuối trang", "Slide trang chủ",
               "GIỚI THIỆU BLUE LOTUS","Các dịch vụ hội thảo",
                "OFFEE AND TEA","BUFFER SEN XANH", "LIÊN HỆ ĐẶT BÀN",
                "mạng xã hội cuối trang","Instagram"
                
            };
            values = new string[]
            {
                "0", "1", "2",
                "4","5",
                "6","7","8","9",
                "10"

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

