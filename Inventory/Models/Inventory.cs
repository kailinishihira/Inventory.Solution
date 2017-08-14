using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Inventory.Models
{
  public class InventoryCollection
  {
    private int _id;
    private string _name;
    private int _value;
    private string _year;

    public InventoryCollection(string name, int value, string year, int id=0)
    {
      _name = name;
      _value = value;
      _year = year;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetValue()
    {
      return _value;
    }
    public string GetYear()
    {
      return _year;
    }

    public static List<InventoryCollection> GetAll()
    {
      List<InventoryCollection> allCollections = new List<InventoryCollection>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText= @"SELECT * FROM collection;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int collectionId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int value = rdr.GetInt32(2);
        string year = rdr.GetString(3);
        InventoryCollection newCollection = new InventoryCollection(name,value,year,collectionId);
        allCollections.Add(newCollection);
      }
      return allCollections;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `collection`(`name`, `value`, `year`) VALUES(@name, @value, @year);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter value = new MySqlParameter();
      value.ParameterName = "@value";
      value.Value = this._value;
      cmd.Parameters.Add(value);

      MySqlParameter year = new MySqlParameter();
      year.ParameterName = "@year";
      year.Value = this._year;
      cmd.Parameters.Add(year);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
}
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM collection;";
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherEntry)
    {
      if(!(otherEntry is InventoryCollection))
      {
        return false;
      }
      else
      {
        InventoryCollection newEntry = (InventoryCollection) otherEntry;
        bool collectionEquality = (this.GetName() == newEntry.GetName());
        return collectionEquality;
      }
    }
    public static InventoryCollection Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `collection` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int collectionId = 0;
      string name = "";
      int value = 0;
      string year = "";

      while (rdr.Read())
      {
        collectionId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        value = rdr.GetInt32(2);
        year = rdr.GetString(3);
      }
      InventoryCollection actual = new InventoryCollection(name,value,year,collectionId);
      return actual;
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}
