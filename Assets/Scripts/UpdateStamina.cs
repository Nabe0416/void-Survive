using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStamina : MonoBehaviour
{
    private PC_Stats pcs;
    [SerializeField]
    private Text t;
    float ratio;

    private void Awake()
    {
        pcs = FindObjectOfType<PC_Stats>();
    }

    private void Update()
    {
        var img = GetComponent<Image>();
        ratio = pcs.GetCurStamina() / pcs.GetMaxStamina();
        img.fillAmount = ratio;

        if(ratio >= 0.8f)
        {
            img.color = Color.green;
        }else if(ratio < 0.8f && ratio >= 0.3f)
        {
            img.color = Color.yellow;
        }else if(ratio < 0.3f)
        {
            img.color = Color.red;
        }

        t.GetComponent<Text>().text = Mathf.Round(pcs.GetCurStamina()).ToString();
        t.GetComponent<Text>().color = GetComponent<Image>().color;
    }
}
