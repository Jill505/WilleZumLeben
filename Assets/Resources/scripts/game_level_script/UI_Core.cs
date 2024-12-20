using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Core : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject John;
    public GameObject May;
    public Camera theCameraComponent;

    public Vector2 trackingTarget;
    public float orthographic = 7.2f;
    public Vector2 trackingVariable = new Vector2(1,1);
    public float trackingDelayDistance = 0.8f;

    public float trackingDelay;
    public float trackingFluence = 0.5f;

    public float cameraFunc_defultShakeTime = 0.1f;
    public float cameraFunc_defultShakeStrength = 1.0f;

    public enum CameraTrackingMode
    {
        followJohn,
        followMay,
        Middle,
        locate
    }
    public CameraTrackingMode cameraTrackingMode = CameraTrackingMode.followJohn;

    void Awake()
    {
        //selfCamera = GetComponent<Camera>();
        DefaultRect = selfCamera.rect;
        shakeFrameTime = 1f / shakeFps;
        theCameraComponent = theCamera.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theCameraComponent.orthographicSize = orthographic;

        Reset();
        if (cameraTrackingMode == CameraTrackingMode.followJohn)
        {
            trackingTarget = John.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 点击测试
        if (Input.GetMouseButtonDown(0))
        {
            //shake(); 
        }

        switch (cameraTrackingMode)
        {
            case (CameraTrackingMode.followJohn):
                trackingTarget = John.transform.position;
                break;
        }

        if (isshakeCamera)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Reset();
                    selfCamera.rect = DefaultRect;
                }
                else
                {
                    frameTime += Time.deltaTime;
                    if (frameTime >= shakeFrameTime)
                    {
                        frameTime -= shakeFrameTime;
                        changeRect.x = DefaultRect.x + shakeDelta * (shakeLevel * (UnityEngine.Random.value - 0.5f));
                        changeRect.y = DefaultRect.y + shakeDelta * (shakeLevel * (UnityEngine.Random.value - 0.5f));

                        selfCamera.rect = changeRect;
                    }
                }
            }
        }
    }
    private void LateUpdate()
    {
        Vector2 calVector2 = new Vector2(Mathf.Abs(gameObject.transform.position.x - trackingTarget.x), Mathf.Abs(gameObject.transform.position.y - trackingTarget.y));
        /*    if (Vector2.Distance(calVector2, trackingVariable) > 0)
        {

        }*/
        //    if (Vector2.Distance(gameObject.transform.position, trackingTarget.transform.position) > trackingDelayDistance)
        //{
        //  gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, trackingTarget.transform.position, trackingFluence);
        //}
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, trackingTarget, trackingFluence);
    }

    static public void CameraFunc_shake()
    {

    }
    static public void CameraFunc_shake(float strength)
    {

    }
    static public void CameraFunc_shake(float strength, float time)
    {

    }

    //==================================================================
    //從CSDN幹來的代碼
    //原文連結：https://blog.csdn.net/yjy99yjy999/article/details/121052210

    // 震动标志位
    [SerializeField] bool isshakeCamera = false;
    // 震动幅度
    public float shakeLevel = 3f;
    // 震动时间
    public float ShakeTime = 0.15f;
    // 震动的帧率
    public float shakeFps = 45f;

    [SerializeField] private float shakeFrameTime;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float frameTime = 0.0f;
    [SerializeField] private float shakeDelta = 0.005f;
    public Camera selfCamera;
    [SerializeField] private Rect DefaultRect;
    [SerializeField]private Rect changeRect;


    void Reset()
    {
        isshakeCamera = false;

        timer = ShakeTime;
        frameTime = shakeFrameTime;
        changeRect = DefaultRect;
    }

    public void shake()
    {
        isshakeCamera = true;
    }

    //==================================================================
    public void JohnCameraLocateLerp()
    {
        CameraLocateLerp(orthographic,0.8f);
        cameraTrackingMode = CameraTrackingMode.followJohn;
    }
    public void CameraLocateLerp( float optSize, float lerp)
    {
        //theCameraComponent.orthographicSize = Mathf.Lerp(theCameraComponent.orthographicSize, optSize, lerp);
        StartCoroutine(CameraLocateLerpCoroutine(optSize, lerp));
    }
    public void CameraLocateLerp(Vector2 pos, float optSize,float lerp)
    {
        //theCameraComponent.orthographicSize = Mathf.Lerp(theCameraComponent.orthographicSize, optSize, lerp);
        trackingTarget = pos;
        StartCoroutine(CameraLocateLerpCoroutine(optSize,lerp));
    }
    IEnumerator CameraLocateLerpCoroutine(float optSize, float lerp)
    {
        float counter =0;
        while (counter < 3)
        {
            theCameraComponent.orthographicSize = Mathf.LerpUnclamped(theCameraComponent.orthographicSize, optSize, lerp);
            counter += Time.deltaTime;

            //Switch Update mode and FixedUpdate mode
            //yield return 0;
            yield return new WaitForSeconds(Time.fixedDeltaTime);

        }
        yield return 0;
    }
}
