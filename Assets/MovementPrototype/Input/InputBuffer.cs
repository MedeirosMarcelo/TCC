using UnityEngine;
using System.Collections;

public abstract class InputEvent {

    public class Dash : InputEvent {
        public Stick move { get; private set; }
        public Dash(Stick move) {
            this.move = move;
        }
    }

    public class Attack : InputEvent {
    }

    public class Block : InputEvent {
    }

}


public class InputBuffer {

    private InputEvent buffer = null;

    public bool NextEventIs<T>() where T : InputEvent {
        return (buffer != null) ? (buffer.GetType() == typeof(T)) : false;
    }

    public T Pop<T>() where T : InputEvent {
        Debug.Log("InputBuffer poped: " + buffer);
        var evt = (T)buffer;
        buffer = null;
        return evt;
    }

    public void Push<T>(T ev) where T : InputEvent {
        buffer = ev;
        elapsed = 0f;
    }

    static readonly float timeout = 0.5f;
    float elapsed = 0f;

    public void Update() {
        if (buffer != null) {

            elapsed += Time.deltaTime;
            if (elapsed > timeout) {

                Debug.Log("InputBuffer timeout: " + buffer);
                buffer = null;
            }
        }
    }
}