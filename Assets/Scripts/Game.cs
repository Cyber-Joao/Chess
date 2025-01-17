using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    // Positions and team for each chesspiece
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    public List<GameObject> pieces = new List<GameObject>();

    private string currentPlayer = "White";

    private bool Checkmate = false;

    public AudioClip intenseMusic;
    bool intenseMusicPlaying;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("WhiteRook",0,0), Create("WhiteKnight", 1, 0),
            Create("WhiteBishop", 2, 0), Create("WhiteQueen", 3, 0), Create("WhiteKing", 4, 0),
            Create("WhiteBishop", 5, 0), Create("WhiteKnight", 6, 0), Create("WhiteRook", 7, 0),
            Create("WhitePawn", 0, 1), Create("WhitePawn", 1, 1), Create("WhitePawn", 2, 1),
            Create("WhitePawn", 3, 1), Create("WhitePawn", 4, 1), Create("WhitePawn", 5, 1),
            Create("WhitePawn", 6, 1), Create("WhitePawn", 7, 1)
        };
        playerBlack = new GameObject[]
        {
            Create("BlackRook", 0, 7), Create("BlackKnight", 1, 7),
            Create("BlackBishop", 2, 7), Create("BlackQueen", 3, 7), Create("BlackKing", 4, 7),
            Create("BlackBishop", 5, 7), Create("BlackKnight", 6, 7), Create("BlackRook", 7, 7),
            Create("BlackPawn", 0, 6), Create("BlackPawn", 1, 6), Create("BlackPawn", 2, 6),
            Create("BlackPawn", 3, 6), Create("BlackPawn", 4, 6), Create("BlackPawn", 5, 6),
            Create("BlackPawn", 6, 6), Create("BlackPawn", 7, 6)
        };

        // Set all piece positions on the position board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        pieces.Add(obj);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //
        return obj;
    }

    public void RemovePiece(GameObject piece)
    {
        //pieces.Remove(piece);
        //if (intenseMusicPlaying == false)
        {
            //if (pieces.Count <= 15)
            {
                //Debug.Log("Change Music");
                //GameObject bgm = GameObject.Find("BGM"); //Find the gameobject called BGM'
                //AudioSource audiosource = bgm.GetComponent<AudioSource>();

                //audiosource.clip = intenseMusic;
                //audiosource.Play();
                //intenseMusicPlaying = true;
            }
        }
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPostitionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsCheckMate()
    {
        return Checkmate;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (Checkmate == true && Input.GetMouseButtonDown(0))
        {
            Checkmate = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        Checkmate = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " WON!!";
        //GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().color = Color.white;


        //TODO
        //create if statement to check the value of playerWinner (black/white)
        //Get the Winner Text Text Component
        //set color to white or black

        if (playerWinner == "BLACK")
        {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().color = Color.black;
        }
        else
            
        {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().color = Color.white;
        }


         GameObject.FindGameObjectWithTag("RematchText").GetComponent<Text>().enabled = true;
    }
}
