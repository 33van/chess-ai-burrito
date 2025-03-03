using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pawnw;
    public GameObject rookw;
    public GameObject knightw;
    public GameObject bishopw;
    public GameObject queenw;
    public GameObject kingw;

    public GameObject pawnb;
    public GameObject rookb;
    public GameObject knightb;
    public GameObject bishopb;
    public GameObject queenb;
    public GameObject kingb;

    public GameObject block;
    public List<GameObject> dead_pieces = new List<GameObject>();

    public int turn;
    public GameObject[,] game = new GameObject[8, 8];
    public GameObject[,] blocks = new GameObject[8, 8];
    public bool end;
    public bool player1_turn;
    public bool player2_turn;
    public bool p_one_AI;
    public bool p_two_AI;
    public bool p1_check;
    public bool p2_check;
    public AIScript AI1;
    public AIScript AI2;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            for (int z = 0; z < 8; z++)
                for (int x = 0; x < 8; x++)
                    if (game[z, x] != null)
                        Debug.Log(z + ", " + x + ": " + game[z, x].name);
                    else
                        Debug.Log(z + ", " + x + ": null");
        }
    }

    void InitializeBoard() {
        for (int z = 0; z < 8; z++)
            for (int x = 0; x < 8; x++) {
                GameObject temp = Instantiate(block, new Vector3(x - 3.5f, -.5f, z - 3.5f), Quaternion.identity);
                if ((z+x) % 2 == 0)
                    temp.GetComponent<Renderer>().material.color = Color.white;
                else
                    temp.GetComponent<Renderer>().material.color = Color.black;
                blocks[z,x] = temp;
            }
        game[0, 0] = Instantiate(rookw, new Vector3(-3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 7] = Instantiate(rookw, new Vector3(3.5f, 0, -3.5f), Quaternion.identity);
        game[0, 1] = Instantiate(knightw, new Vector3(-2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 6] = Instantiate(knightw, new Vector3(2.5f, 0, -3.5f), Quaternion.identity);
        game[0, 2] = Instantiate(bishopw, new Vector3(-1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 5] = Instantiate(bishopw, new Vector3(1.5f, 0, -3.5f), Quaternion.identity);
        game[0, 3] = Instantiate(queenw, new Vector3(-.5f, 0, -3.5f), Quaternion.identity);
        game[0, 4] = Instantiate(kingw, new Vector3(.5f, 0, -3.5f), Quaternion.identity);
        for (int x = 0; x < 8; x++) {
            game[1, x] = Instantiate(pawnw, new Vector3(-3.5f + x, 0, -2.5f), Quaternion.identity);
        }

        game[7, 0] = Instantiate(rookb, new Vector3(-3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 7] = Instantiate(rookb, new Vector3(3.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 1] = Instantiate(knightb, new Vector3(-2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 6] = Instantiate(knightb, new Vector3(2.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 2] = Instantiate(bishopb, new Vector3(-1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 5] = Instantiate(bishopb, new Vector3(1.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 3] = Instantiate(queenb, new Vector3(-.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        game[7, 4] = Instantiate(kingb, new Vector3(.5f, 0, 3.5f), Quaternion.Euler(0, 180, 0));
        for (int x = 0; x < 8; x++) {
            game[6, x] = Instantiate(pawnb, new Vector3(-3.5f + x, 0, 2.5f), Quaternion.Euler(0, 180, 0));
        }
        end = false;
        turn = 0;
        p_one_AI = false;
        p_two_AI = true;
        if (p_two_AI)
            AI2 = new AIScript();
        p1_check = false;
        p2_check = false;
        player1_turn = false;
        player2_turn = false;
        P1Turn();
    }
    public void P1Turn() {
        if (end == false) {
            turn += 1;
            if (turn != 1) {
                GameObject king = GameObject.Find("KingLight(Clone)");
                if (king.GetComponent<King>().alive == false) {
                    end = true;
                    return;
                }
                if (king.GetComponent<King>().danger(true, game)) {
                    if (king.GetComponent<King>().checkmate(true)) {
                        end = true;
                        return;
                    }
                    else
                        p1_check = true;
                }
                if (king.GetComponent<King>().stalemate(true)) {
                    end = true;
                    return;
                }
            }
            if (p_one_AI == false) {
                player1_turn = true;
            }
        }
    }
    public void P2Turn() {
        if (end == false) {
            GameObject king = GameObject.Find("KingDark(Clone)");
            if (king.GetComponent<King>().alive == false) {
                end = true;
                return;
            }
            if (king.GetComponent<King>().danger(false, game)) {
                if (king.GetComponent<King>().checkmate(false)) {
                    end = true;
                    return;
                }
                else
                    p2_check = true;
            }
            if (king.GetComponent<King>().stalemate(false)) {
                end = true;
                return;
            }
            if (p_two_AI == false) {
                player2_turn = true;
                Debug.Log("ben");
            }
            else {
                Move best = AI2.BestMove(game, false);
                Debug.Log(best.startz + ", " + best.startx + ", " + best.endz + ", " + best.endx);
                GameObject temp = game[best.startz, best.startx];
                Debug.Log(temp.GetComponent<Pawn>() != null);
                game[best.startz, best.startx] = null;
                if (game[best.endz, best.endx] != null) {
                    GameObject temp1 = game[best.endz, best.endx];
                    temp1.GetComponent<Piece>().alive = false;
                    temp1.GetComponent<Piece>().targetPosition = new Vector3(-10, 0, transform.position.z);
                    dead_pieces.Add(temp1);
                }
                game[best.endz, best.endx] = temp;
                game[best.endz, best.endx].GetComponent<Piece>().hasMovedBefore = true;
                game[best.endz, best.endx].GetComponent<Piece>().MovePiece(new Vector3 (best.endx - 3.5f, 0, best.endz - 3.5f));
                Debug.Log(game[best.endz, best.endx].GetComponent<Piece>().hasMovedBefore);
                Debug.Log(game[best.endz, best.endx].transform.position + " " + game[best.endz, best.endx].GetComponent<Piece>().targetPosition);
                player1_turn = false;
                player2_turn = false;
                p1_check = false;
                p2_check = false;
                P1Turn();
            }
        }
    }
}
