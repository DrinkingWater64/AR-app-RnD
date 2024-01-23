using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidePlayerPrompt : MonoBehaviour
{
    [SerializeField] GameObject videoPlayer;

    /// <summary>
    /// Activates the VideoPlayerPromt Prefab or Object. This method is used by a unity button.
    /// </summary>
    public void PlayVideoOnScreen()
    {
        Debug.Log("----------------------------------------"+videoPlayer);
        videoPlayer.SetActive(true);
    }
}
