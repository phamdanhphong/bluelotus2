using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.ProductModul;
using TatThanhJsc.TSql;

public partial class cms_admin_Moduls_Product_Item_ShortCutItem : System.Web.UI.UserControl
{
    private string app = CodeApplications.Product;
    private string language = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string pic = FolderPic.Product;
    private string propertyModul = CodeApplications.ProductProperty;
    
    private string iid = "";
    private string igid = "";
    private bool insert = false;
    private string hd_insert_update = "";
    private string p = "";
    private string ni = "";

    private string top = "";
    private string fields = "";
    private string condition = "";
    private string orderBy = "";

    string parramSpitString = ",";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["suc"] != null)
            hd_insert_update = Request.QueryString["suc"];
        if (Request.QueryString["iid"] != null)
            iid = Request.QueryString["iid"];
        if (Request.QueryString["igid"] != null)
            igid = Request.QueryString["igid"];
        if (Request.QueryString["p"] != null)
            p = Request.QueryString["p"];
        if (Request.QueryString["ni"] != null)
            ni = Request.QueryString["ni"];
        if (hd_insert_update.Equals("CreateItem"))
            insert = true;

        Index1.btnHandler += new cms_api_Product_Item_Index.OnButtonClick(WebUserControl1_btnHandler);

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
            SetEnableControls();
            GetGroupsInDdl();
            GetNicks();
            GetProperties();
            InitialControlsValue(insert);
            KhoiTaoXuLyAnh();
        }
    }

    

    #region Khởi tạo các thông tin bổ xung
    #region NickHoTro
    void GetNicks()
    {
        if (ProductConfig.KeyHienThiAddNickChoSanPham)
        {
            fields = "*";
            condition = DataExtension.AndConditon
                (
                GroupsTSql.GetGroupsByVglang(language),
                GroupsTSql.GetGroupsByVgapp(TatThanhJsc.OtherModul.CodeApplications.SupportOnline),
                ItemsTSql.GetItemsByIienable("1")
                );
            orderBy = " Groups.igid asc, " + GroupsItemsColumns.IorderColumn + " asc ";
            DataTable dt = new DataTable();
            dt = GroupsItems.GetAllData("", fields, condition, orderBy);
            rptNicks.DataSource = dt;
            rptNicks.DataBind();
        }
    }
    #endregion
    #region ThuocTinhSanPham
    void GetProperties()
    {
        if (ProductConfig.KeyHienThiQuanLyThuocTinhSanPham)
        {
            fields = "*";
            condition = DataExtension.AndConditon
                (
                GroupsTSql.GetGroupsByVglang(language),
                GroupsTSql.GetGroupsByVgapp(propertyModul),
                " IGENABLE <> '2' "
                );
            orderBy = GroupsColumns.IgorderColumn;
            DataTable dt = new DataTable();
            dt = Groups.GetGroups("", fields, condition, orderBy);
            rptProperties.DataSource = dt;
            rptProperties.DataBind();
        }
    }
    #endregion
    #region ThuocTinhLocSanPham
    string appFilter = CodeApplications.ProductFilterProperties;
    /// <summary>
    /// Lấy danh igid các thuộc tính lọc đã được add vào danh mục (kết quả trả về dạng ,igid1,igid2,)
    /// </summary>
    /// <returns></returns>
    string GetListFilterProperties()
    {
        top = ""; fields = GroupsColumns.VgparamsColumn + "," + GroupsColumns.VgnameColumn;
        orderBy = "";
        condition = GroupsTSql.GetGroupsByIgid(ddl_group_product.SelectedValue);
        DataTable dt = new DataTable();
        dt = Groups.GetGroups(top, fields, condition, orderBy);
        if (dt.Rows.Count > 0)
            return dt.Rows[0][GroupsColumns.VgparamsColumn].ToString();
        else
            return "";
    }

    /// <summary>
    /// Lấy danh sách các thuộc tính đã được add cho danh mục
    /// </summary>
    void GetFilterProperties()
    {
        if (ProductConfig.KeyHienThiThuocTinhLocSanPham)
        {
            condition = DataExtension.AndConditon(
                        GroupsTSql.GetGroupsByVglang(language),
                        GroupsTSql.GetGroupsByVgapp(appFilter),
                        GroupsTSql.GetGroupsByIgenable("1"),
                        "charindex('" + parramSpitString + "'+cast(" + GroupsColumns.IgidColumn + " as varchar(10))+'" + parramSpitString + "','" + GetListFilterProperties() + "') >0");
            DataTable dt = new DataTable();
            dt = Groups.GetGroups("", "*", condition, "");

            rptParentFilter.DataSource = dt;
            rptParentFilter.DataBind();
        }
    }
    /// <summary>
    /// Lấy danh sách các thuộc tính lọc con
    /// </summary>
    /// <param name="igid">igid của thuộc tính cha</param>
    /// <param name="vgparrams">parram của thuộc tính cha (lưu thiết lập cho chọn 1 hoặc nhiều thuộc tính con)</param>
    /// <param name="allowMultipSelect">0: chỉ lấy ra thuộc tính con nếu thuộc tính cha thiết lập chỉ cho chọn 1 thuộc tính con/1 sp</param>
    /// <returns></returns>
    protected DataTable GetSubFilter(string igid, string vgparrams, string allowMultipSelect)
    {
        DataTable dtSubs = new DataTable();
        if (vgparrams == allowMultipSelect)
        {
            fields = GroupsColumns.IgidColumn + "," + GroupsColumns.VgnameColumn;
            condition = DataExtension.AndConditon
                (
                GroupsTSql.GetGroupsByVgapp(appFilter),
                GroupsTSql.GetGroupsByIgparentid(igid),
                GroupsTSql.GetGroupsByIgenable("1")
                );
            dtSubs = Groups.GetGroups("", fields, condition, "");
        }
        return dtSubs;
    }
    #endregion
    #endregion

    #region Kiểm tra các chức năng phụ(add nick, thuộc tính, lọc...) có được hiển thị hay không
    /// <summary>
    /// Hiển thị hoặc ẩn các chức năng theo thiết lập trong bảng Settings
    /// </summary>
    void SetEnableControls()
    {
        //Ẩn hiện chức năng Add nick
        if (ProductConfig.KeyHienThiAddNickChoSanPham) pnAddNickHoTroTrucTuyen.Visible = true;
        else pnAddNickHoTroTrucTuyen.Visible = false;

        //Ẩn hiện chức năng Thuộc tính sản phẩm
        if (ProductConfig.KeyHienThiQuanLyThuocTinhSanPham) pnThuocTinhSanPham.Visible = true;
        else pnThuocTinhSanPham.Visible = false;

        //Ẩn hiện chức năng Thuộc tính lọc
        if (ProductConfig.KeyHienThiThuocTinhLocSanPham) pnThuocTinhLoc.Visible = true;
        else pnThuocTinhLoc.Visible = false;

        if (ProductConfig.KeyThemSanPhamVaoNhieuDanhMuc) pnCacDanhMucKhac.Visible = true;
        else pnCacDanhMucKhac.Visible = false;
    }
    #endregion

    void KhoiTaoXuLyAnh()
    {
        #region Đóng dấu ảnh
        if (SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct, language) == "1")
            cbDongDauAnh.Checked = true;
        else
            cbDongDauAnh.Checked = false;
        #region Ảnh làm dấu
        hdLogoImage.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_AnhDau, language);
        #endregion
        hdViTriDongDau.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_ViTri, language);
        hdLeX.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_LeNgang, language);
        hdLeY.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_LeDoc, language);
        hdTyLe.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_TyLe, language);
        hdTrongSuot.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhProduct_TrongSuot, language);
        #endregion

        #region Hạn chế kích thước ảnh đại diện
        if (SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhProduct, language) == "1")
            cbHanCheKichThuoc.Checked = true;
        else
            cbHanCheKichThuoc.Checked = false;

        tbHanCheW.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhProduct_MaxWidth, language);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhProduct_MaxHeight, language);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        if (SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhProduct, language) == "1")
            cbTaoAnhNho.Checked = true;
        else
            cbTaoAnhNho.Checked = false;

        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhProduct_MaxWidth, language);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhProduct_MaxHeight, language);
        #endregion
    }

    private string LinkRedrect()
    {
        if (!ni.Equals("") && !p.Equals(""))
        {
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.Product + "&igid=" + ddl_group_product.SelectedValue + "&suc=" + TypePage.Item + "&ni=" + ni + "&p=" + p;
        }
        else
        {
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.Product + "&igid=" + ddl_group_product.SelectedValue + "&suc=" + TypePage.Item;
        }
    }

    void GetGroupsInDdl()
    {
        DataTable dt = new DataTable();
        dt = Groups.GetAllGroups("*", DataExtension.AndConditon(GroupsTSql.GetGroupsByVgapp(app) + " AND IGENABLE <> '2' ", GroupsTSql.GetGroupsByVglang(language)), "");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl_group_product.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i]["IGLEVEL"].ToString()) + dt.Rows[i]["VGNAME"].ToString(), dt.Rows[i]["IGID"].ToString()));
            }
        }
        if (!igid.Equals(""))
        {
            ddl_group_product.SelectedValue = igid;
        }

        #region Hiển thị các danh mục ra checkboxlist
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cbListCates.Items.Add(new ListItem(DropDownListExtension.FormatForDdl(dt.Rows[i]["IGLEVEL"].ToString()) + dt.Rows[i]["VGNAME"], dt.Rows[i]["IGID"].ToString()));
        }
        #endregion
    }

    void InitialControlsValue(bool insert)
    {
        #region update
        if (!insert)
        {
            //btn_insert_update.Text = "Đồng ý";
            ckbContinue.Visible = false;
            fields = "*";
            condition = DataExtension.AndConditon(GroupsTSql.GetGroupsByVgapp(app), ItemsTSql.GetItemsByIid(iid));
            DataTable dt = new DataTable();
            dt = GroupsItems.GetAllData("", fields, condition, "");//Sắp xếp theo igiid để bản ghi đầu tiên là danh mục chính --> mặc định thử tục mới lấy ra 1 bản ghi của danh mục chính

            hdigi_id.Value = dt.Rows[0][GroupsItemsColumns.IgiidColumn].ToString();
            
            CheckOtherIgid(dt.Rows[0][ItemsColumns.IidColumn].ToString());

            lnk_delete_Image_current.Visible = true;
            ddl_group_product.SelectedValue = dt.Rows[0]["IGID"].ToString();
            txt_title.Text = dt.Rows[0]["VITITLE"].ToString();
            txt_description.Text = dt.Rows[0]["VIDESC"].ToString();
            string vicontent = dt.Rows[0]["VICONTENT"].ToString();
            txt_content.Text = StringExtension.LayChuoi(vicontent, "", 1);
            txtThongSoKyThuat.Text = StringExtension.LayChuoi(vicontent, "", 2);
            txtVideoGioiThieu.Text = StringExtension.LayChuoi(vicontent, "", 3);
            txtXuatXu.Text = StringExtension.LayChuoi(vicontent, "", 4);
            txtCongXuat.Text = StringExtension.LayChuoi(vicontent, "", 5);
            txtDienAp.Text = StringExtension.LayChuoi(vicontent, "", 6);
            txtLuuLuong.Text = StringExtension.LayChuoi(vicontent, "", 7);
            txtCotAp.Text = StringExtension.LayChuoi(vicontent, "", 8);
            txtTruynDong.Text = StringExtension.LayChuoi(vicontent, "", 9);
            txtVatLieu.Text = StringExtension.LayChuoi(vicontent, "", 10);
            txtBaoHanh.Text = StringExtension.LayChuoi(vicontent, "", 11);
            
            hdOldProduct.Value = StringExtension.LayChuoi(vicontent, "", 1);
            hdThongSoKyThua.Value = StringExtension.LayChuoi(vicontent, "", 2);
            hdVideoGioiThieu.Value = StringExtension.LayChuoi(vicontent, "", 3);
            tbThuongHieu.Text = dt.Rows[0]["VIAUTHOR"].ToString();
            #region SEO
            textLinkRewrite.Text = dt.Rows[0]["VISEOLINK"].ToString();
            textTagTitle.Text = dt.Rows[0]["VISEOTITLE"].ToString();
            textTagKeyword.Text = dt.Rows[0]["VISEOMETAKEY"].ToString();
            textTagDescription.Text = dt.Rows[0]["VISEOMETADESC"].ToString();
            #endregion
            txtCreateDate.Text = dt.Rows[0]["DCREATEDATE"].ToString();

            #region Image
            if (!dt.Rows[0]["VIIMAGE"].ToString().Equals(""))
            {
                ltimg.Text = ImagesExtension.GetImage(pic, dt.Rows[0]["VIIMAGE"].ToString(), "", "imgItem", false, false, "", false);
                lnk_delete_Image_current.Visible = true;
            }
            else
            {
                ltimg.Visible = false;
                lnk_delete_Image_current.Visible = false;
            }
            hd_img.Value = dt.Rows[0]["VIIMAGE"].ToString();

            if (hd_img.Value.Length < 1)
                cbLayAnhTuNoiDung.Checked = true;
            else cbLayAnhTuNoiDung.Checked = false;
            #endregion
            HdIitotalview.Value = dt.Rows[0]["IITOTALVIEW"].ToString();
            #region IIENABLE
            if (dt.Rows[0]["IIENABLE"].ToString().Equals("0"))
            {
                chk_status.Checked = false;
            }
            else
            {
                chk_status.Checked = true;
            }
            #endregion
            string conhang = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 2);
            if (conhang.Equals("1"))
            {
                ckbConHang.Checked = true;
            }
            else
            {
                ckbConHang.Checked = false;
            }
            string hangmoi = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 3);
            if (hangmoi.Equals("1"))
            {
                ckCheckHot.Checked = true;
            }
            else
            {
                ckCheckHot.Checked = false;
            }

            tbPrice.Text = dt.Rows[0][ItemsColumns.FipriceColumn].ToString();
            tbPriceOld.Text = dt.Rows[0][ItemsColumns.FisalepriceColumn].ToString();

            tbKey.Text = dt.Rows[0][ItemsColumns.VikeyColumn].ToString();
            tbOrder.Text = dt.Rows[0][ItemsColumns.IiorderColumn].ToString();

            tbThongSo.Text = StringExtension.LayChuoi(dt.Rows[0][ItemsColumns.ViparamsColumn].ToString(), "", 1);

            //Get File
            #region Files
            //catalog
            string viUrl=dt.Rows[0][ItemsColumns.ViurlColumn].ToString();
            txtLinkCatalog.Text = StringExtension.LayChuoi(viUrl, "", 2);
            string fileNameCatalog = StringExtension.LayChuoi(viUrl, "", 1);
            if (!fileNameCatalog.Equals(""))
            {
                ltrFileCatalog.Text = "Tệp hiện tại: <a href='" + TatThanhJsc.Extension.UrlExtension.WebisteUrl + pic + "/" +
                             fileNameCatalog + "' target='_blank'>" +
                             fileNameCatalog + "</a>";
                linkDeleteFileCatalog.Visible = true;
                // lưu giá trị > insert and update
                hdFileCatalog.Value = fileNameCatalog;
            }

            // document
            txtLinkDocument.Text = StringExtension.LayChuoi(viUrl, "", 4);
            string fileNameDocument = StringExtension.LayChuoi(viUrl, "", 3);
            if (!fileNameDocument.Equals(""))
            {
                ltrDoc.Text = "Tệp hiện tại: <a href='" + TatThanhJsc.Extension.UrlExtension.WebisteUrl + pic + "/" +
                             fileNameDocument + "' target='_blank'>" +
                             fileNameDocument + "</a>";
                linkDeleteDocument.Visible = true;
                // lưu giá trị > insert and update
                hdFileDocument.Value = fileNameDocument;
            }
            
            #endregion

            GetFilterProperties();           
 
            #region ThuocTinhSanPham-Chi thực hiện khi chức năng Quản lý thuộc tính được hiển thị
            if (ProductConfig.KeyHienThiQuanLyThuocTinhSanPham)
            {
                string properties = "";
                condition = DataExtension.AndConditon(
                    SubitemsTSql.GetSubitemsByIid(iid),
                    SubitemsTSql.GetSubitemsByVskey(propertyModul));
                fields = SubitemsColumns.VscontentColumn;
                dt = Subitems.GetSubItems("", fields, condition, "");
                if (dt.Rows.Count > 0)
                    properties = dt.Rows[0][SubitemsColumns.VscontentColumn].ToString();                
                for (int i = 0; i < rptProperties.Items.Count; i++)
                {
                    CheckBox checkBoxProperties = (CheckBox)rptProperties.Items[i].FindControl("checkBoxProperties");                    
                    if (properties.IndexOf(parramSpitString + checkBoxProperties.ToolTip + parramSpitString) > -1)
                    {
                        checkBoxProperties.Checked = true;                        
                    }
                    else
                        checkBoxProperties.Checked = false;
                }
            }
            #endregion

            #region Nicks- Chỉ hiển thị khi chức năng add nick được hiển thị
            if (ProductConfig.KeyHienThiAddNickChoSanPham)
            {
                string nicks = "";
                condition = DataExtension.AndConditon(
                    SubitemsTSql.GetSubitemsByIid(iid),
                    SubitemsTSql.GetSubitemsByVskey(TatThanhJsc.OtherModul.CodeApplications.SupportOnline));
                fields = SubitemsColumns.VscontentColumn;
                dt = Subitems.GetSubItems("", fields, condition, "");
                if (dt.Rows.Count > 0)
                    nicks = dt.Rows[0][SubitemsColumns.VscontentColumn].ToString();
                for (int i = 0; i < rptNicks.Items.Count; i++)
                {
                    CheckBox checkBoxNicks = (CheckBox)rptNicks.Items[i].FindControl("checkBoxNicks");
                    if (nicks.IndexOf(parramSpitString + checkBoxNicks.ToolTip + parramSpitString) > -1)
                        checkBoxNicks.Checked = true;
                    else
                        checkBoxNicks.Checked = false;
                }
            }
            #endregion

            #region ThuocTinhLoc- Chỉ hiển thị khi chức năng quản lý thuộc tính lọc được hiển thị
            string filterProperties = "";
            condition = DataExtension.AndConditon(
                SubitemsTSql.GetSubitemsByIid(iid),
                SubitemsTSql.GetSubitemsByVskey(CodeApplications.ProductFilterProperties));
            fields = SubitemsColumns.VscontentColumn;
            dt = Subitems.GetSubItems("", fields, condition, "");
            if (dt.Rows.Count > 0)
                filterProperties = dt.Rows[0][SubitemsColumns.VscontentColumn].ToString();
            for (int i = 0; i < rptParentFilter.Items.Count; i++)
            {
                //Đánh dấu radiobuttonlist
                RadioButtonList rdblListAnswer = (RadioButtonList)rptParentFilter.Items[i].FindControl("rdblAnswer");
                if (rdblListAnswer != null)
                {
                    for (int j = 0; j < rdblListAnswer.Items.Count; j++)
                    {
                        if (filterProperties.IndexOf(parramSpitString + rdblListAnswer.Items[j].Value + parramSpitString) > -1)
                            rdblListAnswer.Items[j].Selected = true;
                        else
                            rdblListAnswer.Items[j].Selected = false;
                    }
                }

                //Đánh dấu checkboxlist
                CheckBoxList cblListAnswer = (CheckBoxList)rptParentFilter.Items[i].FindControl("cblAnswer");
                if (cblListAnswer != null)
                {
                    for (int j = 0; j < cblListAnswer.Items.Count; j++)
                    {
                        if (filterProperties.IndexOf(parramSpitString + cblListAnswer.Items[j].Value + parramSpitString) > -1)
                            cblListAnswer.Items[j].Selected = true;
                        else
                            cblListAnswer.Items[j].Selected = false;
                    }
                }
            }
            #endregion

        }
        #endregion
        #region  insert
        else
        {
            //btn_insert_update.Text = "Đồng ý";
            txtCreateDate.Text = DateTime.Now.ToString();
            GetFilterProperties();
        }
        #endregion
    }

    private void CheckOtherIgid(string iid)
    {
        DataTable dt = GroupsItems.GetGroupsItems("", GroupsItemsColumns.IgidColumn,
            GroupsItemsTSql.GetGroupsItemsByIid(iid), "");

        string listigid = ",";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            listigid += dt.Rows[i][GroupsColumns.IgidColumn].ToString() + ",";
        }
        //hdIgid.Value = listigid;
        foreach (ListItem item in cbListCates.Items)
        {
            if (listigid.IndexOf("," + item.Value + ",") > -1)
                item.Selected = true;
        }
    }

    void ResetControls()
    {
        txt_title.Text = "";
        txt_description.Text = "";
        txt_content.Text = "";
        txtVideoGioiThieu.Text = "";
        txtThongSoKyThuat.Text = "";

        
        txtXuatXu.Text = "";
        txtDienAp.Text = "";
        txtLuuLuong.Text = "";
        txtCotAp.Text = "";
        txtVatLieu.Text = "";
        txtBaoHanh.Text = "";       

        hdOldProduct.Value = "";
        hdThongSoKyThua.Value = "";
        hdVideoGioiThieu.Value = "";
        txtCreateDate.Text = DateTime.Now.ToString();
        ltimg.Text = "";
        hd_img.Value = "";        
        textLinkRewrite.Text = "";
        textTagTitle.Text = "";
        textTagKeyword.Text = "";
        textTagDescription.Text = "";

        tbPrice.Text = "";
        tbPriceOld.Text = "";

        cbListCates.ClearSelection();

        tbThongSo.Text = "";

        try
        {
            tbOrder.Text = (Convert.ToInt32(tbOrder.Text) + 1).ToString();
        }
        catch { }
        
        txt_title.Focus();

        // nhatpd add
        hdFileDocument.Value = "";
        hdFileCatalog.Value = "";
    }

    
    protected void btn_insert_update_Click(object sender, EventArgs e)
    {
        WebUserControl1_btnHandler("");
    }
    
    protected void WebUserControl1_btnHandler(string strValue)
    {
        #region Image
        string vimg = "";
        string vimg_thumb = "";
        string contentDetail =StringExtension.GhepChuoi("",
            ContentExtendtions.ProcessStringContent(txt_content.Text, hdOldProduct.Value, pic),
            ContentExtendtions.ProcessStringContent(txtThongSoKyThuat.Text, hdThongSoKyThua.Value, pic),
            ContentExtendtions.ProcessStringContent(txtVideoGioiThieu.Text, hdVideoGioiThieu.Value, pic),
            txtXuatXu.Text,txtCongXuat.Text,txtDienAp.Text,txtLuuLuong.Text,txtCotAp.Text,txtTruynDong.Text,txtVatLieu.Text,txtBaoHanh.Text);
        if (flimg.PostedFile.ContentLength > 0)
        {
            string filename = flimg.FileName;
            string fileex = filename.Substring(filename.LastIndexOf("."));
            string path = Request.PhysicalApplicationPath + "/" + pic + "/";
            if (ImagesExtension.ValidType(fileex))
            {
                string fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".") - 1));
                if (fileNotEx.Length > 9) fileNotEx = fileNotEx.Remove(9);
                string ticks = DateTime.Now.Ticks.ToString();
                #region Lưu ảnh đại diện theo 2 trường hợp: tạo ảnh nhỏ hoặc không.
                //Kiểm tra xem có tạo ảnh nhỏ hay ko
                //Nếu không tạo ảnh nhỏ, tên tệp lưu bình thường theo kiểu: tên_tệp.phần_mở_rộng
                //Nếu tạo ảnh nhỏ, tên tệp sẽ theo kiểu: tên_tệp_HasThumb.phần_mở_rộng
                //Khi đó tên tệp ảnh nhỏ sẽ theo kiểu:   tên_tệp_HasThumb_Thumb.phần_mở_rộng
                //Với cách lưu tên ảnh này, khi thực hiện lưu vào csdl chỉ cần lưu tên ảnh gốc
                //khi hiển thị chỉ cần dựa vào tên ảnh gốc để biết ảnh đó có ảnh nhỏ hay không, việc này được thực hiện bởi ImagesExtension.GetImage, lập trình không cần làm gì thêm.
                if (cbTaoAnhNho.Checked)
                    vimg = fileNotEx + "_" + ticks + "_HasThumb" + fileex;
                else
                    vimg = fileNotEx + "_" + ticks + fileex;
                flimg.SaveAs(path + vimg);
                #endregion
                #region Hạn chế kích thước
                if (cbHanCheKichThuoc.Checked)
                    ImagesExtension.ResizeImage(path + vimg, "", tbHanCheW.Text, tbHanCheH.Text);
                #endregion
                #region Đóng dấu ảnh
                if (cbDongDauAnh.Checked)
                    ImagesExtension.CreateWatermark(path + vimg, path + hdLogoImage.Value, hdViTriDongDau.Value, hdLeX.Value, hdLeY.Value, hdTyLe.Value, hdTrongSuot.Value);
                #endregion
                #region Tạo ảnh nhỏ: Thực hiện cuối để đảm bảo ảnh nhỏ cũng có con dấu
                if (cbTaoAnhNho.Checked)
                {
                    vimg_thumb = fileNotEx + "_" + ticks + "_HasThumb_Thumb" + fileex;
                    ImagesExtension.ResizeImage(path + vimg, path + vimg_thumb, tbAnhNhoW.Text, tbAnhNhoH.Text);

                    
                }
                #endregion
                
            }
        }
        else
        {
            if (hd_img.Value.Length < 1 || cbLayAnhTuNoiDung.Checked)//nếu không upload ảnh và cũng không có ảnh cũ -> tìm ảnh đầu tiên trong nội dung làm ảnh đại diện
            {
                if (hd_img.Value.Length > 0)
                    TatThanhJsc.Extension.ImagesExtension.DeleteImageWhenDeleteItem(pic, hd_img.Value);

                string urlImg = ImagesExtension.GetFirstImageInContent(contentDetail);

                if (urlImg.Length > 0)
                {
                    string filename = urlImg;
                    string fileex = filename.Substring(filename.LastIndexOf("."));
                    string path = Request.PhysicalApplicationPath + "/" + pic + "/";
                    if (TatThanhJsc.Extension.ImagesExtension.ValidType(fileex))
                    {
                        string fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".") - 1));
                        if (fileNotEx.Length > 9) fileNotEx = fileNotEx.Remove(9);
                        string ticks = DateTime.Now.Ticks.ToString();
                        #region Lưu ảnh đại diện theo 2 trường hợp: tạo ảnh nhỏ hoặc không.
                        //Kiểm tra xem có tạo ảnh nhỏ hay ko
                        //Nếu không tạo ảnh nhỏ, tên tệp lưu bình thường theo kiểu: tên_tệp.phần_mở_rộng
                        //Nếu tạo ảnh nhỏ, tên tệp sẽ theo kiểu: tên_tệp_HasThumb.phần_mở_rộng
                        //Khi đó tên tệp ảnh nhỏ sẽ theo kiểu:   tên_tệp_HasThumb_Thumb.phần_mở_rộng
                        //Với cách lưu tên ảnh này, khi thực hiện lưu vào csdl chỉ cần lưu tên ảnh gốc
                        //khi hiển thị chỉ cần dựa vào tên ảnh gốc để biết ảnh đó có ảnh nhỏ hay không, việc này được thực hiện bởi TatThanhJsc.Extension.ImagesExtension.GetImage, lập trình không cần làm gì thêm.
                        if (cbTaoAnhNho.Checked)
                            vimg = fileNotEx + "_" + ticks + "_HasThumb";
                        else
                            vimg = fileNotEx + "_" + ticks;

                        if (ImagesExtension.SaveImageFromUrl(path + vimg, urlImg).Length > 0)
                        {
                            vimg += fileex;

                            #region Hạn chế kích thước
                            if (cbHanCheKichThuoc.Checked)
                                ImagesExtension.ResizeImage(path + vimg, "", tbHanCheW.Text, tbHanCheH.Text);
                            #endregion
                            #region Đóng dấu ảnh
                            if (cbDongDauAnh.Checked)
                                ImagesExtension.CreateWatermark(path + vimg, path + hdLogoImage.Value, hdViTriDongDau.Value, hdLeX.Value, hdLeY.Value, hdTyLe.Value, hdTrongSuot.Value);
                            #endregion
                            #region Tạo ảnh nhỏ: Thực hiện cuối để đảm bảo ảnh nhỏ cũng có con dấu
                            if (cbTaoAnhNho.Checked)
                            {
                                vimg_thumb = fileNotEx + "_" + ticks + "_HasThumb_Thumb" + fileex;
                                ImagesExtension.ResizeImage(path + vimg, path + vimg_thumb, tbAnhNhoW.Text, tbAnhNhoH.Text);

                                
                            }
                            #endregion

                            
                        }
                        else
                        {
                            vimg = "";
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion
        #region file
        // file Catalog
        string fileNameCatalog = "";
        string fileLink = txtLinkCatalog.Text;
        if (fileCatalog.PostedFile.ContentLength > 0)
        {
            string filename = fileCatalog.FileName;
            string fileex = filename.Substring(filename.LastIndexOf("."));
            string path = Request.PhysicalApplicationPath + "/" + pic + "/";

            string fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".") - 1));
            if (fileNotEx.Length > 9) fileNotEx = fileNotEx.Remove(9);
            // lấy giá trị random, tránh trường hợp đường dẫn trùng nhau
            string ticks = DateTime.Now.Ticks.ToString();
            #region Lưu tệp đính kèm
            fileNameCatalog = fileNotEx + "_" + ticks + fileex;
            fileCatalog.SaveAs(path + fileNameCatalog);
            #endregion
        }

        //file Document
        string fileNameDocument = "";
        string fileLinkCatalog = txtLinkDocument.Text;
        if (fileDocument.PostedFile.ContentLength > 0)
        {
            string filename = fileDocument.FileName;
            string fileex = filename.Substring(filename.LastIndexOf("."));
            string path = Request.PhysicalApplicationPath + "/" + pic + "/";

            string fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".") - 1));
            if (fileNotEx.Length > 9) fileNotEx = fileNotEx.Remove(9);
            // lấy giá trị random, tránh trường hợp đường dẫn trùng nhau
            string ticks = DateTime.Now.Ticks.ToString();
            #region Lưu tệp đính kèm
            fileNameDocument = fileNotEx + "_" + ticks + fileex;
            fileDocument.SaveAs(path + fileNameDocument);
            #endregion
        }
        #endregion
        #region Status
        string status = "0";
        string conhang = "0";
        string hangmoi = "0";
        if (chk_status.Checked == true)
        {
            status = "1";
        }
        if (ckbConHang.Checked)
        {
            conhang = "1";
        }
        if (ckCheckHot.Checked)
        {
            hangmoi = "1";
        }
        #endregion
        #region Time Create Date
        string timeCreateDate = "";
        timeCreateDate = txtCreateDate.Text;
        #endregion
        #region Seo
        if (textLinkRewrite.Text.Trim().Equals(""))
        {
            textLinkRewrite.Text = txt_title.Text;
        }
        if (textTagTitle.Text.Trim().Equals(""))
        {
            textTagTitle.Text = txt_title.Text;
        }
        if (textTagKeyword.Text.Trim().Equals(""))
        {
            textTagKeyword.Text = txt_title.Text;
        }
        if (textTagDescription.Text.Trim().Equals(""))
        {
            textTagDescription.Text = txt_description.Text;
        }
        #endregion

        string viparams = StringExtension.GhepChuoi("", tbThongSo.Text, conhang,hangmoi);

        // Check lưu file Document and catalog
        if (fileNameCatalog.Equals("")) fileNameCatalog = hdFileCatalog.Value;
        if (fileNameDocument.Equals("")) fileNameDocument = hdFileDocument.Value;
        string viUrl = StringExtension.GhepChuoi("", 
                                                fileNameCatalog, 
                                                txtLinkCatalog.Text, 
                                                fileNameDocument, 
                                                txtLinkDocument.Text);
        #region Insert
        if (insert)
        {
            GroupsItems.InsertItemsGroupsItems(language, app, tbKey.Text, txt_title.Text, txt_description.Text,
                contentDetail, vimg, viUrl, tbThuongHieu.Text, textTagTitle.Text, textLinkRewrite.Text,
                StringExtension.ReplateTitle(textLinkRewrite.Text), textTagKeyword.Text, textTagDescription.Text, txtXuatXu.Text, "",
                "", viparams, tbPrice.Text, tbPriceOld.Text, "", "", timeCreateDate, DateTime.Now.ToString(),
                DateTime.Now.ToString(), tbOrder.Text, ddl_group_product.SelectedValue, timeCreateDate,
                DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, status);

            #region Lay ra iid cua item vua duoc luu
            condition = DataExtension.AndConditon(
                ItemsTSql.GetItemsByDicreatedate(timeCreateDate),
                ItemsTSql.GetItemsByViapp(app));
            DataTable dtInsertedItems = new DataTable();

            dtInsertedItems = GroupsItems.GetAllData("1", "Items.iid", condition, ItemsColumns.IidColumn + " desc");
            if (dtInsertedItems.Rows.Count > 0)
                iid = dtInsertedItems.Rows[0][ItemsColumns.IidColumn].ToString();
            #endregion

        }
        #endregion
        #region Update
        else
        {
            
            if (vimg.Equals(""))
            {
                vimg = hd_img.Value;
            }
            else
            {
                ImagesExtension.DeleteImageWhenDeleteItem(pic, hd_img.Value);
            }
           
            GroupsItems.DeleteGroupsItems(GroupsItemsTSql.GetByIgiid(hdigi_id.Value));
            GroupsItems.UpdateItemsGroupsItems(language, app, tbKey.Text, txt_title.Text, txt_description.Text,
                contentDetail, vimg, viUrl, tbThuongHieu.Text, textTagTitle.Text, textLinkRewrite.Text,
                StringExtension.ReplateTitle(textLinkRewrite.Text), textTagKeyword.Text, textTagDescription.Text, txtXuatXu.Text, "",
                "", viparams, tbPrice.Text, tbPriceOld.Text, "", HdIitotalview.Value, timeCreateDate, DateTime.Now.ToString(),
                DateTime.Now.ToString(), tbOrder.Text, ddl_group_product.SelectedValue, timeCreateDate,
                DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, status, iid);
        }
        #endregion

        #region Thêm vào các danh mục khác

        //string igparentsid = "";
        //foreach (ListItem item in cbListCates.Items)
        //{

        //    if(item.Selected && item.Value != ddl_group_product.SelectedValue)
        //    {
        //        igparentsid = GetParentsId(item.Value);
        //        GroupsItems.InsertGroupsItems(item.Value, iid, igparentsid, DateTime.Now.ToString(),
        //            DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text);
        //    }                
        //}
        #endregion

        #region Properties-Chi thực hiện khi chức năng Quản lý thuộc tính được hiển thị
        if (ProductConfig.KeyHienThiQuanLyThuocTinhSanPham)
        {
            string properties = parramSpitString;
            for (int i = 0; i < rptProperties.Items.Count; i++)
            {
                CheckBox checkBoxProperties = (CheckBox)rptProperties.Items[i].FindControl("checkBoxProperties");
                if (checkBoxProperties.Checked == true)
                    properties += checkBoxProperties.ToolTip + parramSpitString;
            }

            condition = DataExtension.AndConditon(
                SubitemsTSql.GetSubitemsByIid(iid),
                SubitemsTSql.GetSubitemsByVskey(propertyModul));
            fields = DataExtension.GetListColumns(SubitemsColumns.IsidColumn, SubitemsColumns.VscontentColumn);
            DataTable dt = new DataTable();
            dt = Subitems.GetSubItems("", fields, condition, "");

            if (dt.Rows.Count > 0)
            {
                string isid = dt.Rows[0][SubitemsColumns.IsidColumn].ToString();
                //Cap nhat
                Subitems.UpdateSubitems(iid, language, propertyModul, "", properties, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1", isid);
            }
            else
            {
                //Them moi
                Subitems.InsertSubitems(iid, language, propertyModul, "", properties, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1");
            }
        }
        #endregion

        #region Nicks - Chỉ hiển thị khi chức năng add nick được hiển thị
        if (ProductConfig.KeyHienThiAddNickChoSanPham)
        {
            string nicks = parramSpitString;
            for (int i = 0; i < rptNicks.Items.Count; i++)
            {
                CheckBox checkBoxNicks = (CheckBox)rptNicks.Items[i].FindControl("checkBoxNicks");
                if (checkBoxNicks.Checked == true)
                    nicks += checkBoxNicks.ToolTip + parramSpitString;
            }

            condition = DataExtension.AndConditon(
                SubitemsTSql.GetSubitemsByIid(iid),
                SubitemsTSql.GetSubitemsByVskey(TatThanhJsc.OtherModul.CodeApplications.SupportOnline));
            fields = DataExtension.GetListColumns(SubitemsColumns.IsidColumn, SubitemsColumns.VscontentColumn);
            DataTable dt = new DataTable();
            dt = Subitems.GetSubItems("", fields, condition, "");

            if (dt.Rows.Count > 0)
            {
                string isid = dt.Rows[0][SubitemsColumns.IsidColumn].ToString();
                //Cap nhat
                Subitems.UpdateSubitems(iid, language, TatThanhJsc.OtherModul.CodeApplications.SupportOnline, "", nicks, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1", isid);
            }
            else
            {
                //Them moi
                Subitems.InsertSubitems(iid, language, TatThanhJsc.OtherModul.CodeApplications.SupportOnline, "", nicks, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1");
            }
        }
        #endregion

        #region FilterProperties - Chỉ hiển thị khi tính năng lọc sản phẩm được hiển thị
        if (ProductConfig.KeyHienThiThuocTinhLocSanPham)
        {
            string filterProperties = parramSpitString;
            for (int i = 0; i < rptParentFilter.Items.Count; i++)
            {

                RadioButtonList rdblListAnswer = (RadioButtonList)rptParentFilter.Items[i].FindControl("rdblAnswer");
                if (rdblListAnswer != null)
                {
                    if (rdblListAnswer.SelectedValue.Length > 0)
                        filterProperties += rdblListAnswer.SelectedValue + parramSpitString;
                }

                CheckBoxList cblListAnswer = (CheckBoxList)rptParentFilter.Items[i].FindControl("cblAnswer");
                if (cblListAnswer != null)
                {
                    for (int j = 0; j < cblListAnswer.Items.Count; j++)
                    {
                        if (cblListAnswer.Items[j].Selected == true)
                            filterProperties += cblListAnswer.Items[j].Value + parramSpitString;
                    }
                }
            }

            condition = DataExtension.AndConditon(
                SubitemsTSql.GetSubitemsByIid(iid),
                SubitemsTSql.GetSubitemsByVskey(CodeApplications.ProductFilterProperties));
            fields = DataExtension.GetListColumns(SubitemsColumns.IsidColumn, SubitemsColumns.VscontentColumn);
            DataTable dt = new DataTable();
            dt = Subitems.GetSubItems("", fields, condition, "");

            if (dt.Rows.Count > 0)
            {
                string isid = dt.Rows[0][SubitemsColumns.IsidColumn].ToString();
                //Cap nhat
                Subitems.UpdateSubitems(iid, language, CodeApplications.ProductFilterProperties, "", filterProperties, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1", isid);
            }
            else
            {
                //Them moi
                Subitems.InsertSubitems(iid, language, CodeApplications.ProductFilterProperties, "", filterProperties, "", "", "", "", DateTime.Now.ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1");
            }
        }
        #endregion
        
        #region After Insert/Update

        if (ckbContinue.Checked == true)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess",
            //    "ThongBao(3000,'Đã tạo: " + txt_title.Text + "');", true);
            //Lưu vào session để gọi đến bên api
            Session["CotinuteCreate"] = true;
            Session["CotinuteCreateTitle"] = txt_title.Text;
            ResetControls();
        }
        else
        {
            Session["CotinuteCreate"] = false;
            Session["CotinuteCreateRedirectLink"] = LinkRedrect();
        }

        #endregion
    }

    private string GetParentsId(string p)
    {
        DataTable dt = Groups.GetGroups("1", GroupsColumns.IgparentsidColumn, GroupsTSql.GetGroupsByIgid(p), "");
        return dt.Rows[0][GroupsColumns.IgparentsidColumn].ToString();
    }

    public void AfterInsertUpdate()
    {
        if (ckbContinue.Checked == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "ThongBao(3000,'Đã tạo: " + txt_title.Text + "');", true);
            ResetControls();
        }
        else
            Response.Redirect(LinkRedrect());
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(LinkRedrect());
    }

    protected void lnk_delete_Image_current_Click(object sender, EventArgs e)
    {
        ImagesExtension.DeleteImageWhenDeleteItem(pic, hd_img.Value);
        ltimg.Visible = false;
        hd_img.Value = "";
    }

    protected void ddl_group_product_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFilterProperties();        
    }        
    protected void linkDeleteDocument_Click(object sender, EventArgs e)
    {
        TatThanhJsc.Extension.ImagesExtension.DeleteImageWhenDeleteItem(pic, hdFileDocument.Value);
        ltrDoc.Visible = false;
        hdFileDocument.Value = "";
    }
    protected void linkDeleteFileCatalog_Click(object sender, EventArgs e)
    {
        TatThanhJsc.Extension.ImagesExtension.DeleteImageWhenDeleteItem(pic, hdFileCatalog.Value);
        ltrFileCatalog.Visible = false;
        hdFileCatalog.Value = "";
    }
}