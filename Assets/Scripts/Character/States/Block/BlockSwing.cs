using UnityEngine;
using BaseSwing = Assets.Scripts.Character.States.Attack.BaseSwing;
using HeavySwing = Assets.Scripts.Character.States.Attack.HeavySwing;

namespace Assets.Scripts.Character.States.Block
{
    public class BlockSwing : AnimatedState
    {
        private bool holding;
        private float minSwingTime = 0.3f;
        public BlockSwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/SWING";
            nextState = "BLOCK/RECOVER";
            totalTime = 0.8f;
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;
            Animation = "Block Mid";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding)
            {
                if (Character.input.block == false)
                {
                    holding = false;
                }
            }
            else
            {
                if (elapsed > minSwingTime)
                {
                    UnityEngine.Debug.Log("Early end");
                    Fsm.ChangeState(nextState);
                }
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = true;
            holding = true;
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
                var otherCharacter = collider.transform.parent.parent.GetComponent<CharacterController>();
                if (!ReferenceEquals(Character, otherCharacter))
                {
                    var attackerState = otherCharacter.fsm.Current as BaseSwing;
                    if (attackerState != null)
                    {
                        Vector3 myForward = Transform.forward;
                        Vector3 otherForward = attackerState.Character.transform.forward;
                        float dot = Vector3.Dot(myForward, otherForward);
                        float dotAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                        float deltaAngle = Mathf.Abs(Mathf.DeltaAngle(180, dotAngle));
                        if (dot < 0 && deltaAngle <= defenseAngle / 2f)
                        {
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
