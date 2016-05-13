using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy
{
    public class FuzzySet
    {
        Dictionary<string, IFuzzyVariable> variables = new Dictionary<string, IFuzzyVariable>();

        void AddValue(IFuzzyVariable variable) {
            variables.Add(variable.Name, variable);
        }
    }
}
