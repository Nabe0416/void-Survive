using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int i_id;
    public Sprite i_img;
    public string i_name;
    public string i_description;
    public int i_tier;                  //There will be 4 tier of items.
    public int i_surv_value;
    public int i_emo_value;

    public int staminaRestore;
}
