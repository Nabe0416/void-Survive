using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Res_Gen : MonoBehaviour
{
    [SerializeField]
    private List<Item> surv_items_samp, emo_items_samp = new List<Item>();

    [SerializeField]
    private List<Item> surv_items_tier1, surv_items_tier2, surv_items_tier3, surv_items_tier4 = new List<Item>();

    [SerializeField]
    private int tier1_perc, tier2_perc, tier3_perc, tier4_perc;

    [SerializeField]
    private int sum_surv_value, curr_surv_value;

    [SerializeField]
    private List<Item> final_item_list = new List<Item>();

    [SerializeField]
    private Res_Con[] containers;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        ResourceGeneration();
    }

    public void ResourceGeneration()
    {
        SortItemsByTier();
        curr_surv_value = sum_surv_value;
        GenSurvItems();
        GetAllContainers();
        FillContainer();
    }

    private void GenSurvItems()
    {
        int r;
        Item i;
        r = UnityEngine.Random.Range(0, tier1_perc + tier2_perc + tier3_perc + tier4_perc);

        if(curr_surv_value > 0)
        {
            if (r < tier1_perc)
            {
                i = surv_items_tier1[UnityEngine.Random.Range(0, surv_items_tier1.Count)];
                curr_surv_value -= i.i_surv_value;
                final_item_list.Add(i);

                //print("Tier1: " + r + " " + i.i_name);
            }
            else if (r < tier1_perc + tier2_perc && r >= tier1_perc)
            {
                i = surv_items_tier2[UnityEngine.Random.Range(0, surv_items_tier2.Count)];
                curr_surv_value -= i.i_surv_value;
                final_item_list.Add(i);

                //print("Tier2: " + r + " " + i.i_name);
            }
            else if (r < tier1_perc + tier2_perc + tier3_perc && r >= tier1_perc + tier2_perc)
            {
                i = surv_items_tier3[UnityEngine.Random.Range(0, surv_items_tier3.Count)];
                curr_surv_value -= i.i_surv_value;
                final_item_list.Add(i);

                //print("Tier3: " + r + " " + i.i_name);
            }
            else if (r >= tier1_perc + tier2_perc + tier3_perc)
            {
                i = surv_items_tier4[UnityEngine.Random.Range(0, surv_items_tier4.Count)];
                curr_surv_value -= i.i_surv_value;
                final_item_list.Add(i);

                //print("Tier4: " + r + " " + i.i_name);
            }
            else
            {
                i = null;

                print("Result was " + r +". No item was added to the final items list.");
                GenSurvItems();
            }

            if (curr_surv_value > 0)
                GenSurvItems();
        }

    }

    private void SortItemsByTier()
    {
        foreach(Item i in surv_items_samp)
        {
            switch(i.i_tier)
            {
                case 1:
                    surv_items_tier1.Add(i);
                    break;
                case 2:
                    surv_items_tier2.Add(i);
                    break;
                case 3:
                    surv_items_tier3.Add(i);
                    break;
                case 4:
                    surv_items_tier4.Add(i);
                    break;
            }
        }
    }

    private void GetAllContainers()
    {
        containers = FindObjectsOfType<Res_Con>();
    }

    private void FillContainer()
    {
        while(final_item_list.Count > 0)
        {
            int r = UnityEngine.Random.Range(0, containers.Length);
            if (final_item_list.Count != 0)
            {
                if (containers[r].AddItemToCon(final_item_list[0]))
                {
                    final_item_list.RemoveAt(0);
                }
                if (CheckIfConFull())
                    break;
            }
        }
    }

    private bool CheckIfConFull()
    {
        foreach(Res_Con rc in containers)
        {
            if (!rc.GetIfFull())
            {
                return false;
            }
        }
            return true;
        }
}
