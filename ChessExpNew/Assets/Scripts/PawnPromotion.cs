using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnPromotion : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject promotionPanel; // The promotion UI panel
    public Button queenButton, rookButton, bishopButton, knightButton; // Buttons for promotion choices
    private GameObject promotedPawn;
    private int zPos, xPos;
    private bool isWhite;

    // Show the promotion panel and enable the buttons
    public void PromotePawn(GameObject pawn, int z, int x, bool white)
    {
        promotedPawn = pawn;
        zPos = z;
        xPos = x;
        isWhite = white;

        // Activate the promotion panel
        promotionPanel.SetActive(true);

        // Enable the buttons when the promotion panel is shown
        queenButton.interactable = true;
        rookButton.interactable = true;
        bishopButton.interactable = true;
        knightButton.interactable = true;
    }

    // Function to handle piece choice after promotion
    public void ChoosePiece(string pieceType)
    {
        GameObject newPiece = null;
        if (promotedPawn != null)
        {
            Destroy(promotedPawn); // Remove the promoted pawn
            switch (pieceType)
            {
                case "Q":
                    newPiece = Instantiate(isWhite ? gameManager.queenw : gameManager.queenb, new Vector3(xPos - 3.5f, 0, zPos - 3.5f), Quaternion.identity);
                    break;
                case "R":
                    newPiece = Instantiate(isWhite ? gameManager.rookw : gameManager.rookb, new Vector3(xPos - 3.5f, 0, zPos - 3.5f), Quaternion.identity);
                    break;
                case "B":
                    newPiece = Instantiate(isWhite ? gameManager.bishopw : gameManager.bishopb, new Vector3(xPos - 3.5f, 0, zPos - 3.5f), Quaternion.identity);
                    break;
                case "K":
                    newPiece = Instantiate(isWhite ? gameManager.knightw : gameManager.knightb, new Vector3(xPos - 3.5f, 0, zPos - 3.5f), Quaternion.identity);
                    break;
            }

            if (newPiece != null)
            {
                gameManager.game[zPos, xPos] = newPiece; // Place the new piece on the board
            }
        }

        // Hide the promotion panel and disable the buttons
        promotionPanel.SetActive(false);
        queenButton.interactable = false;
        rookButton.interactable = false;
        bishopButton.interactable = false;
        knightButton.interactable = false;

        // End the turn and move to the next player's turn
        gameManager.P2Turn();
    }
}
