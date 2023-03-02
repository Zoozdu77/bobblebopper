using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallzManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    private readonly List<Transform> spawnedSpawners = new();
    [SerializeField] private Vector3 movementPerTick;
    [SerializeField] private float baseTimer;
    [SerializeField] private float dividePerMinute;
    private float Timer;

    private void Start()
    {
        InstanciateSpawners();
    }

    private void Update()
    {
        if (Timer <= 0)
        {
            InstanciateSpawners();
        } else
        {
            Timer -= Time.deltaTime;
        }
    }

    private void InstanciateSpawners()
    {
        for (int i = 0; i < spawnedSpawners.Count; i++)
        {
            spawnedSpawners[i].position += movementPerTick;
        }
        spawnedSpawners.Add(Instantiate(spawner, transform.position, transform.rotation, transform).transform);
        Timer += baseTimer / 1 + (dividePerMinute * (Time.time / 60));
    }
}
