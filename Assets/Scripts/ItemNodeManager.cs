using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ۿ� ���� ������ csv ���Ͽ��� �޾ƿ��� ���ο� ������ ��尡 �ʿ��Ҷ����� �����ϰ� �ʱ�ȭ�ؼ� �����ش�.
public class ItemNodeManager : Singleton<ItemNodeManager>
{
    public ItemDataLoader dataloader;
    public BaseNode nodeprefab;



    public BaseNode InstantiateNode(Transform parent)
    {
        BaseNode copynode = GameObject.Instantiate<BaseNode>(nodeprefab);
        copynode.transform.parent = parent;

        int rannum = Random.Range(0, dataloader.iteminfos.Count);
        
        
        
        return copynode;
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
        }
    }
}
