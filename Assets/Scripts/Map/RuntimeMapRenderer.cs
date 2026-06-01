using System.Collections.Generic;
using UnityEngine;

public class RuntimeMapRenderer : MonoBehaviour
{
    [SerializeField] private RuntimeMapSystem runtimeMapSystem;
    [SerializeField] private MeshFilter meshFilter;
    private Mesh mesh ;
    private float timer = 0;

    void Start()
    {
        mesh = new Mesh();
        InitMesh();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= 0.1f)
        {
            UpdateMeshFromRuntimeMap();
            timer = 0;
        }
    }

    void InitMesh()
    {
        List<Vector3> verticesList = new List<Vector3>();
        List<int> triangles = new List<int>();

        Vector3[,] vertices = new Vector3[runtimeMapSystem.width, runtimeMapSystem.height];

        for (int i = 0; i < vertices.GetLength(0); i++)
        {
            for (int j = 0; j < vertices.GetLength(1); j++)
            {
                vertices[i, j] = new Vector3(i, 0, j);
                verticesList.Add(vertices[i, j]);
            }
        }

        for (int i = 0; i < vertices.GetLength(0); i++)
        {
            for (int j = 0; j < vertices.GetLength(1); j++)
            {
                if (i + 1 < vertices.GetLength(0) && j + 1 < vertices.GetLength(1))
                {
                    triangles.Add(i + j * vertices.GetLength(0));
                    triangles.Add(i + 1 + j * vertices.GetLength(0));
                    triangles.Add(i + (j + 1) * vertices.GetLength(0));
                    triangles.Add(i + 1 + (j + 1) * vertices.GetLength(0));
                    triangles.Add(i + (j + 1) * vertices.GetLength(0));
                    triangles.Add(i + 1 + j * vertices.GetLength(0));
                }
            }
        }

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

    }

    void UpdateMeshFromRuntimeMap()
    {
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[mesh.vertices.Length];
        
        
        for (int i = 0; i < runtimeMapSystem.RuntimeMap.GetLength(0); i++)
        {
            for (int j = 0; j < runtimeMapSystem.RuntimeMap.GetLength(1); j++)
            {
                int height = runtimeMapSystem.RuntimeMap.GetLength(1);
                int transposedIndex = j + i * height;

                if (!runtimeMapSystem.RuntimeMap[i, j].isExplored)
                {
                    colors[transposedIndex] = new Color(0,0,0,0);
                }
                else if( runtimeMapSystem.RuntimeMap[i, j].isExplored )
                {
                    float angle = Vector3.Angle(
                        runtimeMapSystem.RuntimeMap[i, j].normal,
                        Vector3.up);
                    float t = Mathf.InverseLerp(45f, 0f, angle);
                    Color color = Color.Lerp(Color.red, Color.green, t);
                    colors[transposedIndex] = color;
                }

                    
                vertices[transposedIndex] = new Vector3(vertices[transposedIndex].x,  runtimeMapSystem.RuntimeMap[i,j].worldPos.y, vertices[transposedIndex].z);
            }
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
}