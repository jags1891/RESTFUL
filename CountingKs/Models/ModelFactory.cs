using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using CountingKs.Data.Entities;


namespace CountingKs.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelper = new UrlHelper(request);
        }

        public FoodModel Create(Food food)
        {
            return new FoodModel()
            {
                Url = _urlHelper.Link("Food", new { foodId = food.Id }),
                Description = food.Description,
                Measures = food.Measures.Select(m =>Create(m))
               
            };
        }

        public MeasureModel Create(Measure m)
        {
            return new MeasureModel()
            {
                Url = _urlHelper.Link("Measures", new { foodId = m.Food.Id, id=m.Id }),
                Description = m.Description,
                Calories = m.Calories
            };
        }
    }
}