using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPInstru : MonoBehaviour
{
    [SerializeField]
    private GameObject instru;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PC_Control>())
        {
            instru.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PC_Control>())
        {
            instru.SetActive(false);
        }
    }
}
