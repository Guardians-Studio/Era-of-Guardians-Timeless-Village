using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] UIPlayer uiPlayer;

    [SerializeField] GameObject finalPanel;
    [SerializeField] GameObject infoPanel;


    private string name;
    public float health = 80;
    public float maxHealth = 100;
    private float armor = 10;
    private List<GameObject> items;
    private float xpAmount = 10;
    private int level = 1;
    public int gemmeCount = 0;

    private bool inChat = false;

    private bool hazeltown = true;
    private bool langdale = false;
    private bool cineLangdale = false;
    private bool turon = false;
    private bool cineTuron = false;
    private bool boss = false;

    private bool sout = false;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        XP(0);
        Heal(0);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        print(scene.name);
        if (scene.name == "hazeltownInter" && !hazeltown)
        {
            hazeltown = true;
            langdale = false;
            cineLangdale = false;
            turon = false;
            cineTuron = false;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        else if (scene.name == "cineLangdale" && !cineLangdale)
        {
            hazeltown = false;
            langdale = false;
            cineLangdale = true;
            turon = false;
            cineTuron = false;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        else if (scene.name == "langdale" && !langdale)
        {
            langdale = true;
            hazeltown = false;
            turon = false;
            cineTuron = false;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        else if(scene.name == "cineTuron" && !cineTuron)
        {
            hazeltown = false;
            langdale = false;
            cineLangdale = false;
            turon = false;
            cineTuron = true;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        else if (scene.name == "turon" && !turon)
        {
            hazeltown = false;
            langdale = false;
            cineLangdale = false;
            turon = true;
            cineTuron = false;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        else if (scene.name == "finalHazeltown" && !boss)
        {
            boss = true;
            GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");
            this.gameObject.transform.position = tp[0].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Return) && inChat)
        {
            ResumeGame();
            StartCoroutine(Chat());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !inChat)
        {
            PauseGame();
            StartCoroutine(Chat());
        }

        if (Input.GetKeyDown(KeyCode.M) && !sout)
        {
            GameObject[] tp = GameObject.FindGameObjectsWithTag("sout");
            this.gameObject.transform.position = tp[0].transform.position;
            StartCoroutine(Sout());
        }
    }
    public void StartFinal ()
    {
        finalPanel.SetActive(true);
    }

    public void InfoFinal()
    {
        infoPanel.SetActive(true);
    }

    public void CancelInfoFinal()
    {
        infoPanel.SetActive(false);
    }

    public void CancelFinal()
    {
        finalPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        this.health -= amount;
        healthBarExtern.UpdateHealth(this.health / maxHealth);
        uiPlayer.UpdateHealth(this.health / maxHealth, maxHealth);

        if (this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        this.health += amount;
        uiPlayer.UpdateHealth(this.health / maxHealth, maxHealth);
    }

    public void XP(float amount)
    {
        this.xpAmount += amount;
        if (xpAmount >= 100)
        {
            this.level++;
            xpAmount = 0;
            this.maxHealth += 10;
            this.health = maxHealth;
            Heal(0);
            uiPlayer.UpdateLevel(this.level);
        }
        uiPlayer.UpdateXPBar(this.xpAmount / 100);
    }


    private void Die()
    {
        Destroy(gameObject);
        PhotonNetwork.Disconnect();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PhotonNetwork.LoadLevel("MainMenu");
        
    }

    public void ResumeGame()
    {
        GetComponent<PlayerLook>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<WeaponController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void PauseGame()
    {
        GetComponent<PlayerLook>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<WeaponController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Chat()
    {
        yield return new WaitForSeconds(1);
        inChat = !inChat;
        
    }
    IEnumerator Sout()
    {
        yield return new WaitForSeconds(1);
        sout = !sout;

    }
}
