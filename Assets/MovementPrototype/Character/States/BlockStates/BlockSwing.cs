using UnityEngine;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockSwing : BlockState
    {
        const float speed = 2f;
        public BlockSwing(CFsm fsm) : base(fsm)
        {
            Name = "BLOCK/SWING";
            nextState = "BLOCK/RECOVER";
            totalTime = 0.3f;
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
                    if (attackFsm != null && (attackFsm.Current.Name == "SWING" ||
                                              attackFsm.Current.Name == "RIGHTSWING" ||
                                              attackFsm.Current.Name == "DOWNSWING"))
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
