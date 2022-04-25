using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageDisplayer : MonoBehaviour
{
    public Transform messageItemContainer;

    public GameObject messageItemPrefab;

    public void DisplayMessage(string message)
    {
        GameObject instance = Instantiate(messageItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        instance.transform.SetParent(messageItemContainer, false);
        instance.GetComponent<MessageItem>().DiplayMessageContent(message);
    }
}

