using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public Image resultScreen_IMG;
    public Image selectSignScreen_IMG;
    public BoardManager boardManager;
    public TextMeshProUGUI winner_TXT;
    public RectTransform settings_Menu;


    public void SetupGameResults()
    {
        resultScreen_IMG.rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f);
        resultScreen_IMG.DOColor(new Color32(160, 160, 255, 150), 1);
        winner_TXT.text = boardManager.currentShape.ToString() + "  is Winner";
    }

    public void SetupGameResultAsATie()
    {
        resultScreen_IMG.rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f);
        resultScreen_IMG.DOColor(new Color32(160, 160, 255, 150), 1);
        winner_TXT.text = "It's a Tie";
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


    //Setting Menu Section
    bool isSettingMenuShowing;
    public void ToggleSettings()
    {
        isSettingMenuShowing = !isSettingMenuShowing;

        if (isSettingMenuShowing)
            settings_Menu.DOAnchorPos(new Vector2(0, 0), 0.5f);
        else
            settings_Menu.DOAnchorPos(new Vector2(0, 1500), 0.5f);
    }
}