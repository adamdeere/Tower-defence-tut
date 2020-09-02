using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public Vector3[] verts;

    public int[] tris;

    private Mesh _mesh;
    // Start is called before the first frame update
    public void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        CreateShape();
    }

    private void CreateShape()
    {
        verts = new[]
        {
            new Vector3(0,0,0), 
            new Vector3(0,0,1), 
            new Vector3(1,0,0), 
        };
        tris = new[]
        {
            0,1,2
        };
        UpdateMeshes();
    }

    private void UpdateMeshes()
    {
        _mesh.Clear();
        _mesh.vertices = verts;
        _mesh.triangles = tris;
        _mesh.RecalculateNormals();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
