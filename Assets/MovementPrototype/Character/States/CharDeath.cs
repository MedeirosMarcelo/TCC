using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CharDeath : CharState
    {
        public CharDeath(CharFsm fsm)
            : base(fsm)
        {
            Name = "DEATH";
            turnRate = 0f;
        }
        public override void Enter(string lastName, string nextName, float additionalDeltaTime, params object[] args)
        {
            Character.animator.Play("Death");
        }

        public override void OnTriggerEnter(Collider collider) { }
    }
}