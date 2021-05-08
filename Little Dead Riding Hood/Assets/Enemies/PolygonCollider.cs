using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCollider : MonoBehaviour
{
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    public PolygonCollider2D polygonCollider2D;
    public Sprite sprite;

    public void UpdatePolygonCollider2D(float tolerance = 0.05f)
    {
        polygonCollider2D.pathCount = sprite.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider2D.pathCount; i++)
        {
            sprite.GetPhysicsShape(i, points);
            LineUtility.Simplify(points, tolerance, simplifiedPoints);
            polygonCollider2D.SetPath(i, simplifiedPoints);
        }
    }
}
