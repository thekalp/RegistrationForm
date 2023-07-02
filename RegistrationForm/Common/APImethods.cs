﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RegistrationForm.Common
{
    public class APImethods
    {
        public HttpResponseMessage GetApi(string Path)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var Url = request.GetAsync(Path);
            Url.Wait();
            var result = Url.Result;
            return result;
        }

        public HttpResponseMessage DeleteApi(string Path)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var Url = request.DeleteAsync(Path);
            Url.Wait();
            var result = Url.Result;
            return result;
        }

        #region Call API [Comman] for POST
        public HttpResponseMessage PostApi(string Path, dynamic obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var Url = request.PostAsync(Path, byteContent);
            Url.Wait();
            var result = Url.Result;
            return result;
        }
        #endregion

        #region Call API [Comman] for PUT
        public HttpResponseMessage PutApi(string Path, dynamic obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var Url = request.PutAsync(Path, byteContent);
            Url.Wait();
            var result = Url.Result;
            return result;
        }
        #endregion
    }
}
