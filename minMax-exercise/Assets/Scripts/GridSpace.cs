using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{

    public Button button;
    public Text buttonText;
    public int index;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        if (gameController.GetPlayerSide() == "X") {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.MakeMove("X", index);
            gameController.EndTurn();
        }  
    }

    public void SetSpace(string player)
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.MakeMove("O", index);
        gameController.EndTurn();
    }
}