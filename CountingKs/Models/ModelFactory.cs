using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using CountingKs.Data.Entities;
using CountingKs.Data;

namespace CountingKs.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        ICountingKsRepository _repo;

        public ModelFactory(HttpRequestMessage request, ICountingKsRepository repo)
        {
            _urlHelper = new UrlHelper(request);
            _repo = repo;
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

        public DiarySummaryModel CreateSummary(Diary diary)
        {
            return new DiarySummaryModel()
            {
                DiaryDate = diary.CurrentDate,
                TotalCalories = Math.Round(diary.Entries.Sum(e => e.Measure.Calories * e.Quantity))
            };
        }

        public DiaryModel Create(Diary diary)   
        {
            return new DiaryModel()
            {
                Url = _urlHelper.Link("Diaries", new { diaryid = diary.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate =diary.CurrentDate
            };

        }

        public DiaryEntryModel Create(DiaryEntry entry)
        {
            return new DiaryEntryModel()
            {
                Url = _urlHelper.Link("DiaryEntries", new { diaryid = entry.Diary.CurrentDate.ToString("yyyy-MM-dd"), id = entry.Id }),
                Quantity = entry.Quantity,
                FoodDescription = entry.FoodItem.Description,
                MeasureDescription = entry.Measure.Description,
                MeasureUrl = _urlHelper.Link("Measures", new { foodid = entry.FoodItem.Id, id = entry.Measure.Id })
            };
        }

        public DiaryEntry Parse(DiaryEntryModel model)
        {
            try
            {
                var entry = new DiaryEntry();

                if(model.Quantity!=default(double))
                    entry.Quantity = model.Quantity;
                if (!string.IsNullOrWhiteSpace(model.MeasureUrl))
                {
                    var uri = new Uri(model.MeasureUrl);
                    var measureId = int.Parse(uri.Segments.Last());
                    var measure = _repo.GetMeasure(measureId);

                    entry.Measure = measure;
                    entry.FoodItem = measure.Food;
                }
                

                return entry;
            }
            catch
            {
                return null;
            }
        }
    }
}