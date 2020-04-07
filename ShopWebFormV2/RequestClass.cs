using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ShopWebForm
{

    public class RequestClass
    {
        public AuthenticationEnvelope AuthenticationEnvelope = new AuthenticationEnvelope();
        public Request Request = new Request();
    }
    public class AuthenticationEnvelope
    {
        public string Data { get; set; }
        public string Iv { get; set; }
    }
    public class Request
    {
        public string AcceptorId { get; set; }
        public long amount { get; set; }
        public BillInfo BillInfo { get; set; }
        public string CmsPreservationId { get; set; }
        public List<MultiplexParameter> multiplexParameters { get; set; }
        public string PaymentId { get; set; }
        public string RequestId { get; set; }
        public long RequestTimestamp { get; set; }
        public string RevertUri { get; set; }
        public string terminalId { get; set; }
        public string transactionType { get; set; }
    }
    public struct TransactionType
    {
        public const string Purchase = "Purchase";
        public const string Bill = "Bill";

    }
    public class BillInfo
    {
        public string BillId { get; set; }
        public string billPaymentId { get; set; }
    }
    public class MultiplexParameter
    {
        public string Iban { get; set; }
        public int Amount { get; set; }
    }
    public static class CreateJsonRequest
    {
        public static string CreateJasonRequest(string terminalId, string acceptorId, long amount, string revertUri, string passPhrase,
         string requestId, string paymentId, string cmsPreservationId, string transactionType, BillInfo billInfo, List<MultiplexParameter> multiplexParameters)
        {
            RequestClass requestClass = new RequestClass();
            requestClass.Request.CmsPreservationId = cmsPreservationId;
            requestClass.Request.AcceptorId = acceptorId;
            requestClass.Request.amount = amount;
            requestClass.Request.BillInfo = billInfo;
            requestClass.Request.multiplexParameters = multiplexParameters;
            requestClass.Request.PaymentId = paymentId;
            requestClass.Request.RequestId = requestId;
            requestClass.Request.RevertUri = revertUri;
            requestClass.Request.terminalId = terminalId;
            requestClass.Request.RequestTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            requestClass.Request.transactionType = transactionType;
            try
            {

            }
            catch (Exception ex)
            {


            }
            return   JsonConvert.SerializeObject(requestClass); ;
        }

        public static string CreateJasonRequest(IPGData iPGData)
        {
            RequestClass requestClass = new RequestClass();
            requestClass.Request.CmsPreservationId = iPGData.CmsPreservationId;
            requestClass.Request.AcceptorId = iPGData.AcceptorId;
            requestClass.Request.amount = iPGData.Amount;
            requestClass.Request.BillInfo = iPGData.BillInfo;
            requestClass.Request.multiplexParameters = iPGData.MultiplexParameters;
            requestClass.Request.PaymentId = iPGData.PaymentId;
            requestClass.Request.RequestId = iPGData.RequestId;
            requestClass.Request.RevertUri = iPGData.RevertURL;
            requestClass.Request.terminalId = iPGData.TreminalId;
            requestClass.Request.RequestTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            requestClass.Request.transactionType = iPGData.TransactionType;

            CreateAESCoding(requestClass,iPGData.RsaPublicKey, iPGData.PassPhrase, requestClass.Request.multiplexParameters == null ? false : true);
            try
            {

            }
            catch (Exception ex)
            {


            }
            return JsonConvert.SerializeObject(requestClass); ;
        }
        private static void CreateAESCoding(this RequestClass requestClass,string rsaPublicKey ,string passPhrase, bool isMultiplex)
        {
            try
            {
                string baseString =
              requestClass.Request.terminalId +
               passPhrase +
                requestClass.Request.amount.ToString().PadLeft(12, '0') +
               (isMultiplex ? "01" : "00") +
               (isMultiplex ?
                requestClass.Request.multiplexParameters.Select(t => $"{t.Iban.Replace("IR", "2718")}{t.Amount.ToString().PadLeft(12, '0')}")
               .Aggregate((a, b) => $"{a}{b}")
               : string.Empty);
                using (Aes myAes = Aes.Create())
                {
                    myAes.KeySize = 128;
                    myAes.GenerateKey();
                    myAes.GenerateIV();
                    byte[] keyAes = myAes.Key;
                    byte[] ivAes = myAes.IV;

                    byte[] resultCoding = new byte[48];
                    byte[] baseStringbyte = baseString.HexStringToByteArray();

                    byte[] encrypted = EncryptStringToBytes_Aes(baseStringbyte, myAes.Key, myAes.IV);
                    byte[] hsahash = new SHA256CryptoServiceProvider().ComputeHash(encrypted);

                    resultCoding = CombinArray(keyAes, hsahash);

                    requestClass.AuthenticationEnvelope.Data = RSAData(resultCoding,rsaPublicKey).ByteArrayToHexString();
                    //  string decripte = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                    requestClass.AuthenticationEnvelope.Iv = ivAes.ByteArrayToHexString();

                }
            }
            catch (Exception ex)
            {


            }

        }


        private static byte[] RSAData(byte[] aesCodingResult,string publicKey)
        {
            try
            {
                var csp = new RSACryptoServiceProvider();
                csp.FromXmlString(publicKey);

                return csp.Encrypt(aesCodingResult, false);

   
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        private static byte[] CombinArray(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Array.Copy(first, 0, bytes, 0, first.Length);
            Array.Copy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
        private static byte[] HashHSA256(byte[] date)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(date);

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return bytes;
            }
        }
        private static byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key, byte[] IV)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.KeySize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
                // Create the streams used for encryption.

            }

        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
    public static class Extension
    {
        public static byte[] HexStringToByteArray(this string hexString)
            => Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value: hexString.Substring(startIndex: x, length: 2), fromBase: 16))
                .ToArray();

        public static string ByteArrayToHexString(this byte[] bytes)
            => bytes.Select(t => t.ToString(format: "X2")).Aggregate((a, b) => $"{a}{b}");
    }


    
    public partial class IPGData
    {

        private string treminalId;

        private string acceptorId;

        private string passPhrase;

        private string revertURL;

        private string rsaPublicKey;

        private long amount;

        private string paymentId;

        private string requestId;

        private string cmsPreservationId;

        private BillInfo billInfo;

        private List<MultiplexParameter> multiplexParameters;

        private string transactionType;
        public string RsaPublicKey { get; set; }//{ get => rsaPublicKey; set => rsaPublicKey = value; }
        public string TreminalId { get; set; }//{ get => treminalId; set => treminalId = value; }
        public string AcceptorId { get; set; }//{ get => acceptorId; set => acceptorId = value; }
        public string PassPhrase { get; set; }//{ get => passPhrase; set => passPhrase = value; }
        public string RevertURL { get; set; }//{ get => revertURL; set => revertURL = value; }
        public long Amount { get; set; }//{ get => amount; set => amount = value; }
        public string PaymentId { get; set; }//{ get => paymentId; set => paymentId = value; }
        public string CmsPreservationId { get; set; }//{ get => cmsPreservationId; set => cmsPreservationId = value; }
        public string TransactionType { get; set; }//{ get => transactionType; set => transactionType = value; }
        public BillInfo BillInfo { get; set; }//{ get => billInfo; set => billInfo = value; }
        public List<MultiplexParameter> MultiplexParameters { get; set; }//{ get => multiplexParameters; set => multiplexParameters = value; }
        public string RequestId { get; set; }//{ get => requestId; set => requestId = value; }
    }





}
