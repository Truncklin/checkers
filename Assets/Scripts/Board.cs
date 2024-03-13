using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Material selectedMaterial;
    private Material defaultMaterial;
    
    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint), piece.transform.rotation, gameObject.transform);
        return newPiece;
    }
    
    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void SelectPiece(GameObject piece)
    {
        piece.transform.position = new Vector3(piece.transform.position.x, 15, piece.transform.position.z);
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        defaultMaterial = renderers.material;
        renderers.material = selectedMaterial;
    }
    
    public void DeselectPiece(GameObject piece)
    {
        piece.transform.position = new Vector3(piece.transform.position.x, 14.25f, piece.transform.position.z);
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = defaultMaterial;
    }
}
