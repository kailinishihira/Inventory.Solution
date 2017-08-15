// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using Inventory.Models;
//
// namespace Inventory.Controllers
// {
// 	public class HomeController : Controller
// 	{
//     [HttpGet("/")]
//     public ActionResult Index()
//     {
//       List<InventoryCollection> newList = InventoryCollection.GetAll();
//       return View("Index", newList);
//     }
//     [HttpPost("/collection")]
//     public ActionResult Collection()
//     {
//       string name = Request.Form["coinname"];
//       int value = int.Parse(Request.Form["coinvalue"]);
//       string year = Request.Form["coinyear"];
//       InventoryCollection newCollection = new InventoryCollection(name,value,year);
//       newCollection.Save();
//       // List<InventoryCollection> newList = InventoryCollection.GetAll();
//       // return View("Index", newList);
//       return RedirectToAction("Index");
//       // RedirectToAction will redirect the control to Index Method. So that we dont need to make the list in Collection method also.
//       // half working is done by Index Method for Collection Method. thus we can Prevent duplication of code.
//     }
//     [HttpGet("/deleteall")]
// 		public ActionResult DeleteAll()
// 		{
// 			InventoryCollection.DeleteAll();
// 			return View();
// 		}
//   }
// }
