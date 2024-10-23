using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;
    public GameObject pieceHighlight;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable to keep track of "black"or "white" player
    private string player;

    // References for all the sprites that the chesspiece can be
    public Sprite BlackKing, BlackQueen, BlackKnight, BlackBishop, BlackRook, BlackPawn;
    public Sprite WhiteKing, WhiteQueen, WhiteKnight, WhiteBishop, WhiteRook, WhitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //take the instantiate location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "BlackQueen": this.GetComponent<SpriteRenderer>().sprite = BlackQueen; player = "black"; break;
            case "BlackKnight": this.GetComponent<SpriteRenderer>().sprite = BlackKnight; player = "black"; break;
            case "BlackBishop": this.GetComponent<SpriteRenderer>().sprite = BlackBishop; player = "black"; break;
            case "BlackKing": this.GetComponent<SpriteRenderer>().sprite = BlackKing; player = "black"; break;
            case "BlackRook": this.GetComponent<SpriteRenderer>().sprite = BlackRook; player = "black"; break;
            case "BlackPawn": this.GetComponent<SpriteRenderer>().sprite = BlackPawn; player = "black"; break;

            case "WhiteQueen": this.GetComponent<SpriteRenderer>().sprite = WhiteQueen; player = "white"; break;
            case "WhiteKnight": this.GetComponent<SpriteRenderer>().sprite = WhiteKnight; player = "white"; break;
            case "WhiteBishop": this.GetComponent<SpriteRenderer>().sprite = WhiteBishop; player = "white"; break;
            case "WhiteKing": this.GetComponent<SpriteRenderer>().sprite = WhiteKing; player = "white"; break;
            case "WhiteRook": this.GetComponent<SpriteRenderer>().sprite = WhiteRook; player = "white"; break;
            case "WhitePawn": this.GetComponent<SpriteRenderer>().sprite = WhitePawn; player = "white"; break;

        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 1.00f;
        y *= 1.00f;

        //x += -3.48f;
        //y += -3.57f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }


    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {

        DestroyMovePlates();

        ShowPieceHighlight();

        InitiateMovePlates();
      
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }

    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "BlackQueen":
            case "WhiteQueen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "BlackKnight":
            case "WhiteKnight":
                LMovePlate();
                break;
            case "BlackBishop":
            case "WhiteBishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "BlackKing":
            case "WhiteKing":
                SurroundMovePlate();
                break;
            case "BlackRook":
            case "WhiteRook":
                LineMovePlate(1, 0);
                LineMovePlate(-1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(0, -1);
                break;
            case "BlackPawn":
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                }
                break;
            case "WhitePawn":
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                    PawnMovePlate(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            } else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x+1, y) != null &&
                sc.GetPosition(x+1,y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
    public void ShowPieceHighlight()
    {
        GameObject highlight = Instantiate(pieceHighlight, transform.position, Quaternion.identity);
    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.00f;
        y *= 1.00f;

        //x += -3.48f;
        //y += -3.57f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.00f;
        y *= 1.00f;

        //x += -3.48f;
        //y += -3.57f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    void Update()
    {
        switch (this.name)
        {
            case "BlackPawn":
                if (yBoard == 0)
                {

                    GameObject cp = controller.GetComponent<Game>().GetPosition(xBoard, yBoard);
                    cp.name = "BlackQueen";
                    this.GetComponent<SpriteRenderer>().sprite = BlackQueen; player = "black";

                }
                break;

            case "WhitePawn":
                if (yBoard == 7)
                {
                    GameObject cp = controller.GetComponent<Game>().GetPosition(xBoard, yBoard);
                    cp.name = "WhiteQueen";
                    this.GetComponent<SpriteRenderer>().sprite = WhiteQueen; player = "white";

                }
                break;
        }
    }


}
