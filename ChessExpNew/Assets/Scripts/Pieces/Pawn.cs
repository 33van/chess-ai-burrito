using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    // Add a flag to keep track of whether en passant is available
    private bool enPassantAvailable = false;

    public override List<Move> getAllMoves(GameObject[,] arr) {
        List<Move> moves = new List<Move>();
        if (isWhite) {
            // Normal movement
            if (transform.position.z + 3.5f < 7)
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5),
                    200,
                    0));
            if (hasMovedBefore == false && arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 3.5)] == null
            && arr[(int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 5.5f), (int) (transform.position.x + 3.5),
                200,
                0));
            
            // Regular diagonal captures
            if (transform.position.x + 3.5f < 7 && transform.position.z + 3.5f < 7) {
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)] != null)
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == false)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)])));
            }
            if (transform.position.x + 3.5f > 0 && transform.position.z + 3.5f < 7) 
                if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)] != null)
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == false)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)])));
            
            // En Passant Check
            if (enPassantAvailable) {
                if (transform.position.z + 3.5f < 7) {
                    // Check for the opponent's pawn next to the current pawn's position for en passant capture
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)] != null &&
                        arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == false) {
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        0)); // En passant capture
                    }
                    if (arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)] != null &&
                        arr[(int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == false) {
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 4.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        0)); // En passant capture
                    }
                }
            }
        }
        else {
            // Similar logic for black pawns, adjusted for downward movement
            if (transform.position.z + 3.5f > 0) {
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null)
                    moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5),
                    200,
                    0));
            }
            if (hasMovedBefore == false && arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 3.5)] == null
            && arr[(int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5)] == null)
                moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 1.5f), (int) (transform.position.x + 3.5),
                200,
                0));

            // Regular diagonal captures
            if (transform.position.x + 3.5f < 7 && transform.position.z + 3.5f > 0)
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)] != null)
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == true)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)])));
            if (transform.position.x + 3.5f > 0 && transform.position.z + 3.5f > 0)    
                if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)] != null)
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == true)
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        PieceValues(arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)])));

            // En Passant logic for black pawns
            if (enPassantAvailable) {
                if (transform.position.z + 3.5f > 0) {
                    // Check for opponent's white pawn next to the current pawn's position for en passant capture
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)] != null &&
                        arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5f)].GetComponent<Piece>().isWhite == true) {
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 4.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        0)); // En passant capture
                    }
                    if (arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)] != null &&
                        arr[(int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5f)].GetComponent<Piece>().isWhite == true) {
                        moves.Add(new Move((int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f), (int) (transform.position.z + 2.5f), (int) (transform.position.x + 2.5),
                        PieceValues(arr[(int) (transform.position.z + 3.5f), (int) (transform.position.x + 3.5f)]),
                        0)); // En passant capture
                    }
                }
            }
        }
        return moves;
    }
}
