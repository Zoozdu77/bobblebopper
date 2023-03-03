using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Rotations")]
    [SerializeField] private float turnMax;
    [SerializeField] private float sensitivity;
    private Vector2 rotation;

    [Header("Shoot")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Animator anims;
    [SerializeField] private Ballz nextBall;
    [SerializeField] private Ballz currBall;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip pop;
    public static List<Int16> existingColors = new();
    private readonly List<Int16> ballsToCome = new();
    public static int numberOfColors = 4;
    private float cooldown;

    private void Start()
    {
        ballsToCome.Add(Convert.ToInt16(UnityEngine.Random.Range(0, numberOfColors)));
        ballsToCome.Add(Convert.ToInt16(UnityEngine.Random.Range(0, numberOfColors)));
        currBall.BallType = ballsToCome[0];
        nextBall.BallType = ballsToCome[1];
        currBall.ChangeColor();
        nextBall.ChangeColor();
    }

    void Update()
    {
        Mouvement();
        if (Input.GetButton("Fire1") && cooldown <= 0)
        {
            Shoot();
        } else if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void Mouvement()
    {
        rotation.x += -Input.GetAxis("Vertical") * sensitivity * Time.deltaTime;
        rotation.y += Input.GetAxis("Horizontal") * sensitivity * Time.deltaTime;
        rotation = Vector2.ClampMagnitude(rotation, turnMax);

        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }

    private void Shoot()
    {
        anims.SetTrigger("Shoot");
        cooldown = shootCooldown;
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, null);
        newBullet.GetComponent<MyBallz>().audioSource = audioS;
        audioS.PlayOneShot(pop);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        newBullet.GetComponent<Ballz>().BallType = ballsToCome[0] ;
        ballsToCome[0] = ballsToCome[1];

        if (existingColors.Count > 0)
        {
            int randomnumber = UnityEngine.Random.Range(0, existingColors.Count);
            ballsToCome[1] = existingColors[randomnumber];

        }
        currBall.BallType = ballsToCome[0];
        nextBall.BallType = ballsToCome[1];
        currBall.ChangeColor();
        nextBall.ChangeColor();
    }
}
