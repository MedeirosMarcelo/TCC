using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy.StateMachine
{
    public class BaseState : IState
    {
        List<IStateBehaviour> behaviours = new List<IStateBehaviour>();
        public Fsm Fsm { get; protected set; }
        public string Name { get; protected set; }
        public string DebugString
        {
            get { return Name; }
        }
        public void AddBehaviour(IStateBehaviour behaviour)
        {
            behaviours.Add(behaviour);
        }
        public virtual void PreUpdate()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.PreUpdate();
            }
        }
        public virtual void FixedUpdate()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.FixedUpdate();
            }
        }
        public virtual void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            }
        }
        public virtual void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            }
        }
        public virtual void OnTriggerEnter(Collider colllider)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnTriggerEnter(colllider);
            }
        }
        public virtual void OnCollisionEnter(Collision collision)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnCollisionEnter(collision);
            }
        }
    }
}