using UnityEngine;


public class AnimationBehaviour : BaseBehaviour
{
    public Animator Animator { get; protected set; }

    public float TotalTime { get; set; }
    public string Name { get; set; }

    public AnimationBehaviour(IState state, Animator animator) : base(state)
    {
        Animator = animator;
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        if (TotalTime != 0f)
        {
            Animator.SetFloat("Speed", 1f / TotalTime);
        }
        if (Name != "")
        {
            Animator.PlayInFixedTime(Name);
        }
    }
    public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        Animator.PlayInFixedTime(Name, layer: -1, fixedTime: 0f);
    }
}

