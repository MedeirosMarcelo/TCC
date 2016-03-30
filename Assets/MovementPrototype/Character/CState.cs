using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum CStateEnum {
    Idle,
    Walk,
    Dash,
    Attacking
}

public class CState {

    public CFsm fsm { get; private set; }
    public CStateEnum value { get; private set; }

    public CState(CFsm fsm, CStateEnum value) {
        this.fsm = fsm;
        this.value = value;
    }

    public CController character {
        get { return fsm.character; }
    }

    public Transform transform {
        get { return character.transform; }
    }

    public Rigidbody rbody {
        get { return character.rbody; }
    }

    public BaseInput input {
        get { return character.input; }
    }

    public Vector3 position {
        get { return transform.position; }
        set { transform.position = value; }
    }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    public virtual void PreUpdate() {
        if (input.dash) {
            fsm.ChangeState(CStateEnum.Dash);
            return;
        }
        if (input.move.vector.magnitude > 0.2f) {
            fsm.ChangeState(CStateEnum.Walk);
            return;
        }
        if (value != CStateEnum.Idle) {
            fsm.ChangeState(CStateEnum.Idle);
        }
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class CIdle : CState {
    public CIdle(CFsm fsm) : base(fsm, CStateEnum.Idle) {
    }

    public override void Update() {
        character.Look();
    }
}
