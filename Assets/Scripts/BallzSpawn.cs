using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallzSpawn : MonoBehaviour
{
    private readonly Ballz[,] baballes = new Ballz[9, 9];
    [SerializeField] private GameObject bouleObjet;

    void Start()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                baballes[x, y] = Instantiate(bouleObjet, transform.position + new Vector3(x, y, 0), transform.rotation, transform).GetComponent<Ballz>();
                baballes[x, y].BallType = Convert.ToInt16(UnityEngine.Random.Range(0, GunScript.numberOfColors));
            }
        }
    }

    public bool StillBaballes()
    {
        bool IsAlive = false;
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (baballes[x, y])
                {
                    IsAlive = true;
                }
            }
        }
        return IsAlive;
    }
}
