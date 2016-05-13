using System.Diagnostics;

namespace Assets.Scripts.Fuzzy.Membership
{
    public class TrapezoidalFunction : MembershipFunction<float>
    {
        float alpha;
        float beta;
        float gama;
        float delta;

        public TrapezoidalFunction(float alpha, float beta, float gama, float delta)
        {
            Debug.Assert(delta > gama);
            Debug.Assert(gama > beta);
            Debug.Assert(beta > alpha);

            this.alpha = alpha;
            this.beta = beta;
            this.gama = gama;
            this.delta = delta;
        }
        public override float Eval(float value)
        {
            if (value < alpha) //  u <= a
            {
                return 0f;
            }
            else if (value < beta) // a < u < b
            {
                return (value - alpha) / (beta - alpha);
            }
            else if (value <= gama) // b <= u <= c
            {
                return 1f;
            }
            else if (value < delta) // c < u < d
            {
                return (delta - value) / (delta - gama);
            }
            else // u >= d
            {
                return 0f;
            }
        }
    }
}
