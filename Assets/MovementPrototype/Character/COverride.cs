using System;
using UnityEngine;
using UnityEngine.Assertions;

public class COverride : MonoBehaviour
{
#if UNITY_EDITOR

    public enum Action
    {
        ATTACK,
        DASH,
        BLOCK
    };

    [Header("F9 Enable/Disable")]
    public bool Enable = false;

    [Header("F10 toggle Action")]
    public Action action;

    CController character;
    Transform Transform { get { return character.transform; } }
    BaseFsm Fsm { get { return character.fsm; } }
    IState Current { get { return Fsm.Current; } }

    void Start()
    {
        character = gameObject.GetComponent<CController>();
        Assert.IsNotNull(character);

    }

    void UpdateAction()
    {
        switch (action)
        {
            case Action.ATTACK:
                if (Current.Name == "IDLE")
                {
                    var stick = new Stick();
                    stick.horizontal = Transform.forward.x;
                    stick.vertical = Transform.forward.z;
                    var evt = new InputEvent.Attack(stick);
                    Fsm.ChangeState("ATTACK", 0f, evt);
                }
                break;
            case Action.DASH:
                if (Current.Name == "IDLE")
                {
                    var stick = new Stick();
                    stick.horizontal = Transform.forward.x;
                    stick.vertical = Transform.forward.z;
                    var evt = new InputEvent.Dash(stick);
                    Fsm.ChangeState("DASH", 0f, evt);
                }
                break;
            case Action.BLOCK:
                if (Current.Name == "IDLE")
                {
                    Fsm.ChangeState("BLOCK");
                }
                break;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Enable = !Enable;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            action++;
            if ((int)action == Enum.GetValues(typeof(Action)).Length)
            {
                action = 0;
            }
        }
        if (Enable)
        {
            UpdateAction();
        }
    }

#endif
}
