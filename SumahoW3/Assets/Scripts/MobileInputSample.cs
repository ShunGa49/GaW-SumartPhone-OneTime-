using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class MobileInputVisualizer : MonoBehaviour
{
    // フリックした方向を外部へ通知するイベント
    public static Action<Vector2> OnFlick;

    [Header("フリック判定距離")]
    [SerializeField] private float swipeDistance = 80f;

    private Vector2 startPosition;   // タッチ開始位置
    private Vector2 currentPosition; // 現在位置
    private bool isTouching;         // タッチ中フラグ

    void OnEnable()
    {
        // EnhancedTouch有効化
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

    /// <summary>
    /// マウス入力
    /// </summary>
    void UpdateMouseInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPosition = Mouse.current.position.ReadValue();
            isTouching = true;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            currentPosition = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            currentPosition = Mouse.current.position.ReadValue();
            HandleFlick();
            isTouching = false;
        }
    }

    /// <summary>
    /// タッチ入力
    /// </summary>
    void UpdateTouchInput()
    {
        if (Touch.activeTouches.Count == 0) return;

        Touch t = Touch.activeTouches[0];

        if (t.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startPosition = t.screenPosition;
            isTouching = true;
        }

        if (t.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            currentPosition = t.screenPosition;
        }

        if (t.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            currentPosition = t.screenPosition;
            HandleFlick();
            isTouching = false;
        }
    }

    /// <summary>
    /// フリック判定処理
    /// </summary>
    void HandleFlick()
    {
        Vector2 diff = currentPosition - startPosition;

        // タップは無視
        if (diff.magnitude < swipeDistance)
            return;

        // 方向
        Vector2 direction = diff.normalized;

        // 砲台へ通知
        OnFlick?.Invoke(direction);
    }
}