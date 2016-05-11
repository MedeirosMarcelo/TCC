using UnityEngine;

public abstract class MinionState : BehaviourState
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

    public MinionState(MinionFsm fsm)
    {
        Fsm = fsm;
        Minion = fsm.Minion;
        Rigidbody = Minion.Rbody;
        Transform = Minion.transform;
        Animator = Minion.Animator;
    }
}