using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Fuzzy.Defuzzification
{
    public class Centroid
    {
        Variable variable;
        List<float> samples;
        public Centroid(Variable variable, List<float> samples)
        {
            this.variable = variable;
            this.samples = samples;
            this.samples.Sort();
        }
        public float Eval(Dictionary<string, float> inferred)
        {
            Assert.IsTrue(inferred.Keys.Except(variable.dict.Keys).Count() == 0);
            float numerator = 0f;
            float denominator = 0f;

            foreach (var sample in samples)
            {
                variable.Value = sample;
                float max = 0f;
                foreach (var item in inferred)
                {
                    max = Mathf.Max(max, Mathf.Min(variable[item.Key], item.Value));
                }
                denominator += max;
                numerator += max * sample;
                // Debug.Log("s=" + sample + "m=" + max + "n=" + numerator + "d=" + denominator);
            }
            return numerator / denominator;
        }
    }
}