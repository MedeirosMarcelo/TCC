using UnityEngine;
using System.Collections;

public abstract class InputEvent
{
    public class Dash : InputEvent
    {
        public Stick Move { get; private set; }
        public Dash(Stick move)
        {
            Move = move;
        }
    }
    public class Attack : InputEvent
    {
        public Stick Move { get; private set; }
        public Attack(Stick move)
        {
            Move = move;
        }
    }
    public class Block : InputEvent
    {
    }
}


public class InputBuffer
{
    static readonly float Timeout = 0.5f;
    private InputEvent buffer = null;
    float elapsed = 0f;

    public bool NextEventIs<T>() where T : InputEvent
    {
        return (buffer != null) ? (buffer.GetType() == typeof(T)) : false;
    }
    public T Pop<T>() where T : InputEvent
    {
        Debug.Log("InputBuffer poped: " + buffer);
        var evt = (T)buffer;
        buffer = null;
        return evt;
    }
    public void Push<T>(T ev) where T : InputEvent
    {
        buffer = ev;
        elapsed = 0f;
    }
    public void Update()
    {
        if (buffer != null)
        {
            elapsed += Time.deltaTime;
            if (elapsed > Timeout)
            {
                Debug.Log("InputBuffer timeout: " + buffer);
                buffer = null;
            }
        }
    }
}