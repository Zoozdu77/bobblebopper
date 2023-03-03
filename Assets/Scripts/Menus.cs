using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HScore;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private GameObject NEWHSCORE;
    [SerializeField] private AudioSource sons;
    [SerializeField] private AudioClip typing;
    [SerializeField] private AudioClip Yeah;

    private void Start()
    {
        GunScript.existingColors.Clear();
        if (HScore != null)
        {
            HScore.text = "High Score : " + PlayerPrefs.GetInt("HScore").ToString();
        }
        
        if (Score != null)
        {
            Score.text = "Score : " + PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Typing()
    {
        sons.PlayOneShot(typing);
    }

    public void YEAH()
    {
        if (PlayerPrefs.GetInt("HScore") <= PlayerPrefs.GetInt("Score") && NEWHSCORE != null)
        {
            NEWHSCORE.SetActive(true);
            sons.PlayOneShot(Yeah);
        }
    }
}
