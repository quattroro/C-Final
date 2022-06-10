using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템에 대한 정보를 csv 파일에서 받아오고 새로운 아이템 노드가 필요할때마다 생성하고 초기화해서 돌려준다.
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
          //아이템을 하나 무작위로 생성해서 아이템가방에 넣어준다.  
        }
    }
}
