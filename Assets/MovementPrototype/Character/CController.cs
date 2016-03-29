﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CController : MonoBehaviour {

    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CFsm fsm { get; private set; }

    public void Awake() {
        fsm = new CFsm(this);
        input = new ControllerInput(ControllerId.One);
        rbody = GetComponent<Rigidbody>();
    }

    public void Update() {
        input.Update();
    }

    public void FixedUpdate() {
        fsm.Update();
        input.FixedUpdate();
    }

    public float turnSpeed = 0.2f;
    public void Look() {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25f) {
            transform.forward = Vector3.Lerp(transform.forward, vec, turnSpeed);
        }
    }
}
