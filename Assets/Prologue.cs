using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Text storyTxt;
    public Text skipButtonTxt;

    string story = "서기 2XXX년..." + System.Environment.NewLine + System.Environment.NewLine +
    "인류는 심각한 에너지 부족 사테에 직면해 있다." + System.Environment.NewLine + System.Environment.NewLine + "이 사태를 해결할 자원 궁극의 광물 <언옵테늄>을 찾아야 한다."
        + System.Environment.NewLine + System.Environment.NewLine + "우주를 돌아다니면서 로봇과 우주선을 강화하고, 고레벨 행성에 도달하여 언옵테늄을 채굴하자.";

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
