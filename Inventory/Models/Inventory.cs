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
    private int _categoryId;

    public InventoryCollection(string name, int value, string year, int categoryId , int id=0)
    {
      _name = name;
      _value = value;
      _year = year;
      _categoryId = categoryId;
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
    public int GetCategoryId()
    {
      return _categoryId;
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
        int collectionCategoryId = rdr.GetInt32(4);
        InventoryCollection newCollection = new InventoryCollection(name,value,year,collectionCategoryId,collectionId);
        allCollections.Add(newCollection);
      }
      return allCollections;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `collection`(`name`, `value`, `year`, category_id) VALUES(@name, @value, @year, @category_id);";

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

      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@category_id";
      categoryId.Value = this._categoryId;
      cmd.Parameters.Add(categoryId);

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
        bool idEquality = this.GetId() == newEntry.GetId();
        bool nameEquality = (this.GetName() == newEntry.GetName());
        bool valueEquality = (this.GetValue() == newEntry.GetValue());
        bool yearEquality = (this.GetYear() == newEntry.GetYear());
        bool categoryEquality = (this.GetCategoryId() == newEntry.GetCategoryId());
        return (idEquality && nameEquality && valueEquality && yearEquality && categoryEquality);
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
      // int categoryEquality = 2;
      int collectionCategoryId = 0;

      while (rdr.Read())
      {
        collectionId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        value = rdr.GetInt32(2);
        year = rdr.GetString(3);
        collectionCategoryId = rdr.GetInt32(4);
      }
      InventoryCollection actual = new InventoryCollection(name,value,year,collectionCategoryId,collectionId);
      return actual;
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}
