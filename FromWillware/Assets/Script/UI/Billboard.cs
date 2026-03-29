using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camTransform != null)
        {
            // 获取摄像机的前方矢量
            Vector3 targetDirection = camTransform.forward;

            // 如果你不想让血条跟着摄像机抬头/低头而倾斜（保持垂直立着）
            // 请取消下面这行的注释：
            // targetDirection.y = 0; 

            // 让血条转向
            transform.LookAt(transform.position + targetDirection);
        }
    }
}