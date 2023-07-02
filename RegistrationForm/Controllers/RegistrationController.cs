using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistrationForm.Common;
using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationForm.Controllers
{
    public class RegistrationController : Controller
    {
        APImethods APICalls = new APImethods();
        string BaseUrl = "https://localhost:44364/api/Registration";
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Get(int id = 0)
        {
            List<TblUserRegistration> jsonObjectData = new List<TblUserRegistration>();
            try
            {
                var resultClient = APICalls.GetApi(BaseUrl + "/GetAllData?id=" + id);

                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    jsonObjectData = JsonConvert.DeserializeObject<List<TblUserRegistration>>(data);
                    var result = jsonObjectData;
                    var jsonResult = Json(new { aaData = result });
                    return jsonResult;
                }
                return Json(new { aaData = jsonObjectData });
            }
            catch (Exception ex)
            {
                return Json(new { aaData = jsonObjectData });
            }
        }
        [HttpPost]
        public ActionResult Add(TblUserRegistration obj)
        {
            try
            {
                var success = "";
                var json = JsonConvert.SerializeObject(obj);

                var resultClient = APICalls.PostApi(BaseUrl + "/NewRegistraction", obj);
                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    success = JsonConvert.DeserializeObject<string>(data);
                    if (success == "1")
                    {
                        return Json(new { success, message = "Data has been inserted successfully" });
                    }
                    else
                    {
                        return Json(new { success, message = "Error while Inserting records!!" });
                    }
                }
                else
                {
                    string error = "Error";
                    return Json(new { error, message = "Error while Inserting records!!" });
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpPost]
        public ActionResult Update(TblUserRegistration obj)
        {
            try
            {
                var success = "Updated";
                var json = JsonConvert.SerializeObject(obj);
                var resultClient = APICalls.PutApi(BaseUrl + "/UpdateRegistraction", obj);
                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    success = JsonConvert.DeserializeObject<string>(data);
                    if (data == "1")
                    {
                        return Json(new { success, message = "Data has been updated successfully" });
                    }
                    else
                    {
                        string warning = "Warning";
                        return Json(new { warning, message = "Error while Updating records!!" });
                    }
                }
                else
                {
                    string error = "Error";
                    return Json(new { error, message = "Error while Updating records!!" });
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteModal success = new DeleteModal();
                var resultClient = APICalls.DeleteApi(BaseUrl + "/DeleteRegistraction?id=" + id);
                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    success = JsonConvert.DeserializeObject<DeleteModal>(data);
                    if (success.value == "1")
                    {
                        return Json(new { success, message = "Data Successfully Deleted" });
                    }

                }
                return Json(null);
            }
            catch (Exception ex)
            {
                return Json(null);
            }

        }
        [HttpGet]
        public JsonResult GetState(int id = 0)
        {
            List<TblState> jsonObjectData = new List<TblState>();
            try
            {
                var resultClient = APICalls.GetApi(BaseUrl + "/GetAllState?id=" + id);

                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    jsonObjectData = JsonConvert.DeserializeObject<List<TblState>>(data);
                    var result = jsonObjectData;
                    var jsonResult = Json(new { aaData = result });
                    return jsonResult;
                }
                return Json(new { aaData = jsonObjectData });
            }
            catch (Exception ex)
            {
                return Json(new { aaData = jsonObjectData });
            }
        }
        [HttpGet]
        public JsonResult GetCity(int id = 0)
        {
            List<TblCity> jsonObjectData = new List<TblCity>();
            try
            {
                var resultClient = APICalls.GetApi(BaseUrl + "/GetAllCity?id=" + id);

                if (resultClient.IsSuccessStatusCode)
                {
                    var data = resultClient.Content.ReadAsStringAsync().Result;
                    jsonObjectData = JsonConvert.DeserializeObject<List<TblCity>>(data);
                    var result = jsonObjectData;
                    var jsonResult = Json(new { aaData = result });
                    return jsonResult;
                }
                return Json(new { aaData = jsonObjectData });
            }
            catch (Exception ex)
            {
                return Json(new { aaData = jsonObjectData });
            }
        }
    }
}
