using UnityEngine;
using Assets.Scripts.Common;

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
    }
}
