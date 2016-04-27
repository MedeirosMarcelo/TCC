using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MatchData {

    public static Dictionary<PlayerIndex, int> PlayerLives { get; set; }

    static MatchData() {
    }

    public static void Reload() {
        LoadDictionary();
    }

    static void LoadDictionary() {
        PlayerLives = new Dictionary<PlayerIndex, int>();
        foreach(Player pl in PlayerManager.GetPlayerList()){
            PlayerLives.Add(pl.PlayerId, 3);
        }
    }
}
