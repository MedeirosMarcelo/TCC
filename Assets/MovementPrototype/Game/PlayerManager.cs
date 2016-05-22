using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager {

    static IList<Player> PlayerList = new List<Player>();

    static PlayerManager() {
        //AddPlayer(PlayerIndex.One);
        //AddPlayer(PlayerIndex.Two);
        //AddPlayer(PlayerIndex.Three);
        //AddPlayer(PlayerIndex.Four);
    }

    public static Player AddPlayer(PlayerIndex playerId) {
        Player pl = new Player(playerId);
        pl.color = ChoosePlayerColor(playerId);
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

    public static Color ChoosePlayerColor(PlayerIndex index)
    {
        switch (index){
            default:
            case PlayerIndex.One:
                return Color.white;
            case PlayerIndex.Two:
                return Color.white;
            case PlayerIndex.Three:
                return Color.white;
            case PlayerIndex.Four:
                return Color.white;
        }
    }
}