using UnityEngine;
using System.Collections;

public class Stick {
    public float deadZone = 0.25f;
    public float horizontal = 0f;
    public float vertical = 0f;
    public Vector3 vector {
        get { return new Vector3(horizontal, 0, vertical); }
    }
    public bool isActive {
        get { return (vector.magnitude > deadZone);  }
    }
}

public class BaseInput {

    public virtual string name { get; protected set; }

    public virtual Stick move { get; private set; }
    public virtual Stick look { get; private set; }

    public virtual float run { get; protected set; }

    public virtual bool dash { get; protected set; }
    public virtual bool attack { get; protected set; }
    public virtual bool heavyAttack { get; protected set; }
    public virtual bool lunge { get; protected set; }
    public virtual bool block { get; protected set; }
    public virtual bool blockHigh { get; protected set; }

    public virtual InputBuffer buffer { get; private set; }

    public BaseInput() {
        name = "Generic input";
        move = new Stick();
        look = new Stick();
        buffer = new InputBuffer();

        dash = false;
        attack = false;
        heavyAttack = false;
        lunge = false;
        block = false;
        blockHigh = false;
        run = 0f;
    }

    // must be called each Update before consuming any input.
    public virtual void Update() {
    }

    // must be called each FixedUpdate after consuming all input;
    public virtual void FixedUpdate() {
    }

    // get debug msg;
    public virtual string Debug { get
        {
            return "";
        }
    }
}
