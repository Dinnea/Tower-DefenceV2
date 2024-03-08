using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enableble : MonoBehaviour
{
    public void EnableSwitch()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
