/// <summary>
/// Summary description for ProductPosition
/// </summary>

namespace Developer.Position
{
    public class ProductPosition
    {
        private string[] values;
        private string[] text;

        public ProductPosition()
        {
            text = new string[]{
                    "Sản phẩm khuyến mại",
                    "Sản phẩm bán chạy",
                    "Sản phẩm mới",
                    "Sản phẩm giá sốc"};
            values = new string[] {
                    "0",
                    "1",
                    "2",
                    "3"};
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
