using Assets.MovementPrototype.Character.States.AttackStates;
using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockHighSwing : BlockState
    {
        const float speed = 2f;
        public BlockHighSwing(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/SWING";
            nextState = "BLOCK/HIGH/RECOVER";
            totalTime = 0.5f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.Play("Block High");
            Character.BlockHighCollider.enabled = true;
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockHighCollider.enabled = false;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Attack Collider")
            {
                var otherCharacter = collider.transform.parent.parent.GetComponent<CharController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackerState = otherCharacter.fsm.Current as AttackSwing;
                    if (attackerState != null && (attackerState.Name == "DOWN/LIGHT/SWING" ||
                                                   attackerState.Name == "DOWN/HEAVY/SWING"))
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
