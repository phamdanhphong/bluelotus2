﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TatThanhJsc.ContactModul;


public partial class cms_admin_Moduls_ContactUs_Loadcontrol : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string suc = "";
        suc = Request.QueryString["suc"];
        switch (suc)
        {
            #region Cate
            case TypePage.Cate:
                phControl.Controls.Add(LoadControl("Cate/ControlCate.ascx"));
                break;
            case TypePage.UpdateCate:
            case TypePage.CreateCate:
                phControl.Controls.Add(LoadControl("Cate/ShortCutCate.ascx"));
                break;
            case TypePage.RecycleCate:
                phControl.Controls.Add(LoadControl("Cate/RecycleCate.ascx"));
                break;
            #endregion

            #region Item
            case TypePage.Item:
                phControl.Controls.Add(LoadControl("Item/ControlItem.ascx"));
                break;
            case TypePage.Item + "2":
                phControl.Controls.Add(LoadControl("Item/ControlItem2.ascx"));
                break;
            case TypePage.UpdateItem:
            case TypePage.CreateItem:
                phControl.Controls.Add(LoadControl("Item/ShortCutItem.ascx"));
                break;
            case TypePage.RecycleItem:
                phControl.Controls.Add(LoadControl("Item/RecycleItem.ascx"));
                break;
            case TypePage.RecycleItem + "2":
                phControl.Controls.Add(LoadControl("Item/RecycleItem2.ascx"));
                break;
            #endregion

            #region Content
            case TypePage.ContactContent:
                phControl.Controls.Add(LoadControl("AboutUs/ControlItem.ascx"));
                break;

            #endregion

            default:
                //phControl.Controls.Add(LoadControl("Index.ascx"));
                phControl.Controls.Add(LoadControl("Item/ControlItem.ascx"));
                break;
        }
    }
}