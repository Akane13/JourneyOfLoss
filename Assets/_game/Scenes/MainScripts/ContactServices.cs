using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactServices
{
    DB dB;
    public ContactServices()
    {
        dB = new DB();
    }
    public void CreatePlayerTable()
    {
        dB.GetConnection().DropTable<Player>();
        dB.GetConnection().CreateTable<Player>();

    }
    public int AddPlayer(Player player)
    {
        return dB.GetConnection().Insert(player);
    }
    
    public Player GetFirstPlayer()
    {
        var connection = dB.GetConnection();
        var query = connection.Table<Player>();
        foreach (var player in query)
        {
            return player; // Return the first player
        }
        return null; // Return null if no players exist
    }

}
