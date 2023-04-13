using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public AudioSource audioSource;
    public AudioClip clip;

    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        audioSource.PlayOneShot(clip);
    }

    public void Secondary()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        //rotated slightly to the left
        var bullet1 = Instantiate(bulletPrefab, bulletSpawnPoint1.position, bulletSpawnPoint1.rotation);
        bullet1.GetComponent<Rigidbody>().velocity = bulletSpawnPoint1.forward * bulletSpeed;
        //rotated slightly to the right
        var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
        bullet2.GetComponent<Rigidbody>().velocity = bulletSpawnPoint2.forward * bulletSpeed;
        audioSource.PlayOneShot(clip);
    }
}