using UnityEngine;
using System.Collections;

public class Player {

    public PlayerIndex PlayerId { get; set; }
    public CharController Character { get; set; }

    public Player(PlayerIndex playerId) {
        this.PlayerId = playerId;
    }
}