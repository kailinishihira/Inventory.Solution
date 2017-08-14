using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Inventory.Models;
using System;

namespace Inventory.Tests
{
  [TestClass]
  public class InventoryCollectionTest : IDisposable
  {
    public void Dispose()
    {
      InventoryCollection.DeleteAll();
    }

    public InventoryCollectionTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseCounts_2()
    {
      //Arrange
      InventoryCollection newCollection1 = new InventoryCollection("dime",10,"05/10/1700",1);
      newCollection1.Save();
      InventoryCollection newCollection2 = new InventoryCollection("dime",10,"05/10/1700",1);
      newCollection2.Save();
      int expected = 2;
      //Act
      int actual = InventoryCollection.GetAll().Count;
      //Assert
      Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Equals_ReturnTrueIfPropertyValuesAreSame_InventoryCollection()
    {
      bool expected = true;
      InventoryCollection firstEntry = new InventoryCollection("coin1", 1, "20/10/1880");
      InventoryCollection secondEntry = new InventoryCollection("coin1", 1, "20/10/1880");
      Assert.AreEqual(expected, InventoryCollection.Equals(firstEntry,secondEntry));
    }
    [TestMethod]
    public void Save_SavesToDatabase_allCollections()
    {
      InventoryCollection newCollection = new InventoryCollection("dime",10,"05/10/1700");
      newCollection.Save();
      List<InventoryCollection> actual = InventoryCollection.GetAll();
      List<InventoryCollection> expected = new List<InventoryCollection>();
      expected.Add(newCollection);
      CollectionAssert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Find_FindrecordCollectionInDatabase_InventoryCollection()
    {
      InventoryCollection newCollection = new InventoryCollection("coin1", 1, "20/10/1880", 0);

    }
  }
}
