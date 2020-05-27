using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject environ_Inven;
    [SerializeField]
    private GameObject pc_inven;

    [SerializeField]
    private List<Sprite> uis = new List<Sprite>();

    [SerializeField]
    private List<Item> con_items;
    [SerializeField]
    private List<Item> pc_items;

    [SerializeField]
    public List<Button> con_buttons;
    [SerializeField]
    private List<Button> pc_buttons;

    [SerializeField]
    private Text t_name, t_desc, t_value;

    private int type;

    private void Awake()
    {
        pc_items = FindObjectOfType<PC_Stats>().GetPcItems();
    }

    public void OpenInven(Res_Con con)
    {
        con_items = con.GetItems();
        type = con.GetType();

        environ_Inven.GetComponent<Image>().sprite = uis[type];

        for(int i =0; i < con_items.Count; i++)
        {
            if(con_items[i].i_img != null)
            {
                con_buttons[i].GetComponent<Image>().sprite = con_items[i].i_img;
            }
        }
    }

    public GameObject GetInven()
    {
        return environ_Inven;
    }

    public void RefreshInvens()
    {
        pc_items = FindObjectOfType<PC_Stats>().GetPcItems();
        /**
        foreach(Item i in pc_items)
        {
            pc_buttons[pc_items.IndexOf(i)].GetComponent<Image>().sprite = i.i_img;
            //print(pc_items.IndexOf(i));
        }**/
        foreach(Button b in pc_buttons)
        {
            if(!(pc_buttons.IndexOf(b) + 1 > pc_items.Count))
            {
                b.GetComponent<Image>().sprite = pc_items[pc_buttons.IndexOf(b)].i_img;
            }
            else
            {
                b.GetComponent<Image>().sprite = null;
            }
        }

        con_items = FindObjectOfType<PC_Control>().preInteractObj.GetComponent<Res_Con>().GetItems();
        /**
        foreach(Item i in con_items)
        {
            con_buttons[con_items.IndexOf(i)].GetComponent<Image>().sprite = i.i_img;
        }**/
        foreach(Button b in con_buttons)
        {
            if (!(con_buttons.IndexOf(b) + 1 > con_items.Count))
            {
                b.GetComponent<Image>().sprite = con_items[con_buttons.IndexOf(b)].i_img;
            }
            else
            {
                b.GetComponent<Image>().sprite = null;
            }
        }
    }

    public void TakeItem(Button btn)
    {
        var index = con_buttons.IndexOf(btn);
        //print(index + 1 + " " + con_items.Count);
        if(index + 1 <= con_items.Count)
        {
            FindObjectOfType<PC_Stats>().PcAddItem(con_items[index]);
        }

        RefreshInvens();
    }

    public void PutItem(Button btn)
    {
        var index = pc_buttons.IndexOf(btn);
        if(index + 1 <= pc_items.Count)
        {
            FindObjectOfType<PC_Control>().preInteractObj.GetComponent<Res_Con>().AddItem(pc_items[index]);
        }

        RefreshInvens();
    }

    public void DisplayInfo(Button btn)
    {
        if(con_items.Count >= con_buttons.IndexOf(btn) + 1)
        {
            Item i = con_items[con_buttons.IndexOf(btn)];
            t_name.text = i.i_name;
            t_desc.text = i.i_description;
            t_value.text = "Survival value: " + i.i_surv_value.ToString();
        }
    }

    public void CleanDisplay()
    {
        t_name.text = "Waiting input...";
        t_desc.text = "Hover on items.";
        t_value.text = "Survival value: Unknown";
    }
}
