using RestSharp;
using RestSharp.Serialization.Json;
using SCBPVD.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCBPVD
{
  public class Session
    {
        private static readonly string API_KEY = "0050ee5fe33992b4a0ee9fea77b892dffa8a3904";
        private static readonly string SECRERT_KEY = "2a4156e7eaf0307f3d58281f939f2cf92d30c521";
        private static readonly string URL = "https://api.taximail.com/v2/user/login";

        public static async Task<ResponseTaximail.SessionStatus> GetSession()
        {
            try
            {
                var client = new RestClient(URL);
                var request = new RestRequest(Method.POST);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", $"api_key={API_KEY}&secret_key={SECRERT_KEY}", ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                IRestResponse respone = await client.ExecuteAsync(request);

                JsonDeserializer deserializer = new JsonDeserializer();

                var sessionStatus = deserializer.Deserialize<ResponseTaximail.SessionStatus>(respone);
                return sessionStatus;

            }
            catch (Exception ex)
            {

                ResponseTaximail.SessionStatus sessionStatus = new ResponseTaximail.SessionStatus();

                sessionStatus.status = "reject";
                sessionStatus.err_msg = ex.Message;

                return sessionStatus;
            }
        }
    }
}

