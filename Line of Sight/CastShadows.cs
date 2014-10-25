﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastShadows : MonoBehaviour
{
    Transform playerTrans; 
    
    Mesh mesh;

    Vector3[] verts;
    Vector3[] norms;
    int[] tris;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        Transform transform = gameObject.transform;

        GameObject shadow = new GameObject("Shadow", typeof(MeshFilter), typeof(MeshRenderer));
        shadow.transform.parent = transform;

        MeshFilter shadowMF = shadow.GetComponent<MeshFilter>();
        mesh = shadowMF.mesh;

        verts = new Vector3[8];
        norms = new Vector3[verts.Length];
        tris = new int[]
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

        float x = transform.position.x;
        float y = transform.position.y;
        Vector3 extents = renderer.bounds.extents;

        verts[0] = new Vector3(x - extents.x, y - extents.y);
        verts[1] = new Vector3(x + extents.x, y - extents.y);
        verts[2] = new Vector3(x + extents.x, y + extents.y);
        verts[3] = new Vector3(x - extents.x, y + extents.y);

        for (int i = 0; i < norms.Length; i++)
        {
            norms[i] = Vector3.back;
        }
    }

    void Update()
    {
        for (int i = 0; i < verts.Length / 2; i++)
        {
            verts[i + verts.Length / 2] = verts[i] + (verts[i] - playerTrans.position) * 100f;

        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = norms;
    }
}
