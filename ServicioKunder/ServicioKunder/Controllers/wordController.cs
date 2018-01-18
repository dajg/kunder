using Newtonsoft.Json;
using ServicioKunder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ServicioKunder.Controllers
{
    public class wordController : ApiController
    {
        public HttpResponseMessage Post(Data data)
        {
            try
            {
                if (data != null && !String.IsNullOrEmpty(data.data))
                {
                    if (data.data.Length < 4 || data.data.Length > 4)
                    {
                        // Bad Request
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        Regex rgx = new Regex(@"^[A-Za-z ]+$");
                        if (rgx.IsMatch(data.data))
                        {
                            data.data = data.data.ToUpper();
                            var response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                            return response;
                        }
                        else
                        {
                            // Bad Request
                            return new HttpResponseMessage(HttpStatusCode.BadRequest);
                        }
                    }
                }
                else
                {
                    // Bad Request
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            
            }
            catch (Exception ex)
            {
                // Internal Server Error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}