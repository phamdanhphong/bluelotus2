/// <summary>
/// Summary description for NewsPosition
/// </summary>

namespace Developer.Position
{
    public class NewPosition
    {
        private string[] values;
        private string[] text;

        public NewPosition()
        {
            text = new string[]{"Chương trình","Xem nhiều nhất"};
            values = new string[] {"1","2"};
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
