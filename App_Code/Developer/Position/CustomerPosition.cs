/// <summary>
/// Summary description for CustomerPosition
/// </summary>

namespace Developer.Position
{
    public class CustomerPosition
    {
        private string[] values;
        private string[] text;

        public CustomerPosition()
        {
            text = new string[] { "Nhóm 1" };
            values = new string[] {"0"};
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
