using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CBlock : CState
    {
        public CBlock(CFsm fsm) : base(fsm, fsm.Character)
        {
            Name = "BLOCK";
            nextState = "IDLE";
            totalTime = 0.5f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.Play("Block Mid");
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Sword")
            {
                var otherCharacter = collider.transform.parent.GetComponent<CController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackFsm = otherCharacter.fsm.Current as AttackFsm;
                    if (attackFsm != null && attackFsm.Current.Name == "SWING")
                    {
                        Character.ShowBlockSpark(collider.transform.position);
                        return;
                    }
                }
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }
    }
}