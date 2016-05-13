using System;
using UnityEngine;
using UnityEngine.Assertions;
using Assets.MovementPrototype.Character.States.HoldAttackStates;

public class CharOverride : MonoBehaviour
{
#if UNITY_EDITOR

    public enum Action
    {
        ATTACKMID,
        ATTACKHIGH,
        HATTACKLIGHT,
        HATTACKHEAVY,
        DASH,
        HBLOCK,
        BLOCKMID,
        BLOCKHIGH,
        BLOCKMIDFOREVER,
        BLOCKHIGHFOREVER,
        BLOCKFOREVER
    };

    [Header("F9 Enable/Disable")]
    public bool Enable = false;

    [Header("F10 toggle Action")]
    public Action action = Action.ATTACKMID;

    CharController character;
    Transform Transform { get { return character.transform; } }
    BaseFsm Fsm { get { return character.fsm; } }
    IState Current { get { return Fsm.Current; } }

    void Start()
    {
        character = gameObject.GetComponent<CharController>();
        Assert.IsNotNull(character);
    }


    void UpdateAction()
    {
        const string returnState = "MOVEMENT";
        Assert.IsTrue(Fsm.dict.ContainsKey(returnState), "Unknown return state: " + returnState);

        switch (action)
        {
            case Action.ATTACKMID:
                if (Current.Name == returnState)
                {
                    //var evt = new InputEvent.Attack();
                    //Fsm.ChangeState("ATTACK", 0f, evt);
                    Fsm.ChangeState("RIGHT/LIGHT/WINDUP");
                }
                break;
            case Action.ATTACKHIGH:
                if (Current.Name == returnState)
                {
                    //var evt = new InputEvent.Attack(isHigh: true);
                    //Fsm.ChangeState("ATTACK", 0f, evt);
                    Fsm.ChangeState("DOWN/LIGHT/WINDUP");
                }
                break;
            case Action.HATTACKLIGHT:
                if (Current.Name == returnState)
                {
                    Fsm.ChangeState("HATTACK/LIGHT/WINDUP");
                }
                break;
             case Action.HATTACKHEAVY:
                if (Current.Name == returnState)
                {
                    Fsm.ChangeState("HATTACK/HEAVY/WINDUP");
                }
                break;
            case Action.DASH:
                if (Current.Name == returnState)
                {
                    var stick = new Stick();
                    stick.horizontal = Transform.forward.x;
                    stick.vertical = Transform.forward.z;
                    var evt = new InputEvent.Dash(stick);
                    Fsm.ChangeState("DASH", 0f, evt);
                }
                break;
            case Action.HBLOCK:
                if (Current.Name == returnState)
                {
                    Fsm.ChangeState("BLOCK/WINDUP");
                }
                break;
            case Action.BLOCKFOREVER:
                Fsm.ChangeState("BLOCK/SWING");
                break;
            case Action.BLOCKMID:
                if (Current.Name == returnState)
                {
                    //var evt = new InputEvent.Block();
                    //Fsm.ChangeState("BLOCK/SWING");
                    Fsm.ChangeState("BLOCK/MID/WINDUP");
                }
                break;
            case Action.BLOCKHIGH:
                if (Current.Name == returnState)
                {
                    //var evt = new InputEvent.Block(isHigh: true);
                    //Fsm.ChangeState("BLOCK/HIGH/WINDUP", 0f, evt);
                    Fsm.ChangeState("BLOCK/HIGH/WINDUP");
                }
                break;
                case Action.BLOCKMIDFOREVER:
                Fsm.ChangeState("BLOCK/MID/SWING");
                break;
                    case Action.BLOCKHIGHFOREVER:
                Fsm.ChangeState("BLOCK/HIGH/WINDUP");
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
