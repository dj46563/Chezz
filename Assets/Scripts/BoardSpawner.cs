using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    public GameObject whitePawnPrefab;
    public GameObject whiteKnightPrefab;
    public GameObject whiteBishopPrefab;
    public GameObject whiteRookPrefab;
    public GameObject whiteQueenPrefab;
    public GameObject whiteKingPrefab;
    public GameObject blackPawnPrefab;
    public GameObject blackKnightPrefab;
    public GameObject blackBishopPrefab;
    public GameObject blackRookPrefab;
    public GameObject blackQueenPrefab;
    public GameObject blackKingPrefab;

    public Piece[] SpawnBoard()
    {
        Piece[] board = new Piece[64];
        Piece piece;

        // Create pawns
        for (int i = 0; i < 8; i++)
        {
            piece = CreatePawn(true, new Coordinate((char)('a' + i), 2));
            board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

            piece = CreatePawn(false, new Coordinate((char)('a' + i), 7));
            board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        }

        // Create knights
        piece = CreateKnight(true, new Coordinate('b', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateKnight(true, new Coordinate('g', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        piece = CreateKnight(false, new Coordinate('b', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateKnight(false, new Coordinate('g', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        // Create bishops
        piece = CreateBishop(true, new Coordinate('c', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateBishop(true, new Coordinate('f', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        piece = CreateBishop(false, new Coordinate('c', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateBishop(false, new Coordinate('f', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        // Create rooks
        piece = CreateRook(true, new Coordinate('a', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateRook(true, new Coordinate('h', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        piece = CreateRook(false, new Coordinate('a', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;
        piece = CreateRook(false, new Coordinate('h', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        // Create queens
        piece = CreateQueen(true, new Coordinate('d', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        piece = CreateQueen(false, new Coordinate('d', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        // Create kings
        piece = CreateKing(true, new Coordinate('e', 1));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        piece = CreateKing(false, new Coordinate('e', 8));
        board[CoordinateHelper.CoordinateToArrayIndex(piece.Position)] = piece;

        return board;
    }

    private Piece CreatePawn(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite? whitePawnPrefab : blackPawnPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.Pawn, isWhite, 1, coordinate);
        return piece;
    }

    private Piece CreateKnight(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite ? whiteKnightPrefab : blackKnightPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.Knight, isWhite, 3, coordinate);
        return piece;
    }

    private Piece CreateBishop(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite ? whiteBishopPrefab : blackBishopPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.Bishop, isWhite, 3, coordinate);
        return piece;
    }

    private Piece CreateRook(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite ? whiteRookPrefab : blackRookPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.Rook, isWhite, 5, coordinate);
        return piece;
    }

    private Piece CreateQueen(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite ? whiteQueenPrefab : blackQueenPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.Queen, isWhite, 9, coordinate);
        return piece;
    }

    private Piece CreateKing(bool isWhite, Coordinate coordinate)
    {
        Piece piece = Instantiate(isWhite ? whiteKingPrefab : blackKingPrefab, CoordinateHelper.CoordinateToVector3(coordinate), Quaternion.identity).GetComponent<Piece>();
        piece.Initialize(ChessPiece.King, isWhite, 0, coordinate);
        return piece;
    }
}
