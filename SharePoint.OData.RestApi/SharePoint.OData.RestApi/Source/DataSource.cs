using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace SharePoint.OData.RestApi
{
    public class DataSource
    {
        public static List<String> MySource()
        {

            string data = string.Empty; List<string> myList = new List<string>();
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create("http://eimskip/sites/DCNorwegian/_api/site/rootWeb/webinfos");

            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";
            NetworkCredential cred = new System.Net.NetworkCredential("asharma", "span@123", "spivpc");
            endpointRequest.Credentials = cred;
            HttpWebResponse endpointResponse = (HttpWebResponse)endpointRequest.GetResponse();

            WebResponse webResponse = endpointRequest.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            JObject jobj = JObject.Parse(response);
            JArray jarr = (JArray)jobj["d"]["results"];
            foreach (JObject j in jarr)
            {
                data = j["Title"] + ", ";

                myList.Add(data);
            }

            responseReader.Close();
            return myList;
        }



        private static RestClient RestClient(string baseSiteUrl, string user, string password, string domain, out string formDigestValue)
        {
            RestClient restClient = new RestClient(baseSiteUrl);
            NetworkCredential nCredential = new NetworkCredential(user, password, domain);
            restClient.Authenticator = new NtlmAuthenticator(nCredential);

            RestRequest request = new RestRequest("contextinfo?$select=FormDigestValue", Method.POST);
            request.AddHeader("Accept", "application/json;odata=verbose");
            request.AddHeader("Body", "");

            string returnedStr = restClient.Execute(request).Content; // change variable names
            int startPos = returnedStr.IndexOf("FormDigestValue", StringComparison.Ordinal) + 18;
            int length = returnedStr.IndexOf(@""",", startPos, StringComparison.Ordinal) - startPos;
            formDigestValue = returnedStr.Substring(startPos, length);
            return restClient;
        }


        public static string GetCaseFolderDetails(string caseId, string baseSiteUrl, string username, string password, string domain)
        {
            string siteUrl = baseSiteUrl;
            //string siteUrl = string.Empty;

            try
            {
                Uri uri = new Uri(baseSiteUrl);//fullUrl is absoluteUrl
                string relativeUrl = uri.AbsolutePath;

                if (baseSiteUrl.EndsWith("/", StringComparison.Ordinal))
                {
                    baseSiteUrl += "_api/";
                    //siteUrl = archiveModel.BaseSiteUrl + Constants.KeywordApi;
                }
                else
                {
                    baseSiteUrl += "/" + "_api/";
                    //siteUrl = archiveModel.BaseSiteUrl + "/" + Constants.KeywordApi;
                }

                string formDigestValue, libraryName = "Dokumenter", caseFolderName = "Saker";
                var restClient = RestClient(baseSiteUrl, username, password, domain, out formDigestValue);

                
                string folderServerRelativeUrl = string.Concat(relativeUrl, libraryName, "/", caseFolderName, "/", caseId);

                RestRequest request = new RestRequest("web/GetFolderByServerRelativeUrl('" + folderServerRelativeUrl + "')/ListItemAllFields", Method.GET)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddHeader("Accept", "application/json;odata=verbose");
                request.AddHeader("Content-Type", "application/json;odata=verbose");
                IRestResponse response = restClient.Execute(request);
                string content = response.Content;

                JObject jobj = JObject.Parse(content);
                string caseFolderDetails = Convert.ToString(jobj["d"]["Id"]);

                baseSiteUrl = siteUrl;

                //if (string.IsNullOrEmpty(caseFolderDetails))
                //{
                //    //return CreateCaseFolder(caseId, siteUrl, user, password, domain);
                //    return null; //CreateCaseFolder(caseId, archiveModel);
                //}
                //else
                //{
                //    return content;
                //}

                //return string.IsNullOrEmpty(caseFolderDetails) ? CreateCaseFolder(caseId, objArchiveModel) : content;
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
