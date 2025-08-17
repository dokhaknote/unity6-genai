using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float Amplitude = 0.1f;  // 움직임의 크기
    public float Frequency = 2f;    // 움직임의 속도
    
    private void LateUpdate()
    {
        Vector3 position = transform.position;

        // 시간에 따른 사인 값을 계산 (0~1 사이 값)
        float yOffset = Amplitude * Mathf.Sin(Time.time * Frequency);
        
        // 새로운 위치 계산 (x, z는 그대로, y만 변경)
        Vector3 newPosition = new Vector3(
            position.x, 
            position.y + yOffset, 
            position.z
        );
        
        // 오브젝트 위치 업데이트
        transform.position = newPosition;
    }
}