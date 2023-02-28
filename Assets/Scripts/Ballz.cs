using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballz : MonoBehaviour
{
    public int BallType;
    public List<Ballz> Links = new();

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
}
