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

    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        totalFrame = (long)_videoPlayer.frameCount;
        GotoFrameAtPercent(.5f);

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
        if (!isDragging)
        {
            _slider.value = (float)_videoPlayer.frame / (float)totalFrame;
            Debug.Log("whaaaa");
        }
    }

    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }

    public void ResumeVideo()
    {
        _videoPlayer.Play();
    }

    public void GotoFrameAtPercent(float percent)
    {
        float newFrame = percent * _videoPlayer.frameCount;
        _videoPlayer.frame = (long)newFrame;
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
        Debug.Log("down");
    }

    private void OnSliderPointerUp(PointerEventData eventData)
    {
        // Reset the dragging flag when the slider dragging ends
        isDragging = false;
    }
}
