using Assets.MovementPrototype.Character.States.AttackStates;
using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidSwing : BlockBase
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
            Character.BlockMidCollider.enabled = true;
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = false;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Attack Collider")
            {
                var otherCharacter = collider.transform.parent.parent.GetComponent<CharController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackerState = otherCharacter.fsm.Current as AttackSwing;
                    if (attackerState != null && (attackerState.Name == "RIGHT/LIGHT/SWING" ||
                                                  attackerState.Name == "RIGHT/HEAVY/SWING" ||
                                                  attackerState.Name == "LEFT/LIGHT/SWING" ||
                                                  attackerState.Name == "LEFT/HEAVY/SWING" ||
                                                  attackerState.Name == "LUNGE/LIGHT/SWING"))
                    {
                        Character.ShowBlockSpark(collider.transform.position);
                        otherCharacter.fsm.ChangeState("STAGGER");
                        return;
                    }
                }
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }
    }
}
