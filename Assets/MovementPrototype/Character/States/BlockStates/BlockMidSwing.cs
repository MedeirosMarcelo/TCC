using Assets.MovementPrototype.Character.States.AttackStates;
using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidSwing : BlockState
    {
        const float speed = 2f;
        public BlockMidSwing(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/SWING";
            nextState = "BLOCK/MID/RECOVER";
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
                var otherCharacter = collider.transform.parent.GetComponent<CharController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackerState = otherCharacter.fsm.Current as AttackSwing;
                    if (attackerState != null && (attackerState.Name == "SWING" ||
                                                  attackerState.Name == "RIGHTSWING" ||
                                                  attackerState.Name == "DOWNSWING"))
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
