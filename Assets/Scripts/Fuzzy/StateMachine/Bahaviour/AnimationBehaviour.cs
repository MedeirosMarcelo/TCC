using UnityEngine;


namespace Assets.Scripts.Fuzzy.StateMachine.Behaviour
{
    public class AnimationBehaviour : BaseBehaviour
    {
        public Animator Animator { get; protected set; }

        public float AnimationTime { get; set; }
        public string Animation { get; set; }

        public AnimationBehaviour(IState state, Animator animator) : base(state)
        {
            Animator = animator;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Animator.SetFloat("Speed", 1f / AnimationTime);
            Animator.PlayInFixedTime(Animation);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Animator.PlayInFixedTime(Animation, layer: -1, fixedTime: 0f);
        }
    }
}

