using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public StageData data;
    public Block block;
    public Ore ore;
    // Start is called before the first frame update
    void Start()
    {
        Generate(data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate(StageData _data)
    {
        // Init
        int[,] stage = new int[_data.width, _data.height];
        for(int i = 0; i < _data.width; ++i)
        {
            for(int j = 0; j < _data.height; ++j)
            {
                stage[i, j] = 0;
                float prob = Random.Range(0f, 1f);
                for(int p = _data.ores.Count - 1; p >= 0; --p)
                {
                    if (prob < _data.oreProb[p] && j > _data.oreDepth[p])
                    {
                        stage[i, j] = p + 1;
                        break;
                    }
                    else
                    {
                        prob = prob - _data.oreProb[p];
                    }
                }
            }
        }
        // Mix
        for(int c = 0; c < _data.mixCount; ++c)
        {
            int[,] temp = new int[_data.width, _data.height];
            for (int i = 0; i < _data.width; ++i)
            {
                for (int j = 0; j < _data.height; ++j)
                {
                    int rand = Random.Range(0, 8);
                    switch (rand)
                    {
                        case 0:
                            if (i == 0 || j == 0) {
                                goto default;
                            }
                            temp[i, j] = stage[i - 1, j - 1];
                            break;
                        case 2:
                            if (i == _data.width - 1 || j == 0)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i + 1, j - 1];
                            break;
                        case 7:
                            if (i == _data.width - 1 || j == _data.height - 1)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i + 1, j + 1];
                            break;
                        case 5:
                            if (i == 0 || j == _data.height - 1)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i - 1, j + 1];
                            break;
                        case 1:
                            if (j == 0)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i, j - 1];
                            break;
                        case 3:
                            if (i == 0)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i - 1, j];
                            break;
                        case 4:
                            if (i == _data.width - 1)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i + 1, j];
                            break;
                        case 6:
                            if (j == _data.height - 1)
                            {
                                goto default;
                            }
                            temp[i, j] = stage[i, j + 1];
                            break;
                        default:
                            temp[i, j] = 0;
                            break;
                    }
                }
            }
            stage = temp;
            Debug.Log(c);
        }

        // Set
        for(int i = 0; i < _data.width; ++i)
        {
            for(int j = 0; j < _data.height; ++j)
            {
                Block newBlock = GameObject.Instantiate<Block>(block);
                newBlock.transform.position = new Vector3(i - _data.width / 2, -j, 0);

                for (int d = 0; d < _data.depth.Count; ++d)
                {
                    if(j < _data.depth[d])
                    {
                        newBlock.SetData(_data.blocks[d]);
                        break;
                    }
                }
                for (int p = 0; p < _data.ores.Count; ++p)
                {
                    if (stage[i, j] == p + 1)
                    {
                        Ore newOre = GameObject.Instantiate<Ore>(ore);
                        newOre.transform.position = new Vector3(i - _data.width / 2, -j, 0);
                        newOre.SetData(_data.ores[p]);
                    }

                }
            }
        }
    }
}
