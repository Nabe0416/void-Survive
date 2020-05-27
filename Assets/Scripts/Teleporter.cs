using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform destination;
    private Transform pc;

    [SerializeField]
    private bool mapTP = false;
    [SerializeField]
    private GameObject currMap, destMap;
    [SerializeField]
    private float staminaCost;

    private void Awake()
    {
        pc = FindObjectOfType<PC_Control>().gameObject.transform;
    }

    public void TP()
    {
        pc.position = pc.position - transform.position + destination.position;
        if(mapTP)
        {
            currMap.SetActive(false);
            destMap.SetActive(true);
        }
        FindObjectOfType<PC_Stats>().AdjustStamina(staminaCost);
    }

}
