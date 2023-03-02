using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallzManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private readonly List<Transform> spawnedSpawners = new();
    [SerializeField] private Vector3 movementPerTick;
    [SerializeField] private float DamageZValue;
    [SerializeField] private float baseTimer;
    [SerializeField] private float dividePerMinute;
    [SerializeField] private int MoveBeetweenSpawns;
    private float Timer;
    public static int Score;
    private int moveSinceLastInstance;
    private bool danger;

    private void Start()
    {
        InstanciateSpawners();
    }

    private void Update()
    {
        ScoreText.text = "Score : " + Score;
        if (Timer <= 0)
        {
            MoveSpawners();
            moveSinceLastInstance++;
            if (moveSinceLastInstance >= MoveBeetweenSpawns)
            {
                InstanciateSpawners();
                moveSinceLastInstance = 0;
            }
        } else
        {
            Timer -= Time.deltaTime;
        }
    }

    private void MoveSpawners()
    {
        for (int i = 0; i < spawnedSpawners.Count; i++)
        {
            if (spawnedSpawners[i].gameObject.activeSelf)
            {
                spawnedSpawners[i].position += movementPerTick;
                if (spawnedSpawners[i].position.z < DamageZValue - 3 && danger == false)
                {
                    danger = true;
                    //Danger
                }else if (spawnedSpawners[i].position.z < DamageZValue)
                {
                    if (spawnedSpawners[i].GetComponent<BallzSpawn>().StillBaballes())
                    {
                        if (Score >= PlayerPrefs.GetInt("HScore", 0))
                        {
                            PlayerPrefs.SetInt("HScore", Score);
                        }
                        PlayerPrefs.SetInt("Score", Score);
                        SceneManager.LoadScene(2);
                    }
                    spawnedSpawners[i].gameObject.SetActive(false);
                }
            }
        }
        Timer += baseTimer / 1 + (dividePerMinute * (Time.time / 60));
    }

    private void InstanciateSpawners()
    {
        
        spawnedSpawners.Add(Instantiate(spawner, transform.position, transform.rotation, transform).transform);
    }
}
