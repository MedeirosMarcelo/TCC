﻿using System.Diagnostics;

namespace Assets.Scripts.Fuzzy.Membership
{
    public class Gamma: IFunction
    {
        float alpha;
        float beta;
        public Gamma(float alpha, float beta)
        {
            Debug.Assert(beta > alpha);
            this.alpha = alpha;
            this.beta = beta;
        }
        public float Eval(float value)
        {
            if (value <= alpha) // u <= a
            {
                return 0f;
            }
            else if (value < beta) // a < u < b
            {
                return (value - alpha) / (beta - alpha);
            }
            else // u >= b
            {
                return 1f;
            }
        }
    }
}
