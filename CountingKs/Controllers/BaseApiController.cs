using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        ICountingKsRepository _repo;
        ModelFactory _modelFactory;

        public BaseApiController(ICountingKsRepository repo)
        {
            _repo = repo;
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                    _modelFactory = new ModelFactory(this.Request, _repo);
                return _modelFactory;
            }
        }

        protected ICountingKsRepository TheRepository
        {
            get
            {
                return _repo;
            }
        }

    }
}   