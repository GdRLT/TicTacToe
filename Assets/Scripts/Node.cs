using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public ShapeType.Shape shape;
    public BoardManager boardManager;
    public TextMeshProUGUI nodeText;
    public bool isPicked = false;
    public int nodeX;
    public int nodeY;

    private void Start()
    {
        shape = ShapeType.Shape.None;
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void NodeSelected()
    {
        if (isPicked)
            return;

        isPicked = true;
        shape = boardManager.currentShape;
        NodeTextChange();
        boardManager.Calculate();
    }

    void NodeTextChange()
    {
        if (shape == ShapeType.Shape.O)
            nodeText.text = "O";

        if (shape == ShapeType.Shape.X)
            nodeText.text = "X";
    }
}
