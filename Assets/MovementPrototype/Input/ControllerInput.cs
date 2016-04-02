using UnityEngine;
using System.Collections;

public enum ControllerId : int {
    One = 1,
    Two = 2,
    //Three = 3,
    //Four  = 4
}

public class ControllerInput : BaseInput {
    public ControllerId id { get; private set; }

    public ControllerInput(ControllerId id) {
        this.id = id;
        name = "Controller " + id;
    }

    public override void Update() {
        buffer.Update();

        move.horizontal = Input.GetAxis("Move Horizontal " + (int)id);
        move.vertical = Input.GetAxis("Move Vertical " + (int)id);

        look.horizontal = Input.GetAxis("Look Horizontal " + (int)id);
        look.vertical = Input.GetAxis("Look Vertical " + (int)id);

        var dashed = Input.GetButtonDown("Dash " + (int)id);
        var attacked = Input.GetButtonDown("Attack " + (int)id);

        dash |= dashed;
        attack |= attacked;

        if (dashed) {
            buffer.Push(InputEvent.Dash);
        }
        else if (attacked) {
            buffer.Push(InputEvent.Attack);
        }
    }

    public override void FixedUpdate() {
        dash = false;
        attack = false;
    }
}
