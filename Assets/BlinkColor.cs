using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkColor : MonoBehaviour
{
    public Image myImage;
    public Image touchToStart;
    bool oneToZero = true;

    // Start is called before the first frame update
    void Start()
    {
        if (myImage != null)
        {
            myImage.rectTransform.localScale = new Vector3(Screen.width / 1080f, Screen.height / 1920f, myImage.rectTransform.localScale.z);
        }
        else if (touchToStart != null)
        {
            StartCoroutine(BlinkText());
        }
           
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BlinkImage()
    {
        while (true)
        {
            if (oneToZero)
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a - (Time.deltaTime));
                if (myImage.color.a <= 0)
                {
                    myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);
                    oneToZero = false;
                }
            }
            else
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a + (Time.deltaTime));
                if (myImage.color.a >= 0.75f)
                {
                    myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0.75f);
                    oneToZero = true;
                }
            }

            yield return null;
        }
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            if (oneToZero)
            {
                touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, touchToStart.color.a - (Time.deltaTime));
                if (touchToStart.color.a <= 0)
                {
                    touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, 0);
                    oneToZero = false;
                }
            }
            else
            {
                touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, touchToStart.color.a + (Time.deltaTime));
                if (touchToStart.color.a >= 1f)
                {
                    touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, 1f);
                    oneToZero = true;
                }
            }
            yield return null;
        }
    }

}
