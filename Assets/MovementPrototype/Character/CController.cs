using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerIndex : int {
    One = 1,
    Two = 2,
    //Three = 3,
    //Four  = 4
}

public class CController : MonoBehaviour {

    public PlayerIndex joystick = PlayerIndex.One;

    public Animator swordAnimator;
    public Animator bloodAnimator;
    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CFsm fsm { get; private set; }

    public void Awake() {
        input = new GamePadInput(joystick);
        rbody = GetComponent<Rigidbody>();
        fsm = new CFsm(this);
    }

    public void Update() {
        input.Update();
    }

    public void FixedUpdate() {
        fsm.Update();
        input.FixedUpdate();
    }

    public void ChangeVelocity(Vector3 velocity)
    {
        rbody.velocity = velocity;
    }

    public void Move(Vector3 position)
    {
        rbody.MovePosition(position);
    }

    public float turnSpeed = 0.2f;
    public void Look() {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25f) {
            transform.forward = Vector3.Lerp(transform.forward, vec, turnSpeed);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Sword") {
            PlayerIndex swordJoystick = col.transform.parent.parent.GetComponent<CController>().joystick;
            if (swordJoystick != this.joystick && fsm.current.Name != "BLOCK") {
                bloodAnimator.SetTrigger("Bleed");
            }
        }
    }

    void OnGUI() {
        input.OnGUI();
    }
}
