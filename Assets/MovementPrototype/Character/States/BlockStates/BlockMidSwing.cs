using UnityEngine;
using BaseSwing = Assets.MovementPrototype.Character.States.HoldAttackStates.BaseSwing;
using HeavySwing = Assets.MovementPrototype.Character.States.HoldAttackStates.HeavySwing;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockMidSwing : BlockBase
    {
        const float speed = 2f;
        public BlockMidSwing(CharFsm fsm)
            : base(fsm)
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
            if (Character.collidedWith != collider.gameObject)
            {
                if (collider.name == "Attack Collider")
                {
                    var otherCharacter = collider.transform.parent.parent.GetComponent<CharController>();
                    if (!ReferenceEquals(Character, otherCharacter))
                    {
                        var attackerState = otherCharacter.fsm.Current;
                        if (attackerState is BaseSwing)
                        {
                            Character.collidedWith = collider.gameObject;
                            Character.ResetCollision();
                            Character.ShowBlockSpark(collider.transform.position);
                            otherCharacter.fsm.ChangeState("STAGGER");
                            if (attackerState is HeavySwing)
                            {
                                Fsm.ChangeState("STAGGER");
                            }
                            else
                            {
                                Fsm.ChangeState(nextState);
                            }
                            return;
                        }
                    }
                }
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }
    }
}
