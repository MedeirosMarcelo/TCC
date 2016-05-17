using System.Diagnostics;

namespace Assets.Scripts.Fuzzy.Membership
{
    public class Trapezoidal : IFunction
    {
        float alpha;
        float beta;
        float gama;
        float delta;

        public Trapezoidal(float alpha, float beta, float gama, float delta)
        {
            Debug.Assert(delta > gama);
            Debug.Assert(gama > beta);
            Debug.Assert(beta > alpha);

            this.alpha = alpha;
            this.beta = beta;
            this.gama = gama;
            this.delta = delta;
        }
        public float Eval(float value)
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
