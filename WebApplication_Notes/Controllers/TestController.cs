using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication_Notes.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTestList1Partial()
        {
            Thread.Sleep(4000);

            return PartialView("_TestList1Partial");
        }

        public IActionResult GetTestList2Data()
        {
            Thread.Sleep(4000);

            List<string> list = new List<string>();

            for (int i = 0; i < NumberData.GetNumber(5, 10); i++)
            {
                list.Add(TextData.GetSentence());
            }

            return Json(list);
        }

        [HttpPost]
        public IActionResult GetTestList2DataWithPost(string text)
        {
            Thread.Sleep(4000);

            List<string> list = new List<string>();

            for (int i = 0; i < NumberData.GetNumber(5, 10); i++)
            {
                list.Add(TextData.GetSentence());
            }

            list.Add(text);

            return Json(list);
        }
    }
}
