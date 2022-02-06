using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Text storyTxt;
    public Text skipButtonTxt;

    string story = "�����ٶ󸶹ٻ������īŸ����" + System.Environment.NewLine + "������������������������������"
        +System.Environment.NewLine + "������������������������������"
        +System.Environment.NewLine + "������������������������������"
        +System.Environment.NewLine + "������������������������������"
        +System.Environment.NewLine + "������������������������������"
        +System.Environment.NewLine + "������������������������������";

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
        skipButtonTxt.text = "�ݱ�";
    }

    public void SkipBtnClick()
    {
        GameManager.Instance.curSaveData.isFirstTime = false;
        gameObject.SetActive(false);
    }
}
