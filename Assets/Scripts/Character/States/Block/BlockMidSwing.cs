using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Block
{
    public class BlockMidSwing : BlockSwing
    {
        public BlockMidSwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/SWING";
            nextState = "BLOCK/MID/RECOVER";
        }
        public override void OnTriggerEnter(Collider collider)
        {
            IAttack attack;
            if (collider.IsAttack(out attack))
            {
                if (attack != null && attack.Direction == AttackDirection.Horizontal)
                {
                    RaycastHit hitInfo;
                    Assert.IsTrue(attack.GetCollisionPoint(out hitInfo), "IT SHOULD HAVE HIT BUT IT DID NOT HIT SEND HELP");
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
            // otherwise defer to base
            base.OnTriggerEnter(collider);
        }
    }
}
