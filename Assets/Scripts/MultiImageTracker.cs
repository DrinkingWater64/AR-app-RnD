using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// This component helps with tracking multiple images and change them in runtime
/// </summary>
public class MultiImageTracker : MonoBehaviour
{
    #region Fields

    private int _count = 0;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _countText;
    [SerializeField]
    private GameObject[] _PrefabsToSpawn;
    Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _aRTrackedImageManager;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        InitPrefabs();
    }

    public void OnEnable()
    {
        _aRTrackedImageManager.trackedImagesChanged += OnImageChange;
    }

    public void OnDisable()
    {
        _aRTrackedImageManager.trackedImagesChanged -= OnImageChange;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// For every prefab in <see cref="_PrefabsToSpawn"/> is instantiated and disabled at start. These new objects are renamed to the Prefab name.
    /// Then they are added to the dictionary <see cref="prefabList"/> to search the by name
    /// This is called at Awake()
    /// </summary>
    private void InitPrefabs()
    {
        foreach (var prefab in _PrefabsToSpawn)
        {
            var newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            prefabList.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }

    /// <summary>
    /// Handles changes of tracked images and updates accordingly when a image is added and updated.    
    /// <see cref="UpdateImage(ARTrackedImage)"/> helps to act accordingly to the changes
    /// </summary>
    /// <param name="args"> Event arguments for the <see cref="ARTrackedImageManager.trackedImagesChanged"/> event.</param>
    private void OnImageChange(ARTrackedImagesChangedEventArgs args)
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

    /// <summary>
    /// Takes the name of currently tracked image and it's position in the world. Then finds the object of that name in <see cref="prefabList"/>, sets its postion to the position of the tracked image and then activates the object.
    /// Then loops through all the other objects in the list and deactivate  them so there's no other objects other the new one.
    /// </summary>
    /// <param name="aRTrackedImage"> Represents a tracked image in the physical environment. </param>
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

    #endregion
}
