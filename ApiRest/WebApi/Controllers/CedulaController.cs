using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CedulaController : ApiController
    {
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
                DataUser dataUser = CedulaData.GetDataByCedula(id.ToString());
                if(dataUser == null) 
                    return request.CreateResponse(HttpStatusCode.NotFound);
            return request.CreateResponse(HttpStatusCode.OK, dataUser);
        }
    }
}