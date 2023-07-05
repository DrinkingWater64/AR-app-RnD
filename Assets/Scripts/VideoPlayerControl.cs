using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoPlayerControl : MonoBehaviour
{
    private long currentFrame;
    private long totalFrame;

    [SerializeField]
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private Slider _slider;
    private bool isPaused = false;
    private bool isDragging;
    private float lastValue = 0;


    private void OnEnable()
    {
        GotoFrameAtPercent(lastValue);
        Debug.Log("enabled on " + lastValue);
    }

    private void OnDisable()
    {
        lastValue = _slider.value;
        Debug.Log("Disabled");
    }
    // Start is called before the first frame update
    void Start()
    {
        totalFrame = (long)_videoPlayer.frameCount;
        //GotoFrameAtPercent(.5f);

        // Add event listeners for slider dragging using EventTrigger
        EventTrigger eventTrigger = _slider.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = _slider.gameObject.AddComponent<EventTrigger>();
        }

        // Add PointerDown event trigger
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => OnSliderPointerDown((PointerEventData)data));
        eventTrigger.triggers.Add(pointerDownEntry);

        // Add PointerUp event trigger
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => OnSliderPointerUp((PointerEventData)data));
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (!isDragging)
            {
                if (_videoPlayer.isPaused)
                {
                    _videoPlayer.Play();
                }
                _slider.value = (float)_videoPlayer.frame / (float)totalFrame;
            }
            else
            {
                _videoPlayer.Pause();
                GotoFrameAtPercent(_slider.value);
            }

        }
    }

    public void PauseVideo()
    {
        if (!_videoPlayer.isPaused)
        {
        _videoPlayer.Pause();
        isPaused = true;
        }
        else
        {
            _videoPlayer.Play();
            isPaused  = false;
        }
        Debug.Log("Pausing");

    }

    public void ResumeVideo()
    {
        _videoPlayer.Play();
    }

    public void GotoFrameAtPercent(float percent)
    {
        float newFrame = percent * _videoPlayer.frameCount;
        _videoPlayer.frame = (long)newFrame;
        Debug.Log($"{percent} {newFrame}");
    }

    private void OnSliderValueChanged(float value)
    {
        // Update video frame based on the slider value
        long newFrame = (long)(value * totalFrame);
        _videoPlayer.frame = newFrame;
    }

    private void OnSliderPointerDown(PointerEventData eventData)
    {
        // Set the dragging flag when the slider is being dragged
        isDragging = true;
        Debug.Log("down at "+ _slider.value);
    }

    private void OnSliderPointerUp(PointerEventData eventData)
    {
        // Reset the dragging flag when the slider dragging ends
        isDragging = false;
        GotoFrameAtPercent(_slider.value);
        Debug.Log("up at "+ _slider.value);
    }
}
