using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertirRegularMesh : MonoBehaviour {

    [ContextMenu("Convertir a Mesh Regular")]
    void Convertir()
    {
        SkinnedMeshRenderer pielMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer renderizarMesh = gameObject.AddComponent<MeshRenderer>();
        MeshFilter filtroMesh = gameObject.AddComponent<MeshFilter>();

        filtroMesh.sharedMesh = pielMeshRenderer.sharedMesh;
        renderizarMesh.sharedMaterials = pielMeshRenderer.sharedMaterials;

        DestroyImmediate(pielMeshRenderer);
        DestroyImmediate(this);
    }
}
