using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CCommon : CState
    {
        public CCommon(CFsm fsm) : base(fsm)
        {
            Name = "COMMON";
        }
        public override void PreUpdate()
        {
            if (Character.health <= 0 && Fsm.Current.Name != "DEATH") {
                Fsm.ChangeState("DEATH");
            }
        }
    }
}