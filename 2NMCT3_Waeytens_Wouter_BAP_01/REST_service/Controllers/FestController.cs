using REST_service.Models.DAL;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST_service.Controllers
{
    public class FestController : ApiController
    {
        public String Get()
        {
            return FestRepo.GetFestName();
        }
    }
}
