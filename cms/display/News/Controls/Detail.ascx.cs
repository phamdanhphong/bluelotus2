using System;
using System.Data;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.TSql;
public partial class cms_display_News_Controls_Detail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDetail();
        }
    }
    void LoadDetail()
    {
        DataTable dt = (DataTable)Session["dataByTitle"];//Thông tin chi tiết về Items hoặc Groups đã được gán ở Defualt.aspx vào session
        if (dt.Rows.Count > 0)
        {
            UpdateTotalView(dt.Rows[0][ItemsColumns.IidColumn].ToString());

            ltrTitle.Text = dt.Rows[0][ItemsColumns.VititleColumn].ToString();
            ltrDesc.Text = dt.Rows[0][ItemsColumns.VidescColumn].ToString();
            ltrContent.Text = dt.Rows[0][ItemsColumns.VicontentColumn].ToString();         
            if (ltrContent.Text.Length==0)
                ltrContent.Text = "<div class='emptyresult'>" +
                                           LanguageItemExtension.GetnLanguageItemTitleByName("Nội dung bài viết đang được chúng tôi cập nhật. Cảm ơn quý khách đã quan tâm!") + "</div>";

        }
    }
    private void UpdateTotalView(string iid)
    {
        string[] fields = { "IITOTALVIEW" };
        string[] values = { "IITOTALVIEW + 1" };
        Items.UpdateItems(DataExtension.UpdateTransfer(fields, values), ItemsTSql.GetItemsByIid(iid));
    }
   
}