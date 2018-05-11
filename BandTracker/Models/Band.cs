using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BandTracker;
using System;

namespace BandTracker.Models
{
  public class Band
  {
    private int _id;
    private string _bandName;

    public Band(string bandName, int id = 0)
    {
      _id = id;
      _bandName = bandName;
    }
    public string GetBandName()
    {
      return _bandName;
    }
    public int GetBandId()
    {
      return _id;
    }
    public void SaveBand()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (name) VALUES (@bandName);";

      cmd.Parameters.Add(new MySqlParameter("@bandName", _bandName));
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
          conn.Dispose();
      }
    }
    public static List<Band> GetAllBands()
    {
      List<Band> allBands = new List<Band>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }
    public static void DeleteAllBands()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public override bool Equals(System.Object otherBand)
    {
        if (!(otherBand is Band))
        {
          return false;
        }
        else
        {
          Band newBand = (Band) otherBand;
          bool idEquality = (this.GetBandId() == newBand.GetBandId());
          bool bandNameEquality = (this.GetBandName() == newBand.GetBandName());
          return (idEquality && bandNameEquality);
        }
    }
    public override int GetHashCode()
    {
        return this.GetBandName().GetHashCode();
    }
  }
}
