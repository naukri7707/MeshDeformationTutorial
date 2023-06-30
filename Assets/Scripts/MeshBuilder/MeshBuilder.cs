using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public abstract class MeshBuilder : MonoBehaviour
{
    protected MeshFilter MeshFilter { get; private set; }

    protected MeshRenderer MeshRender { get; private set; }

    [SerializeField]
    private bool createOnStart = true;

    protected abstract string MeshName { get; }

    protected virtual void Awake()
    {
        MeshFilter = GetComponent<MeshFilter>();
        MeshRender = GetComponent<MeshRenderer>();
    }

    protected virtual void Start()
    {
        if (createOnStart)
        {
            BuildMesh();
        }
    }

    public void BuildMesh()
    {
        var mesh = new Mesh()
        {
            name = MeshName
        };

        OnMeshBuilding(mesh);
        MeshFilter.mesh = mesh;
    }

    public void DestoryMesh()
    {
        MeshFilter.sharedMesh = null;
    }

    public void RebuildMesh()
    {
        DestoryMesh();
        BuildMesh();
    }

    protected abstract void OnMeshBuilding(Mesh mesh);
}
