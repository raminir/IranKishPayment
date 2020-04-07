using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using Newtonsoft.Json;

namespace ShopWebForm
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtAmount.Text = "1000";
                InitialTextBox();
            }
        }

        private void InitialTextBox()
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("DataXmlFile.xml")); // suppose that myXmlString contains "<Names>...</Names>"

            XmlNodeList xnList = doc.SelectNodes("DocumentElement/IPGData/RSAPublicKey");
            string d = xnList[0].InnerXml.ToString();
            doc.Load(Server.MapPath("DataXmlFile.xml"));
            //Get URL
            XmlNode elemList = doc.GetElementsByTagName("IPGData").Item(0);
            txtTerminalId.Text = elemList.ChildNodes[0].InnerText;
            txtAcceptorID.Text = elemList.ChildNodes[1].InnerText;
            txtPassPhrase.Text = elemList.ChildNodes[2].InnerText;
        }

        protected void getToken_onclick(object sender, EventArgs e)
        {
            try
            {
                WebHelper webHelper = new WebHelper();
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("DataXmlFile.xml"));
                string paymetId = new Random().Next().ToString();
                string requestId = new Random().Next().ToString();
                string request = string.Empty;
                IPGData iPGData = new IPGData();
                iPGData.TreminalId = txtTerminalId.Text;
                iPGData.AcceptorId = txtAcceptorID.Text;
                iPGData.PassPhrase = txtPassPhrase.Text;
                iPGData.RevertURL = doc.SelectNodes("DocumentElement/IPGData")[0].SelectNodes("URL")[0].InnerText;
                iPGData.Amount = long.Parse(txtAmount.Text);
                iPGData.PaymentId = paymetId;
                iPGData.RequestId = requestId;
                iPGData.CmsPreservationId = "989206006206";
                iPGData.TransactionType = TransactionType.Purchase;
                iPGData.BillInfo = null;
                iPGData.RsaPublicKey = doc.SelectNodes("DocumentElement/IPGData/RSAPublicKey")[0].InnerXml.ToString();
                if (false)
                {
                    // پرداخت تسهیمی
                    MultiplexParameter account1 = new MultiplexParameter();
                    List<MultiplexParameter> multiplexParameters = new List<MultiplexParameter>();
                    account1.Amount = int.Parse(txtAmount.Text);
                    account1.Iban = "IR Your Iban";
                    multiplexParameters.Add(account1);
                    iPGData.MultiplexParameters = multiplexParameters;
                    request = CreateJsonRequest.CreateJasonRequest(iPGData);
                }
                if(false)
                {
                    // :) پرداخت معمولی نقی
                    request = CreateJsonRequest.CreateJasonRequest(iPGData);
                }
                Uri url = new Uri(string.Format(@"https://ikc.shaparak.ir/api/v3/tokenization/make"));
                string jresponse = webHelper.Post(url, request);

                if (jresponse != null)
                {
                    TokenResult jResult = JsonConvert.DeserializeObject<TokenResult>(jresponse);
                    //Handle your reponse here 
                    txtToken.Text = jResult.result.token;
                    if (jResult.status)
                    {
                        // tblForm.Visible = true;
                        Literal2.Text = string.Format("<input id='token' type='hidden' name='tokenIdentity' value='{0}' runat='server'/>", txtToken.Text);
                    }
                }
            }
            catch (Exception exe)
            {
                txtToken.Text = exe.Message;
            }
        }
    }


}