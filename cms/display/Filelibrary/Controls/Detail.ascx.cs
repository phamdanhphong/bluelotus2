using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.FileLibraryModul;
using TatThanhJsc.TSql;
using System.IO;
public partial class cms_display_Filelibrary_Controls_Category : System.Web.UI.UserControl
{
    #region Các thông số cần chỉnh theo từng modul (News, News, News...)
    private string app = CodeApplications.FileLibrary;
    protected string rewrite = RewriteExtension.FileLibrary;
    private string pic = FolderPic.FileLibrary;
    private string maxItemKey = SettingKey.SoFileLibraryTrenTrangDanhMuc;
    #endregion

    #region Các thông số chung
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueDisplay();
    protected string title = "";
    string igid = "";
    string p = "1";
    int rows = 6;
    string key = "";
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dataByTitle"]!=null)
            {
                DataTable dt = (DataTable)Session["dataByTitle"];
                if (dt.Rows.Count>0)
                {
                    ltrTitle.Text = dt.Rows[0][ItemsColumns.VititleColumn].ToString();
                    string content = dt.Rows[0][ItemsColumns.VicontentColumn].ToString();
                    string linkDownLoad = dt.Rows[0][ItemsColumns.ViauthorColumn].ToString();
                    string linkChiTiet = dt.Rows[0][ItemsColumns.ViurlColumn].ToString();
                    string dungluong = "";
                    linkChiTiet = pic + "/" + linkChiTiet;
                    if (File.Exists(Server.MapPath("\\" + linkChiTiet)))
                    {
                        FileInfo fi = new FileInfo(Server.MapPath("\\" + linkChiTiet));
                        dungluong = Math.Round(fi.Length / ((decimal)1024*1024), 2).ToString();
                    }
                    if (dungluong!="")
                    {
                        linkDownLoad = @"<a class='download' href='" + linkChiTiet + "'download >Download (" + dungluong + "MB)</a>";
                        linkChiTiet = @"
                        <a class='khungAnhCrop'>
                            <iframe allowtransparency='true' style='width: 73.5%; height: 452px; margin: 0 auto; background: #000;' id='loaddocdetail2' name='loaddocdetail2' frameborder='0' scrolling='no' src='" + linkChiTiet + @"'></iframe>
                        </a>";
                    }        
                    if (dungluong==""&&content=="")
                    {
                        ltrContent.Text = "<div class='emptyresult'>" +
                                          LanguageItemExtension.GetnLanguageItemTitleByName("Nội dung bài viết đang được chúng tôi cập nhật. Cảm ơn quý khách đã quan tâm!") + "</div>";
                    }
                    else
                    {
                        ltrContent.Text = @"
                        "+content+@"
                        <div class='doconline'>
                            <div class='khungAnh'>
                                " + linkChiTiet + @"
                            </div>
                            " + linkDownLoad + @"
                        </div>";
                    }
                    
                   
                }
            }
        }
    }
    // Lấy danh sách các nhóm tin
    


}