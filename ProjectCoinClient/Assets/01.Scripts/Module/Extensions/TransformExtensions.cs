using System;
using UnityEngine;

namespace H00N.Extensions
{
    public static class TransformExtensions
    {
        public static int DistanceCompare(this Transform transform, Transform a, Transform b)
        {
            float sqrDistanceA = (a.position - transform.position).sqrMagnitude;
            float sqrDistanceB = (b.position - transform.position).sqrMagnitude;
            return sqrDistanceA.CompareTo(sqrDistanceB);
        }
    }
}