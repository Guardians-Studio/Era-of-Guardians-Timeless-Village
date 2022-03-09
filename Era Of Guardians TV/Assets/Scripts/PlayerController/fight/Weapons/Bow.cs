using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject bowProjectileSpawnPoint;
    public GameObject bowProjectile;
    public AudioClip bowAttack;
    public float damage = 40f;
    public float projectileSpeed = 30f;
    public float cooldown = 2.5f;
    
}
