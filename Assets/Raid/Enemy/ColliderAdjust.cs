using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAdjust : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshCollider meshCollider;

    private void Awake() 
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Update() 
    {
        Mesh colliderMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(colliderMesh, true);
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = colliderMesh;
    }
}
