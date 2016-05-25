using UnityEngine;

public static class Extensions
{
    public static Vector3 xy(this Vector3 v)
    {
        v.z = 0;
        return v;
    }
    public static Vector3 xz(this Vector3 v)
    {
        v.y = 0;
        return v;
    }
    public static Vector3 yz(this Vector3 v)
    {
        v.x = 0;
        return v;
    }
}
