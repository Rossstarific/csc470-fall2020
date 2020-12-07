using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitNavigation : MonoBehaviour
{
    public EventSystem eventSystem;
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && (eventSystem.currentSelectedGameObject == null || !eventSystem.currentSelectedGameObject.activeSelf))
        {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
    }
}
