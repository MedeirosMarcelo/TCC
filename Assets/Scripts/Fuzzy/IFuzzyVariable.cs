namespace Assets.Scripts.Fuzzy
{
    public interface IFuzzyVariable
    {
        string Name { get; }
        float this[string name] { get; }
    }
}