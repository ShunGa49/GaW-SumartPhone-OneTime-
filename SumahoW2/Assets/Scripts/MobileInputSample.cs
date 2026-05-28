using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using System;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class MobileInputVisualizer : MonoBehaviour
{
    [SerializeField]
    private float swipeDistance = 80f;

    // スワイプ通知
    public Action<Vector2> OnSwipe;

    private Vector2 startPosition;
    private Vector2 currentPosition;

    private bool isTouching;

    private string currentState = "None";

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
#if UNITY_EDITOR

        UpdateMouseInput();

#else

        UpdateTouchInput();

#endif
    }

    void UpdateMouseInput()
    {
        // タッチ開始
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPosition = Mouse.current.position.ReadValue();
            currentPosition = startPosition;

            isTouching = true;
        }

        // タッチ中
        if (Mouse.current.leftButton.isPressed)
        {
            currentPosition = Mouse.current.position.ReadValue();
        }

        // 指を離した
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            currentPosition = Mouse.current.position.ReadValue();

            CheckGesture();

            isTouching = false;
        }
    }

    void UpdateTouchInput()
    {
        if (Touch.activeTouches.Count == 0)
        {
            return;
        }

        Touch touch = Touch.activeTouches[0];

        // タッチ開始
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startPosition = touch.screenPosition;
            currentPosition = startPosition;

            isTouching = true;
        }

        // タッチ中
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            currentPosition = touch.screenPosition;
        }

        // 指を離した
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            currentPosition = touch.screenPosition;

            CheckGesture();

            isTouching = false;
        }
    }

    void CheckGesture()
    {
        Vector2 diff = currentPosition - startPosition;

        // スワイプ距離不足
        if (diff.magnitude < swipeDistance)
        {
            currentState = "Tap";
            return;
        }

        currentState = "Swipe";

        // スワイプ方向を通知
        OnSwipe?.Invoke(diff);
    }
}