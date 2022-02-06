using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Text storyTxt;
    public Text skipButtonTxt;

    string story = "가나다라마바사아자차카타파하" + System.Environment.NewLine + "가가가가가가가가가가가가가가가"
        +System.Environment.NewLine + "가가가가가가가가가가가가가가가"
        +System.Environment.NewLine + "가가가가가가가가가가가가가가가"
        +System.Environment.NewLine + "가가가가가가가가가가가가가가가"
        +System.Environment.NewLine + "가가가가가가가가가가가가가가가"
        +System.Environment.NewLine + "가가가가가가가가가가가가가가가";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showStory());
    }

    IEnumerator showStory()
    {
        storyTxt.text = "";
        for(int i=0; i < story.Length; i++)
        {
            storyTxt.text += story[i];
            yield return new WaitForSeconds(0.1f);
        }
        skipButtonTxt.text = "닫기";
    }

    public void SkipBtnClick()
    {
        GameManager.Instance.curSaveData.isFirstTime = false;
        gameObject.SetActive(false);
    }
}
