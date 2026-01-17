using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;
public class ResolutionManager : MonoBehaviour
{
    #region Field        

    public Camera camera;
    [SerializeField]
    private Transform canvasScreenSize;
    [SerializeField]
    private float perUnitSize = 100;
    [SerializeField]
    private Vector2 renderCanvasSize, tempRenderCanvasSize;
    //[SerializeField, ReadOnly]
    private float screenWidth = 0f;
    //[SerializeField, ReadOnly]
    private float screenHeight = 0f;
    //[SerializeField, ReadOnly]
    private float canvasWidth = 0f;
    //[SerializeField, ReadOnly]
    private float canvasHeight = 0f;
    private float zoomAccuracy = 0.01f;
    #endregion
    #region Property

    [SerializeField]
    private float screenLeftEdge = 0f;
    [SerializeField]
    private float screenRightEdge = 0f;
    [SerializeField]
    private float screenTopEdge = 0f;
    [SerializeField]
    private float screenBottomEdge = 0f;

    public float ScreenLeftEdge => screenLeftEdge;
    public float ScreenRightEdge => screenRightEdge;
    public float ScreenBottomEdge => screenBottomEdge;
    public float ScreenTopEdge => screenTopEdge;
    private Vector3 currentPosition, previousePosition;
    private float defaultCameraSize = 0f;
    private void GetEdge()
    {

        Vector2 bottomLeft = camera.ViewportToWorldPoint(Vector2.zero);
        screenLeftEdge = bottomLeft.x;
        screenBottomEdge = bottomLeft.y;

        Vector2 topRight = camera.ViewportToWorldPoint(new Vector2(1, 1));
        screenRightEdge = topRight.x;
        screenTopEdge = topRight.y;

    }

    private void Awake()
    {
        defaultCameraSize = camera.orthographicSize;
        currentPosition = previousePosition = transform.position;
        tempRenderCanvasSize = renderCanvasSize;
        RefreshResolution();
    }

    private void Instance_OnChangeScreenSize()
    {
        renderCanvasSize = tempRenderCanvasSize;
        camera.orthographicSize = defaultCameraSize;
        RefreshResolution();
    }


    public void RefreshResolution()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        renderCanvasSize = new Vector2(renderCanvasSize.x / perUnitSize, renderCanvasSize.y / perUnitSize);
        canvasWidth = renderCanvasSize.x;
        canvasHeight = renderCanvasSize.y;
        ZoomCameraToCanvasSize();
        GetEdge();
    }

    private void LateUpdate()
    {
        if (Time.frameCount % 5 == 0)
        {
            currentPosition = transform.position;
            if (currentPosition != previousePosition)
            {
                previousePosition = currentPosition;
                GetEdge();
            }
        }
    }

    private void ZoomCameraToCanvasSize()
    {
        Vector2 canvasTopLeft = new Vector2(canvasScreenSize.position.x - renderCanvasSize.x / 2, canvasScreenSize.position.y - renderCanvasSize.y / 2);
        if (PointIsVisibleToCamera(canvasTopLeft, camera))
            ZoomIn(canvasTopLeft, camera);
        else
            ZoomOut(canvasTopLeft, camera);

    }
    void ZoomIn(Vector2 point, Camera cam)
    {
        while (PointIsVisibleToCamera(point, cam))
            cam.orthographicSize -= zoomAccuracy;
    }

    void ZoomOut(Vector2 point, Camera cam)
    {
        while (!PointIsVisibleToCamera(point, cam))
            cam.orthographicSize += zoomAccuracy;
    }
    bool PointIsVisibleToCamera(Vector2 point, Camera cam)
    {
        if (cam.WorldToViewportPoint(point).x < 0 || cam.WorldToViewportPoint(point).x > 1 || cam.WorldToViewportPoint(point).y > 1 || cam.WorldToViewportPoint(point).y < 0)
            return false;
        return true;
    }

    public bool IsPointInScreen(Vector2 point, float offsetX = 0, float offsetY = 0)
    {
        if (point.x > screenRightEdge + offsetX || point.x < screenLeftEdge - offsetX || point.y > screenTopEdge + offsetY || point.y < screenBottomEdge - offsetY)
            return false;
        return true;
    }

    public Vector2 ScreenCentre
    {
        get
        {
            Vector2 zeroZero = new Vector2(0.5f, 0.5f);
            Vector2 zeroZeroToWorld = camera.ViewportToWorldPoint(zeroZero);
            return zeroZeroToWorld;
        }

    }
    #endregion
    #region Method
    #endregion
}