using UnityEngine;

namespace H00N.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 PlaneVector(this Vector3 origin)
        {
            return new Vector2(origin.x, origin.y);
        }
    }
}