using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlink : MonoBehaviour
{
    public void OpenUrl(string url)
    {
        // UnityEngine.Application : Access to application run-time data
        Application.OpenURL(url);
    }
}
