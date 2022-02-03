using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject dynamitePrefab;
    public GameObject dynamiteB_Prefab;
    public GameObject rocketBombPrefab;
    public Rigidbody2D playerRigid;
    public Animator warpAnimator;
    public MessageBoxManager messageBoxManager;
    public GameObject insuranceEffect;

    // Start is called before the first frame update
    void Start()
    {
        //playerRigid = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool ItemEffect(int itemCode)
    {
        bool success = true;

        switch (itemCode)
        {
            case 14:
                if (playerManager.hp == playerManager.maxHp)
                {
                    success = false;
                    messageBoxManager.ShowMessageBox("최대 체력입니다.");
                }
                else
                    playerManager.StartCoroutine(playerManager.RestoreHpItem(500));
                break;
            case 15:
                if (playerManager.hp == playerManager.maxHp)
                {
                    success = false;
                    messageBoxManager.ShowMessageBox("최대 체력입니다.");
                }
                else
                    playerManager.StartCoroutine(playerManager.RestoreHpItem(1000));
                break;
            case 16:
                if (playerManager.hp == playerManager.maxHp)
                {
                    success = false;
                    messageBoxManager.ShowMessageBox("최대 체력입니다.");
                }
                else
                    playerManager.StartCoroutine(playerManager.RestoreHpItem(2000));
                break;
            case 17:
                CreateDynamite();
                break;
            case 18:
                CreateDynamiteB();
                break;
            case 19:
                CreateRocketBomb();
                break;
            case 20:
                if (playerManager.canUseItem)
                    StartCoroutine(EscapeWarp());
                else
                {
                    success = false;
                    messageBoxManager.ShowMessageBox("지금은 사용할 수 없습니다.");
                }
                break;
            case 21:
                if (GameManager.Instance.curSaveData.itemProtected)
                {
                    success = false;
                    messageBoxManager.ShowMessageBox("이미 효과가 적용 중입니다.");
                }
                else
                {
                    GameManager.Instance.curSaveData.itemProtected = true;
                    messageBoxManager.ShowMessageBox("보험이 적용되었습니다.");
                    insuranceEffect.SetActive(true);
                }
                break;
        }
        return success;
    }

    void CreateDynamite()
    {
        GameObject temp = Instantiate(dynamitePrefab);
        temp.transform.position = player.transform.position;
    }
    void CreateDynamiteB()
    {
        GameObject temp = Instantiate(dynamiteB_Prefab);
        temp.transform.position = player.transform.position;
    }
    void CreateRocketBomb()
    {
        GameObject temp = Instantiate(rocketBombPrefab);
        temp.transform.position = player.transform.position;
        temp.transform.Translate(0, 0.2f, 0);
    }

    IEnumerator EscapeWarp()
    {
        playerManager.canUseItem = false;
        playerManager.PausePlayer(true);
        warpAnimator.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7.0f, warpAnimator.transform.position.z);
        warpAnimator.gameObject.SetActive(true);
        warpAnimator.Play("WarpEffect");
        //warpAnimator.SetTrigger("Warp");
        while (true)
        {
            if (warpAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                warpAnimator.gameObject.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }


        player.transform.position = new Vector2(0.0f, 1.0f);

        warpAnimator.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7.0f, warpAnimator.transform.position.z);
        warpAnimator.gameObject.SetActive(true);
        warpAnimator.Play("WarpEffectReverse");
        playerManager.PausePlayer(false);
        //warpAnimator.SetTrigger("Warp");
        while (true)
        {
            if (warpAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                warpAnimator.gameObject.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        playerManager.canUseItem = true;

        //playerManager.PausePlayer(false);
    }
}
