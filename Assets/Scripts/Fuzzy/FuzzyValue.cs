using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy
{
    public class FuzzyValue<T>
    {
        MembershipFunction<T> membership;
        public FuzzyValue(string name, MembershipFunction<T> membershipFunction)
        {
            Name = name;
            membership = membershipFunction;
        }


        public string Name { get; protected set; }
        public float Membership(T value)
        {
            return membership.Eval(value);
        }
    }
}
