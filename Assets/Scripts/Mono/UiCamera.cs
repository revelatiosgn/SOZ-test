using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCamera : MonoBehaviour
{
    private static UiCamera _instance;
    public static UiCamera Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
