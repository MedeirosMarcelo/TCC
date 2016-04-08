using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class CState : BaseState
{
    public CController Character{ get; protected set; }
    public BaseInput Input { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }

    public CState(CFsm fsm)
    {
        Fsm = fsm;
        Character = fsm.Character;
        Input = Character.input;
        Rigidbody = Character.rbody;
        Transform = Character.transform;
    }
}

