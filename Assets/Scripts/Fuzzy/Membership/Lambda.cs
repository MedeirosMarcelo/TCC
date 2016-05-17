using System.Diagnostics;

namespace Assets.Scripts.Fuzzy.Membership
{
    public class Lambda : IFunction
    {
        float alpha;
        float beta;
        float gama;

        public Lambda(float alpha, float beta, float gama)
        {
            Debug.Assert(gama > beta);
            Debug.Assert(beta > alpha);

            this.alpha = alpha;
            this.beta = beta;
            this.gama = gama;
        }
        public float Eval(float value)
        {
            if (value < alpha) //  u <= a
            {
                return 0f;
            }
            else if (value < beta) // a < u <= b
            {
                return (value - alpha) / (beta - alpha);
            }
            else if (value < gama) // c < u < d
            {
                return (gama - value) / (gama - beta);
            }
            else // u >= d
            {
                return 0f;
            }
        }
    }
}
