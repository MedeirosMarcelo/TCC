using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MatchData {

    public static Dictionary<PlayerIndex, int> PlayerScore { get; set; }

    static MatchData() {
        Reload();
    }

    public static void Reload() {
        LoadDictionary();
    }

    static void LoadDictionary() {
        PlayerScore = new Dictionary<PlayerIndex, int>();
        foreach(Player pl in PlayerManager.GetPlayerList()){
            PlayerScore.Add(pl.PlayerId, 0);
        }
    }
}
