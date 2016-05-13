namespace Assets.Scripts.Fuzzy
{
    public abstract class MembershipFunction<T> {
        public abstract float Eval(T value);
    }
}
