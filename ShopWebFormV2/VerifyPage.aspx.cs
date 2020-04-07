using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Windows;
using Newtonsoft.Json;

namespace ShopWebForm
{
    public partial class VerifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    resp.InnerText = Request["responseCode"];
                    lbltoken.InnerText = Request["token"];
                    lblParam.InnerText = string.Format("Acceptor={0} _ Amount={1} _ PaymentId={2} _ InvoiceNumber={3}",
                        Request["acceptorId"], Request["amount"], Request["paymentId"], Request["requestId"]);
                    txtAcceptorId.Text = "02010523";
                    lblAmount.InnerText = Request["amount"];
                    txtRRN.Text = Request["retrievalReferenceNumber"];
                    txtTraceNo.Text = Request["systemTraceAuditNumber"];

                }
                catch (Exception exe)
                {

                    message.InnerText = "خطا" + exe.Message;
                }
            }
        }

        protected void btnVerification_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(resp.InnerText) && resp.InnerText == "00")
                {
                    WebHelper webHelper = new WebHelper();


                    RequestVerify requestVerify = new RequestVerify();
                    requestVerify.terminalId = txtAcceptorId.Text;
                    requestVerify.systemTraceAuditNumber = txtTraceNo.Text;
                    requestVerify.retrievalReferenceNumber = txtRRN.Text;
                    requestVerify.tokenIdentity = lbltoken.InnerText;
                    string request = JsonConvert.SerializeObject(requestVerify);
                    Uri url = new Uri(string.Format(@"https://ikc.shaparak.ir/api/v3/confirmation/purchase"));
                    string jresponse = webHelper.Post(url, request);
                    if (jresponse != null)
                    {
                        VerifyResult jResult = JsonConvert.DeserializeObject<VerifyResult>(jresponse);
                        //Handle your reponse here
                        if (jResult.status)
                        {
                            message.InnerText = "عملیات تایید تراکنش با موفقیت انجام شد" + " result=" + jResult.description;
                            //  Verification succed , your statements Goes here
                        }
                        else
                        {
                            message.InnerText = "عملیات تایید تراکنش با موفقیت انجام نشد" + " result=" + jResult.description;
                            //  Verification Failed , your statements Goes here
                        }
                    }
                }
                else
                {
                    message.InnerText = "تراکنش  نا موفق بوده است";
                }
            }
            catch (Exception exe)
            {
                message.InnerText = "خطا" + exe.Message;
            }
        }

        protected void btnEspecialVerification_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(resp.InnerText) && resp.InnerText == "100")
                {

                    var res = 1;

                    //if (res.result.Code == "00")
                    //{

                    //    message.InnerText = "عملیات تایید مشروط تراکنش با موفقیت انجام شد" + "Result:" + res.result.Code;
                    //    btnReverce.Enabled = true;
                    //    //  Verification succed , your statements Goes here

                    //}
                    //else
                    //{
                    //    message.InnerText = "عملیات تایید مشروط تراکنش با شکست مواجه شد" + "Result:" + res.result.Code;
                    //    //  Verification Failed , your statements Goes here

                    //}
                }
                else
                {
                    message.InnerText = "تراکنش نا موفق می باشد";
                }

            }
            catch (Exception exe)
            {

                message.InnerText = "خطا سیستمی رخ داده است " + exe.Message;
            }

        }

        protected void btnReverce_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (var VerifyService = new Verify.VerifyClient())
            //    {
            //        var rev = VerifyService.KicccReverse(lbltoken.InnerText, txtMerchantID.Text, txtRRN.Text);
            //        if (rev.result.Code == "00")
            //        {

            //            message.InnerText = "عملیات برگشت تراکنش با موفقیت انجام شد" + "Result:" + rev.result.Code;
            //            btnReverce.Enabled = true;
            //            //  Verification succed , your statements Goes here

            //        }
            //        else
            //        {
            //            message.InnerText = "عملیات بر گشت تراکنش با شکست مواجه شد" + "Result:" + rev.result.Code;
            //            //  Verification Failed , your statements Goes here

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    message.InnerText = "خطای سیستمی رخ داد " + ex.Message;
            //}
        }

    }
    class WebHelper
    {
        public string Post(Uri url, string value)
        {
            var request = HttpWebRequest.Create(url);
            var byteData = Encoding.ASCII.GetBytes(value);
            request.ContentType = "application/json";
            request.Method = "POST";

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public string Get(Uri url)
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException e)
            {
                return null;
            }
        }
    }

    public class RequestVerify
    {
        [DataMember]
        public string terminalId { get; set; }

        [DataMember]
        public string retrievalReferenceNumber { get; set; }

        [DataMember]
        public string systemTraceAuditNumber { get; set; }

        [DataMember]
        public string tokenIdentity { get; set; }
    }


    public class VerifyResult
    {
        public string responseCode { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public SubResult result { get; set; }
    }

    public class SubResult
    {
        public string responseCode { get; set; }
        public string systemTraceAuditNumber { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public DateTime transactionDateTime { get; set; }
        public int transactionDate { get; set; }
        public int transactionTime { get; set; }
        public string processCode { get; set; }
        public object requestId { get; set; }
        public object additional { get; set; }
        public object billType { get; set; }
        public object billId { get; set; }
        public string paymentId { get; set; }
        public string amount { get; set; }
        public object revertUri { get; set; }
        public object acceptorId { get; set; }
        public object terminalId { get; set; }
        public object tokenIdentity { get; set; }
    }

}
