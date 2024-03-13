using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    protected Vector2Int[] CheckersDirections = {new Vector2Int(2,2), new Vector2Int(2, -2),
        new Vector2Int(-2, -2), new Vector2Int(-2, 2)};
    public GameObject checkerB;
    public GameObject checkerW;

    [SerializeField] private Board board;

    private GameObject[,] pieces;
    private List<GameObject> movedPawns;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pieces = new GameObject[8, 8];
        movedPawns = new List<GameObject>();


        InitialSetup();
    }
        

    private void InitialSetup()
    {
        AddPiece(checkerW,  0,0);
        AddPiece(checkerW,  2,0);
        AddPiece(checkerW,  1,1);
        AddPiece(checkerW,  0,2);
        AddPiece(checkerW,  2,2);
        AddPiece(checkerW,  1,3);
        AddPiece(checkerW,  0,4);
        AddPiece(checkerW,  2,4);
        AddPiece(checkerW,  1,5);
        AddPiece(checkerW,  0,6);
        AddPiece(checkerW,  2,6);
        AddPiece(checkerW,  1,7);

        AddPiece(checkerB, 6, 0);
        AddPiece(checkerB, 7, 1);
        AddPiece(checkerB, 5, 1);
        AddPiece(checkerB, 6, 2);
        AddPiece(checkerB, 7, 3);
        AddPiece(checkerB, 5, 3);
        AddPiece(checkerB, 6, 4);
        AddPiece(checkerB, 7, 5);
        AddPiece(checkerB, 5, 5);
        AddPiece(checkerB, 6, 6);
        AddPiece(checkerB, 7, 7);
        AddPiece(checkerB, 5, 7);


    }

    private void AddPiece(GameObject prefab, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        pieces[col, row] = pieceObject;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {

        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, gridPoint);
    }

    private List<Vector2Int>MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        Vector2Int nextGridPoint;
        foreach (var direction in CheckersDirections)
        {
            nextGridPoint = gridPoint + direction ;
            if (GameManager.instance.PieceAtGrid(nextGridPoint)){}
            else locations.Add(nextGridPoint);
        }

        
        return locations;
    }
    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = MoveLocations(gridPoint);
        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);
        return locations;
    }
    
    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }
    
    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }
    
    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }
    
    private Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }
    
    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        pieces[gridPoint.x, gridPoint.y] = null;
        Destroy(pieceToCapture);
    }
}