using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;

namespace BitskinsSellTF2
{

    class RequestBuilder
    {
        private RestRequest request;

        private RestClient restClient;

        public RequestBuilder(Uri uri)
        {
            this.restClient = new RestClient(uri);
        }

        public RequestBuilder(string url)
        {
            this.restClient = new RestClient(url);
        }

        public RequestBuilder AddCookie(string cookie, string value)
        {
            this.request.AddCookie(cookie, value);
            return this;
        }

        public RequestBuilder AddCookies(Dictionary<string, string> cookies)
        {
            if (cookies != null)
            {
                foreach (var cookie in cookies)
                {
                    this.request.AddCookie(cookie.Key, cookie.Value);
                }
            }

            return this;
        }


        public RequestBuilder AddDefaultHeader()
        {
            this.AddHeader(
                HttpRequestHeader.UserAgent,
                "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Safari/537.36");
            this.AddHeader(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded; charset=UTF-8");
            return this;
        }

        public RequestBuilder AddHeader(HttpRequestHeader header, string value)
        {
            this.request.AddHeader(header.ToString(), value);
            return this;
        }

        public RequestBuilder AddHeader(string header, string value)
        {
            this.request.AddHeader(header, value);
            return this;
        }

        public RequestBuilder AddPOSTParam(string name, string value)
        {
            this.request.AddParameter(name, value);
            return this;
        }


        public RequestBuilder AddCookies(SteamAuth.SessionData cookies)
        {
            if (cookies != null)
            {
                this.request.AddCookie("sessionid", cookies.SessionID);
                this.request.AddCookie("SteamID", Convert.ToString(cookies.SteamID));
                this.request.AddCookie("steamLogin", cookies.SteamLogin);
                this.request.AddCookie("steamLoginSecure", cookies.SteamLoginSecure);
                this.request.AddCookie("webCookie", cookies.WebCookie);
                this.request.AddCookie("oAuthToken", cookies.OAuthToken);

            }

            return this;
        }


        public RequestBuilder AddFile_Bytes(string name, byte[] bytes, string contentType)
        {
            this.request.AddFileBytes(name, bytes, "image", contentType);
            return this;
        }

        public RequestBuilder AddPOSTParam_int(string name, int value)
        {
            this.request.AddParameter(name, value);
            return this;
        }
        public RequestBuilder AddFile(string name, string path)
        {
            this.request.AddFile(name, path);
            return this;
        }

        public IRestResponse Execute()
        {
            var response = this.restClient.Execute(this.request);
            return response;
        }

        public RequestBuilder GET(bool defaultHeaders = true)
        {
            this.request = new RestRequest(Method.GET);
            if (defaultHeaders)
            {
                this.AddDefaultHeader();
            }

            return this;
        }

        public RequestBuilder POST(bool defaultHeaders = true)
        {
            this.request = new RestRequest(Method.POST);
            if (defaultHeaders)
            {
                this.AddDefaultHeader();
            }

            return this;
        }
    }
}