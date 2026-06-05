using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class MobileInputVisualizer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player;


    [Header("SwipeDistance")]
    [SerializeField] private float swipeDistance = 80f;

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
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPosition = Mouse.current.position.ReadValue();
            currentPosition = startPosition;

            isTouching = true;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            currentPosition = Mouse.current.position.ReadValue();
        }

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

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startPosition = touch.screenPosition;
            currentPosition = startPosition;

            isTouching = true;
        }

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            currentPosition = touch.screenPosition;
        }

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

        if (diff.magnitude < swipeDistance)
        {
            currentState = "Tap";

            //（必要ならここにタップ処理）
            HandleTap(startPosition);
        }
        else
        {
            currentState = "Swipe";

            //（必要ならここにスワイプ処理）
        }
    }

    /// <summary>
    /// 画面タップしたときの操作
    /// </summary>
    void HandleTap(Vector2 position)
    {
        if (player == null)
            return;

        // 画面座標 → ワールド座標に変換
        Vector3 tapWorldPos = Camera.main.ScreenToWorldPoint(position);
        tapWorldPos.z = 0f;

        // プレイヤーの位置と比較
        if (tapWorldPos.x < player.transform.position.x)
        {
            player.MoveLeft();
        }
        else
        {
            player.MoveRight();
        }
    }

}