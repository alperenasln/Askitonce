using Microsoft.AspNetCore.Mvc;

namespace Askitonce.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Askitonce.Models;

    public class QuestionController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public QuestionController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            //return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c WHERE c.title = '"+ id+"' OR c.description = '"+id+"'"));
             return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [ActionName("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            //return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c WHERE c.title = '" + searchString + "' OR c.description = '" + searchString + "' OR c.author = '"+ searchString+ "'"));
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c WHERE c.title LIKE '%"+searchString+ "%'" +
                " OR c.description LIKE '%"+searchString+"%'" +
                " OR c.author LIKE '%"+searchString+"%'"));

        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Title,Description,Author")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(question);
                return RedirectToAction("Index");
            }

            return View(question);
        }

        

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }
    }
}
