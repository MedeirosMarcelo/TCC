using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy.StateMachine
{
    public class BaseStateBehaviour : IStateBehaviour
    {
        public IState State { get; protected set; }
        public BaseStateBehaviour(IState state)
        {
            State = state;
            State.AddBehaviour(this);
        }
        public string Name { get; protected set; }
        public string DebugString
        {
            get { return Name; }
        }
        public virtual void PreUpdate()
        {
        }
        public virtual void FixedUpdate()
        {
        }
        public virtual void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
        }
        public virtual void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
        }
        public virtual void OnTriggerEnter(Collider colllider)
        {
        }
        public virtual void OnCollisionEnter(Collision collision)
        {
        }
    }
}