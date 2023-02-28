using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class MyBallz : MonoBehaviour
{
    public List<Ballz> chaine = new();
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        chaine.Add(GetComponent<Ballz>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Ballz>(out _))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
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
        }
    }
}
