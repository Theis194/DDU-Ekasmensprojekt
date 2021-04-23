using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    public GameObject playButton;

    public Sprite selected;
    public Sprite notSelected;

    void Start()
    {
        if(playButton != null)
        {
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponent<SpriteRenderer>().sprite = selected;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponent<SpriteRenderer>().sprite = notSelected;
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
