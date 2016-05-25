using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Fuzzy
{
    public class Variable
    {
        public Dictionary<string, Set> dict { get; protected set; }
        public float Value { get; set; }

        public Variable(params Set[] sets)
        {
            dict = new Dictionary<string, Set>();
            foreach (var set in sets)
            {
                Add(set);
            }
        }

        public float this[string name]
        {
            get { return dict[name].Membership(Value); }
        }
        void Add(Set set)
        {
            dict.Add(set.Name, set);
        }
        public new string ToString()
        {
            return "[" + string.Join(",", dict.Keys.ToArray()) + "]"
                + "(" + string.Join(",", dict.Values.Select(item => item.Membership(Value).ToString("N2")).ToArray()) + ")";
        
        }
    }
}
