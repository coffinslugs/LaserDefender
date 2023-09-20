using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float timeBetweenEnemyFire = 0.7f;
    [SerializeField] float firingTimeVariance = 0.5f;
    [SerializeField] float minFireRate = 0.2f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }

    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponentInChildren<Rigidbody2D>();

           if (rb != null)
            {
                rb.velocity = (transform.up * projectileSpeed);
            }

            Destroy(projectile, projectileLifetime);

            firingRate = UnityEngine.Random.Range(timeBetweenEnemyFire - firingTimeVariance,
                                      timeBetweenEnemyFire + firingTimeVariance);

            firingRate = Mathf.Clamp(firingRate, minFireRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(firingRate);
        }

    }
}
