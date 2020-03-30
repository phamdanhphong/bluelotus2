using System;
using TatThanhJsc.AdminModul;

public partial class cms_admin_Controls_HorizalMenu_AdmControlsHorizaMenu : System.Web.UI.UserControl
{
    private string uc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uc"] != null)
        {
            uc = Request.QueryString["uc"].ToString();
        }
        if (!IsPostBack)
        {
            PnMenu.Visible = HorizaMenuConfig.ShowMenu;
            PnContent.Visible = HorizaMenuConfig.ShowContent;
            PnProduct.Visible = HorizaMenuConfig.ShowProduct;
            PnNew.Visible = HorizaMenuConfig.ShowNew;
            PnTour.Visible = HorizaMenuConfig.ShowTour;
            pnHotel.Visible = HorizaMenuConfig.ShowHotel;
            PnTrainTicket.Visible = HorizaMenuConfig.ShowTrainTicket;
            PnService.Visible = HorizaMenuConfig.ShowService;
            PnPhotoAlbum.Visible = HorizaMenuConfig.ShowPhotoAlbum;
            PnFileLibrary.Visible = HorizaMenuConfig.ShowFilelibrary;
            PnVideo.Visible = HorizaMenuConfig.ShowVideo;
            PnAdvertising.Visible = HorizaMenuConfig.ShowAdv;
            PnContact.Visible = HorizaMenuConfig.ShowContact;
            PnMember.Visible = HorizaMenuConfig.ShowMember;
            pnQA.Visible = HorizaMenuConfig.ShowQA;
            pnDeal.Visible = HorizaMenuConfig.ShowDeal;
            pnCustomer.Visible = HorizaMenuConfig.ShowCustomer;

            PnOther.Visible = HorizaMenuConfig.ShowOther;
            PnSupportOnline.Visible = HorizaMenuConfig.ShowSupportOnline;
            PnPsg.Visible = HorizaMenuConfig.ShowPsg;
            PnVote.Visible = HorizaMenuConfig.ShowVote;
            pnSiteMap.Visible = HorizaMenuConfig.ShowSiteMap;
            pnDcLink.Visible = HorizaMenuConfig.ShowDcLink;

            PnTag.Visible = HorizaMenuConfig.ShowTag;
            PnFileLibrary2.Visible = HorizaMenuConfig.ShowFilelibrary2;

            PnCopyItem.Visible = HorizaMenuConfig.ShowCopyItem;

            PnEmail.Visible = HorizaMenuConfig.ShowEmail;

            pnCruises.Visible = HorizaMenuConfig.ShowCruises;
            pnDestination.Visible = HorizaMenuConfig.ShowDestination;

            pnAboutUs.Visible = HorizaMenuConfig.ShowAboutUs;
            pnCustomerReviews.Visible = HorizaMenuConfig.ShowCustomerReviews;
            pnBlog.Visible = HorizaMenuConfig.ShowBlog;
        }
    }

    

    protected string GetCurrent(string typeModul)
    {
        string str = "";
        if (Request.QueryString["uc"] != null)
        {
            if (Request.QueryString["uc"].Equals(typeModul))
            {
                str = " currentMenu";
            }
        }        

        return str;
    }

}
