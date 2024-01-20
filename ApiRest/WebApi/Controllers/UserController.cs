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
    public class UserController : ApiController
    {
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] User user)
        {
            UserFlag userFlag= UserData.RegisterUser(user);

            if(userFlag.Error)
                return request.CreateResponse(HttpStatusCode.Forbidden, userFlag);

            return request.CreateResponse(HttpStatusCode.OK, userFlag);
        }
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            List<User> listUser = UserData.ListarUsers();

            if (listUser.Count <= 0)
                return request.CreateResponse(HttpStatusCode.NotFound, listUser);

            return request.CreateResponse(HttpStatusCode.OK, listUser);
        }
    }
}