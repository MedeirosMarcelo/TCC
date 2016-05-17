using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy.StateMachine
{
    public class Fsm
    {
        Dictionary<string, IState> stateDic = new Dictionary<string, IState>();
        public IState Current { get; private set; }
        public string DebugString
        {
            get { return "/" + Current.DebugString; }
        }
        void AddStates(params IState[] states)
        {
            foreach (var state in states)
            {
                Assert.IsNotNull(state.Name, "State doesnt have a Name: " + state.GetType());
                stateDic.Add(state.Name, state);
            }
        }
        public void Start(string startStateName)
        {
            Assert.IsTrue(stateDic.ContainsKey(startStateName), "Unknown next state: " + startStateName);
            Current.Enter("", startStateName, 0f);
        }
        public void Stop()
        {
            Current.Exit(Current.Name, "", 0f);
        }
        public virtual void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            Assert.IsTrue(stateDic.ContainsKey(nextStateName), "Unknown next state: " + nextStateName);
            string lastStateName = Current.Name;
            Current.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Current = stateDic[nextStateName];
            Current.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
        public virtual void PreUpdate()
        {
            Current.PreUpdate();
        }

        public virtual void FixedUpdate()
        {
            Current.FixedUpdate();
        }
        public virtual void OnTriggerEnter(Collider collider)
        {
            Current.OnTriggerEnter(collider);
        }
        public virtual void OnCollisionEnter(Collision collision)
        {
            Current.OnCollisionEnter(collision);
        }
    }
}