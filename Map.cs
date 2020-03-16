using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Map : MonoBehaviour {
    public GameObject BOX;
    public GameObject TARGET;
    public GameObject HERO;
    public GameObject WALL;
    public GameObject AGGTARGET;
    const int wall = 1, hero = 2, box = 3, target = 4, aggtarget = 7;
    //人物坐标
    int HeroPosX = 0, HeroPosY = 0;
    //偏移量
    int OffsetX = 0, OffsetY = 0;
    //步数
    int StepsNum = 0;
    //走过的地图
    int[,] copymap;

    //UI界面
    public Text steps;
    public KeyCode keycode; 
    int[,] beginmap=new int[10, 10]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,4,0,0,0,1},
        {1,0,0,0,0,3,0,0,0,1},
        {1,0,0,4,3,2,3,4,0,1},
        {1,0,0,0,0,3,0,0,0,1},
        {1,0,0,0,0,4,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1},
    };

    int[,] map = new int[10, 10]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,4,0,0,0,1},
        {1,0,0,0,0,3,0,0,0,1},
        {1,0,0,4,3,2,3,4,0,1},
        {1,0,0,0,0,3,0,0,0,1},
        {1,0,0,0,0,4,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1},
    };

    Stack<int[,]> _map = new Stack<int[,]>();

	// Use this for initialization
	void Start () {
        GetPos();
        GameObject.Find("Passmeun").transform.Find("GameObject").gameObject.SetActive(false);
	}
	
	// Update is called once per frame
    void Update()
    {
        darw();
        Move();
        if(Input.GetKeyDown(keycode))
        {
            Rollback();
        }
        steps.text = "步数:"+StepsNum.ToString();
        //Back();
        if(Victory())
        {
            Time.timeScale = 0f;
            GameObject.Find("Passmeun").transform.Find("GameObject").gameObject.SetActive(true);
            //destroymap();
        }
	}

    void darw()
    {
        //销毁旧地图
        destroymap();
        for(int i=0;i<10;++i)
        {
            for(int j=0;j<10;++j)
            {
                //print(i + "," + j);
                switch(map[i,j])
                {
                    case 1:
                        Instantiate(WALL,
                             new Vector3(j - 4.5f, 0, 4.5f - i),
                            Quaternion.identity);
                        break;
                    case 2:case 6:
                        Instantiate(HERO,
                             new Vector3(j - 4.5f, 0, 4.5f - i),
                            Quaternion.Euler(0,180,0));
                        break;
                    case 3:
                        Instantiate(BOX,
                             new Vector3(j - 4.5f, 0, 4.5f - i),
                            Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(TARGET,
                             new Vector3(j - 4.5f, 0, 4.5f - i),
                            Quaternion.identity);
                        break;
                    case 7:
                        Instantiate(AGGTARGET,
                             new Vector3(j - 4.5f, 0, 4.5f - i),
                            Quaternion.identity);
                        break;
                }
            }
        }
    }

    void destroymap()
    {
        GameObject[] gos =
            GameObject.FindGameObjectsWithTag("Cube");
        foreach(var g in gos)
        {
            Destroy(g);
        }
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OffsetX = -1;
            OffsetY = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OffsetX = 1;
            OffsetY = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OffsetX = 0;
            OffsetY = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OffsetX = 0;
            OffsetY = 1;
        }

        if (map[HeroPosX + OffsetX, HeroPosY + OffsetY] == 0 || map[HeroPosX + OffsetX, HeroPosY + OffsetY] == target)
        {
            copymap = new int[10, 10];
            for (int i = 0; i < 10;++i )
            {
                for(int j=0;j<10;++j)
                {
                    copymap[i,j] = map[i,j];
                }
            }
            _map.Push(copymap);

            map[HeroPosX, HeroPosY] -= hero;
            map[HeroPosX + OffsetX, HeroPosY + OffsetY] += hero;
            HeroPosX += OffsetX;
            HeroPosY += OffsetY;
            ++StepsNum;
        }
        else if(map[HeroPosX + OffsetX, HeroPosY + OffsetY] == box || map[HeroPosX + OffsetX, HeroPosY + OffsetY] == aggtarget)
        {
            if(map[HeroPosX + 2*OffsetX, HeroPosY + 2*OffsetY] == 0 || map[HeroPosX + 2*OffsetX, HeroPosY + 2*OffsetY] == target)
            {
                copymap = new int[10, 10];
                for (int i = 0; i < 10; ++i)
                {
                    for (int j = 0; j < 10; ++j)
                    {
                        copymap[i, j] = map[i, j];
                    }
                }
                _map.Push(copymap);

                map[HeroPosX, HeroPosY] -= hero;
                map[HeroPosX + OffsetX, HeroPosY + OffsetY] += hero;
                map[HeroPosX + OffsetX, HeroPosY + OffsetY] -= box;
                map[HeroPosX + 2*OffsetX, HeroPosY + 2*OffsetY] += box;
                HeroPosX += OffsetX;
                HeroPosY += OffsetY;
                ++StepsNum;
            }
        }
        OffsetX = 0;
        OffsetY = 0;
    }

    void GetPos()
    {
        for (int i = 0; i < 10; ++i) 
        {
             for (int j = 0; j < 10; ++j)
             {
                 if (hero == map[i,j] || hero + target == map[i,j]) 
                 {
                     HeroPosX = i;
                     HeroPosY = j;
                     return;
                 }
             }
        }
    }

    bool Victory()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                if (box == map[i,j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void JumpMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Rollback()
    {
        if (StepsNum > 0)
        {
            map = _map.Peek();
            _map.Pop();
            --StepsNum;
        }
        GetPos();
    }

    public void Init()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                map[i, j] = beginmap[i, j];
            }
        }
        GetPos();
        StepsNum = 0;
        while (_map != null)
        {
            _map.Pop();
        }
    }
}
