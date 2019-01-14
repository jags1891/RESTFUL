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
    public class FoodsController : BaseApiController
    {
        public FoodsController(ICountingKsRepository repo) : base (repo)
        {
        }

        public IEnumerable<Models.FoodModel> Get(bool includeMeasures=true)
        {
            IQueryable<Food> result;
            if (includeMeasures)
                result = TheRepository.GetAllFoodsWithMeasures();
            else
                result = TheRepository.GetAllFoods();

            return result.OrderBy(f => f.Description).Take(25).ToList()
                .Select(f => TheModelFactory.Create(f));
           
        }

        public FoodModel Get(int foodid)
        {
            return TheModelFactory.Create(TheRepository.GetFood(foodid));
        }
    }
}
