using UnityEngine;

namespace Assets.Scripts.Fuzzy.Operators
{
    public static class Mamdami
    {
        public static float And(float rs, float ls)
        {
            return Mathf.Min(rs, ls);
        }
        public static float Or(float rs, float ls)
        {
            return Mathf.Max(rs, ls);
        }
        public static float Not(float ls)
        {
            return (1f - ls);
        }
    }
}