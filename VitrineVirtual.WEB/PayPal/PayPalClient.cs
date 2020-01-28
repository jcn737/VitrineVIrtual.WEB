using System;
using PayPalCheckoutSdk.Core;
using BraintreeHttp;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace VitrineVirtual.WEB.PayPal
{
    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment("Afjt_DxhRJVOzw3dKxh2gcU3x6zBXmma8i0H883e-kuEArLIdTIomkBkxkU-l-oZfyzhERPbLSrLhgm",
                "ECcQYhZ5He3pNqNK6OGmhYnt1nbCTk3JFh6VXwGLsR3k0PRbcKECcQYhZ5He3pNqNK6OGmhYnt1nbCTk3JFh6VXwGLsR3k0PRbcK-l93yMt2nok59G5TL2oOg1sSEd2ouQ");
        }

        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}