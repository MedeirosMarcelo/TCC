﻿using UnityEngine;
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

        public override void OnCollisionEnter(Collision collision)
        {
            IAttack attack;
            if (collision.collider.IsAttack(out attack))
            {

            }
            Debug.Log("Collision contsacts=" + collision.contacts.Length);
            foreach(var contact in collision.contacts)
            {
                Debug.Log("Colllider this="+ contact.thisCollider.name
                          + " parent=" + contact.thisCollider.transform.root.name
                          + " other" + contact.otherCollider.name
                          + " parent=" + contact.otherCollider.transform.root.name);

                Debug.DrawLine(contact.point, contact.point + (Vector3.up * 5f), Color.yellow, 5f);
            }
        }

        public override void OnTriggerEnter(Collider collider)
        {
            IAttack attack;
            if (collider.IsAttack(out attack))
            {
                if (attack != null && attack.Direction == AttackDirection.Horizontal)
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
