using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Res_Con : MonoBehaviour
{
    [SerializeField]
    private List<Item> con_items = new List<Item>(3);
    [SerializeField]
    private bool opened = false;
    public bool isOpened = false;
    [SerializeField]
    private GameObject open;

    private InteractionManager im;
    private PC_Control pcc;
    

    [SerializeField]
    private int type; //0 = Cabinet, 1 = Fridge, 2 = Drawer

    private void Awake()
    {
        #region Refs.
        im = FindObjectOfType<InteractionManager>();
        pcc = FindObjectOfType<PC_Control>();
        #endregion
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpened)
            {
                Close_Con();
            }
        }
    }

    public bool AddItemToCon(Item i)
    {
        if(con_items.Count < 3)
        {
            con_items.Add(i);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIfFull()
    {
        if(con_items.Count == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Open_Con()
    {
        opened = true;

        print("Container Opened");
        foreach (Item i in con_items)
        {
            print(i.i_name);
        }

        im.GetInven().SetActive(true);
        im.OpenInven(this);

        pcc.SetAbleToMove(false);
        isOpened = true;
    }

    private void Close_Con()
    {
        im.GetInven().SetActive(false);
        foreach(Button btn in im.con_buttons)
        {
            btn.GetComponent<Image>().sprite = null;
        }
        pcc.SetAbleToMove(true);
        isOpened = false;
    }

    public bool GetIfOpened()
    {
        return opened;
    }

    public List<Item> GetItems()
    {
        return con_items;
    }

    public void RemoveItem(Item item)
    {
        if(con_items.Contains(item))
        {
            con_items.Remove(item);
        }
    }

    public void AddItem(Item item)
    {
        if(con_items.Count < 3)
        {
            con_items.Add(item);
            FindObjectOfType<PC_Stats>().PcRemoveItem(item);
        }
    }

    public int GetType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PC_Control>())
        {
            open.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PC_Control>())
        {
            open.SetActive(false);
        }
    }

    private void OnDisable()
    {
        con_items.Clear();
    }


}
