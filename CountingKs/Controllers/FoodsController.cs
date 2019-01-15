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

namespace CountingKs.Controllers
{
    public class FoodsController : BaseApiController
    {
        public FoodsController(ICountingKsRepository repo) : base (repo)
        {
        }

        const int PAGE_SIZE = 50;

        public object Get(bool includeMeasures=true, int page=0)
        {
            IQueryable<Food> foods;
            if (includeMeasures)
                foods = TheRepository.GetAllFoodsWithMeasures();
            else
                foods = TheRepository.GetAllFoods();

            var baseQuery = foods.OrderBy(f => f.Description);
            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);
            var helper = new UrlHelper(Request);
            var prevUrl = page>0 ? helper.Link("Food", new { page = page - 1 }) : "";
            var nextvUrl = page< totalPages? helper.Link("Food", new { page = page + 1 }):"";


            var results = baseQuery
                .Skip(page*PAGE_SIZE)
                .Take(PAGE_SIZE).ToList()
                .Select(f => TheModelFactory.Create(f));

            return new
            {
                PageSize=PAGE_SIZE,
                CurrentPage=page,
                TotalCount=totalCount,
                TotalPages=totalPages,
                Prev= prevUrl,
                Next=nextvUrl,
                Results = results,
            };
        }

        public FoodModel Get(int foodid)
        {
            return TheModelFactory.Create(TheRepository.GetFood(foodid));
        }
    }
}
