using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public ShapeType.Shape currentShape;
    public int boardSize;
    public Node nodePrefab;
    public List<List<Node>> rows = new List<List<Node>>();
    public List<List<Node>> columns = new List<List<Node>>();
    public List<Node> nodes = new List<Node>();
    public List<Node> rowsTemp = new List<Node>();
    public List<Node> columnTemp = new List<Node>();
    public MenuManager menuManager;

    public bool gameOver;

    private void Start()
    {
        GetComponent<GridLayoutGroup>().constraintCount = boardSize;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            Node node = Instantiate(nodePrefab, gameObject.transform);
            nodes.Add(node);
        }
        CreateRows();
        CreateColumns();
    }
    void CreateRows()
    {
        int rowCounter = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                rowsTemp.Add(nodes[j + rowCounter]);
            }
            rows.Add(rowsTemp);
            rowCounter += boardSize;
            rowsTemp = new List<Node>();
        }
    }
    void CreateColumns()
    {
        for (int x = 0; x < boardSize; x++)
        {
            int columnCounter = 0;
            for (int y = 0; y < boardSize; y++)
            {
                columnTemp.Add(nodes[x + columnCounter]);
                columnCounter += boardSize;
            }
            columns.Add(columnTemp);
            columnTemp = new List<Node>();
        }
    }
    public void Calculate()
    {
        if (gameOver)
        {
            Debug.Log(currentShape.ToString() + " Wins");
            menuManager.SetupGameResults();
            return;
        }
        CheckRows();
        CheckColumns();
        ChangeShape();
    }

    public void ChangeShape()
    {
        if (currentShape == ShapeType.Shape.O)
            currentShape = ShapeType.Shape.X;
        else
        if (currentShape == ShapeType.Shape.X)
            currentShape = ShapeType.Shape.O;
    }

    public void CheckRows()
    {
        int counter = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (rows[i][j].shape == currentShape)
                {
                    counter++;
                    if (counter == boardSize)
                    {
                        for (int k = 0; k < boardSize; k++)
                        {
                            rows[i][k].GetComponent<Image>().color = new Color32(0, 255, 255, 255);
                        }
                        gameOver = true;
                        Calculate();
                        return;
                    }
                }
            }
            counter = 0;
        }
    }

    public void CheckColumns()
    {
        int counter = 0;
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (columns[x][y].shape == currentShape)
                {
                    counter++;
                    if (counter == boardSize)
                    {
                        for (int k = 0; k < boardSize; k++)
                        {
                            columns[x][k].GetComponent<Image>().color = new Color32(0, 255, 255, 255);
                        }
                        gameOver = true;
                        Calculate();
                        return;
                    }
                }
            }
            counter = 0;
        }
    }
}