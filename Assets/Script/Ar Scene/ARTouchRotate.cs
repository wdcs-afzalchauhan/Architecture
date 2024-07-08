using UnityEngine;

public class ARTouchRotate : MonoBehaviour
{
    public float rotationSpeed = 0.1f; // Speed of rotation
    public bool isTouching = false;

    void Update()
    {

        if (UIManager.Instance.isShowList)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    break;
                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        float rotateX = touch.deltaPosition.y * rotationSpeed;
                        float rotateY = -touch.deltaPosition.x * rotationSpeed;

                        // Apply rotation to the object
                        transform.Rotate(Vector3.up, rotateY, Space.World);
                        //transform.Rotate(Vector3.right, rotateX, Space.World);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }
}
