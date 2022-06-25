using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCameraElement : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + UiCamera.Instance.transform.forward);
    }
}
