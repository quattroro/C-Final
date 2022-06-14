using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템에 대한 정보를 csv 파일에서 받아오고 새로운 아이템 노드가 필요할때마다 생성하고 초기화해서 돌려준다.
public class ItemNodeManager : Singleton<ItemNodeManager>
{
    public ItemDataLoader dataloader;
    public BaseNode nodeprefab;
    public Sprite[] itemsprites;
    public ItemInfoPanel infoPanel;

    public BaseNode InstantiateNode(int itemcode, Transform parent)
    {
        Dictionary<int, string> data = dataloader.iteminfos[itemcode];

        BaseNode copynode = GameObject.Instantiate<BaseNode>(nodeprefab);
        copynode.transform.parent = parent;

        int rannum = Random.Range(0, dataloader.iteminfos.Count);



        int code;
        int.TryParse(data[(int)EnumTypes.ItemCollums.ItemCode], out code);
        string name = data[(int)EnumTypes.ItemCollums.Name];
        int spritenum;
        int.TryParse(data[(int)EnumTypes.ItemCollums.SpriteNum], out spritenum);
        string category = data[(int)EnumTypes.ItemCollums.Category];

        string parts = data[(int)EnumTypes.ItemCollums.Parts];


        copynode.InitSetting(data, itemsprites[spritenum]);
        
        
        return copynode;
    }

    private void Awake()
    {
        itemsprites = Resources.LoadAll<Sprite>("Sprites/items");
        infoPanel = GetComponentInChildren<ItemInfoPanel>();
        infoPanel.gameObject.SetActive(false);
    }

    public ItemInfoPanel getInfoPanel()
    {
        if (infoPanel.gameObject.activeSelf == false)
            infoPanel.gameObject.SetActive(true);
        return infoPanel;
    }

    // Start is called before the first frame update
    void Start()
    {
        dataloader = GetComponent<ItemDataLoader>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        if(Input.GetKeyDown(KeyCode.A))
        {
            //아이템을 하나 무작위로 생성해서 아이템가방에 넣어준다.  
            Debug.Log("아이템 생성");
            BaseNode copynode = InstantiateNode(1001, this.transform);
            ItemBag.Instance.InsertItem(copynode);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //아이템을 하나 무작위로 생성해서 아이템가방에 넣어준다.  
            Debug.Log("아이템 생성");
            BaseNode copynode = InstantiateNode(2001, this.transform);
            ItemBag.Instance.InsertItem(copynode);
        }
    }
}
