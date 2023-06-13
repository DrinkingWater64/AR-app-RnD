using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ImageRecognationXR : MonoBehaviour
{
    private int _count = 0;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _countText;
    
    private ARTrackedImageManager _aRTrackedImageManager;
    private List<GameObject> _screens = new List<GameObject>();

    private void Awake()
    {
        _aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _aRTrackedImageManager.trackedImagesChanged += OnImageChange;
    }

    public void OnDisable()
    {
        _aRTrackedImageManager.trackedImagesChanged -= OnImageChange;
    }

    public void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            _text.text = trackedImage.name.ToString();
        }
        foreach (var trackedImage in args.updated)
        {
            if (trackedImage.trackingState.ToString().Equals("Limited") || trackedImage.trackingState.ToString().Equals("None"))
            {
                //_text.text = trackedImage.trackingState.ToString();
                _aRTrackedImageManager.trackedImagePrefab.SetActive(false);
            }
        }


        if (_count.Equals(0))
        {
            Destroy(_aRTrackedImageManager.trackedImagePrefab);
        }
    }

    private void Update()
    {
        _countText.text = _aRTrackedImageManager.trackables.count.ToString();
    }

}
