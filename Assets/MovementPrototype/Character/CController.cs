using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerIndex : int
{
    One = 1,
    Two = 2,
    //Three = 3,
    //Four  = 4
}

public class CController : MonoBehaviour
{
    public PlayerIndex joystick = PlayerIndex.One;

    public GameObject opponent;
    public Animator swordAnimator;
    public Animator bloodAnimator;
    public BaseInput input { get; private set; }
    public Rigidbody rbody { get; private set; }
    public CFsm fsm { get; private set; }

    public void Awake()
    {
        input = new GamePadInput(joystick);
        rbody = GetComponent<Rigidbody>();
        fsm = new CFsm(this);
    }

    public void Update()
    {
        input.Update();
    }

    public void FixedUpdate()
    {
        fsm.PreUpdate();
        fsm.Update();
        input.FixedUpdate();
    }

    public void ChangeVelocity(Vector3 velocity)
    {
        rbody.velocity = velocity;
    }

    public void Move(Vector3 position)
    {
        rbody.MovePosition(position);
    }

    float maxTurnSpeed = Mathf.PI / 30;
    public void Look(float turnRate = 1f)
    {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25f)
        {
            transform.forward = Vector3.RotateTowards(
                transform.forward,
                vec.normalized,
                maxTurnSpeed * turnRate,
                1f);
        }
        else if (opponent != null)
        {
            transform.forward = Vector3.RotateTowards(
                transform.forward,
                (opponent.transform.position - transform.position).normalized,
                maxTurnSpeed * turnRate,
                1f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Sword")
        {
            PlayerIndex swordJoystick = col.transform.parent.parent.GetComponent<CController>().joystick;
            if (swordJoystick != this.joystick && fsm.Current.Name != "BLOCK")
            {
                bloodAnimator.SetTrigger("Bleed");
            }
        }
    }

    void OnGUI()
    {
        input.OnGUI();
    }
}
