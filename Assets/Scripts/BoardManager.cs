using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public ShapeType.Shape currentShape;
    public int boardSize;
    public Node nodePrefab;
    public List<List<Node>> rows = new List<List<Node>>();
    public List<List<Node>> columns = new List<List<Node>>();
    public List<List<Node>> diognals = new List<List<Node>>();
    public List<Node> nodes = new List<Node>();
    [HideInInspector] public List<Node> rowsTemp = new List<Node>();
    [HideInInspector] public List<Node> columnTemp = new List<Node>();
    [HideInInspector] public List<Node> diognalTemp = new List<Node>();
    [HideInInspector]public MenuManager menuManager;

    public bool gameOver;

    private void OnEnable()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void InstallBoard()
    {
        CreateNodes();
        StartCoroutine(NodesBirthAnimation());
        CreateRows();
        CreateColumns();
        CreateDiognals();
    }

    void CreateNodes()
    {
        GetComponent<GridLayoutGroup>().constraintCount = boardSize;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            Node node = Instantiate(nodePrefab, gameObject.transform);
            nodes.Add(node);
        }
    }
    IEnumerator NodesBirthAnimation()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            nodes[i].BirthTween();
        }
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
    void CreateDiognals()
    {
        //Left to Right
        int indicatorL2R = 0;
        for (int y = 0; y < boardSize; y++)
        {
            diognalTemp.Add(nodes[indicatorL2R]);
            indicatorL2R += (boardSize + 1);
        }
        diognals.Add(diognalTemp);
        diognalTemp = new List<Node>();

        //Right to Left
        int indicatorR2L = boardSize - 1;
        for (int i = 0; i < boardSize; i++)
        {
            diognalTemp.Add(nodes[indicatorR2L]);
            indicatorR2L += (boardSize - 1);
        }
        diognals.Add(diognalTemp);
        diognalTemp = new List<Node>();
    }

    public void Calculate()
    {
        if (gameOver)
        {

            Debug.Log(currentShape.ToString() + " Wins");
            menuManager.SetupGameResults();
            return;
        }
        if (AllSlotsOccupated())
        {
            if (gameOver == false)
            {
                Debug.Log("its a Tie");
                menuManager.SetupGameResultAsATie();
                return;
            }
        }
        CheckDiognal();
        CheckRows();
        CheckColumns();
        ChangeShape();
    }

    private bool AllSlotsOccupated()
    {
        int counter = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].shape == ShapeType.Shape.None)
            {
                counter++;
            }
        }
        if (counter == 0)
        {
            return true;
        }
        else
            return false;
    }

    public void ChangeShape()
    {
        if (currentShape == ShapeType.Shape.Cat)
            currentShape = ShapeType.Shape.Rabit;
        else
        if (currentShape == ShapeType.Shape.Rabit)
            currentShape = ShapeType.Shape.Cat;
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

    public void CheckDiognal()
    {
        int counter = 0;
        for (int i = 0; i < diognals.Count; i++)
        {
            for (int r = 0; r < boardSize; r++)
            {
                if (diognals[i][r].shape == currentShape)
                {
                    counter++;
                    Debug.Log(counter);
                    if (counter == boardSize)
                    {
                        for (int k = 0; k < boardSize; k++)
                        {
                            diognals[i][k].GetComponent<Image>().color = new Color32(0, 255, 255, 255);
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