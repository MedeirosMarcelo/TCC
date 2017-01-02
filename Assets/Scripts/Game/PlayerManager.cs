using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public static class PlayerManager
    {
        public static int MinionAmount { get; set; }
        static List<Player> PlayerList = new List<Player>();
        static PlayerManager()
        {
            MinionAmount = -1;
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            if (gm != null)
            {
                for (int i = 0; i < gm.debugAddPlayers; i++)
                {
                    AddPlayer((PlayerIndex)i + 1);
                }
            }
        }
        public static void Reset()
        {
            PlayerList.Clear();
        }

        public static Player AddPlayer(PlayerIndex playerId)
        {
            Player pl = new Player(playerId);
            pl.Color = ChoosePlayerColor(playerId);
            PlayerList.Add(pl);
            PlayerList = PlayerList.OrderBy(p => p.Id).ToList();
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