using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Inventory.Models;

namespace Inventory.Controllers
{
	public class HomeController : Controller
	{
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

		[HttpGet("/categories")]
		public ActionResult Categories()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }
		[HttpGet("/categories/{id}")]
		public ActionResult CategoryDetail(int id)
		{
			Dictionary<string, object> model = new Dictionary<string, object>();
			Category thisCategory = Category.Find(id);
			List<Coin> coins = thisCategory.GetCollection();
			model.Add("category", thisCategory);
			model.Add("collection", coins);
			return View(model);
		}

		[HttpGet("/categories/new")]
    public ActionResult CategoryForm()
    {
      return View();
    }
		[HttpPost("/categories")]
    public ActionResult AddCategory()
    {
        Category newCategory = new Category(Request.Form["category-name"]);
				newCategory.Save();
        List<Category> allCategories = Category.GetAll();
        return View("Categories", allCategories);
    }

		[HttpGet("/categories/{id}/collection/new")]
    public ActionResult CategoryTaskForm(int id)
    {
        Category selectedCategory = Category.Find(id);
        return View("CollectionForm", selectedCategory);
    }

		[HttpPost("/categories/{id}/collection/new")]
    public ActionResult AddCollection(int id)
    {
			//Collect category using the id
      Category selectedCategory = Category.Find(id);
			//gets info from the user
			string name = Request.Form["coin-name"];
			int value = int.Parse(Request.Form["coin-value"]);
			string year = Request.Form["coin-year"];
			int categoryId = selectedCategory.GetId();
			//instatiaties and saves a new coin
      Coin newCoin = new Coin(name,value,year,categoryId);
			newCoin.Save();
			//build our model to pass to the view (Dictionary)
			Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("collection", selectedCategory.GetCollection());
      model.Add("category", selectedCategory);
			//render our view
      return View("CategoryDetail", model);
    }

		[HttpGet("/categories/delete")]
		public ActionResult DeleteAll()
		{
			Category.DeleteAll();
			return View();
		}
		
		[HttpGet("/collection/{id}")]
    public ActionResult CollectionDetail(int id)
    {
        Coin collection = Coin.Find(id);
        return View(collection);
    }
	}
}

//       string name = Request.Form["coinname"];
//       int value = int.Parse(Request.Form["coinvalue"]);
//       string year = Request.Form["coinyear"];
//       Coin newCollection = new Coin(name,value,year);
//       newCollection.Save();
//       // List<Coin> newList = Coin.GetAll();
//       // return View("Index", newList);
//       return RedirectToAction("Index");
//       // RedirectToAction will redirect the control to Index Method. So that we dont need to make the list in Collection method also.
//       // half working is done by Index Method for Collection Method. thus we can Prevent duplication of code.
//     }
//     [HttpGet("/deleteall")]
// 		public ActionResult DeleteAll()
// 		{
// 			Coin.DeleteAll();
// 			return View();
// 		}
//   }
// }
