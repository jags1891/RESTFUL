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
    public class MeasuresController : BaseApiController
    {

        public MeasuresController(ICountingKsRepository repo) : base(repo)
        {
  
        }

        public IEnumerable<Models.MeasureModel> Get(int foodId)
        {
            return TheRepository.GetFood(foodId)
                .Measures
                .ToList()
                .Select(f => TheModelFactory.Create(f));
        }

        public MeasureModel Get(int foodId, int id)
        {
            var Measure = TheRepository.GetMeasure(id);

            if (Measure.Food.Id == foodId)
              return  TheModelFactory.Create(Measure);

            return null;

        }
    }
}