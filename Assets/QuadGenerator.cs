using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadGenerator : MonoBehaviour
{
    void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Quad";

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1, 1),
            new Vector3(1, 1),
            new Vector3(-1, -1),
            new Vector3(1, -1)
        };

        int[] triangles = new int[]
        {
            1, 0, 2,
            3, 1, 2
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        List<Vector3> uvs = new List<Vector3>()
        {
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };

        mesh.SetVertices(points);
        mesh.triangles = triangles;
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
