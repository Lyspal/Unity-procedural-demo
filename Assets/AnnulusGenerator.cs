using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnulusGenerator : MonoBehaviour
{
    private const float TAU = 6.28318530718f;
    
    [Range(0.01f, 2)]
    [SerializeField] private float innerRadius;
    [Range(0.01f, 2)]
    [SerializeField] private float thickness;

    private float outerRadius => innerRadius + thickness;
    
    [Range(3, 32)]
    [SerializeField] private int subdivisions = 3;


    private void OnDrawGizmosSelected()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;

        DrawWireCircle(position, rotation, innerRadius, subdivisions);
        DrawWireCircle(position, rotation, outerRadius, subdivisions);
    }
    
    private static void DrawWireCircle(Vector3 position, Quaternion rotation, float radius, int detail = 32)
    {
        Vector3[] points3D = new Vector3[detail];

        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angleRad = t * TAU;

            // Compute point in 2D
            Vector2 point2D = new Vector2(
                Mathf.Cos(angleRad) * radius,
                Mathf.Sin(angleRad) * radius
            );

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
