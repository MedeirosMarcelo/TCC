using UnityEngine;


public class AnimationBehaviour : BaseBehaviour
{
    public Animator Animator { get; protected set; }

    public float TotalTime { get; set; }
    public float CrossFadeTime { get; set; }
    public new string Name { get; set; }

    public AnimationBehaviour(IState state, Animator animator) : base(state)
    {
        Animator = animator;
        TotalTime = 0f;
        CrossFadeTime = 0.1f;
        Name = "";
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        if (Name != "")
        {
            Animator.CrossFade(Name, CrossFadeTime);
        }
        if (TotalTime != 0f)
        {
            Animator.SetFloat("Speed", 1f / TotalTime);
        }
    }
}

