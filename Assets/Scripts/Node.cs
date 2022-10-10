using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public ShapeType.Shape shape;
    public Sprite cat_IMG;
    public Sprite rabit_IMG;
    public BoardManager boardManager;
    public bool isPicked = false;
    public Image nodeImage;
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
        //lalala
    }

    void NodeTextChange()
    {
        if (shape == ShapeType.Shape.Cat)
            nodeImage.sprite = cat_IMG;
        //nodeText.text = "O";

        if (shape == ShapeType.Shape.Rabit)
            nodeImage.sprite = rabit_IMG;
            //nodeText.text = "X";
    }
}
