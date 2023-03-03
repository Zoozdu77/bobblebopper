using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallzManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private Light[] lights;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private readonly List<Transform> spawnedSpawners = new();
    [SerializeField] private Vector3 movementPerTick;
    [SerializeField] private float DamageZValue;
    [SerializeField] private float baseSpeedPerSec;
    [SerializeField] private float MultPerSec;
    [SerializeField] private int MoveBeetweenSpawns;
    [SerializeField] private AudioSource sons;
    private float lightTimer;
    public static int Score;
    private float moveSinceLastInstance;
    private bool danger = false;

    private void Start()
    {
        InstanciateSpawners();
    }

    private void Update()
    {
        ScoreText.text = "Score : " + Score;
        MoveSpawners();
        if (moveSinceLastInstance >= MoveBeetweenSpawns)
        {
            InstanciateSpawners();
            moveSinceLastInstance = 0;
        }
    }

    private void MoveSpawners()
    {
        for (int i = 0; i < spawnedSpawners.Count; i++)
        {
            if (spawnedSpawners[i].gameObject.activeSelf)
            {
                spawnedSpawners[i].position += (MultPerSec * Time.time + 1) * baseSpeedPerSec * Time.deltaTime * movementPerTick;
                moveSinceLastInstance += (MultPerSec * Time.time + 1) * baseSpeedPerSec * Time.deltaTime;
                if (spawnedSpawners[i].position.z < DamageZValue +5 && danger == false)
                {
                    print("ok");
                    sons.pitch = 1.3f;
                    danger = true;
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
    }

    private void InstanciateSpawners()
    {
        
        spawnedSpawners.Add(Instantiate(spawner, transform.position, transform.rotation, transform).transform);
    }
}
