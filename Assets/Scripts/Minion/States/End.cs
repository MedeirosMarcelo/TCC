using UnityEngine;

namespace Assets.Scripts.Minion.States
{
    public class End : MinionState
    {
        public End(MinionFsm fsm) : base(fsm)
        {
            Name = "END";
        }
        public override void PreUpdate()
        {
        }
        public override void FixedUpdate()
        {
        }
        public override void OnCollisionEnter(Collision collision)
        {
        }
        public override void OnTriggerEnter(Collider colllider)
        {
        }
    }
}