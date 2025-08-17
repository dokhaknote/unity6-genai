using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public float ScaleSpeed = 5f;       // 애니메이션 속도
    public float ScaleAmount = 0.05f;    // 크기 변화량 (5%)
    
    private Vector3 _originalScale;     // 원래 크기 저장용
    
    void Start()
    {
        // 시작할 때 원래 크기 저장해두기
        _originalScale = transform.localScale;
    }
    
    void Update()
    {
        // 시간에 따라 0~1 사이를 반복하는 값 (Pingpong)
        float scaleProgress = Mathf.PingPong(Time.time * ScaleSpeed, 1f);
        
        // 0~1 값을 -ScaleAmount ~ +ScaleAmount 범위로 변환
        float scaleChange = (scaleProgress * 2 - 1) * ScaleAmount;
        
        // 새로운 크기 계산
        Vector3 newScale = _originalScale * (1 + scaleChange);
        
        // 크기 적용
        transform.localScale = newScale;
    }
}