using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public StageData data;
    public Block block;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        int[,] stage = new int[data.width, data.height];
        for(int i = 0; i < data.width; ++i)
        {
            for(int j = 0; j < data.height; ++j)
            {
                stage[i, j] = 0;
            }
        }

        for(int i = 0; i < data.width; ++i)
        {
            for(int j = 0; j < data.height; ++j)
            {
                Block newBlock = GameObject.Instantiate<Block>(block);
                newBlock.transform.position = new Vector3(i - data.width / 2, -j, 0);
                for(int d = 0; d < data.depth.Count; ++d)
                {
                    if(j < data.depth[d])
                    {
                        newBlock.SetData(data.blocks[d]);
                        break;
                    }
                }
            }
        }
    }
}
