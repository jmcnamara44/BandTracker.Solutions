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
    public void AddVenue(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);";

      cmd.Parameters.Add(new MySqlParameter("@venueId", newVenue.GetVenueId()));
      cmd.Parameters.Add(new MySqlParameter("@bandId", _id));
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Venue> GetVenues()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT venues.* FROM bands
            JOIN bands_venues ON (bands.id = bands_venues.band_id)
            JOIN venues ON (bands_venues.venue_id = venues.id)
            WHERE bands.id = @bandId;";

        cmd.Parameters.Add(new MySqlParameter("@bandId", _id));
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Venue> venues = new List<Venue>{};

        while(rdr.Read())
        {
          int venueId = rdr.GetInt32(0);
          string venueName = rdr.GetString(1);
          Venue newVenue = new Venue(venueName, venueId);
          venues.Add(newVenue);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return venues;
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
    public static Band FindBand(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int bandId =0;
      string bandName="";

      while (rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
      }
      Band foundBand= new Band(bandName, bandId);

      conn.Close();
      if (conn!= null)
      {
        conn.Dispose();
      }
      return foundBand;
    }
    public void UpdateBand(string newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE bands SET name = @bandName WHERE id = @searchId";
      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));
      cmd.Parameters.Add(new MySqlParameter("@bandName", newBand));
      cmd.ExecuteNonQuery();
      _bandName = newBand;
      conn.Close();
      if (conn !=null)
      {
          conn.Dispose();
      }
    }
    public static void DeleteAllBands()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands; DELETE FROM bands_venues;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void DeleteBand()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands WHERE id = @thisId; DELETE FROM bands_venues WHERE band_id = @thisId;";
      cmd.Parameters.Add(new MySqlParameter("@thisId", _id));
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
