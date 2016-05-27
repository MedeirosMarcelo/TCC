using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameData
{
    public static CharController Winner { get; set; }
    public static Dictionary<PlayerIndex, int> PlayerLives { get; set; }

    static GameData()
    {
    }

    public static void Reload()
    {
        LoadDictionary();
        Winner = null;
    }

    static void LoadDictionary()
    {
        PlayerLives = new Dictionary<PlayerIndex, int>();
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            PlayerLives.Add(pl.PlayerId, 3);
        }
    }
}
