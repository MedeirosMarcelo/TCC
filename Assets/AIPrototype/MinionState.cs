using UnityEngine;
using Assets.Scripts.Fuzzy;
using Assets.Scripts.Fuzzy.Operators;

public abstract class MinionState : BaseState
{
    public MinionController Minion { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }
    public Animator Animator { get; private set; }
    public Transform Target
    {
        get { return Minion.Target; }
        protected set { Minion.Target = value; }
    }
    // Fuzzy Variables
    public Variable Stamina { get; protected set; }
    public Variable Bravery { get; protected set; }
    public Variable Distance { get; protected set; }

    // How much stamina this state drains per FixedUpdate
    protected float staminaCost;

    public MinionState(MinionFsm fsm)
    {
        Fsm = fsm;
        Minion = fsm.Minion;
        Rigidbody = Minion.Rigidbody;
        Transform = Minion.transform;
        Animator = Minion.Animator;
        Stamina = fsm.Stamina;
        Bravery = fsm.Bravery;
        Distance = fsm.Distance;
    }

    public override void PreUpdate()
    {
        Distance.Value = (Transform.position - Target.position).xz().magnitude;
        base.PreUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Stamina.Value = Mathf.Clamp(Stamina.Value - staminaCost, 0f, 1f);
    }
    public void NextState()
    {
        var advance = Mamdami.And(Distance["far"], Stamina["high"]);
        var circle = Mamdami.And(Distance["mid"], Stamina["high"]);
        var attack = Mamdami.And(Distance["close"], Stamina["high"]);
        Debug.Log("Distance:" + Distance.ToString() + " Stamina:" + Stamina.ToString());
        Debug.Log("advance,circle,attack = " + advance + "," + circle + "," + attack);

        if (advance > 0.5f)
        {
            Fsm.ChangeState("ADVANCE");
            return;
        }
        if (circle > 0.5f)
        {
            Fsm.ChangeState("CIRCLE");
            return;
        }
        if (attack > 0.5f)
        {
            Fsm.ChangeState("ATTACK/WINDUP");
            return;
        }
        Fsm.ChangeState("IDLE");
    }

    public void Look()
    {
        var forward = Vector3.RotateTowards(
            Transform.forward,
            (Target.transform.position - Transform.position).xz().normalized,
            Mathf.Deg2Rad * Minion.NavAgent.angularSpeed * Time.fixedDeltaTime,
            0f);
        Minion.Forward(forward);
    }
}