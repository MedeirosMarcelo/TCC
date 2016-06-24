using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Block
{
    public abstract class BlockSwing : CharacterState
    {
        private TimerBehaviour timer;
        private bool holding;
        private bool locked;
        protected new string nextState;
        public BlockSwing(CharacterFsm fsm) : base(fsm)
        {
            canPlayerMove = true;
            moveSpeed = 0.75f;
            turnRate = 0.25f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.3f;
            timer.OnFinish = () => locked = false;
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
                if (!locked)
                {
                    Fsm.ChangeState(nextState);
                }
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = true;
            holding = true;
            locked = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.BlockMidCollider.enabled = false;
        }

        public void Block(Collider collider, AttackDirection blockDirection)
        {
            IAttack attack;
            if (collider.IsAttack(out attack))
            {
                if (attack != null && attack.Direction == blockDirection)
                {
                    RaycastHit hitInfo;
                    if (attack.GetCollisionPoint(out hitInfo))
                    {
                        Vector3 myForward = Transform.forward.xz().normalized;
                        Vector3 otherForward = (hitInfo.point - Transform.position).xz().normalized;
                        Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.black, 2f);
                        float dot = Vector3.Dot(myForward, otherForward);
                        if (Mathf.Abs(Vector3.Angle(myForward, otherForward)) <= defenseAngle / 2f)
                        {
                            Character.ShowBlockSpark(collider.transform.position);
                            attack.Blocked();
                            if (attack.IsHeavy)
                            {
                                Fsm.ChangeState("STAGGER");
                            }
                            return;
                        }
                        else
                        {
                            Debug.Log("HIT DOT = " + dot);
                        }
                    }
                }
            }
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }


    }
}
