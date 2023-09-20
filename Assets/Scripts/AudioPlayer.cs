using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)]float shootingVolume = 0.4f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f,1f)]float damageVolume = 0.4f;

    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip,
                                        Camera.main.transform.position,
                                        shootingVolume);
        }
    }

    public void PlayDamageClip()
    {
        if (damageClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip,
                                        Camera.main.transform.position,
                                        damageVolume);
        }
    }
}
