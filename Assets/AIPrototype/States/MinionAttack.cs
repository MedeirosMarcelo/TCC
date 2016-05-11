using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionAttack : MinionState
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;
        public MinionAttack(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
            timer = new TimerBehaviour(this);
            timer.TotalTime = 1f;

            animation = new AnimationBehaviour(this, Animator);
            animation.AnimationTime = 1f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            timer.NextState = (Random.value > 0.2) ? "ATTACK" : "WANDER";
            animation.Animation = (Random.value > 0.5) ? "LeftSwing" : "RightSwing";
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = false;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
        public void Look()
        {
            var forward = Vector3.RotateTowards(
                Transform.forward,
                (Target.transform.position - Transform.position).normalized,
                Mathf.Deg2Rad * Minion.NavAgent.angularSpeed * Time.fixedDeltaTime,
                0f);
            Minion.Forward(forward);
        }
    }
}
