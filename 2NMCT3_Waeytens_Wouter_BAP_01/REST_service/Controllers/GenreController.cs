﻿using PortableLibrary;
using REST_service.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST_service.Controllers
{
    public class GenreController : ApiController
    {
        public List<Genre> Get()
        {
            return GenreRepo.GetGenres();
        }
    }
}
