using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public static class PlayerManager
    {
        static List<Player> PlayerList = new List<Player>();
        static PlayerManager()
        {
            AddPlayer(PlayerIndex.One);
            AddPlayer(PlayerIndex.Two);
            AddPlayer(PlayerIndex.Three);
            AddPlayer(PlayerIndex.Four);
        }

        public static Player AddPlayer(PlayerIndex playerId)
        {
            Player pl = new Player(playerId);
            pl.Color = ChoosePlayerColor(playerId);
            PlayerList.Add(pl);
            return pl;
        }

        public static void RemovePlayer(Player player)
        {
            PlayerList.Remove(player);
        }

        public static void RemovePlayer(PlayerIndex playerId)
        {
            Player removedPlayer = PlayerList.FirstOrDefault(p => p.Id == playerId);
            if (removedPlayer != null)
            {
                PlayerList.Remove(removedPlayer);
            }
        }

        public static IList<Player> GetPlayerList()
        {
            return PlayerList;
        }

        public static Player GetPlayer(PlayerIndex playerId)
        {
            return PlayerList.FirstOrDefault(p => p.Id == playerId);
        }

        public static Color ChoosePlayerColor(PlayerIndex index)
        {
            switch (index)
            {
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
}