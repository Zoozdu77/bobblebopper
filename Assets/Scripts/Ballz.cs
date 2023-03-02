using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ballz : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private bool isToShow;
    [SerializeField] private GameObject ParticleSys;
    public Int16 BallType;
    [HideInInspector]public List<Ballz> Links = new();

    private void Start()
    {
        ChangeColor();
        if (!isToShow)
        {
            GunScript.existingColors.Add(BallType);
        }
    }

    public void Destruction()
    {
        Instantiate(ParticleSys, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ballz>(out Ballz ball))
        {
            if (ball.BallType == BallType) 
            {
                Links.Add(ball);
            }
        }
    }

    public void ChangeColor()
    {
        int mat = ((int)BallType);
        if (mat >= materials.Length)
        {
            Debug.LogError("Trop long");
            Debug.Log(mat);
            return;
        }
        gameObject.GetComponent<MeshRenderer>().material = materials[mat];
    }
}
