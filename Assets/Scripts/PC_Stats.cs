using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Stats : MonoBehaviour
{
    [SerializeField]
    private float max_stamina = 100;
    [SerializeField]
    private float curr_stamina;

    [SerializeField]
    private float moving_cost_ratio = 0.5f;

    [SerializeField]
    private bool atHome = true;

    [SerializeField]
    private List<Item> pc_items = new List<Item>(5);

    private InteractionManager im;

    [SerializeField]
    private GameObject shelter;
    private void Update()
    {
        if(shelter.activeSelf)
        {
            atHome = true;
        }
        else
        {
            atHome = false;
        }
    }

    private void Awake()
    {
        im = FindObjectOfType<InteractionManager>();
        Init_stamina();
    }

    private void LateUpdate()
    {
        MovingCost();
    }

    private void Init_stamina()
    {
        curr_stamina = max_stamina;
    }

    private void MovingCost()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            if(!atHome)
            {
                curr_stamina -= moving_cost_ratio * Time.deltaTime;
            }
        }
    }

    public void InteractionCost(float amount)
    {
        if(!atHome)
        {
            curr_stamina -= (amount + 0.2f * Random.Range(-amount, amount));
        }
    }

    public float GetCurStamina()
    {
        return curr_stamina;
    }

    public float GetMaxStamina()
    {
        return max_stamina;
    }

    public void AdjustStamina(float f)
    {
        curr_stamina += f;
    }

    public void PcAddItem(Item i)
    {
        if (pc_items.Count < 5)
        {
            pc_items.Add(i);
            FindObjectOfType<PC_Control>().preInteractObj.GetComponent<Res_Con>().RemoveItem(i);
        }
    }

    public void PcRemoveItem(Item i)
    {
        if(pc_items.Contains(i))
        {
            pc_items.Remove(i);
        }
    }

    public List<Item> GetPcItems()
    {
        return pc_items;
    }
}
