using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject wandProjectileSpawnPoint;
    public GameObject wandProjectile;
    public AudioClip wandAttack;
    public float damage = 50f;
    public float projectileSpeed = 20f;
    public float cooldown = 3f;
}
