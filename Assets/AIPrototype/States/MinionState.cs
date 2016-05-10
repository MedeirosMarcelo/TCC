using UnityEngine;

public abstract class MinionState : BaseState
{
    public MinionController Minion { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }
    public Transform Target
    {
        get { return Minion.Target; }
        protected set { Minion.Target = value; }
    }

    // Timed state
    protected float elapsed;
    protected float totalTime;
    protected string nextState;

    public MinionState(MinionFsm fsm)
    {
        Fsm = fsm;
        Minion = fsm.Minion;
        Rigidbody = Minion.Rbody;
        Transform = Minion.transform;
    }
    public override void PreUpdate()
    {
        base.PreUpdate();
        if (totalTime > 0f && elapsed >= totalTime)
        {
            Fsm.ChangeState(nextState, totalTime - elapsed);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        elapsed += Time.fixedDeltaTime;
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        elapsed = additionalDeltaTime;
    }
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
}