using UnityEngine;
using System.Collections;

public class CastShadows : MonoBehaviour
{
    public GameObject w;
    public GameObject shadow;

    new Transform transform;

    void Start()
    {
        transform = gameObject.transform;
    }

    void Update()
    {
        Vector3[] points = new Vector3[8];
        Vector3 pos = w.transform.position;
        pos.z = 0;
        Vector3 extents = w.renderer.bounds.extents;

        points[0] = new Vector3(-extents.x, -extents.y);
        points[1] = new Vector3(extents.x, -extents.y);
        points[2] = new Vector3(extents.x, extents.y);
        points[3] = new Vector3(-extents.x, extents.y);

        for (int i = 0; i < points.Length / 2; i++)
        {
            points[i + points.Length / 2] = points[i] + ((points[i] + pos) - transform.position) * 100f;

        }

        Vector3[] norms = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            norms[i] = Vector3.back;
        }

        int[] tris =
        {
            0,7,4,
            0,3,7,
            3,2,7,
            2,6,7,
            2,1,6,
            1,5,6,
            4,5,0,
            5,1,0
        };

        MeshFilter mf = shadow.GetComponent<MeshFilter>();
        if (mf == null)
        {
            mf = shadow.AddComponent<MeshFilter>();
        }
        Mesh m = mf.mesh;
        m.vertices = points;
        m.triangles = tris;
        m.normals = norms;

        mf.mesh = m;
    }

    void OnDrawGizmos()
    {
        Vector3[] points = new Vector3[8];
        Vector3 pos = w.transform.position;
        Vector3 extents = w.renderer.bounds.extents;

        points[0] = new Vector3(pos.x - extents.x, pos.y - extents.y);
        points[1] = new Vector3(pos.x + extents.x, pos.y - extents.y);
        points[2] = new Vector3(pos.x + extents.x, pos.y + extents.y);
        points[3] = new Vector3(pos.x - extents.x, pos.y + extents.y);

        for (int i = 0; i < points.Length / 2; i++)
        {
            points[i + points.Length / 2] = points[i] + (points[i] - transform.position) * 100f;
            Gizmos.DrawLine(points[i], points[i + points.Length / 2]);
        }
    }
}
