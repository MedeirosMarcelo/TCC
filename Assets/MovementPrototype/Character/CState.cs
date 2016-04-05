using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class CState : BaseState {
    public new CFsm Fsm { get; set; }
    protected CController Character {
        get { return Fsm.Character; }
    }
    protected BaseInput Input {
        get { return Fsm.Character.input; }
    }
    protected Rigidbody Rigidbody {
        get { return Fsm.Character.rbody; }
    }
    protected Transform Transform {
        get { return Fsm.Character.transform; }
    }
    public CState(CFsm fsm) : base(fsm) {
        Fsm = fsm;
    }
}

