using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager {

    static IList<Player> PlayerList = new List<Player>();

    static PlayerManager() {
        AddPlayer(PlayerIndex.Two, "2");
        AddPlayer(PlayerIndex.One, "1");
    }

    public static Player AddPlayer(PlayerIndex playerId, string name) {
        Player pl = new Player(playerId, name);
        PlayerList.Add(pl);
        return pl;
    }

    public static void RemovePlayer(Player player) {
        PlayerList.Remove(player);
    }

    public static void RemovePlayer(PlayerIndex playerId) {
        Player removedPlayer = null;
        foreach (Player pl in PlayerList){
            if (pl.PlayerId == playerId) {
                removedPlayer = pl;
                break;
            }
        }
        if (removedPlayer != null) {
            PlayerList.Remove(removedPlayer);
        }
    }

    public static IList<Player> GetPlayerList() {
        return PlayerList;
    }

    public static Player GetPlayer(PlayerIndex playerId) {
        foreach (Player pl in PlayerList) {
            if (pl.PlayerId == playerId) {
                return pl;
            }
        }
        return null;
    }
}