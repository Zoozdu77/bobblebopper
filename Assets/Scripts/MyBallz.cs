using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class MyBallz : MonoBehaviour
{
    [SerializeField] private int destroyChainNum;
    [SerializeField] private int scoreMultiplication;
    [HideInInspector]public List<Ballz> chaine = new();
    public AudioSource audioSource;
    private Rigidbody rb;
    private bool finished;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        chaine.Add(GetComponent<Ballz>());
    }

    private void Update()
    {
        if (finished)
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Ballz>(out _))
        {
            finished = true;
            rb.useGravity = false;
            for (int i = 0; i < chaine.Count; i++)
            {
                for (int l = 0; l < chaine[i].Links.Count; l++)
                {
                    bool isInChain = false;
                    for (int b = 0; b < chaine.Count; b++)
                    {
                        if (chaine[b] == chaine[i].Links[l])
                        {
                            isInChain = true;
                            break;
                        }
                    }
                    if (!isInChain)
                    {
                        chaine.Add(chaine[i].Links[l]);
                    }
                }
            }
            BallzManager.Score += (chaine.Count - 1) * scoreMultiplication;
            if (chaine.Count >= destroyChainNum)
            {
                for (int i = chaine.Count - 1; i >= 0; i--)
                {
                    for (int a = 0; a < GunScript.existingColors.Count; a++)
                    {
                        if (GunScript.existingColors[a] == chaine[i].BallType)
                        {
                            GunScript.existingColors.RemoveAt(a);
                            break;
                        }
                    }
                    chaine[i].Destruction(audioSource);
                }
            }
            rb.velocity = Vector3.zero;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            finished = true;
            rb.useGravity = false;
        }
    }
}
