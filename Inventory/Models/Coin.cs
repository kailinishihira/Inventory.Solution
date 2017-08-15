using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Inventory.Models
{
  public class Coin
  {
    private int _id;
    private string _name;
    private int _value;
    private string _year;
    private int _categoryId;

    public Coin(string name, int value, string year, int categoryId , int id=0)
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

    public static List<Coin> GetAll()
    {
      List<Coin> allCollections = new List<Coin>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText= @"SELECT * FROM coins;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int coinId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int value = rdr.GetInt32(2);
        string year = rdr.GetString(3);
        int coinCategoryId = rdr.GetInt32(4);
        Coin newCoin = new Coin(name,value,year,coinCategoryId,coinId);
        allCollections.Add(newCoin);
      }
      return allCollections;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `coins`(`name`, `value`, `year`, category_id) VALUES(@name, @value, @year, @category_id);";

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
      cmd.CommandText = @"DELETE FROM coins;";
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherEntry)
    {
      if(!(otherEntry is Coin))
      {
        return false;
      }
      else
      {
        Coin newEntry = (Coin) otherEntry;
        bool idEquality = this.GetId() == newEntry.GetId();
        bool nameEquality = (this.GetName() == newEntry.GetName());
        bool valueEquality = (this.GetValue() == newEntry.GetValue());
        bool yearEquality = (this.GetYear() == newEntry.GetYear());
        bool categoryEquality = (this.GetCategoryId() == newEntry.GetCategoryId());
        return (idEquality && nameEquality && valueEquality && yearEquality && categoryEquality);
      }
    }
    public static Coin Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `coins` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int coinId = 0;
      string name = "";
      int value = 0;
      string year = "";
      // int categoryEquality = 2;
      int coinCategoryId = 0;

      while (rdr.Read())
      {
        coinId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        value = rdr.GetInt32(2);
        year = rdr.GetString(3);
        coinCategoryId = rdr.GetInt32(4);
      }
      Coin actual = new Coin(name,value,year,coinCategoryId,coinId);
      return actual;
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}
