using UnityEngine;

namespace Assets.Scripts.Character.States
{
    public class Death : CharacterState
    {
        //public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public Death(CharacterFsm fsm) : base(fsm)
        {
            Name = "DEATH";
            turnRate = 0f;

            //timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.animator);
            animation.TotalTime = 3.067f;
            animation.PlayTime = 3.067f;
            animation.Name = "Defeat";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.rbody.isKinematic = true;
            Character.transform.Find("Hitbox").gameObject.SetActive(false);
            Character.transform.Find("PushCollider").gameObject.SetActive(false);
            Character.AttackCollider.enabled = false;
            Character.animator.Play("Defeat");
            AudioManager.Play(ClipType.Dead, Character.audioSource);
            Character.Die();
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.rbody.isKinematic = false;
            Character.transform.Find("Hitbox").gameObject.SetActive(true);
            Character.transform.Find("PushCollider").gameObject.SetActive(true);
            Character.AttackCollider.enabled = true;
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void OnTriggerEnter(Collider collider) { }
    }
}