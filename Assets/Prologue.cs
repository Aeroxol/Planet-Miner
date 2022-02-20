using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Text storyTxt;
    public Text skipButtonTxt;
    bool showing = false;
    public LobbyScene lobbyScene;

    string story = "���� 2XXX��..." + System.Environment.NewLine + System.Environment.NewLine +
    "�η��� �ɰ��� ������ ���� ���׿� ������ �ִ�." + System.Environment.NewLine + System.Environment.NewLine + "�� ���¸� �ذ��� �ڿ��� �ñ��� ���� <����״�>�� ã�ƾ� �Ѵ�."
        + System.Environment.NewLine + System.Environment.NewLine + "���ָ� ���ƴٴϸ鼭 �κ��� ���ּ��� ��ȭ�ϰ�, ���� �༺�� �����Ͽ� ����״��� ä������.";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showStory());
    }

    public IEnumerator showStory()
    {
        if (!showing)
        {
            showing = true;
            storyTxt.text = "";
            for (int i = 0; i < story.Length; i++)
            {
                storyTxt.text += story[i];
                yield return new WaitForSeconds(0.1f);
            }
            skipButtonTxt.text = "�ݱ�";
        }
        showing = false;
        yield return null;
    }

    public void SkipBtnClick()
    {
        showing = false;
        //GameManager.Instance.curSaveData.isFirstTime = false;
        if (GameManager.Instance.curSaveData.isFirstTime) lobbyScene.ShowFirstMessage();
        gameObject.SetActive(false);
    }
}
