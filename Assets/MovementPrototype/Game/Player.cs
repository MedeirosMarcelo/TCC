using UnityEngine;
using System.Collections;

public class Player {

    public PlayerIndex PlayerId { get; set; }
    public string Name { get; set; }
    public CharController Character { get; set; }

    public Player(PlayerIndex playerId, string name) {
        this.PlayerId = playerId;
        this.Name = name;
    }
}