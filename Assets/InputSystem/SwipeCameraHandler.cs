using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeCameraHandler : MonoBehaviour
{
    public float swipeSensitivity; // Sensitivity for swipe detection
    public float maxMovement; // Maximum allowed camera movement distance
    public Transform cameraTransform; // Reference to the camera transform (child of this GameObject)

    private Vector2 touchStartPos; // Position where the touch began
    private Vector2 touchDelta; // Difference between touch start and end positions
    private bool isSwiping; // Flag to track if a swipe is happening

    // Define touchAction variable here
    private InputAction touchAction;

    void Awake()
    {
        touchStartPos = Vector2.zero;
        touchDelta = Vector2.zero;
        isSwiping = false;
    }

    void OnTouchStarted(InputAction.CallbackContext context)
    {
        isSwiping = true;
        touchStartPos = context.ReadValue<Vector2>(); // Get touch start position
    }

    void OnTouchEnded(InputAction.CallbackContext context)
    {
        isSwiping = false;
        touchDelta = Vector2.zero; // Reset touch delta on touch end
    }

    void Update()
    {
        if (isSwiping)
        {
            // Get current touch position (assuming using TouchContinuous action)
            touchDelta = touchAction.ReadValue<Vector2>() - touchStartPos; 
            MoveCamera(); // Call function to move camera based on swipe
        }
    }

    void MoveCamera()
    {
        Vector3 cameraPos = cameraTransform.localPosition;
        cameraPos.x += touchDelta.x * swipeSensitivity; // Move camera on X based on swipe delta

        // Clamp camera movement within desired limits
        cameraPos.x = Mathf.Clamp(cameraPos.x, -maxMovement, maxMovement);

        cameraTransform.localPosition = cameraPos;
    }
}
