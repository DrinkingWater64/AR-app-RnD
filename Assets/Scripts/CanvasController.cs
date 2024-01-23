using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is attached to a Canvas object. This makes the canvas into a world space canvas and sets the current camera as the event camera
/// </summary>
public class CanvasController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Camera _camera;

    #endregion

    #region Unity Methods

    private void Start()
    {
        SetCameraForCanvas();
    }

    private void OnEnable()
    {
        //this.gameObject.SetActive(true);
        SetCameraForCanvas();
    }

    private void Update()
    {
        //if camera not set properly for the world canvas
        if (_camera != null)
        {
            //Debug.Log(GetComponent<Canvas>().worldCamera.name);
        }
    }

    #endregion
    /// <summary>
    /// Deactivates this game object which is the canvas. 
    /// </summary>
    public void CloseSelf()
    {
        gameObject.SetActive(false);
    }


    #region Private Methods

    /// <summary>
    /// Finds the main camera of scene and set it as the event camera for the canvas object
    /// </summary>
    private void SetCameraForCanvas()
    {
        if (gameObject.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            gameObject.GetComponent<Canvas>().worldCamera = _camera;
            Debug.Log("-----------------------------------------------------------camera is set for worldSpace-------------------------------------------------");
        }
        else if (gameObject.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            gameObject.GetComponent<Canvas>().worldCamera = _camera;
            Debug.Log("-----------------------------------------------------------camera is set for screenSpaceCamera-------------------------------------------------");
        }
    }

    #endregion
}
