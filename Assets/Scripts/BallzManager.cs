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
    [SerializeField] private float timeBeetweenMove;
    [SerializeField] private float MultPerSec;
    [SerializeField] private int MoveBeetweenSpawns;
    [SerializeField] private AudioSource sons;
    public static int Score;
    private float moveSinceLastInstance;
    private float Timer;
    private bool danger = false;

    private void Start()
    {
        Score = 0;
        InstanciateSpawners();
        Timer = timeBeetweenMove;
    }

    private void Update()
    {
        if (Timer<=0)
        {
            ScoreText.text = "Score : " + Score;
            MoveSpawners();
            Timer = timeBeetweenMove;
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
        Debug.Log("Move, " + moveSinceLastInstance);
        moveSinceLastInstance++;
        for (int i = 0; i < spawnedSpawners.Count; i++)
        {
            if (spawnedSpawners[i].gameObject.activeSelf)
            {
                spawnedSpawners[i].position += movementPerTick;
                if (spawnedSpawners[i].position.z < DamageZValue +5 && danger == false)
                {
                    sons.pitch = 1.3f;
                    danger = true;
                }else if (spawnedSpawners[i].position.z < DamageZValue)
                {
                    if (spawnedSpawners[i].GetComponent<BallzSpawn>().StillBaballes())
                    {
                        if (Score >= PlayerPrefs.GetInt("HScore"))
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
    }

    private void InstanciateSpawners()
    {
        Debug.Log("Instance, " + moveSinceLastInstance);
        spawnedSpawners.Add(Instantiate(spawner, transform.position, transform.rotation, transform).transform);
    }
}
