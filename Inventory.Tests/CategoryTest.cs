using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Inventory.Models;

namespace Inventory.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
  {
    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }
   [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_0()
    {
     //Arrange, Act
      int result = Category.GetAll().Count;
     //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void GetCollection_RetrievesAllCollectionwithCategory_collectionList()
    {
      Category testCategory = new Category("Coin Collection");
      testCategory.Save();

      InventoryCollection firstCollection = new InventoryCollection("penny", 1, "1880", testCategory.GetId());
      firstCollection.Save();
      InventoryCollection secondCollection = new InventoryCollection("dime",10,"1886", testCategory.GetId());
      secondCollection.Save();


      List<InventoryCollection> testCollectionList = new List<InventoryCollection> {firstCollection, secondCollection};
      List<InventoryCollection> resultCollectionList = testCategory.GetCollection();

      CollectionAssert.AreEqual(testCollectionList, resultCollectionList);
    }
    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Category()
    {
      bool expected = true;
      //Arrange, Act
      Category firstCategory = new Category("Coin Collection");
      Category secondCategory = new Category("Coin Collection");

      //Assert
      Assert.AreEqual(expected, Category.Equals(firstCategory,secondCategory));
    }
    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      //Arrange
      Category testCategory = new Category("Coin Collection",1);
      testCategory.Save();
      //Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      //Arrange
      Category testCategory = new Category("Coin Collection");
      testCategory.Save();

      //Act
      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsCategoryInDatabase_Category()
    {
      //Arrange
      Category testCategory = new Category("Coin Collection");
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.AreEqual(testCategory, foundCategory);
    }

    public void Dispose()
    {
      InventoryCollection.DeleteAll();
      Category.DeleteAll();
    }
  }
}
