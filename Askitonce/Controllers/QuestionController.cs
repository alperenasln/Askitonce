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
            var questiontype = "question";
            
            return View(await _cosmosDbService.GetQuestionItemsAsync("SELECT * FROM c WHERE c.type= '"+questiontype+"'"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [ActionName("CreateAnswer")]
        public IActionResult CreateAnswer()
        {
            return View();
        }
        [ActionName("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            //return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c WHERE c.title = '" + searchString + "' OR c.description = '" + searchString + "' OR c.author = '"+ searchString+ "'"));
            return View(await _cosmosDbService.GetQuestionItemsAsync("SELECT * FROM c WHERE c.title LIKE '%"+searchString+ "%'" +
                " OR c.description LIKE '%"+searchString+"%'" +
                " OR c.author LIKE '%"+searchString+"%'"));

        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Title,Description,Author,Type")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddQuestionItemAsync(question);
                return RedirectToAction("Index");
            }

            return View(question);
        }
        [HttpPost]
        [ActionName("CreateAnswer")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAnswerAsync([Bind("Id,QuestionId,Body,AnswerAuthor,Type")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                //answer.AnswerId = Guid.NewGuid().ToString();
                
                string path = Request.Path.ToString();
                answer.QuestionId = path.Substring(23);
                answer.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddAnswerItemAsync(answer);
                return RedirectToAction("Index");
            }
            return View(answer);
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
             return View(await _cosmosDbService.GetQuestionItemAsync(id));
            
        }
        [ActionName("Answers")]
        public async Task<ActionResult> AnswersAsync(string id)
        {
            var answertype = "answer";
            
            return View(await _cosmosDbService.GetAnswerItemsAsync("SELECT * FROM c WHERE c.questionid= '"+id+"' AND c.type= '"+answertype+"'"));
        }
    }
}
