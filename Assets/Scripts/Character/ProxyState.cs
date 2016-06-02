using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Character
{
    public abstract class ProxyState : CharacterState
    {
        public ProxyState(CharacterFsm fsm) : base(fsm)
        {
        }
        public sealed override void PreUpdate()
        {
            Assert.IsTrue(false);
        }
        public sealed override void FixedUpdate()
        {
            Assert.IsTrue(false);
        }
        public sealed override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
        }
        public sealed override void OnTriggerEnter(Collider colllider)
        {
            Assert.IsTrue(false);
        }
        public sealed override void OnCollisionEnter(Collision collision)
        {
            Assert.IsTrue(false);
        }
    }
}