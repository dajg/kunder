using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicioKunder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ServicioKunder.Controllers
{
    public class timeController : ApiController
    {
        public HttpResponseMessage Get(string value)
        {
            try
            {
                Regex rgx = new Regex(@"^([0-5][0-9]):[0-5][0-9]$");
                if (rgx.IsMatch(value))
                {
                    DateTime dt = new DateTime();
                    if (!String.IsNullOrEmpty(value) && DateTime.TryParse(value, out dt))
                    {
                        Data objData = new Data();
                        objData.data = dt.ToString("yyyy -MM-dd'T'HH:mm:ssZ", CultureInfo.InvariantCulture);

                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(JsonConvert.SerializeObject(objData), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
