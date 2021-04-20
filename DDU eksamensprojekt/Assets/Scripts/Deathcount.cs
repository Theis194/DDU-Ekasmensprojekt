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

    public List<string> deadPlayers = new List<string>();

    int player1Deaths;
    int player2Deaths;
    int player3Deaths;
    int player4Deaths;

    Text[] text = new Text[4];
    int[] playerDeaths = new int[4];

    private void Start()
    {
        Text[] text = { player1Text, player2Text, player3Text, player4Text };
        int[] playerDeaths = { player1Deaths, player2Deaths, player3Deaths, player4Deaths };
    }

    void Update()
    {
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

        player1Text.text = player1Deaths.ToString();
        player2Text.text = player2Deaths.ToString();
        player3Text.text = player3Deaths.ToString();
        player4Text.text = player4Deaths.ToString();
    }
}
