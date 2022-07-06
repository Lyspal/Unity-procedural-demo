using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilGizmos
{
    public static void DrawWireCircle(Vector3 position, Quaternion rotation, float radius, int detail = 32)
    {
        Vector3[] points3D = new Vector3[detail];

        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angleRad = t * UtilMath.TAU;

            // Compute point in 2D
            Vector2 point2D = UtilMath.GetUnitVectorByAngle(angleRad);

            // Convert point in 3D
            points3D[i] = position + rotation * point2D;
        }
        
        // For debug. Draw the circle
        for (int i = 0; i < detail - 1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i + 1]);
        }
        Gizmos.DrawLine(points3D[detail - 1], points3D[0]);
    }
}
