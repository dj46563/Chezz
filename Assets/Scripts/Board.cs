using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPiece { Pawn, Knight, Bishop, Rook, Queen, King };

public class Board : MonoBehaviour
{
    Piece[] board = new Piece[64];

    bool isPieceClicked = false;
    Piece pieceClicked;
    public GameObject whitePawnPrefab;
    public event Action<Coordinate, Coordinate> OnPieceMove;

    // Start is called before the first frame update
    void Start()
    {
        Piece pawnPiece1 = Instantiate(whitePawnPrefab, new Vector3((float)-3.5, (float)-2.5, 0), Quaternion.identity).GetComponent<Piece>();
        pawnPiece1.Initialize(ChessPiece.Pawn, true, 1, new Coordinate('a', 2));
        board[CoordinateHelper.CoordinateToArrayIndex(pawnPiece1.Position)] = pawnPiece1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Coordinate square = CoordinateHelper.Vector3ToCoordinate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (isPieceClicked)
            {
                // Update when clicking new tile on board
                if (square != pieceClicked.Position)
                {
                    OnPieceMove?.Invoke(pieceClicked.Position, square); 
                    board[CoordinateHelper.CoordinateToArrayIndex(pieceClicked.Position)] = null;
                    pieceClicked.MoveTo(square);
                    board[CoordinateHelper.CoordinateToArrayIndex(square)] = pieceClicked;
                }
                isPieceClicked = false;
            }
            else if (board[CoordinateHelper.CoordinateToArrayIndex(square)] != null)
            { 
                isPieceClicked = true;
                pieceClicked = board[CoordinateHelper.CoordinateToArrayIndex(square)];
            }
        }
    }

    public void MoveTo(Coordinate src, Coordinate dest)
    {
        Piece piece = board[CoordinateHelper.CoordinateToArrayIndex(src)];
        if (piece != null)
        {
            board[CoordinateHelper.CoordinateToArrayIndex(src)] = null;
            piece.MoveTo(dest);
            board[CoordinateHelper.CoordinateToArrayIndex(dest)] = piece;
        }
    }
}
