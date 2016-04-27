using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class DashRecover : CharState
    {
        public DashRecover(CharFsm fsm) : base(fsm)
        {
            Name = "DASH/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            turnRate = 0f;
        }
    }
}
