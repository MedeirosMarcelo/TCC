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
        public Variable Distance { get; protected set; }
        public Variable Threat { get; protected set; }
        public Variable Stress { get; protected set; }

        public MinionFsm(MinionController minion)
        {
            Minion = minion;
            Distance = new Variable(
                new Set("close", new L(1f, 2f)),
                new Set("mid", new Trapezoidal(1f, 2f, 3f, 4f)),
                new Set("far", new Gamma(3f, 4f))
            );
            Threat = new Variable(
                new Set("danger", new L(2f, 3f)),
                new Set("vigilant", new Trapezoidal(2f, 3f, 4f, 7f)),
                new Set("safe", new Gamma(4f, 7f))
            );
            Stress = new Variable(
                new Set("relaxed", new L(-1f, 1f)),
                new Set("stressed", new Gamma(-1f, 1f))
            );

            AddStates(new Idle(this),
                      new Advance(this),
                      new Retreat(this),
                      new Circle(this),
                      new WindUp(this),
                      new Swing(this),
                      new Recover(this),
                      new Defeat(this),
                      new Win(this),
                      new Stand(this),
                      new End(this)
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