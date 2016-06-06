using UnityEngine;
using Assets.Scripts.Minion.States;
using Assets.Scripts.Minion.States.Attack;
using Assets.Scripts.Fuzzy;
using Assets.Scripts.Fuzzy.Membership;

namespace Assets.Scripts.Minion
{
    public class MinionFsm : BaseFsm
    {
        public MinionController Minion { get; protected set; }
        // Fuzzy variables
        public Variable Stamina { get; protected set; }
        public Variable Bravery { get; protected set; }
        public Variable Distance { get; protected set; }

        public MinionFsm(MinionController minion)
        {
            Minion = minion;
            Stamina = new Variable(
                new Set("low", new L(0f, 1f)),
                new Set("high", new Gamma(0f, 1f))
            );
            Stamina.Value = 50f;
            Bravery = new Variable(
                new Set("low", new L(0f, 1f)),
                new Set("high", new Gamma(0f, 1f))
            );
            Bravery.Value = Random.value;
            //Debug.Log("Minion Bravary = " + Bravery.Value);
            Distance = new Variable(
                new Set("close", new L(1f, 2f)),
                new Set("mid", new Trapezoidal(1f, 2f, 3f, 4f)),
                new Set("far", new Gamma(3f, 4f))
            );

            AddStates(new Idle(this),
                      new Advance(this),
                      new Circle(this),
                      new WindUp(this),
                      new Swing(this),
                      new Recover(this),
                      new End(this),
                      new Death(this)
                      );
            Start("IDLE");
        }
        public override void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            //Debug.Log(string.Format("{0} >> {1}", Current.Name, nextStateName));
            base.ChangeState(nextStateName, additionalDeltaTime, args);
        }
    }
}