using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocate : MonoBehaviour
{
    public float cameraOpt = 7.2f;
    public UI_Core uiCore;
    public float lerp = 0.8f;
    public float lerpTime = 3f;

    public UI_Core.CameraTrackingMode previousTrackingMode;
    // Start is called before the first frame update
    void Start()
    {
        uiCore = GameObject.Find("UICore").GetComponent<UI_Core>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public UI_Core.CameraTrackingMode trackingMode = UI_Core.CameraTrackingMode.locate;
    public void callCamera()
    {
        previousTrackingMode = uiCore.cameraTrackingMode;
        uiCore.cameraTrackingMode = trackingMode;

        uiCore.CameraLocateLerp(gameObject.transform.position,cameraOpt, lerp);
    }
}
