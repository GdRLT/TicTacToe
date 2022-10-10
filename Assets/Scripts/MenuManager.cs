using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Image resultScreen_IMG;
    public Image selectSignScreen_IMG;
    public BoardManager boardManager;
    public TextMeshProUGUI winner_TXT;

    public void SetupGameResults()
    {
        resultScreen_IMG.gameObject.SetActive(true);
        winner_TXT.text = boardManager.currentShape.ToString() + "  is Winner";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    [SerializeField]
    public void SelectSign(string shapeType)
    {
        boardManager.gameObject.SetActive(true);

        if (shapeType == "Cat")
        {
            boardManager.currentShape = ShapeType.Shape.Cat;
        }
        if (shapeType == "Rabit")
        {
            boardManager.currentShape = ShapeType.Shape.Rabit;
        }

        selectSignScreen_IMG.gameObject.SetActive(false);
    }
}