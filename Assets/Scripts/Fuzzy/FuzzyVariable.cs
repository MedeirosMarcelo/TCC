using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy
{
    public class FuzzyVariable<T> : IFuzzyVariable
    {
        public string Name { get; protected set; }
        public float this[string name] { get { return values[name].Membership(variable); } }

        T variable;
        Dictionary<string, FuzzyValue<T>> values = new Dictionary<string, FuzzyValue<T>>();

        void Add(FuzzyValue<T> value)
        {
            values.Add(value.Name, value);
        }
    }
}
