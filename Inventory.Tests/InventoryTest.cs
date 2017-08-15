using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Inventory.Models;
using System;

namespace Inventory.Tests
{
  [TestClass]
  public class CoinTest : IDisposable
  {
    public void Dispose()
    {
      Coin.DeleteAll();
    }

    public CoinTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseCounts_2()
    {
      //Arrange
      Coin newCollection1 = new Coin("dime",10,"05/10/1700",1);
      newCollection1.Save();
      Coin newCollection2 = new Coin("dime",10,"05/10/1700",1);
      newCollection2.Save();
      int expected = 2;
      //Act
      int actual = Coin.GetAll().Count;
      //Assert
      Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Equals_ReturnTrueIfPropertyValuesAreSame_Coin()
    {
      bool expected = true;
      Coin firstEntry = new Coin("coin1", 1, "20/10/1880",1);
      Coin secondEntry = new Coin("coin1", 1, "20/10/1880",1);
      Assert.AreEqual(expected, Coin.Equals(firstEntry,secondEntry));
    }
    [TestMethod]
    public void Save_SavesToDatabase_allCollections()
    {
      Coin newCollection = new Coin("dime",10,"05/10/1700",1);
      newCollection.Save();
      List<Coin> actual = Coin.GetAll();
      List<Coin> expected = new List<Coin>();
      expected.Add(newCollection);
      CollectionAssert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Find_FindrecordCollectionInDatabase_Coin()
    {
      Coin expected = new Coin("coin1", 1, "20/10/1880", 1);
      expected.Save();
      Coin actual = Coin.Find(expected.GetId());
      Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void Update_UpdatesTaskInDatabase_String()
    {
      //Arrange
      Coin testCoin = new Coin("Quarter",10,"05/10/1700", 1);
      testCoin.Save();
      string newName = "Yen";

      //Act
      testCoin.UpdateName(newName);

      string result = testCoin.GetName();

      //Assert
      Assert.AreEqual(newName, result);
    }
  }
}
