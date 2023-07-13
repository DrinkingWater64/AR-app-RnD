using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (gameObject.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
        {
            gameObject.GetComponent<Canvas>().worldCamera = _camera;
            Debug.Log("-----------------------------------------------------------camera is set-------------------------------------------------");
        }
    }

    private void OnEnable()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (gameObject.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
        {
            gameObject.GetComponent<Canvas>().worldCamera = _camera;
            Debug.Log("-----------------------------------------------------------camera is set-------------------------------------------------");
        }
    }

    private void Update()
    {
        if (_camera != null)
        {
            Debug.Log(GetComponent<Canvas>().worldCamera.name);

        }
    }
}
