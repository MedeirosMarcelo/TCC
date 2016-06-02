using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum PlayerIndex : int
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }
    public class Player
    {
        public PlayerIndex Id { get; private set; }
        public Color Color { get; set; }
        public CharController Character { get; set; }

        public Player(PlayerIndex playerId)
        {
            Id = playerId;
        }
    }
}
