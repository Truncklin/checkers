using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CageSelector : MonoBehaviour
{
    public GameObject tileHighlightPrefab;

    private GameObject tileHighlight;

    void Start()
    {
        Vector2Int gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Первое прикосновение

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 point = hit.point;
                    Vector2Int gridPoint = Geometry.GridFromPoint(point);

                    GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);
                    if (selectedPiece != null)
                    {
                        GameManager.instance.SelectPiece(selectedPiece);
                        ExitState(selectedPiece);
                    }
                }
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    public void EnterState()
    {
        enabled = true;
    }

    private void ExitState(GameObject movingPiece)
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        Move move = GetComponent<Move>();
        move.EnterState(movingPiece);
        Debug.Log("MoveEnterState");
    }
}
