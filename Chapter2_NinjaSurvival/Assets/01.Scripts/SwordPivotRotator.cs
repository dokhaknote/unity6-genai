using UnityEngine;

public class SwordPivotRotator : MonoBehaviour
{
    public float RotateSpeed = -100f;  // 회전 속도
    
    void Update()
    {
        // 매 프레임 회전시키기
        transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
    }
}