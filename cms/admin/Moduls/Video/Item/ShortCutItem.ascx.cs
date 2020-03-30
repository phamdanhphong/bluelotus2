﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TatThanhJsc.Columns;
using TatThanhJsc.Database;
using TatThanhJsc.Extension;
using TatThanhJsc.VideoModul;
using TatThanhJsc.TSql;

public partial class cms_admin_Moduls_Video_Item_ShortCutItem : System.Web.UI.UserControl
{
    private string app = CodeApplications.Video;
    private string language = TatThanhJsc.LanguageModul.Cookie.GetLanguageValueAdmin();
    private string pic = FolderPic.Video;
    private string propertyModul = CodeApplications.VideoProperty;
        
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

        #region Gán đường dẫn cho ckeditor
        foreach(Control control in this.Controls)
        {
            if(control is CKEditor.NET.CKEditorControl)
                ((CKEditor.NET.CKEditorControl)control).FilebrowserImageBrowseUrl
                    =
                    UrlExtension.WebisteUrl + "ckeditor/ckfinder/ckfinder.aspx?type=Images&path=" + pic;
        }
        #endregion

        if (!IsPostBack)
        {
            SetEnableControls();
            GetGroupsInDdl();         
            GetProperties();
            InitialControlsValue(insert);
            KhoiTaoXuLyAnh();            
        }
    }

    #region Khởi tạo các thông tin bổ xung    
    #region ThuocTinhVideo
    void GetProperties()
    {
        if (VideoConfig.KeyHienThiQuanLyThuocTinhVideo)
        {
            fields = "*";
            condition = TatThanhJsc.Extension.DataExtension.AndConditon
                (
                GroupsTSql.GetGroupsByVglang(language),
                TatThanhJsc.TSql.GroupsTSql.GetGroupsByVgapp(propertyModul),
                " IGENABLE <> '2' "
                );
            orderBy = TatThanhJsc.Columns.GroupsColumns.IgorderColumn;
            DataTable dt = new DataTable();
            dt = Groups.GetGroups("", fields, condition, orderBy);
            rptProperties.DataSource = dt;
            rptProperties.DataBind();
        }
    }
    #endregion    
    #endregion   
    #region Kiểm tra các chức năng phụ(add nick, thuộc tính, lọc...) có được hiển thị hay không
    /// <summary>
    /// Hiển thị hoặc ẩn các chức năng theo thiết lập trong bảng Settings
    /// </summary>
    void SetEnableControls()
    {       
        //Ẩn hiện chức năng Thuộc tính video
        if (VideoConfig.KeyHienThiQuanLyThuocTinhVideo) pnThuocTinhVideo.Visible = true;
        else pnThuocTinhVideo.Visible = false;      
    }
    #endregion

    void KhoiTaoXuLyAnh()
    {
        #region Đóng dấu ảnh
        if (SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo, language) == "1")
            cbDongDauAnh.Checked = true;
        else
            cbDongDauAnh.Checked = false;
        #region Ảnh làm dấu
        hdLogoImage.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_AnhDau, language);
        #endregion
        hdViTriDongDau.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_ViTri, language);
        hdLeX.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_LeNgang, language);
        hdLeY.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_LeDoc, language);
        hdTyLe.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_TyLe, language);
        hdTrongSuot.Value = SettingsExtension.GetSettingKey(SettingKey.DongDauAnhVideo_TrongSuot, language);
        #endregion

        #region Hạn chế kích thước ảnh đại diện
        if (SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo, language) == "1")
            cbHanCheKichThuoc.Checked = true;
        else
            cbHanCheKichThuoc.Checked = false;

        tbHanCheW.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxWidth, language);
        tbHanCheH.Text = SettingsExtension.GetSettingKey(SettingKey.HanCheKichThuocAnhVideo_MaxHeight, language);
        #endregion

        #region Tạo ảnh nhỏ cho ảnh đại diện
        if (SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo, language) == "1")
            cbTaoAnhNho.Checked = true;
        else
            cbTaoAnhNho.Checked = false;

        tbAnhNhoW.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxWidth, language);
        tbAnhNhoH.Text = SettingsExtension.GetSettingKey(SettingKey.TaoAnhNhoChoAnhVideo_MaxHeight, language);
        #endregion
    }

    private string LinkRedrect()
    {
        if (!ni.Equals("") && !p.Equals(""))
        {
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.Video + "&igid=" + ddl_group_product.SelectedValue + "&suc="+TypePage.Item+"&ni=" + ni + "&p=" + p;
        }
        else
        {
            return UrlExtension.WebisteUrl + "admin.aspx?uc=" + CodeApplications.Video + "&igid=" + ddl_group_product.SelectedValue + "&suc=" + TypePage.Item;
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
                ddl_group_product.Items.Add(new ListItem(TatThanhJsc.Extension.DropDownListExtension.FormatForDdl(dt.Rows[i]["IGLEVEL"].ToString()) + dt.Rows[i]["VGNAME"].ToString(), dt.Rows[i]["IGID"].ToString()));
            }
        }
        if (!igid.Equals(""))
        {
            ddl_group_product.SelectedValue = igid;
        }
    }

    void InitialControlsValue(bool insert)
    {
        #region update
        if (!insert)
        {
            btn_insert_update.Text = "Đồng ý";
            ckbContinue.Visible = false;
            fields = "*";
            condition = DataExtension.AndConditon(GroupsTSql.GetGroupsByVgapp(app), ItemsTSql.GetItemsByIid(iid));
            DataTable dt = new DataTable();
            dt = GroupsItems.GetAllData(top, fields, condition, orderBy);
            lnk_delete_Image_current.Visible = true;
            ddl_group_product.SelectedValue = dt.Rows[0]["IGID"].ToString();
            txt_title.Text = dt.Rows[0]["VITITLE"].ToString();
            txt_description.Text = dt.Rows[0]["VIDESC"].ToString();
            txt_content.Text = dt.Rows[0]["VICONTENT"].ToString();
            hdOldVideo.Value = dt.Rows[0]["VICONTENT"].ToString();
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
                ltimg.Text = TatThanhJsc.Extension.ImagesExtension.GetImage(pic, dt.Rows[0]["VIIMAGE"].ToString(), "", "imgItem", false, false, "", false);
                lnk_delete_Image_current.Visible = true;
            }
            else
            {
                ltimg.Visible = false;
                lnk_delete_Image_current.Visible = false;
            }
            hd_img.Value = dt.Rows[0]["VIIMAGE"].ToString();
            if(hd_img.Value.Length<1)
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
         
            tbKey.Text = dt.Rows[0][ItemsColumns.VikeyColumn].ToString();
            tbOrder.Text = dt.Rows[0][ItemsColumns.IiorderColumn].ToString();
            txt_youtubecode.Text = dt.Rows[0][ItemsColumns.ViauthorColumn].ToString();
            #region ThuocTinhVideo-Chi thực hiện khi chức năng Quản lý thuộc tính được hiển thị
            if (VideoConfig.KeyHienThiQuanLyThuocTinhVideo)
            {
                string properties = "";
                condition = TatThanhJsc.Extension.DataExtension.AndConditon(
                    TatThanhJsc.TSql.SubitemsTSql.GetSubitemsByIid(iid),
                    TatThanhJsc.TSql.SubitemsTSql.GetSubitemsByVskey(propertyModul));
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
             
        }
        #endregion
        #region  insert
        else
        {
            btn_insert_update.Text = "Đồng ý";
            txtCreateDate.Text = DateTime.Now.ToString();         
            txt_title.Focus();
        }
        #endregion
    }

    void ResetControls()
    {
        txt_title.Text = "";
        txt_description.Text = "";
        txt_content.Text = "";
        hdOldVideo.Value = "";
        txtCreateDate.Text = DateTime.Now.ToString();
        ltimg.Text = "";
        hd_img.Value = "";
        HdIitotalview.Value = "";
        textLinkRewrite.Text = "";
        textTagTitle.Text = "";
        textTagKeyword.Text = "";
        textTagDescription.Text = "";
        txt_youtubecode.Text = "";
        try
        {
            tbOrder.Text = (Convert.ToInt32(tbOrder.Text) + 1).ToString();
        }
        catch { }
        txt_title.Focus();
    }

    protected void btn_insert_update_Click(object sender, EventArgs e)
    {
        string contentDetail = ContentExtendtions.ProcessStringContent(txt_content.Text, hdOldVideo.Value, pic);
        #region Image
        string vimg = "";
        string vimg_thumb = "";
        if (flimg.PostedFile.ContentLength > 0)
        {
            string filename = flimg.FileName;
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
            if(hd_img.Value.Length<1 || cbLayAnhTuNoiDung.Checked)//nếu không upload ảnh và cũng không có ảnh cũ -> tìm ảnh đầu tiên trong nội dung làm ảnh đại diện
            {
                if(hd_img.Value.Length>0)
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
          
                        if (ImagesExtension.SaveImageFromUrl(path+vimg, urlImg).Length > 0)
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
        #region Status
        string status = "0";
        if (chk_status.Checked == true)
        {
            status = "1";
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
        string videoCode = txt_youtubecode.Text;//viauthor: Mã nhúng video do người dùng nhập vào
        string videoKey = TatThanhJsc.Extension.VideoExtension.GetVideoKey(videoCode);//viurl: Mã của video được tách ra từ khối mã người dùng nhập vào (chỉ chứa id của video đó, không chứa các thông tin khác -> phục vụ khi lấy ra hiển thị dễ dàng hơn)
        #region Insert
        if (insert)
        {
            GroupsItems.InsertItemsGroupsItems(language, app, tbKey.Text, txt_title.Text, txt_description.Text, contentDetail, vimg, videoKey, videoCode, textTagTitle.Text, textLinkRewrite.Text, StringExtension.ReplateTitle(textLinkRewrite.Text), textTagKeyword.Text, textTagDescription.Text, "", "", "", "", "0", "0", "", "", timeCreateDate, DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, ddl_group_product.SelectedValue, timeCreateDate, DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, status);

            #region Lay ra iid cua item vua duoc luu
            condition = DataExtension.AndConditon(
                ItemsTSql.GetItemsByDicreatedate(timeCreateDate),
                ItemsTSql.GetItemsByViapp(app));
            DataTable dtInsertedItems = new DataTable();
            dtInsertedItems = GroupsItems.GetAllData("1", "Items.iid", condition, ItemsColumns.IidColumn + " desc");
            if (dtInsertedItems.Rows.Count > 0)
                iid = dtInsertedItems.Rows[0][ItemsColumns.IidColumn].ToString();
            #endregion

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", txt_title.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " tạo mới " + txt_title.Text);
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
                TatThanhJsc.Extension.ImagesExtension.DeleteImageWhenDeleteItem(pic, hd_img.Value);
            }
            GroupsItems.UpdateItemsGroupsItems(language, app, tbKey.Text, txt_title.Text, txt_description.Text, contentDetail, vimg, videoKey, videoCode, textTagTitle.Text, textLinkRewrite.Text, StringExtension.ReplateTitle(textLinkRewrite.Text), textTagKeyword.Text, textTagDescription.Text, "", "", "", "", "0", "0", "", HdIitotalview.Value, timeCreateDate, DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, ddl_group_product.SelectedValue, timeCreateDate, DateTime.Now.ToString(), DateTime.Now.ToString(), tbOrder.Text, status, iid);

            #region Logs
            string logAuthor = CookieExtension.GetCookies("LoginSetting");
            string logCreateDate = DateTime.Now.ToString();
            Logs.InsertLogs(logCreateDate, Request.Url.ToString(), "", txt_title.Text, logAuthor, "", logCreateDate + ": " + logAuthor + " cập nhật " + txt_title.Text);
            #endregion
        }        
        #endregion

        #region Properties-Chi thực hiện khi chức năng Quản lý thuộc tính được hiển thị
        if (VideoConfig.KeyHienThiQuanLyThuocTinhVideo)
        {
            string properties = parramSpitString;
            for (int i = 0; i < rptProperties.Items.Count; i++)
            {
                CheckBox checkBoxProperties = (CheckBox)rptProperties.Items[i].FindControl("checkBoxProperties");
                if (checkBoxProperties.Checked == true)
                    properties += checkBoxProperties.ToolTip + parramSpitString;
            }

            condition = TatThanhJsc.Extension.DataExtension.AndConditon(
                TatThanhJsc.TSql.SubitemsTSql.GetSubitemsByIid(iid),
                TatThanhJsc.TSql.SubitemsTSql.GetSubitemsByVskey(propertyModul));
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

        #region After Insert/Update
        if (ckbContinue.Checked == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "ThongBao(3000,'Đã tạo: " + txt_title.Text + "');", true);
            ResetControls();
        }
        else
            Response.Redirect(LinkRedrect());
        #endregion
    }
    
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(LinkRedrect());
    }

    protected void lnk_delete_Image_current_Click(object sender, EventArgs e)
    {
        TatThanhJsc.Extension.ImagesExtension.DeleteImageWhenDeleteItem(pic, hd_img.Value);
        ltimg.Visible = false;
        hd_img.Value = "";
    } 
}