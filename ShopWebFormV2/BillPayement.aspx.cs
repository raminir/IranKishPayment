using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using Newtonsoft.Json;

namespace ShopWebForm
{
    public partial class BillPayement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
            txtAmount.Text = "1000";
            //txtPassPhrase.Text = elemList.ChildNodes[2].InnerText;
            // txtRevertUri.Text = elemList.ChildNodes[3].InnerText;
        }

        protected void getToken_onclick(object sender, EventArgs e)
        {

            if (GenrateToken())
            {
                var url = "https://ikc.shaparak.ir/iuiv3/IPG/Index";

                Response.Clear();
                var sb = new System.Text.StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat("<body onload='document.forms[0].submit()'>");
                sb.AppendFormat("<form action='{0}' method='post'>", url);

                sb.AppendFormat("<input type='hidden' name='tokenIdentity' value='{0}'>", txtToken.Text);
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");
                Response.Write(sb.ToString());
            }

        }
        private bool GenrateToken()
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
                iPGData.PassPhrase = doc.SelectNodes("DocumentElement/IPGData")[0].SelectNodes("PassPhrase")[0].InnerText;
                iPGData.RevertURL = doc.SelectNodes("DocumentElement/IPGData")[0].SelectNodes("URL")[0].InnerText;
                iPGData.Amount = long.Parse(txtAmount.Text);
                iPGData.PaymentId = null;
                iPGData.RequestId = requestId;
                iPGData.CmsPreservationId = "989206006206";
                iPGData.TransactionType = TransactionType.Bill;
                iPGData.MultiplexParameters = null;
                iPGData.RsaPublicKey = doc.SelectNodes("DocumentElement/IPGData/RSAPublicKey")[0].InnerXml.ToString(); ;
                BillInfo billInfo = new BillInfo();
                billInfo.BillId = txtBillId.Text;
                billInfo.billPaymentId = txtPaymentId.Text;
                iPGData.BillInfo = billInfo;
                request = CreateJsonRequest.CreateJasonRequest(iPGData);
                Uri url = new Uri(string.Format(@"https://ikc.shaparak.ir/api/v3/tokenization/make"));
                string jresponse = webHelper.Post(url, request);
                if (jresponse != null)
                {
                    TokenResult jResult = JsonConvert.DeserializeObject<TokenResult>(jresponse);
                    txtToken.Text = jResult.result.token;
                    return jResult.status;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}