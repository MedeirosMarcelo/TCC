using UnityEngine;
using System.Collections;

public class Stick {
    public float horizontal;
    public float vertical;
    public Vector3 vector {
        get { return new Vector3(horizontal, 0, vertical); }
    }
}

public class BaseInput {
 
    public virtual string name { get; protected set; }

    private Stick _move = new Stick();
    private Stick _look = new Stick(); 

    public virtual Stick move {
        get { return _move; }
        protected set { _move = value; }
    }
    public virtual Stick look {
        get { return _look; }
        protected set { _look = value; }
    }

    public virtual bool dash { get; protected set; }
    public virtual bool slash { get; protected set; }
    public virtual bool heavy { get; protected set; }
    public virtual float deadZone { get; protected set; }

    public BaseInput() {
        name = "Generic input";
        move.horizontal = 0.0f;
        move.vertical = 0.0f;
        look.horizontal = 0.0f;
        look.vertical = 0.0f;
        deadZone = 0.2f;
        dash = false;
        slash = false;
        heavy = false;
    }

    // must be called each Update before consuming any input.
    public virtual void Update() {
    }

    // must be called each FixedUpdate after consuming all input;
    public virtual void FixedUpdate() {
    }

    // get debug msg;
    public virtual string DebugMsg() {
        return (name + ":  M=" + move.vector + "  L=" + look.vector);
    }
}
