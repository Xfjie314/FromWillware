using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    public float MouseSensitivity= 200f;
    public float Distance= 3.5f;
    public float VerticalOffset = 1.5f;
    public float LookAtOffset = 1.5f;
    public float xRotation = 30f;
    public float yRotation = 0f;
    public float MinDistance = 0.6f;
    public LayerMask ObstacleLayerMask = ~0;

    void Start()
    {
        // 隐藏鼠标并锁定光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (IsLocking) return;
        
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 direction = rotation * new Vector3(0, 0, -Distance);

        Vector3 playerPosition = PlayerTransform.position + new Vector3(0, VerticalOffset, 0);
        
        // 检测镜头与玩家之间是否有遮挡
        float adjustedDistance = Distance;
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, direction.normalized, out hit, Distance, ObstacleLayerMask))
        {
            // 如果有遮挡，将相机拉近到碰撞点
            adjustedDistance = hit.distance - 0.5f;
            adjustedDistance = Mathf.Max(adjustedDistance, MinDistance);
        }
        
        // 计算最终相机位置
        Vector3 adjustedDirection = rotation * new Vector3(0, 0, -adjustedDistance);
        Vector3 cameraPosition = playerPosition + adjustedDirection;
        
        transform.position = cameraPosition;
        transform.LookAt(PlayerTransform.position + new Vector3(0, LookAtOffset, 0));
    }
}