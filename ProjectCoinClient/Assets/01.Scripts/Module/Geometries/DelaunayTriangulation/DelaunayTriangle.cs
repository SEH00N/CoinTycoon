using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace H00N.Geometry2D
{
    public class DelaunayTriangle
    {
        protected readonly List<Triangle> triangles = null;
        public List<Triangle> Triangles => triangles;

        public DelaunayTriangle(Vector2[] vertices, float coordinateLimit = 99999f)
        {
            // 슈퍼 삼각형 추가
            Triangle superTriangle = CreateSuperTriangle(coordinateLimit);
            triangles = new List<Triangle>() { superTriangle };

            foreach(Vector2 vertex in vertices)
            {
                // Bad Triangle 솎아내기
                List<Triangle> badTriangles = new List<Triangle>();
                for(int i = triangles.Count - 1; i >= 0; --i)
                {
                    Triangle triangle = triangles[i];
                    if(triangle.InsideCircumcircle(vertex))
                    {
                        badTriangles.Add(triangle);
                        triangles.RemoveAt(i);
                    }
                }

                // Bad Triangle 중 Bad Edge가 아닌 것을 솎아내어 현재 버텍스로 새로운 삼각형 생성
                foreach(Triangle triangle in badTriangles)
                {
                    foreach(Edge edge in triangle.Edges)
                    {
                        if(IsBadEdge(triangle, edge, badTriangles))
                            continue;

                        Triangle newTriangle = new Triangle(edge[0], edge[1], vertex);
                        triangles.Add(newTriangle);
                    }
                }
            }

            // Delaunay Triangulation 끝난 상태. superTriangle 제거해주자.
            triangles.RemoveAll(i => IsLinkedWith(i, superTriangle));
        }

        protected virtual Triangle CreateSuperTriangle(float coordinateLimit)
        {
            return new Triangle(
                new Vector2(-coordinateLimit, -coordinateLimit),
                new Vector2(0f, coordinateLimit),
                new Vector2(coordinateLimit, -coordinateLimit)
            );
        }

        protected virtual bool IsBadEdge(Triangle currentTriagle, Edge currentEdge, List<Triangle> allTriangles)
        {
            foreach (Triangle t in allTriangles)
            {
                if (t == currentTriagle)
                    continue;

                if (t.Edges.Any(e => e == currentEdge))
                {
                    return true;
                }
            }

            return false;
        }

        protected virtual bool IsLinkedWith(Triangle currentTriangle, Triangle compareTriangle)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (compareTriangle[i] == currentTriangle[j])
                        return true;
                }
            }

            return false;
        }
    }
}