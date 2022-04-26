using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterUI : MonoBehaviour
{
    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject secondPanel;

    private bool transition = false;

    private void Start()
    {
        PhotonView view = GetComponentInParent<PhotonView>();

        if (view.IsMine && PhotonNetwork.CurrentRoom != null)
        {
            firstPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && firstPanel.activeSelf)
        {
            firstPanel.SetActive(false);
            secondPanel.SetActive(true);
            StartCoroutine(Transition());
        }
        else if (transition && Input.GetKey(KeyCode.F) && secondPanel.activeSelf)
        {
            secondPanel.SetActive(false);
            this.enabled = false;
            print("second");
        }
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        transition = true;
    }
}
