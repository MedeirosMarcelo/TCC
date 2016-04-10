using UnityEngine;
using System.Collections;

public class ControllerInput : BaseInput {
    public PlayerIndex id { get; private set; }

    public ControllerInput(PlayerIndex id) {
        this.id = id;
        name = "Controller " + id;
    }

    public override void Update() {
        buffer.Update();
        id.toXInput();
        move.horizontal = Input.GetAxis("Move Horizontal " + (int)id);
        move.vertical = Input.GetAxis("Move Vertical " + (int)id);

        look.horizontal = Input.GetAxis("Look Horizontal " + (int)id);
        look.vertical = Input.GetAxis("Look Vertical " + (int)id);

        var dashed = Input.GetButtonDown("Dash " + (int)id);
        var attacked = Input.GetButtonDown("Attack " + (int)id);
        var blocked = Input.GetButtonDown("Block " + (int)id);

        dash |= dashed;
        attack |= attacked;
        block |= blocked;

        if (dashed) {
            buffer.Push(new InputEvent.Dash(move));
        }
        else if (attacked) {
            buffer.Push(new InputEvent.Attack(move));
        }
        else if (blocked) {
            buffer.Push(new InputEvent.Block());
        }
    }

    public override void FixedUpdate() {
        dash = false;
        attack = false;
        block = false;
    }
}
