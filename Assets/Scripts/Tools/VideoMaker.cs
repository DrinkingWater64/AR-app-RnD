using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Video;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEditor.XR.ARSubsystems;

public class VideoMaker : EditorWindow
{
    public GameObject prefab;
    private string newName = "";
    private Object videoObject;
    private Object imageObject;
    public ARTrackedImageManager imageManager;
    public Texture2D imageTexture;
    

    [MenuItem("Tools/Rename Video and Image")]

    static void Init() 
    {
        VideoMaker videoMakerWindow = (VideoMaker)GetWindow(typeof(VideoMaker));
        videoMakerWindow.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Prefab for your video");
        videoObject = EditorGUILayout.ObjectField("Video File:", videoObject, typeof(VideoClip), false);
        imageObject = EditorGUILayout.ObjectField("Image File:", imageObject, typeof(Texture2D), false);
        newName = EditorGUILayout.TextField("New name: "+newName);

        if (GUILayout.Button("Create"))
        {
            CreatePrefab();
        }
    }

    private void CreatePrefab()
    {
        GameObject spawnedImage = Resources.Load<GameObject>("SpawnOnImage");
        GameObject newPrefab = Instantiate(spawnedImage);

        string videoPath = EditorUtility.OpenFilePanel("Select Video", "", "mp4");
        string ImagePath = EditorUtility.OpenFilePanel("Select Video", "", "png,jpg");

        if (!string.IsNullOrEmpty(videoPath) && !string.IsNullOrEmpty(ImagePath))
        {
            Debug.Log("File selection cancelled");
            return;
        }

        File.Move(videoPath, Path.GetDirectoryName(videoPath)+"/"+newName+".mp4");
        File.Move(ImagePath, Path.GetDirectoryName(ImagePath)+"/"+newName+Path.GetExtension(ImagePath));
        

        

        VideoPlayer videoPlayer = newPrefab.GetComponent<VideoPlayer>();
        if (videoPlayer != null)
        {
            videoPlayer.clip =  (VideoClip)videoObject;
        }

        
        imageManager = FindAnyObjectByType<ARTrackedImageManager>();
        if (imageManager != null)
        {
            Debug.LogError("No ARTrackedManager is on the current scene");
            return;
        }


        
        //XRReferenceImageLibrary library = GetReferenceImageLibrary();
        //XRReferenceImage referenceImage = new XRReferenceImage();
        //referenceImage.texture = imageObject as Texture2D;
        //referenceImage.name = newName;
        //referenceImage.size = 1.0f;

        //referenceImage.name = imageName;
        //referenceImage.specifySize = true;
        //referenceImage.size = new Vector2(imageWidth, imageWidth);


        MultiImageTracker tracker = FindObjectOfType<MultiImageTracker>();
        if (tracker != null)
        {
            tracker.AddtoList(newPrefab);
        }

        Debug.Log("Files renmaed Successfullu");
    }

    XRReferenceImageLibrary GetReferenceImageLibrary()
    {
        // Get all XRReferenceImageLibrary assets in the project
        string[] guids = AssetDatabase.FindAssets("t:XRReferenceImageLibrary");

        if (guids.Length > 0)
        {
            // Assuming there is only one XRReferenceImageLibrary in the project for simplicity
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<XRReferenceImageLibrary>(assetPath);
        }

        return null;
    }

}
