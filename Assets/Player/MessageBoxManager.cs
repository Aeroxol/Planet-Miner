using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxManager : MonoBehaviour
{
    public GameObject messageBox;
    public Image messageBoxImg;
    public Text messageBoxTxt;
    int messageCount = 0;

    public void ShowMessageBox(string contents)
    {
        StartCoroutine(ShowMessageBoxCoroutine(contents));
    }

    IEnumerator ShowMessageBoxCoroutine(string contents)
    {
        messageCount++;
        messageBox.SetActive(true);
        messageBoxTxt.text = contents;

        messageBoxImg.color = new Color(messageBoxImg.color.r, messageBoxImg.color.g, messageBoxImg.color.b, 1);
        messageBoxTxt.color = new Color(messageBoxTxt.color.r, messageBoxTxt.color.g, messageBoxTxt.color.b, 1);
        float changeRate = 0.01f;
        while (true)
        {
            messageBoxImg.color = new Color(messageBoxImg.color.r, messageBoxImg.color.g, messageBoxImg.color.b, messageBoxImg.color.a - changeRate * Time.deltaTime);
            messageBoxTxt.color = new Color(messageBoxTxt.color.r, messageBoxTxt.color.g, messageBoxTxt.color.b, messageBoxTxt.color.a - changeRate * Time.deltaTime);
            changeRate *= 1.15f;

            if (messageBoxImg.color.a <= 0.01f)
            {
                messageBoxImg.color = new Color(messageBoxImg.color.r, messageBoxImg.color.g, messageBoxImg.color.b, 0);
                messageBoxTxt.color = new Color(messageBoxTxt.color.r, messageBoxTxt.color.g, messageBoxTxt.color.b, 0);
                break;
            }
            yield return null;
        }

        messageCount--;
        if (messageCount == 0)
            messageBox.SetActive(false);
    }
}
