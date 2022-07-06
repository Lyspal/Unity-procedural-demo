using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnulusGenerator : MonoBehaviour
{
    [Range(0.01f, 2)]
    [SerializeField] private float innerRadius;
    
    [Range(0.01f, 2)]
    [SerializeField] private float thickness;
    
    [Range(3, 32)]
    [SerializeField] private int subdivisions = 3;

    private float outerRadius => innerRadius + thickness;
    private float vertexCount => subdivisions * 2;


    private void OnDrawGizmosSelected()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;

        UtilGizmos.DrawWireCircle(position, rotation, innerRadius, subdivisions);
        UtilGizmos.DrawWireCircle(position, rotation, outerRadius, subdivisions);
    }
}
