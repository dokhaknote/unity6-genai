using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform Target;             // 추적할 대상 (플레이어)
    
    [Header("위치 오프셋")]
    public Vector3 Offset = new Vector3(2, 5, -8);  // 타겟으로부터의 상대적 위치
    
    [Header("회전 설정")]
    public float Pitch = 30.0f;          // 카메라 X축 기울기 (위/아래 각도)
    
    private void LateUpdate()
    {
        if (Target == null)
        {
            Debug.LogWarning("카메라 추적 대상이 설정되지 않았습니다!");
            return;
        }
        
        // 플레이어 위치 + 오프셋으로 카메라 위치 즉시 갱신
        transform.position = Target.position + Offset;
        
        // 카메라 X축 회전(기울기) 적용;
        transform.eulerAngles = new Vector3(Pitch, 0, 0);
    }
}