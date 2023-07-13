using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiImageTracker : MonoBehaviour
{
    private int _count = 0;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _countText;
    [SerializeField]
    private GameObject[] gameObjects;
    Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _aRTrackedImageManager;


    private void Awake()
    {
        _aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (var prefab in gameObjects)
        {
            var newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            prefabList.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            if (trackedImage.trackingState.ToString().Equals("Tracking"))
            {
                UpdateImage(trackedImage);
            }
        }

        foreach (var trackedImage in args.removed)
        {
            prefabList[trackedImage.referenceImage.name].SetActive(false);
        }


    }

    private void UpdateImage(ARTrackedImage aRTrackedImage)
    {
        var name = aRTrackedImage.referenceImage.name;
        var position = aRTrackedImage.transform.position;
        _text.text = name;
        _countText.text = _aRTrackedImageManager.trackables.count.ToString();
        var currentPrefab = prefabList[name];
        currentPrefab.transform.position = position;
        currentPrefab.SetActive(true);

        foreach (var prefab in prefabList)
        {
            if (prefab.Key != name)
            {
                prefab.Value.SetActive(false);
            }
        }
    }


}
