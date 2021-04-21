using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class ControllerSetup : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    //public GameObject player3;
    //public GameObject player4;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    private PlayerControls controls;
    private int maxPlayers = 4;
    public List<Movement> players;

    private void Awake()
    {
        players = new List<Movement>();

        controls = new PlayerControls();
        controls.Enable();

        InputUser.onUnpairedDeviceUsed += UnpairedDeviceUsed;

        StartJoining();
    }

    private void UnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
    {
        if(!(control is ButtonControl)||control.device.displayName == "Keyboard"|| control.device.displayName == "Mouse")
        {
            return;
        }
        Debug.Log("Device: " + control.device.displayName);

        InputUser user = InputUser.PerformPairingWithDevice(control.device);

        PlayerControls controlsForUser = new PlayerControls();

        user.AssociateActionsWithUser(controlsForUser);

        controlsForUser.Enable();

        if (players.Count == 0)
        {
            player1.GetComponent<Movement>().BindControls(controlsForUser);
            player1.SetActive(true);
            player1.transform.position = spawnPoint1.transform.position;
            players.Add(player1.GetComponent<Movement>());
            GetComponent<Deathcount>().player1Deaths = 0;
        }
        else if(players.Count == 1)
        {
            player2.GetComponent<Movement>().BindControls(controlsForUser);
            player2.SetActive(true);
            player2.transform.position = spawnPoint2.transform.position;
            players.Add(player2.GetComponent<Movement>());
            GetComponent<Deathcount>().player2Deaths = 0;
        }

        List<GameObject> tempPlayers = new List<GameObject>();

        foreach (var item in players)
        {
            tempPlayers.Add(item.gameObject);
        }

        InputUser.listenForUnpairedDeviceActivity--;
        if(InputUser.listenForUnpairedDeviceActivity == 0)
        {
            StopJoining();
        }
    }

    void StartJoining()
    {
        InputUser.listenForUnpairedDeviceActivity = maxPlayers;
    }

    void StopJoining()
    {
        InputUser.listenForUnpairedDeviceActivity = 0;
    }
}
