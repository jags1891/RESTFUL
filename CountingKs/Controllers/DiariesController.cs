using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using CountingKs.Services;

namespace CountingKs.Controllers
{
    public class DiariesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiariesController(ICountingKsRepository repo, 
            ICountingKsIdentityService identityService) 
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            var UserName = _identityService.CurrentUser;

            var results = TheRepository.GetDiaries(UserName)
                .OrderBy(d => d.CurrentDate)
                .Take(10)
                .ToList()
                .Select(d => TheModelFactory.Create(d));

            return results;
        }

        public HttpResponseMessage Get(DateTime diaryid)
        {
            var UserName = _identityService.CurrentUser;
            var result = TheRepository.GetDiary(UserName, diaryid);

            if (result == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found!");

            return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(result));
        }

    }
}