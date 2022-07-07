using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(MeshFilter))]
public class AnnulusGenerator : MonoBehaviour
{
    [Range(0.01f, 2)]
    [SerializeField] private float innerRadius;
    [Range(0.01f, 2)]
    [SerializeField] private float thickness;
    [Range(3, 32)]
    [SerializeField] private int subdivisions = 3;

    private enum UVProjection
    {
        AngularRadial,
        ProjectZ
    }

    [SerializeField] private UVProjection uvProjection = UVProjection.AngularRadial;
    
    private Mesh mesh;
    
    private float outerRadius => innerRadius + thickness;
    private int vertexCount => subdivisions * 2;


    private void OnDrawGizmosSelected()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;

        UtilGizmos.DrawWireCircle(position, rotation, innerRadius, subdivisions);
        UtilGizmos.DrawWireCircle(position, rotation, outerRadius, subdivisions);
    }

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "Annulus";
        
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update() => GenerateMesh();

    void GenerateMesh()
    {
        // Ensure that the mesh has no data
        mesh.Clear();

        int vCount = vertexCount;

        // Defined in the mesh's local space
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        // Add 1 iteration for the UVs. Creates one more set of vertices at position 0 and 1
        for (int i = 0; i < subdivisions + 1; i++)
        {
            float t = i / (float)subdivisions;
            float angleRad = t * UtilMath.TAU;
            Vector2 direction = UtilMath.GetUnitVectorByAngle(angleRad);
            
            vertices.Add(direction * outerRadius);
            vertices.Add(direction * innerRadius);
            
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            switch (uvProjection)
            {
                case UVProjection.AngularRadial:
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
                case UVProjection.ProjectZ:
                    uvs.Add(direction * 0.5f + Vector2.one * 0.5f);
                    uvs.Add(direction * ((innerRadius / outerRadius) * 0.5f) + Vector2.one * 0.5f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        List<int> triangles = new List<int>();
        for (int i = 0; i < subdivisions; i++)
        {
            int indexRoot = i * 2;
            int indexInnerRoot = indexRoot + 1;
            int indexOuterNext = (indexRoot + 2);
            int indexInnerNext = (indexRoot + 3);
            
            // Add two triangles per segment
            
            triangles.Add(indexRoot);
            triangles.Add(indexOuterNext);
            triangles.Add(indexInnerNext);
            
            triangles.Add(indexRoot);
            triangles.Add(indexInnerNext);
            triangles.Add(indexInnerRoot);
        }
        
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
    }
}
