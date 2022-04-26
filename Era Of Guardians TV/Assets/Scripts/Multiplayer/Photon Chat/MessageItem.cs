using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    public TMP_Text text;

    public void DiplayMessageContent(string _text)
    {
        text.SetText(_text);
    }
}
