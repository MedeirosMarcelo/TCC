using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CFsm {
    public CController character;
    public Dictionary<CStateEnum, CState> dict;

    public CState last    { get; private set; }
    public CState current { get; private set; }
    public CState next    { get; private set; }

    public CFsm(CController character) {
        this.character = character;
        dict = new Dictionary<CStateEnum, CState>() {
            { CStateEnum.Idle, new CIdle(this) },
            { CStateEnum.Walk, new CWalk(this) },
            { CStateEnum.Dash, new CDash(this) },
        };
        current = dict[CStateEnum.Idle];
    }

    public void Update() {
        current.PreUpdate();
        current.Update();
    }

    public void ChangeState(CStateEnum nextState) {
        Debug.Log("ChangeState: from " + current.value + " to " + nextState);

        // Notice there is no check if is a different state, this is by design
        next = dict[nextState];
        last = current;
        last.Exit();

        current = next;
        next = null;
        current.Enter();
    }
}