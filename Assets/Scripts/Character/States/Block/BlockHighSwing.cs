using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Block
{
    public class BlockHighSwing : BlockSwing
    {
        public BlockHighSwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/SWING";
            nextState = "BLOCK/HIGH/RECOVER";
        }
        public override void OnTriggerEnter(Collider collider)
        {
            IAttack attack;
            if (collider.IsAttack(out attack))
            {
                if (attack != null && attack.Direction == AttackDirection.Vertical)
                {
                    RaycastHit hitInfo;
                    if (attack.GetCollisionPoint(out hitInfo))
                    {
                        Vector3 myForward = Transform.forward.xz().normalized;
                        Vector3 otherForward = (hitInfo.point - Transform.position).xz().normalized;
                        if (Mathf.Abs(Vector3.Angle(myForward, otherForward)) <= defenseAngle / 2f && attack.CanHit(Character))
                        {
                            Character.ShowBlockSpark(collider.transform.position);
                            attack.Blocked();
                            if (attack.IsHeavy)
                            {
                                Fsm.ChangeState("STAGGER");
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
