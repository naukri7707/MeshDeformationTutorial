using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class CubeBuilder : MeshBuilder
{
    private BoxCollider boxCollider;

    public BoxCollider BoxCollider
    {
        get
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }
            return boxCollider;
        }
    }

    [SerializeField]
    private Vector3Int vertexOfFace = new(3, 3, 3);
    
    protected virtual void Reset()
    {
        meshName = "Cube";
    }
    
    protected override void OnMeshBuilding(Mesh mesh)
    {
        SetVertices(mesh);
        SetFaces(mesh);
    }

    protected void SetVertices(Mesh mesh)
    {
        var (xFace, yFace, zFace) = GetQuadOfFaceCount();
        var vertexCount = GetObjectVertexCount();
        var vertices = new Vector3[vertexCount];
        var normals = new Vector3[vertexCount];
        var uvs = new Vector2[vertexCount];
        var tangents = new Vector4[vertexCount];
        var size = BoxCollider.size;
        var offset = BoxCollider.bounds.center - size / 2F;
        var v = 0;

        // set body
        for (var y = 0; y <= yFace; y++)
        {
            var yRatio = (float)y / yFace;
            var yPos = size.y * y / yFace;
            // forward
            for (var x = 0; x <= xFace; x++)
            {
                var xRatio = (float)x / xFace;
                var xPos = size.x * xRatio;
                vertices[v] = new Vector3(xPos, yPos, 0) + offset;
                normals[v] = Vector3.forward;
                uvs[v] = new Vector2(xRatio, yRatio);
                tangents[v] = new Vector4(1, 0, 0, -1);
                v++;
            }
            // right
            for (var z = 1; z <= zFace; z++)
            {
                var zRatio = (float)z / zFace;
                var zPos = size.z * zRatio;
                vertices[v] = new Vector3(size.x, yPos, zPos) + offset;
                normals[v] = Vector3.right;
                uvs[v] = new Vector2(zRatio, yRatio);
                tangents[v] = new Vector4(0, 0, 1, -1);
                v++;
            }
            // back
            for (var x = xFace - 1; x >= 0; x--)
            {
                var xRatio = (float)x / xFace;
                var xPos = size.x * xRatio;
                vertices[v] = new Vector3(xPos, yPos, size.z) + offset;
                normals[v] = Vector3.back;
                uvs[v] = new Vector2(xRatio, yRatio);
                tangents[v] = new Vector4(-1, 0, 0, -1);
                v++;
            }
            // left
            for (var z = zFace - 1; z > 0; z--)
            {
                var zRatio = (float)z / zFace;
                var zPos = size.z * zRatio;
                vertices[v] = new Vector3(0, yPos, zPos) + offset;
                normals[v] = Vector3.left;
                uvs[v] = new Vector2(zRatio, yRatio);
                tangents[v] = new Vector4(0, 0, -1, -1);
                v++;
            }
        }
        // set top cap
        for (var z = 1; z < zFace; z++)
        {
            var zRatio = (float)z / zFace;
            var zPos = size.z * zRatio;
            for (var x = 1; x < xFace; x++)
            {
                var xRatio = (float)x / xFace;
                var xPos = size.x * xRatio;
                vertices[v] = new Vector3(xPos, size.y, zPos) + offset;
                normals[v] = Vector3.up;
                uvs[v] = new Vector2(xRatio, zRatio);
                tangents[v] = new Vector4(0, 1, 0, -1);
                v++;
            }
        }
        // set bottom cap
        for (var z = 1; z < zFace; z++)
        {
            var zRatio = (float)z / zFace;
            var zPos = size.z * z / zFace;
            for (var x = 1; x < xFace; x++)
            {
                var xRatio = (float)x / xFace;
                var xPos = size.x * x / xFace;
                vertices[v] = new Vector3(xPos, 0, zPos) + offset;
                normals[v] = Vector3.down;
                uvs[v] = new Vector2(xRatio, zRatio);
                tangents[v] = new Vector4(0, -1, 0, -1);
                v++;
            }
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.tangents = tangents;
    }

    protected void SetFaces(Mesh mesh)
    {
        var (xFace, yFace, zFace) = GetQuadOfFaceCount();
        var quads = (xFace * yFace + xFace * zFace + yFace * zFace) * 2;
        var triangles = new int[quads * 6];
        var ring = (xFace + zFace) * 2;
        var t = 0;
        var v = 0;

        for (var y = 0; y < yFace; y++, v++)
        {
            for (var q = 0; q < ring - 1; q++, v++)
            {
                t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
            }
            t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
        }

        t = SetTopFace(triangles, t, ring);
        t = SetBottomFace(triangles, t, ring);
        mesh.subMeshCount = MeshRenderer.sharedMaterials.Length;
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
    }

    private int SetTopFace(int[] triangles, int t, int ring)
    {
        var (xFace, yFace, zFace) = GetQuadOfFaceCount();
        var v = ring * yFace;
        for (var x = 0; x < xFace - 1; x++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
        }
        t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

        var vMin = ring * (yFace + 1) - 1;
        var vMid = vMin + 1;
        var vMax = v + 2;

        for (var z = 1; z < zFace - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + xFace - 1);
            for (var x = 1; x < xFace - 1; x++, vMid++)
            {
                t = SetQuad(triangles, t, vMid, vMid + 1, vMid + xFace - 1, vMid + xFace);
            }
            t = SetQuad(triangles, t, vMid, vMax, vMid + xFace - 1, vMax + 1);
        }

        var vTop = vMin - 2;
        t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
        for (var x = 1; x < xFace - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
        }
        t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

        return t;
    }

    private int SetBottomFace(int[] triangles, int t, int ring)
    {
        var (xFace, yFace, zFace) = GetQuadOfFaceCount();
        var v = 1;
        var vertexCount = GetObjectVertexCount();
        var vMid = vertexCount - (xFace - 1) * (zFace - 1);
        t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
        for (var x = 1; x < xFace - 1; x++, v++, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
        }
        t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

        var vMin = ring - 2;
        vMid -= xFace - 2;
        var vMax = v + 2;

        for (var z = 1; z < zFace - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid + xFace - 1, vMin + 1, vMid);
            for (var x = 1; x < xFace - 1; x++, vMid++)
            {
                t = SetQuad(
                    triangles, t,
                    vMid + xFace - 1, vMid + xFace, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vMid + xFace - 1, vMax + 1, vMid, vMax);
        }

        var vTop = vMin - 1;
        t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
        for (var x = 1; x < xFace - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
        }
        t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

        return t;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

    private int GetObjectVertexCount()
    {
        var (xFace, yFace, zFace) = GetQuadOfFaceCount();
        var cornerVertices = 8;
        var edgeVertices = (xFace + yFace + zFace - 3) * 4;
        var faceVertices = (
            (xFace - 1) * (yFace - 1) +
            (xFace - 1) * (zFace - 1) +
            (yFace - 1) * (zFace - 1)) * 2;
        return cornerVertices + edgeVertices + faceVertices;
    }

    private (int xFace, int yFace, int zFace) GetQuadOfFaceCount()
    {
        var face = vertexOfFace - Vector3Int.one;
        return (face.x, face.y, face.z);
    }
}
