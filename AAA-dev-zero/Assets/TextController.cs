using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : Singleton<TextController>
{
    public void setText(string message)
    {
        GetComponent<Text>().text = message;
    }
}
