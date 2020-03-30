using TatThanhJsc.MenuModul;

/// <summary>
/// Lưu các cấu hình cho modul menu
/// </summary>
public class MenuConfig
{
    #region Các menu
    private string[] values;
    private string[] text;
    #endregion

    #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn
    private string[] valuesModul;
    private string[] textModul;
    private string[] appsModul;
    #endregion

    public MenuConfig()
    {
        #region Các menu

        text = new string[]
        {
            "menu chính"
            , "menu cuối trang"
        };
        values = new string[]
        {
            CodeApplications.MenuMain
            , CodeApplications.MenuBottom
        };
        #endregion

        #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn

        textModul = new string[]
        {
            "Chọn modul",
            "Home",
            "Giới thiệu",
            "Tiệc cưới",// - AboutUs - gioi-thieu         
            "Hội thảo", // - News - tin-tuc          
            "Nhà hàng",
            "Cafe",
            "Dịch vụ khác",
            "Tin tức",
            "Liên hệ"
        };
        valuesModul = new string[]
        {
            "",//"Chọn modul",
            "/",//"Home",            
            "?go=" + RewriteExtension.AboutUs,
            "?go=" + RewriteExtension.Product,//"Giới thiệu", // - AboutUs - gioi-thieu           
            "?go=" + RewriteExtension.Service,
            "?go=" + RewriteExtension.QA,
            "?go=" + RewriteExtension.Customer,
            "?go=" + RewriteExtension.FileLibrary2,
            "?go=" + RewriteExtension.News,//"Tin tức", // - News - tin-tuc        
            "?go=" + RewriteExtension.ContactUs
        };
        appsModul = new string[]
        {
            "",//"Chọn modul",
            "",//"Home",
            TatThanhJsc.AboutUsModul.CodeApplications.AboutUs,
            TatThanhJsc.ProductModul.CodeApplications.Product,
            TatThanhJsc.ServiceModul.CodeApplications.Service,
            TatThanhJsc.QAModul.CodeApplications.QA,
            TatThanhJsc.CustomerModul.CodeApplications.Customer,
            TatThanhJsc.FileLibrary2Modul.CodeApplications.FileLibrary2,
            TatThanhJsc.NewsModul.CodeApplications.News,
            TatThanhJsc.ContactModul.CodeApplications.Contact,
        };
        #endregion
    }

    #region Các menu
    /// <summary>
    /// Danh sách tên của menu, vd: menu chính, menu trên...
    /// </summary>
    public string[] Text
    {
        get { return text; }
    }

    /// <summary>
    /// Danh sách tên của app, vd: CodeApplications.MenuMain, CodeApplications.MenuTop...
    /// </summary>
    public string[] Values
    {
        get { return values; }
    }
    #endregion

    #region Các modul để liệt kê khi tạo menu và chọn modul có sẵn
    /// <summary>
    /// Danh sách tên của modul, vd: Tin tức, Sản phẩm...
    /// </summary>
    public string[] TextModul
    {
        get { return textModul; }
    }

    /// <summary>
    /// Danh sách tên của modul, vd: "?go="+RewriteExtension.Product, "?go="+RewriteExtension.News...
    /// </summary>
    public string[] ValuesModul
    {
        get { return valuesModul; }
    }

    /// <summary>
    /// Danh sách app của modul, vd: CodeApplication.Product, CodeApplication.News...
    /// </summary>
    public string[] AppsModul
    {
        get { return appsModul; }
    }
    #endregion
}