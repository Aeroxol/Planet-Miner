using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Uranium : Ore
{
    static int distance = 10;
    PlayerManager player;
    Task task;
    float radioactive;
    public GameObject effectPrefab;

    private void Awake()
    {
        player = PlayerScene.Instance.player;
    }
    private void Start()
    {
        StartCoroutine(CreateEffect());
    }
    private void Update()
    {
        if ((player.transform.position - gameObject.transform.position).magnitude < distance)
        {
            radioactive = Time.time + 1f;
            if (task == null || task.IsCompleted)
            {
                task = Radioactive(player);
            }
        }
        else player.hitByRadiation = false;
    }

    async Task Radioactive(PlayerManager player)
    {
        while (true)
        {
            if (Time.time > radioactive)
            {
                return;
            }
            player.hp--;
            player.hitByRadiation = true;
            if (player.hp < 0)
            {
                player.hp = 0;
                return;
            }
            await Task.Delay((int)(1000 / (player.maxHp * (0.1 - player.radationResist * 0.02))));
        }
    }

    IEnumerator CreateEffect()
    {
        while (true)
        {
            GameObject effect = Instantiate(effectPrefab);
            effect.transform.position = transform.position;
            effect.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
