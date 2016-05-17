namespace Assets.Scripts.Fuzzy
{
    public class Set
    {
        Membership.IFunction membership;
        public Set(string name, Membership.IFunction membership)
        {
            Name = name;
            this.membership = membership;
        }

        public string Name { get; protected set; }
        public float Membership(float value)
        {
            return membership.Eval(value);
        }
    }
}
