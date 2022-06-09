using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataLoader : Singleton<ItemDataLoader>
{
    public List<string> originalstr = null;
    public string FilePath;

    //public List<BaseNode> itemnodes = new List<BaseNode>();
    public Sprite[] sprites = null;

    public Dictionary<int, Dictionary<int, string>> iteminfos;

    public Dictionary<int,Dictionary<int, string>> ItemFileOpen(string filepath)
    {
        originalstr.Clear();
        Dictionary<int, Dictionary<int, string>> data = new Dictionary<int, Dictionary<int, string>>();
        string FilePath = filepath;
        Debug.Log($"road {FilePath}");
       // classname = null;

        //if (OpenDialog.ShowDialog() == DialogResult.OK)
        //{
        //    if ((openStream = OpenDialog.OpenFile()) != null)
        //    {
        //        //return OpenDialog.FileName;
        //        FilePath = OpenDialog.FileName;
        //    }
        //}
        if (FilePath == null)
        {
            Debug.Log("여기서 나감1");
            return null;
        }


        FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

        StreamReader sr = new StreamReader(fs);

        while (true)
        {
            string str = sr.ReadLine();
            originalstr.Add(str);

            if (str == null || str.Length == 0)
            {
                break;
            }
            char[] temp = new char[str.Length];
            bool flag = false;

            Dictionary<int, string> columsdic = new Dictionary<int, string>();
            int Dicindex = 0;
            int index = 0;
            //string temp;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '"'&&flag ==false)
                {
                    flag = true;//여는중괄호가 나오면 닫는중괄호가 나올때까지 앞으로 나오는 , 는 그냥 집어넣는다.  
                    continue;
                }
                else if (str[i] == ',')//,가 나오면 해당 인덱스에는 , string으로 변환해서 columsdic 에 넣어준다.
                {
                    if (!flag)
                    {
                        temp[index] = '\0';
                        string tt = string.Join("", temp);//
                        columsdic.Add(Dicindex++, tt.Split('\0')[0]);//그냥 new string() 생성자를 이용하면 char배열의 크기만큼 뒤에 \0으로 초기화된 문자열이 만들어져버린다.

                        
                        temp = new char[str.Length];
                        index = 0;
                        continue;
                    }
                }
                else if (i == str.Length - 1)//받아온 한 줄의 마지막 문자일때도 쉼표과 같은 동작을 해준다.
                {
                    if (str[i] != '"')//만약 마지막이 " 로 끝났을때는 "문자는 넣어주지 않는다.
                        temp[index++] = str[i];

                    temp[index] = '\0';
                    string tt = string.Join("", temp);
                    columsdic.Add(Dicindex++, tt.Split('\0')[0]);//그냥 new string() 생성자를 이용하면 char배열의 크기만큼 뒤에 \0으로 초기화된 문자열이 만들어져버린다.

                    temp = new char[str.Length];
                    index = 0;
                    break;
                }
                else if (str[i] == '"'&&flag==true)
                {
                    flag = false;
                    continue;
                }


                temp[index++] = str[i];

            }


            int code = 0;

            if (int.TryParse(columsdic[0], out code))//첫번째가 숫자가 아니면 해당 행은 열제목 행이다. 리스트에 넣지 않는다.
            {
                //Debug.Log($"{code}번 아이템 삽입");
                //Debug.Log(columsdic[1]+"/" + columsdic[2]);
                //Debug.Log(columsdic[2]);
                data.Add(code, columsdic);
            }
            else
            {
                //classname = columsdic[0].Split('_')[0];
            }

        }
        int a = 10;
        //Debug.Log("여기서 나감2");
        iteminfos = data;
        return data;
    }

    //불러와진 아이템 정보들을 이용해 아이템 노드를 만들어 준다.
    //csv 파일의 칼럼의 순서와 EnumTypes.ItemCollums 의 순서가 같아야 한다.
    //public void ItemInfo_Road(string classname)
    //{
    //    //string filepath = UnityEngine.Application.persistentDataPath + $"/{classname}_Relation.csv";
    //    Debug.Log(FilePath);
    //    string temppath = Application.streamingAssetsPath + FilePath;//런타임중 읽기만 가능
    //    Debug.Log("파일위치??" + temppath);
    //    List<Dictionary<int, string>> datalist = ItemFileOpen(temppath, out classname);
    //    //nodeobj.gameObject.SetActive(true);
    //    int ItemCode;
    //    string ItemName;
    //    string SpriteName;
    //    int SpriteIndex;
    //    string type;


    //    ItemNode itemNode = Resources.Load<ItemNode>("Prefabs/ItemNode");
    //    int yval = 0;
    //    int xval = 0;

    //    for (int i=0;i<datalist.Count;i++)
    //    {
    //        xval = i % 5;
    //        if(xval==0)
    //        {
    //            yval++;
    //        }

    //        int.TryParse(datalist[i][(int)EnumTypes.ItemCollums.ItemCode], out ItemCode);
    //        ItemName = datalist[i][(int)EnumTypes.ItemCollums.ItemName];
    //        SpriteName = datalist[i][(int)EnumTypes.ItemCollums.ResourceName];
    //        int.TryParse(datalist[i][(int)EnumTypes.ItemCollums.ResourceIndex], out SpriteIndex);
    //        type = datalist[i][(int)EnumTypes.ItemCollums.ItemType];


    //        ItemNode copyobj = GameObject.Instantiate<ItemNode>(itemNode);
    //        copyobj.transform.parent = this.transform;
    //        sprites = Resources.LoadAll<Sprite>($"Sprites/{SpriteName}");
    //        //Debug.Log($"{SpriteName} road");
    //        //Debug.Log($"arr len=> {sprites.Length}");

    //        copyobj.InitSetting(ItemCode, ItemName, sprites[SpriteIndex], new Vector3(80 + xval * 110, -1 * yval * 85), type);


    //        itemnodes.Add(copyobj);
    //    }





    //    //List<SkillNode> parentlist;
    //    //List<SkillNode> childlist;

    //    //List<SkillNode> tempnodelist = new List<SkillNode>();

    //    //for (int i = 0; i < datalist.Count; i++)
    //    //{
    //    //    string[] indexstr = datalist[i][(int)EnumTypes.SkillTreeColums.Index].Split(',');
    //    //    int indexX = -1;
    //    //    int indexY = -1;
    //    //    int.TryParse(indexstr[0], out indexX);
    //    //    int.TryParse(indexstr[1], out indexY);
    //    //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.SkillDamage], out damage);
    //    //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.ReauireLevel], out level);
    //    //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.SkillRank], out rank);
    //    //    skillname = datalist[i][(int)EnumTypes.SkillTreeColums.SkillName];
    //    //    explain = datalist[i][(int)EnumTypes.SkillTreeColums.SkillExplain];


    //    //    SkillNode copyobj = GameObject.Instantiate<SkillNode>(nodeobj);
    //    //    copyobj.name = skillname;
    //    //    //copyobj.transform.parent = SkillNodeObject.transform.parent;
    //    //    copyobj.Init(classname, skillname, rank, damage, level, explain);
    //    //    copyobj.P_NodeActive = false;
    //    //    tempnodelist.Add(copyobj);
    //    //    skillnodes.Add(copyobj);

    //    //    panel.SetTreeNode(new Vector2Int(indexX, indexY), copyobj);//만들어준 슼킬 노드를 바로 슬롯에 장착시켜준다.

    //    //}

    //    //노드들을 다 만들어 준 다음에 연결정보를 읽어와서 노드들을 연결해준다.
    //    //for (int i = 0; i < datalist.Count; i++)
    //    //{
    //    //    ParseRelation(tempnodelist, tempnodelist[i], datalist[i][(int)EnumTypes.SkillTreeColums.Child]);
    //    //}

    //    //if (skillslots == null)
    //    //    skillslots = panel.GetSkillSlotList;

    //    //for (int y = 0; y < skillslots.GetLength(0); y++)
    //    //{
    //    //    for (int x = 0; x < skillslots.GetLength(1); x++)
    //    //    {
    //    //        if (skillslots[y, x].SetNode == null)
    //    //        {
    //    //            skillslots[y, x].gameObject.SetActive(false);
    //    //        }
    //    //    }
    //    //}

    //    //nodeobj.gameObject.SetActive(false);
    //    //for(int i=0;i<skillslots)
    //}


    string temp;
    void Start()
    {
        string temppath = Application.streamingAssetsPath + FilePath;//런타임중 읽기만 가능
        ItemFileOpen(temppath);
        //Debug.Log(iteminfos[1001][(int)EnumTypes.ItemCollums.Damage]);
        //ItemInfo_Road(FilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
