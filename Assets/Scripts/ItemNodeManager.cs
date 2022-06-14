using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ۿ� ���� ������ csv ���Ͽ��� �޾ƿ��� ���ο� ������ ��尡 �ʿ��Ҷ����� �����ϰ� �ʱ�ȭ�ؼ� �����ش�.
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
            //�������� �ϳ� �������� �����ؼ� �����۰��濡 �־��ش�.  
            Debug.Log("������ ����");
            BaseNode copynode = InstantiateNode(1001, this.transform);
            ItemBag.Instance.InsertItem(copynode);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //�������� �ϳ� �������� �����ؼ� �����۰��濡 �־��ش�.  
            Debug.Log("������ ����");
            BaseNode copynode = InstantiateNode(2001, this.transform);
            ItemBag.Instance.InsertItem(copynode);
        }
    }
}
