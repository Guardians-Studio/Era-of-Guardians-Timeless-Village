using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightDetection : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticle;

    private float damage;
    private string currentWeapon;

    private bool isCollided = true;

    private const int hitEventCode = 16;

    private void Start()
    {
        if (this.tag == "Sword")
        {
            currentWeapon = "Sword";
            damage = GetComponent<Sword>().damage;
        }
        else if (this.tag == "Axe")
        {
            currentWeapon = "Axe";
            damage = GetComponent<Axe>().damage;
        }
        else if (this.tag == "Arrow")
        {
            currentWeapon = "Projectile";
            damage = 30f; // does not do link with Bow Script
        }
        else if(this.tag == "Bullet")
        {
            currentWeapon = "Projectile";
            damage = 50f; // does not do link with Bow Script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentWeapon == "Projectile" && other.gameObject.tag != "Player" && isCollided)
        {
            isCollided = false;
            Destroy(gameObject);
        }

        if (other.tag == "Enemy") // can hit twice +, need this.enable = false;
        {
            // other.GetComponent<Animator>().SetTrigger("Hit");
            ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
            particle.Play();
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            HitEvent(other, damage);
        }
        if (other.tag == "Boss")
        {
            ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
            particle.Play();
            other.gameObject.GetComponentInParent<Boss>().TakeDamage(0);
            HitEvent(other, damage);
        }
        if (other.tag == "Enemy2")
        {
            ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
            particle.Play();
            other.gameObject.GetComponentInParent<BasicEnemy2>().TakeDamage(0);
            HitEvent(other, damage);

        }
    }


    private void HitEvent(Collider other, float damage)
    {
        object[] content = new object[] {other.name, other.tag, damage};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(hitEventCode, content, raiseEventOptions, SendOptions.SendReliable);
        print("event sent");
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    public void NetworkingClient_EventReceived(EventData photonEvent)
    {
        print("receive event");
        byte eventCode = photonEvent.Code;

        if (eventCode == hitEventCode) // hit event
        {
            object[] data = (object[])photonEvent.CustomData;

            GameObject[] bots = null;

            switch (data[1])
            {
                case "Enemy":
                    bots = GameObject.FindGameObjectsWithTag("Enemy");
                    break;

                case "Enemy2":
                    bots = GameObject.FindGameObjectsWithTag("Enemy2");
                    break;

                case "Boss":
                    bots = GameObject.FindGameObjectsWithTag("Boss");
                    break;
            }

            GameObject botHit = null;

            foreach (var item in bots)
            {
                if (item.name == (string)data[0])
                {
                    botHit = item;
                }
            }

            switch (data[1])
            {
                case "Enemy":
                    botHit.gameObject.GetComponentInParent<Enemy>().TakeDamage((float)data[2]);
                    break;
            }        
        }
    }
}

