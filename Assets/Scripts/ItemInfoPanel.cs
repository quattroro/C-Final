using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    static bool _isActive;

    public Image itemimage;
    public Text Itemname;

    public Text StrRequire;
    public Text DexRequire;

    public Text ItemClass;

    public Text Damage;
    public Text Price;

    public Text Durability;

    static public bool PanelIsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
        }
    }


    private void Awake()
    {
    }
    
    

    public void ShowInfos(ItemNode node, Vector3 pos)
    {
        this.gameObject.SetActive(true);

        this.itemimage.sprite = node.GetComponent<Image>().sprite;
        this.Itemname.text = node.itemName;
        this.StrRequire.text = string.Format("StrRequire : {0}", node.strrequire);
        this.DexRequire.text = string.Format("DexRequire : {0}", node.dexrequire);

        this.Durability.text = string.Format("MaxDurability : {0}", node.durability);

        this.Damage.text = string.Format("Damage : {0}~{1}", node.damage[0], node.damage[1]);

        this.Price.text = string.Format("{0}G",node.price);

        this.transform.position = pos;

    }

    public void HideInfos()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideInfos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
