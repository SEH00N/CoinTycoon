using System.Collections.Generic;
using H00N.Geometry2D;
using UnityEngine;

namespace ProjectCoin.Tests
{
    public class TDelaunayTriangle : MonoBehaviour
    {
        private DelaunayTriangle delaunayTriangle = null;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2[] vertices = new Vector2[5];
                for (int i = 0; i < vertices.Length; ++i)
                    vertices[i] = Random.insideUnitCircle * 5f;

                delaunayTriangle = new DelaunayTriangle(vertices);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (delaunayTriangle == null)
                return;

            Gizmos.color = Color.red;
            delaunayTriangle.Triangles.ForEach(i =>
            {
                string log = "";
                for (int index = 0; index < 3; ++index)
                {
                    log += i[index].ToString();
                    Gizmos.DrawLine(i[index], i[(index + 1) % 3]);
                }

                Debug.Log(log);
            });
        }
#endif
    }
}