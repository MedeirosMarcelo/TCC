using UnityEngine;

namespace Assets.Scripts.Minion.States
{
    public class Death : MinionState
    {
        public Death(MinionFsm fsm) : base(fsm)
        {
            Name = "DEATH";
        }
        public override void Enter(string lastName, string nextName, float additionalDeltaTime, params object[] args)
        {
            Minion.Rigidbody.isKinematic = true;
            foreach (var collider in Minion.gameObject.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            Minion.Stop();
            Rigidbody.isKinematic = true;
            Animator.Play("Death");
        }
        public override void PreUpdate()
        {
        }
        public override void FixedUpdate()
        {
        }
        public override void OnTriggerEnter(Collider collider)
        {
        }
        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}