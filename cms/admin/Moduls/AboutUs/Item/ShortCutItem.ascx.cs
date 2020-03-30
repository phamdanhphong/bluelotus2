using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Developer;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.AboutUsModul;
using TatThanhJsc.TSql;

public partial class cms_admin_Moduls_AboutUs_Item_ShortCutItem : System.Web.UI.UserControl
{
    private string app = CodeApplications.AboutUs;
    private string appCate = CodeApplications.AboutUs;
        
    private string lang = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string pic = FolderPic.AboutUs;    
        
    protected string iid = "";
    private string igid = "";
    private bool insert = false;
    private string suc = "";
    private string p = "";
    private string ni = "";

    string parramSpitString = ",";

    #region Tên các trường sẽ thay đổi khi nhập bài viết cho danh mục thuộc loại đội ngũ nhân sự
    protected string tieuDe = AboutUsKeyword.TieuDe;//Tiêu đề - Họ tên
    protected string moTa = AboutUsKeyword.MoTa;//Mô tả - Chức vụ

    #endregion

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


        SetEnableControl();
        if (!IsPostBack)
        {            
            GetParentCate();
            InitialControlsValue(insert);
        }
    }

    private void SetEnableControl()
    {
        plFacebook.Visible = AboutUsConfig.EnableFacebook;
        plGooglePlus.Visible = AboutUsConfig.EnableGooglePlus;
        plTwitter.Visible = AboutUsConfig.EnableTwitter;
        plYoutube.Visible = AboutUsConfig.EnableYoutube;
        plInstagram.Visible = AboutUsConfig.EnableInstagram;
        plPhone.Visible = AboutUsConfig.EnablePhone;
        plEmail.Visible = AboutUsConfig.EnableEmail;
        plSkype.Visible = AboutUsConfig.EnableSkype;
        plViber.Visible = AboutUsConfig.EnableViber;
        plZalo.Visible = AboutUsConfig.EnableZalo;
    }

    private string LinkRedrect()
    {
        if(!ni.Equals("") && !p.Equals(""))
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.AboutUs + "&igid=" +
                   ddlParentCate.SelectedValue + "&suc=" + TypePage.Item + "&ni=" + ni + "&p=" + p;
        else
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.AboutUs + "&igid=" +
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
            LtInsertUpdate.Text = Developer.AboutUsKeyword.CapNhatBaiViet;
            btOK.Text = "Đồng ý";
            cbTiepTuc.Visible = false;
            string fields = "*";

            string condition = DataExtension.AndConditon(GroupsTSql.GetGroupsByVgapp(appCate), ItemsTSql.GetItemsByIid(iid));

            DataTable dt = GroupsItems.GetAllData("1", fields, condition, "");

            hdGroupsItemId.Value = dt.Rows[0][GroupsItemsColumns.IgiidColumn].ToString();
            ddlParentCate.SelectedValue = dt.Rows[0]["IGID"].ToString();

            tbTitle.Text = dt.Rows[0][ItemsColumns.VititleColumn].ToString();
            tbKey.Text = dt.Rows[0][ItemsColumns.VikeyColumn].ToString();
            tbDesc.Text = dt.Rows[0][ItemsColumns.VidescColumn].ToString();

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

            #region Các thông tin phụ

            tbFacebook.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 1);
            tbGooglePlus.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 2);
            tbTwitter.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 3);
            tbYoutube.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 4);
            tbInstagram.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 5);

            tbPhone.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 6);
            tbEmail.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 7);
            tbSkype.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 8);
            tbViber.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 9);
            tbZalo.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViParams].ToString(), "", 10);

            #endregion

        }
        #endregion
        #region  insert
        else
        {
            LtInsertUpdate.Text = Developer.AboutUsKeyword.ThemMoiBaiViet;
            btOK.Text = "Đồng ý";
            tbNgayDang.Text = DateTime.Now.ToString();         
            tbTitle.Focus();
        }
        #endregion

        SetInputFormByCate();
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
        tbTitle.Focus();
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
            tbSeoLink.Text = tbTitle.Text;
        }
        if (tbSeoTitle.Text.Trim().Equals(""))
        {
            tbSeoTitle.Text = tbTitle.Text;
        }
        if (tbSeoKeyword.Text.Trim().Equals(""))
        {
            tbSeoKeyword.Text = tbTitle.Text;
        }
        if (tbSeoDescription.Text.Trim().Equals(""))
        {
            tbSeoDescription.Text = tbDesc.Text;
        }
        #endregion

        #region Ngày đăng
        if (tbNgayDang.Text == "")
            tbNgayDang.Text = DateTime.Now.ToString();
        #endregion

        #region Các thông tin liên hệ khác như Facebook, Google +, Twitter,...
        string subInfos = StringExtension.GhepChuoi("", tbFacebook.Text,tbGooglePlus.Text,tbTwitter.Text,tbYoutube.Text,tbInstagram.Text,tbPhone.Text,tbEmail.Text,tbSkype.Text,tbViber.Text,tbZalo.Text);
        #endregion

        #region Insert
        if (insert)
        {
            string image = flAnhDaiDien.Save(false, chiTiet);
            GroupsItems.InsertItemsGroupsItems(lang, app, tbKey.Text, tbTitle.Text, tbDesc.Text, chiTiet,
                image, "", "", tbSeoTitle.Text, tbSeoLink.Text,
                StringExtension.ReplateTitle(tbSeoLink.Text),
                tbSeoKeyword.Text, tbSeoDescription.Text, "", "", "", subInfos, "",
                "", "", "", tbNgayDang.Text,
                DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text, ddlParentCate.SelectedValue,
                tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text, trangThai);

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", tbTitle.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " tạo mới " + tbTitle.Text);
            #endregion

        }
        #endregion
        #region Update
        else
        {
            string image = flAnhDaiDien.Save(true, chiTiet);

            GroupsItems.DeleteGroupsItems(GroupsItemsTSql.GetGroupsItemsByIgiid(hdGroupsItemId.Value));
            GroupsItems.UpdateItemsGroupsItems(lang, app, tbKey.Text, tbTitle.Text, tbDesc.Text, chiTiet,
                image, "", "", tbSeoTitle.Text, tbSeoLink.Text,
                StringExtension.ReplateTitle(tbSeoLink.Text),
                tbSeoKeyword.Text, tbSeoDescription.Text, "", "", "", subInfos, "",
                "", "", hdTotalView.Value,
                tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(), tbThuTu.Text,
                ddlParentCate.SelectedValue, tbNgayDang.Text, DateTime.Now.ToString(), DateTime.Now.ToString(),
                tbThuTu.Text, trangThai, iid);

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", tbTitle.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " cập nhật " + tbTitle.Text);
            #endregion
        }
        #endregion

        #region After Insert/Update

        if (cbTiepTuc.Checked == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess",
                "ThongBao(3000,'Đã tạo: " + tbTitle.Text + "');", true);
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

    protected void ddlParentCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetInputFormByCate();
    }

    private void SetInputFormByCate()
    {
        if (CateIsDoiNguNhanSu(ddlParentCate.SelectedValue))
        {
            tieuDe = "Họ tên";
            moTa = "Chức vụ";

            plNicks.Visible = true;
            plMa.Visible = false;
        }
        else
        {
           tieuDe = AboutUsKeyword.TieuDe;//Tiêu đề - Họ tên
            moTa = AboutUsKeyword.MoTa;//Mô tả - Chức vụ 

            plNicks.Visible = false;
            plMa.Visible = true;
        }
    }

    private bool CateIsDoiNguNhanSu(string cateId)
    {
        DataTable dt = Groups.GetGroups("1", GroupsColumns.IgTotalItems, GroupsTSql.GetById(cateId), "");
        if(dt.Rows.Count>0)
            if (dt.Rows[0][GroupsColumns.IgTotalItems].ToString() == "1")
                return true;

        return false;
    }
}