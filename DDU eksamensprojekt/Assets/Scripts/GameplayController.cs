using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    float timer;

    public Text nextGame;

    void Update()
    {
        if(timer > 0)
        {
            string minutes = Mathf.FloorToInt(timer / 60).ToString("0");
            string seconds = Mathf.RoundToInt(timer % 60).ToString("00");

            nextGame.text = "Exiting to startscreen in " + minutes + ":" + seconds;

            timer -= Time.deltaTime;
        }
    }

    public IEnumerator StartNewGame()
    {
        timer = 10;

        AudioManager.instance.play("slutning");
        AudioManager.instance.play("KlapSalve");

        nextGame.gameObject.SetActive(true);

        yield return new WaitForSeconds(10);

        AudioManager.instance.stop("KlapSalve");

        GetComponent<ChangeScene>().Change();
    }
}
