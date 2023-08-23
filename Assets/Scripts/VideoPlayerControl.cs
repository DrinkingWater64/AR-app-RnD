using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

/// <summary>
/// This component provides controls for a VideoPlayer object.
/// </summary>
public class VideoPlayerControl : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Slider _slider;

    private long totalFrame;
    private bool isPaused = false;
    private bool isDragging;
    private float lastValue = 0;

    #endregion

    #region Unity Events

    private void OnEnable()
    {
        GotoFrameAtPercent(lastValue);
        Debug.Log("Enabled on " + lastValue);
    }

    private void OnDisable()
    {
        lastValue = _slider.value;
        Debug.Log("Disabled");
    }

    void Start()
    {
        totalFrame = (long)_videoPlayer.frameCount;
        InitializeSliderEventTriggers();
    }

    void Update()
    {
        UpdateVideoPlayback();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Pauses or resumes the video playback.
    /// </summary>
    public void PauseOrResumeVideo()
    {
        if (!_videoPlayer.isPaused)
        {
            _videoPlayer.Pause();
            isPaused = true;
        }
        else
        {
            _videoPlayer.Play();
            isPaused = false;
        }
        Debug.Log("Pausing/Resuming");
    }

    /// <summary>
    /// Resumes the video playback.
    /// </summary>
    public void ResumeVideo()
    {
        _videoPlayer.Play();
    }

    /// <summary>
    /// Goes to the frame at the specified percentage of the video.
    /// </summary>
    /// <param name="percent">The percentage of the video to go to.</param>
    public void GotoFrameAtPercent(float percent)
    {
        float newFrame = percent * _videoPlayer.frameCount;
        _videoPlayer.frame = (long)newFrame;
        Debug.Log($"{percent} {newFrame}");
    }

    /// <summary>
    /// Restarts the video playback from the beginning.
    /// </summary>
    public void RestartVideo()
    {
        _videoPlayer.frame = 0;
        _videoPlayer.Play();
        _slider.value = 0f;
        isPaused = false;
    }


    #endregion

    #region Private Methods

    private void UpdateVideoPlayback()
    {
        if (!isPaused)
        {
            if (!isDragging)
            {
                if (_videoPlayer.isPaused)
                {
                    _videoPlayer.Play();
                }
                UpdateSliderValue();
            }
            else
            {
                _videoPlayer.Pause();
                GotoFrameAtPercent(_slider.value);
            }
        }
    }

    private void UpdateSliderValue()
    {
        _slider.value = (float)_videoPlayer.frame / (float)totalFrame;
    }

    private void OnSliderPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        Debug.Log("Slider Pointer Down at " + _slider.value);
    }

    private void OnSliderPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        GotoFrameAtPercent(_slider.value);
        Debug.Log("Slider Pointer Up at " + _slider.value);
    }

    private void InitializeSliderEventTriggers()
    {
        EventTrigger eventTrigger = _slider.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = _slider.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => OnSliderPointerDown((PointerEventData)data));
        eventTrigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => OnSliderPointerUp((PointerEventData)data));
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    #endregion
}
