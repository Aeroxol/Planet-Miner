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

    private void Awake()
    {
        player = PlayerScene.Instance.player;
    }
    private void Update()
    {
        if((player.transform.position - gameObject.transform.position).magnitude < distance){
            radioactive = Time.time + 1f;
            if (task == null || task.IsCompleted)
            {
                task = Radioactive(player);
            }
        }
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
            if (player.hp < 0)
            {
                player.hp = 0;
                return;
            }
            await Task.Delay((int)(1000 / (player.maxHp * (0.1 - player.radationResist * 0.02))));
        }
    }
}
