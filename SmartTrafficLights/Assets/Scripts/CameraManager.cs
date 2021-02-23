using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera northSemaphoreCamera;
    [SerializeField] private Camera southSemaphoreCamera;

    [SerializeField] private UIController uiController;

    void Awake()
    {
        // First Main camera activation
        mainCamera.enabled = true;
        northSemaphoreCamera.enabled = false;
        southSemaphoreCamera.enabled = false;
    }

    public void DOToggleMainCamera()
    {
        if (!mainCamera.enabled)
        {
            mainCamera.enabled = true;
            northSemaphoreCamera.enabled = false;
            southSemaphoreCamera.enabled = false;

            uiController.mainCameraIndicator.SetActive(false);
        }
    }

    public void DOToggleNorthSemaphoreCamera()
    {
        if (!northSemaphoreCamera.enabled)
        {
            northSemaphoreCamera.enabled = true;
            mainCamera.enabled = false;
            southSemaphoreCamera.enabled = false;

            uiController.mainCameraIndicator.SetActive(true);
        }
    }

    public void DOToggleSouhtSemaphoreCamera()
    {
        if (!southSemaphoreCamera.enabled)
        {
            southSemaphoreCamera.enabled = true;
            northSemaphoreCamera.enabled = false;
            mainCamera.enabled = false;

            uiController.mainCameraIndicator.SetActive(true);
        }
    }
}