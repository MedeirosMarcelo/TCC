using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class CState : BaseState
{
    public CController Character { get; protected set; }
    public BaseInput Input { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }

    public CState(BaseFsm fsm, CController character)
    {
        Fsm = fsm;
        Character = character;
        Input = Character.input;
        Rigidbody = Character.rbody;
        Transform = Character.transform;
    }
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.name == "Sword")
        {
            PlayerIndex swordJoystick = collider.transform.parent.GetComponent<CController>().joystick;
            string attackerState = collider.transform.parent.GetComponent<CController>().fsm.Current.Name;
            if (swordJoystick != Character.joystick)
            {
                if (Fsm.Current.Name == "BLOCK")
                {
                    Character.ShowBlockSpark(collider.transform.position);
                }
                else
                {
                    if (attackerState == "ATTACK")
                    {
                        Character.bloodAnimator.SetTrigger("Bleed");
                        Character.ReceiveDamage(1);
                    }
                }
            }
        }
    }
}

