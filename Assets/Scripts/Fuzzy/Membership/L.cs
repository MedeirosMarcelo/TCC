using System.Diagnostics;

namespace Assets.Scripts.Fuzzy.Membership
{
    public class LFunction : MembershipFunction<float>
    {
        float alpha;
        float beta;
        public LFunction(float alpha, float beta)
        {
            Debug.Assert(beta > alpha);
            this.alpha = alpha;
            this.beta = beta;
        }
        public override float Eval(float value)
        {
            if (value <= alpha) // u <= a
            {
                return 1f;
            }
            else if (value < beta) // a < u < b
            {
                return (beta - value) / (beta - alpha);
            }
            else // u >= b
            {
                return 0f;
            }
        }
    }
}
