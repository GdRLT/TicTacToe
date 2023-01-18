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
    public GameObject boardPrefab;
    [HideInInspector]public BoardManager boardManager;
    public TextMeshProUGUI winner_TXT;
    public RectTransform settings_Menu;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }
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
        resultScreen_IMG.rectTransform.DOAnchorPos(new Vector2(-1500, 0), 0.5f);
        selectSignScreen_IMG.rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f);

        // Clear Old Board if Exist
        DestroyBoard();
    }

    void DestroyBoard()
    {
        Destroy(boardManager.gameObject);
    }

    [SerializeField]
    public void SelectSign(string shapeType)
    {
        // Install New Board
        InstallNewBoard();

        if (shapeType == "Cat")
        {
            boardManager.currentShape = ShapeType.Shape.Cat;
        }
        if (shapeType == "Rabit")
        {
            boardManager.currentShape = ShapeType.Shape.Rabit;
        }

        // Tween Select Sign Screen
        selectSignScreen_IMG.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0.5f);
    }


    void InstallNewBoard()
    {
        var board = Instantiate(boardPrefab);
        boardManager = board.GetComponent<BoardManager>();
        boardManager.InstallBoard();
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