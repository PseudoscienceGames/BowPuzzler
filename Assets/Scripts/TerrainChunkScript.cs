using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainChunkScript : MonoBehaviour
{
    public float maxColor;
    public float minColor;
    public List<Color> colors = new List<Color>();
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        List<int> oldTris = new List<int>(mesh.triangles);
        List<Vector3> oldVerts = new List<Vector3>(mesh.vertices);
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int tri = 0;
        foreach(int vert in oldTris)
        {
            verts.Add(oldVerts[vert]);
            tris.Add(tri);
            tri++;
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        for (int i = 0; i < mesh.vertices.Length; i += 3)
        {
            float color = Random.Range(minColor, maxColor);
            Color newColor = new Color(color, color, color);
            colors.Add(newColor);
            colors.Add(newColor);
            colors.Add(newColor);
        }
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }
}
