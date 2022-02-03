using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Block block;
    public BlockData immortalBlock;
    public Ore ore;

    public Ore oreWithBody;

    public void Generate(StageData _data)
    {
        // Init
        int[,] stage = new int[_data.width, _data.height];
        for(int i = 0; i < _data.width; ++i)
        {
            for(int j = 0; j < _data.height; ++j)
            {
                stage[i, j] = 0;
                float prob = Random.Range(0f, 1f);
                for(int p = _data.oreIndex.Count - 1; p >= 0; --p)
                {
                    if (prob < _data.oreProbs[p].Evaluate((float)j/_data.height))
                    {
                        stage[i, j] = p + 1;
                        break;
                    }
                    else
                    {
                        prob = prob - _data.oreProbs[p].Evaluate((float)j / _data.height);
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
        }
        // Immortal Block Set
        // base
        for(int i = 0; i < 5; ++i)
        {
            stage[_data.width/2 + i, 0] = -1;
            stage[_data.width/2 - i, 0] = -1;
        }
        // random
        for(int i = 0; i < _data.disNum; ++i)
        {
            int _x = Random.Range(0, _data.width - 2);
            int _y = Random.Range(0, _data.height - 2);
            stage[_x + 1, _y + 1] = -1;
            for(int j = 0; j < _data.disLength; ++j)
            {
                int direction = Random.Range(0, 4);
                switch (direction)
                {
                    case 0:
                        _y = (_y - 1) % (_data.height - 2);
                        if(_y < 0)
                        {
                            _y += _data.height-1;
                        }
                        stage[_x + 1, _y + 1] = -1;
                        break;
                    case 1:
                        _x = (_x + 1) % (_data.width - 2);
                        stage[_x + 1, _y + 1] = -1;
                        break;
                    case 2:
                        _y = (_y + 1) % (_data.height - 2);
                        if (_y > _data.height - 2)
                        stage[_x + 1, _y + 1] = -1;
                        break;
                    case 3:
                        _x = (_x - 1) % (_data.width - 2);
                        if (_x < 0)
                        {
                            _x += _data.width-1;
                        }
                        stage[_x + 1, _y + 1] = -1;
                        break;
                }
            }
        }

        GameManager.Instance.curSaveData.curStageMap = stage;

        // Render
        RenderStage(_data, stage);
    }

    public void RenderStage(StageData _data, int[,] stage)
    {
        for (int i = 0; i < _data.width; ++i)
        {
            for (int j = 0; j < _data.height; ++j)
            {
                if(stage[i,j] == -9)
                {
                    continue;
                }
                Block newBlock = GameObject.Instantiate<Block>(block);
                newBlock.transform.position = new Vector3(i - _data.width / 2, -j, 0);
                newBlock.stage = this;
                newBlock.x = i;
                newBlock.y = j;
                if (i == 0 || i == _data.width - 1 || j == _data.height - 1)
                {
                    newBlock.SetData(immortalBlock);
                    continue;
                }
                if (stage[i, j] == -1)
                {
                    newBlock.SetData(immortalBlock);
                    newBlock.isInvincible = true;
                    continue;
                }

                for (int d = 0; d < _data.depth.Count; ++d)
                {
                    if (j < _data.depth[d])
                    {
                        newBlock.SetData(GameManager.Instance.dirtBlocks[_data.level + d]);
                        break;
                    }
                }
                for (int p = 0; p < _data.oreIndex.Count; ++p)
                {
                    if (stage[i, j] == p + 1)
                    {
                        Ore newOre = GameObject.Instantiate<Ore>(ore);
                        newOre.transform.position = new Vector3(i - _data.width / 2, -j, 0);
                        newOre.SetData(GameManager.Instance.oreLevelData[_data.level].ores[p]);
                        
                        newBlock.GetComponent<Block>().myOre = newOre.gameObject;
                        Ore newOreWithBody = Instantiate(oreWithBody);
                        newOreWithBody.transform.position = newOre.transform.position;
                        newOreWithBody.SetData(GameManager.Instance.oreLevelData[_data.level].ores[p]);
                        newBlock.GetComponent<Block>().myOreWithBody = newOreWithBody.gameObject;
                        newOreWithBody.gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
