using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BandTracker;
using System;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _venueName;

    public Venue(string venueName, int id = 0)
    {
      _id = id;
      _venueName = venueName;
    }
    public string GetVenueName()
    {
      return _venueName;
    }
    public int GetVenueId()
    {
      return _id;
    }
    public void SaveVenue()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@venueName);";

      cmd.Parameters.Add(new MySqlParameter("@venueName", _venueName));
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
          conn.Dispose();
      }
    }
    public static List<Venue> GetAllVenues()
    {
      List<Venue> allVenues = new List<Venue>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }
    public static void DeleteAllVenues()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public override bool Equals(System.Object otherVenue)
    {
        if (!(otherVenue is Venue))
        {
          return false;
        }
        else
        {
          Venue newVenue = (Venue) otherVenue;
          bool idEquality = (this.GetVenueId() == newVenue.GetVenueId());
          bool venueNameEquality = (this.GetVenueName() == newVenue.GetVenueName());
          return (idEquality && venueNameEquality);
        }
    }
    public override int GetHashCode()
    {
        return this.GetVenueName().GetHashCode();
    }
  }
}
