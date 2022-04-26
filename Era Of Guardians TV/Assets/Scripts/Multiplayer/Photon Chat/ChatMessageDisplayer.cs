using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageDisplayer : MonoBehaviour
{
    public Transform messageItemContainer;

    public GameObject messageItemPrefab;

    private List<GameObject> messages = new List<GameObject>();

    private List<string> messagesContent = new List<string>();

    public void DisplayMessage(string message)
    { 
        
        foreach (var item in messagesContent)
        {
            if (message == item)
            {
                return;
            }
        }

        messagesContent.Add(message);
        GameObject instance = Instantiate(messageItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        instance.transform.SetParent(messageItemContainer, false);
        instance.GetComponent<MessageItem>().DiplayMessageContent(message);
        messages.Add(instance);

        if (messages.Count > 6)
        {
            GameObject messageToDestroy = messages[0];
            messages.RemoveAt(0);
            Destroy(messageToDestroy);
            messagesContent.RemoveAt(0);
        }

    }
}

