using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deathcount : MonoBehaviour
{
    public Text player1Text;
    public Text player2Text;
    public Text player3Text;
    public Text player4Text;
    public Text timer;

    public List<string> deadPlayers = new List<string>();

    public int player1Deaths = -1;
    public int player2Deaths = -1;
    public int player3Deaths = -1;
    public int player4Deaths = -1;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    float gameTimer = 120;

    Text[] text = new Text[4];
    int[] playerDeaths = new int[4];

    private void Start()
    {
        Text[] text = { player1Text, player2Text, player3Text, player4Text };
    }

    void Update()
    {
        if (gameTimer > 0)
        {
            string minutes = Mathf.FloorToInt(gameTimer / 60).ToString("0");
            string seconds = Mathf.RoundToInt(gameTimer % 60).ToString("00");

            timer.text = minutes + ":" + seconds;

            gameTimer -= Time.deltaTime;
        }

        foreach (string item in deadPlayers)
        {
            switch (item)
            {
                case "Player1":
                    player1Deaths++;
                    deadPlayers.RemoveAt(deadPlayers.IndexOf(item));
                    break;
                case "Player2":
                    player2Deaths++;
                    deadPlayers.RemoveAt(deadPlayers.IndexOf(item));
                    break;
                case "Player3":
                    player3Deaths++;
                    deadPlayers.RemoveAt(deadPlayers.IndexOf(item));
                    break;
                case "Player4":
                    player4Deaths++;
                    deadPlayers.RemoveAt(deadPlayers.IndexOf(item));
                    break;
            }
        }
        
        if(player1Deaths >= 0)
            player1Text.text = player1Deaths.ToString();

        if (player2Deaths >= 0)
            player2Text.text = player2Deaths.ToString();

        if (player3Deaths >= 0)
            player3Text.text = player3Deaths.ToString();

        if (player4Deaths >= 0)
            player4Text.text = player4Deaths.ToString();

        Debug.Log(gameTimer);
        if(gameTimer <= 0)
        {
            player1.GetComponent<Movement>().enabled = false;
            player2.GetComponent<Movement>().enabled = false;
            player3.GetComponent<Movement>().enabled = false;
            //player4.GetComponent<Movement>().enabled = false;

            if (GetComponent<ControllerSetup>().players.Count == 2)
            {
                if(player1Deaths < player2Deaths)
                {
                    timer.text = "Green slime won!";
                }
                else if (player1Deaths > player2Deaths)
                {
                    timer.text = "Blue slime won!";
                }
                else
                {
                    timer.text = "Tie";
                }
            }
            else if (GetComponent<ControllerSetup>().players.Count == 3)
            {
                if ((player1Deaths > player2Deaths) && (player1Deaths > player3Deaths))
                {
                    timer.text = "Green slime won!";
                }
                else if ((player1Deaths > player2Deaths) && (player1Deaths > player3Deaths))
                {
                    timer.text = "Blue slime won!";
                }
                else if ((player3Deaths > player2Deaths) && (player3Deaths > player4Deaths))
                {
                    timer.text = "Orange slime won!";
                }
                else
                {
                    timer.text = "Tie";
                }
            }
            //else if (GetComponent<ControllerSetup>().players.Count == 4)
            //{
            //    if ((player1Deaths > player2Deaths) && (player1Deaths > player3Deaths) && (player1Deaths > player4Deaths))
            //    {
            //        timer.text = "Green slime won!";
            //    }
            //    if ((player1Deaths > player2Deaths) && (player1Deaths > player3Deaths) && (player2Deaths > player4Deaths))
            //    {
            //        timer.text = "Blue slime won!";
            //    }
            //    if ((player3Deaths > player2Deaths) && (player3Deaths > player4Deaths) && (player3Deaths > player4Deaths))
            //    {
            //        timer.text = "Orange slime won!";
            //    }
            //    if ((player4Deaths > player2Deaths) && (player4Deaths > player3Deaths) && (player4Deaths > player1Deaths))
            //    {
            //        timer.text = "Red slime won!";
            //    }
            //    else
            //    {
            //        timer.text = "Tie";
            //    }
            //}
        }

        GetComponent<GameplayController>().StartCoroutine("StartNewGame");
    }
}
