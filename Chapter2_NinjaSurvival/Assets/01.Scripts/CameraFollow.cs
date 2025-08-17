using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;        // 카메라가 따라갈 대상 (플레이어)
    public float SmoothSpeed = 10f; // 카메라 이동 부드러움 정도


    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }
        
        // 카메라가 따라갈 목표 위치 계산 (플레이어 위치)
        Vector3 desiredPosition = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        
        // 현재 위치에서 목표 위치로 부드럽게 이동 (Lerp 사용)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        
        // 카메라 위치 업데이트
        transform.position = smoothedPosition;
    }
}