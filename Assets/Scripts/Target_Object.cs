using System.Collections;
using UnityEngine;

public class Target_Object : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TutorialManager.Instance.isArrive = true;
        }
    }
}