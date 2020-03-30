using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.CustomerReviewsModul;
using TatThanhJsc.TSql;

public partial class cms_admin_Moduls_CustomerReviews_Item_ShortCutItem : System.Web.UI.UserControl
{
    private string app = CodeApplications.CustomerReviews;
    private string appCate = CodeApplications.CustomerReviews;
        
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string pic = FolderPic.CustomerReviews;    
        
    protected string iid = "";
    private string igid = "";
    private bool insert = false;
    private string suc = "";
    private string p = "";
    private string ni = "";

    string parramSpitString = ",";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["suc"] != null)
            suc = Request.QueryString["suc"];
        if(suc.Equals(TypePage.CreateItem))
            insert = true;

        if (Request.QueryString["iid"] != null)
            iid = Request.QueryString["iid"];
        if (Request.QueryString["igid"] != null)
            igid = Request.QueryString["igid"];
        if (Request.QueryString["p"] != null)
            p = Request.QueryString["p"];
        if (Request.QueryString["ni"] != null)
            ni = Request.QueryString["ni"];
        
       
        #region Gán app, pic cho user control upload ảnh đại diện
        flAnhDaiDien.App = appCate;
        flAnhDaiDien.Pic = pic;
        #endregion

        #region Gán đường dẫn cho ckeditor
        foreach (Control control in this.Controls)
        {
            if (control is CKEditor.NET.CKEditorControl)
                ((CKEditor.NET.CKEditorControl)control).FilebrowserImageBrowseUrl
                    =
                    UrlExtension.WebisteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + pic;
        }
        #endregion
        if (!IsPostBack)
        {            
            GetParentCate();
            InitialControlsValue(insert);
        }
    }

    private string LinkRedrect()
    {
        if(!ni.Equals("") && !p.Equals(""))
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.CustomerReviews + "&igid=" +
                   ddlParentCate.SelectedValue + "&suc=" + TypePage.Item + "&ni=" + ni + "&p=" + p;
        else
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.CustomerReviews + "&igid=" +
                   ddlParentCate.SelectedValue + "&suc=" + TypePage.Item;
    }

    void GetParentCate()
    {
        DropDownListExtension.LoadParentCates(app, lang, ddlParentCate, false);

        if (!igid.Equals(""))
        {
            ddlParentCate.SelectedValue = igid;
        }
    }

    void InitialControlsValue(bool insert)
    {
        #region update
        if (!insert)
        {
            LtInsertUpdate.Text = Developer.CustomerReviewsKeyword.CapNhatBaiViet;
            btOK.Text = "Đồng ý";
            cbTiepTuc.Visible = false;
            string fields = "*";

            string condition = DataExtension.AndConditon(GroupsTSql.GetGroupsByVgapp(appCate), ItemsTSql.GetItemsByIid(iid));

            DataTable dt = GroupsItems.GetAllData("1", fields, condition, "");

            hdGroupsItemId.Value = dt.Rows[0][GroupsItemsColumns.IgiidColumn].ToString();
            ddlParentCate.SelectedValue = dt.Rows[0]["IGID"].ToString();

            tbHoTen.Text = dt.Rows[0][ItemsColumns.VititleColumn].ToString();
            tbEmail.Text = dt.Rows[0][ItemsColumns.ViauthorColumn].ToString();
            tbDiaChi.Text = dt.Rows[0][ItemsColumns.ViurlColumn].ToString();

            tbCongTy.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 1);
            tbChucVu.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 2);
            tbTieuDeYKien.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 3);


            flAnhDaiDien.Load(dt.Rows[0][ItemsColumns.ViimageColumn].ToString());

            #region SEO
            tbSeoLink.Text = dt.Rows[0]["VISEOLINK"].ToString();
            tbSeoTitle.Text = dt.Rows[0]["VISEOTITLE"].ToString();
            tbSeoKeyword.Text = dt.Rows[0]["VISEOMETAKEY"].ToString();
            tbSeoDescription.Text = dt.Rows[0]["VISEOMETADESC"].ToString();
            #endregion

            tbChiTiet.Text = dt.Rows[0][ItemsColumns.VicontentColumn].ToString();
            hdChiTiet.Value = tbChiTiet.Text;

            tbThuTu.Text = dt.Rows[0][ItemsColumns.IiorderColumn].ToString();
            cbTrangThai.Checked = (dt.Rows[0][ItemsColumns.IienableColumn].ToString() == "1");
        
            tbNgayDang.Text = dt.Rows[0][ItemsColumns.DicreatedateColumn].ToString();
            hdTotalView.Value = dt.Rows[0][ItemsColumns.IitotalviewColumn].ToString();
            
        }
        #endregion
        #region  insert
        else
        {
            LtInsertUpdate.Text = Developer.CustomerReviewsKeyword.ThemMoiBaiViet;
            btOK.Text = "Đồng ý";
            tbNgayDang.Text = DateTime.Now.ToString();         
            tbHoTen.Focus();
        }
        #endregion
    }

    void ResetControls()
    {
        #region Reset các textbox, textbox nào có chứa css class là NotReset thì sẽ không bị reset
        foreach (Control control in this.Controls)
        {
            if (control is TextBox)
                if (((TextBox)control).CssClass != "NotReset")
                    ((TextBox)control).Text = "";

            if (control is HiddenField)
                ((HiddenField)control).Value = "";
        }
        #endregion        

        flAnhDaiDien.Reset();        

        tbNgayDang.Text = DateTime.Now.ToString();
        try
        {
            tbThuTu.Text = (Convert.ToInt32(tbThuTu.Text) + 1).ToString();
        }
        catch { }
        tbHoTen.Focus();
    }

    protected void btOK_Click(object sender, EventArgs e)
    {        
        string chiTiet = ContentExtendtions.ProcessStringContent(tbChiTiet.Text, hdChiTiet.Value, pic);
        #region Trạng thái
        string trangThai = "0";
        if (cbTrangThai.Checked == true)        
            trangThai = "1";        
        #endregion        
        #region Seo
        if (tbSeoLink.Text.Trim().Equals(""))
        {
            tbSeoLink.Text = tbHoTen.Text;
        }
        if (tbSeoTitle.Text.Trim().Equals(""))
        {
            tbSeoTitle.Text = tbHoTen.Text;
        }
        if (tbSeoKeyword.Text.Trim().Equals(""))
        {
            tbSeoKeyword.Text = tbHoTen.Text;
        }
        if (tbSeoDescription.Text.Trim().Equals(""))
        {
            tbSeoDescription.Text = "";
        }
        #endregion

        #region Ngày đăng
        if (tbNgayDang.Text == "")
            tbNgayDang.Text = DateTime.Now.ToString();
        #endregion

        string viparams = StringExtension.GhepChuoi("", tbCongTy.Text, tbChucVu.Text, tbTieuDeYKien.Text);
        #region Insert
        if (insert)
        {
            string image = flAnhDaiDien.Save(false, chiTiet);
            GroupsItems.InsertItemsGroupsItems(lang, app, "", tbHoTen.Text, "", chiTiet,
                image, tbDiaChi.Text, tbEmail.Text, tbSeoTitle.Text, tbSeoLink.Text,
                StringExtension.ReplateTitle(tbSeoLink.Text),
                tbSeoKeyword.Text, tbSeoDescription.Text, "", "", "", viparams, "",
                "", "", "", tbNgayDang.Text,
                DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text, ddlParentCate.SelectedValue,
                tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text, trangThai);

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", tbHoTen.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " tạo mới " + tbHoTen.Text);
            #endregion

        }
        #endregion
        #region Update
        else
        {
            string image = flAnhDaiDien.Save(true, chiTiet);

            GroupsItems.DeleteGroupsItems(GroupsItemsTSql.GetGroupsItemsByIgiid(hdGroupsItemId.Value));
            GroupsItems.UpdateItemsGroupsItems(lang, app, "", tbHoTen.Text, "", chiTiet,
                image, tbDiaChi.Text, tbEmail.Text, tbSeoTitle.Text, tbSeoLink.Text,
                StringExtension.ReplateTitle(tbSeoLink.Text),
                tbSeoKeyword.Text, tbSeoDescription.Text, "", "", "", viparams, "",
                "", "", hdTotalView.Value,
                tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text,
                ddlParentCate.SelectedValue, tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(),
                tbThuTu.Text, trangThai, iid);

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", tbHoTen.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " cập nhật " + tbHoTen.Text);
            #endregion
        }
        #endregion

        #region After Insert/Update

        if (cbTiepTuc.Checked == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess",
                "ThongBao(3000,'Đã tạo: " + tbHoTen.Text + "');", true);
            ResetControls();
        }
        else
        {
            Response.Redirect(LinkRedrect());
        }

        #endregion
    }

    
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(LinkRedrect());
    }
}