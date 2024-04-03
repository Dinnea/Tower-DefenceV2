using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus<BuildingSwitchedEvent>;

public class Enableble : MonoBehaviour
{
    public void EnableSwitch()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void DisableText()
    {
        gameObject.SetActive(false);
    }
    public void DisableText(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        DisableText();
    }
    private void OnEnable()
    {
        EventBus<BuildingSwitchedEvent>.OnEvent += DisableText;
    }
    private void OnDisable()
    {
        EventBus<BuildingSwitchedEvent>.OnEvent -= DisableText;
    }
}
