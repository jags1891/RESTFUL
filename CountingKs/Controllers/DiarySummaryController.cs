using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using CountingKs.Services;


namespace CountingKs.Controllers
{
    public class DiarySummaryController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiarySummaryController(ICountingKsRepository repo, ICountingKsIdentityService identityService)
          : base(repo)
        {
            _identityService = identityService;
        }

        public object Get(DateTime diaryid)
        {
            try
            {
                var diary = TheRepository.GetDiary(_identityService.CurrentUser, diaryid);

                if (diary == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Diary entry is not found.");

                return TheModelFactory.CreateSummary(diary);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}