using System;
using System.Configuration;
using System.Web.UI;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Newtonsoft.Json;

namespace ShopWebForm
{
    public partial class MultiAccountPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataSet shebaList = new DataSet();
                shebaList.ReadXml(Server.MapPath("DataXmlFile.xml"));
                DropDownList0.DataSource = shebaList.Tables["Shebalist"];
                DropDownList0.DataTextField = "Sheba";
                DropDownList0.DataValueField = "name";
                DropDownList0.DataBind();
                DropDownList1.DataSource = shebaList.Tables["Shebalist"];
                DropDownList1.DataTextField = "Sheba";
                DropDownList1.DataValueField = "Name";
                DropDownList1.DataBind();
                InitialTextBox();
            }
        }

        protected void getToken_Click(object sender, EventArgs e)
        {
            try
            {
                WebHelper webHelper = new WebHelper();
                string paymetId = new Random().Next().ToString();
                string requestId = new Random().Next().ToString();
                string request = string.Empty;
                int totalAmount = 0;
                XmlDocument doc = new XmlDocument();

                doc.Load(Server.MapPath("DataXmlFile.xml"));
                IPGData iPGData = new IPGData();
                iPGData.TreminalId = txtTerminalId.Text;
                iPGData.AcceptorId = txtAcceptorID.Text;
                iPGData.PassPhrase = txtPassPhrase.Text;
                iPGData.RevertURL = doc.SelectNodes("DocumentElement/IPGData")[0].SelectNodes("URL")[0].InnerText;
               
                iPGData.PaymentId = paymetId;
                iPGData.RequestId = requestId;
                iPGData.CmsPreservationId = "989127980595";
                iPGData.TransactionType = TransactionType.Purchase;
                iPGData.BillInfo = null;

                List<MultiplexParameter> multiplexParameters = new List<MultiplexParameter>();

                if (!string.IsNullOrEmpty(txtAmount1.Text))
                {
                    MultiplexParameter account1 = new MultiplexParameter();
                    account1.Amount = int.Parse(txtAmount1.Text);
                    account1.Iban = DropDownList0.SelectedItem.Text;
                    multiplexParameters.Add(account1);
                    totalAmount += int.Parse(txtAmount1.Text);

                }
                if (!string.IsNullOrEmpty(txtAmount2.Text))
                {
                    MultiplexParameter account1 = new MultiplexParameter();
                    account1.Amount = int.Parse(txtAmount2.Text);
                    account1.Iban = DropDownList1.SelectedItem.Text; 
                    multiplexParameters.Add(account1);
                    totalAmount += int.Parse(txtAmount2.Text);
                }
                iPGData.MultiplexParameters = multiplexParameters;
                txtAmount.Text = totalAmount.ToString();
                iPGData.Amount = long.Parse(txtAmount.Text);
                iPGData.RsaPublicKey = doc.SelectNodes("DocumentElement/IPGData/RSAPublicKey")[0].InnerXml.ToString();
                request = CreateJsonRequest.CreateJasonRequest(iPGData);


                Uri url = new Uri(string.Format(@"https://ikc.shaparak.ir/api/v3/tokenization/make"));
                string jresponse = webHelper.Post(url, request);

                if (jresponse != null)
                {
                    TokenResult jResult = JsonConvert.DeserializeObject<TokenResult>(jresponse);
                    //Handle your reponse here 
                    txtToken.Text = jResult.result.token;
                }
                Literal2.Text = string.Format("<input id='token' type='hidden'  name='tokenIdentity' value='{0}' runat='server' />", txtToken.Text);
            }
            catch (Exception exe)
            {
                Response.Redirect("VerifyPage.aspx");
            }
        }

        protected void drpSheba1_TextChanged(object sender, EventArgs e)
        {
            string controlId = ((System.Web.UI.Control)sender).UniqueID;
            switch (((System.Web.UI.Control)sender).ID)
            {
                case "DropDownList0":
                    Label0.Text = ((System.Web.UI.WebControls.ListControl)sender).SelectedValue;
                    break;
                case "DropDownList1":
                    Label1.Text = ((System.Web.UI.WebControls.ListControl)sender).SelectedValue;
                    break;
                default:
                    break;
            }
        }

        private void InitialTextBox()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("DataXmlFile.xml"));
            //Get URL
            XmlNode elemList = doc.GetElementsByTagName("IPGData").Item(0);
            txtTerminalId.Text = elemList.ChildNodes[0].InnerText;
            txtAcceptorID.Text = elemList.ChildNodes[1].InnerText;
            txtPassPhrase.Text = elemList.ChildNodes[2].InnerText;
             
        }
    }
}