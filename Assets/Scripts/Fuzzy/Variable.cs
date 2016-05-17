using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy
{
    public class Variable
    {
        public Dictionary<string, Set> values { get; protected set; }
        public float Value { get; set; }
        public string Name { get; protected set; }

        public Variable(string name, params Set[] sets)
        {
            values = new Dictionary<string, Set>();
            Name = name;
            foreach (var set in sets)
            {
                Add(set);
            }
        }

        public float this[string name]
        {
            get { return values[name].Membership(Value); }
        }
        void Add(Set set)
        {
            values.Add(set.Name, set);
        }
    }
}
