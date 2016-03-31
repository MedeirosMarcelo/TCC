using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CController : MonoBehaviour {

    public ControllerId joystick = ControllerId.One;
    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CFsm fsm { get; private set; }

    public void Awake() {
        fsm = new CFsm(this);
        input = new ControllerInput(joystick);
        rbody = GetComponent<Rigidbody>();
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
}
