using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform Target;         // 플레이어 (배경이 따라갈 대상)
    public float ScrollSpeed = 0.1f; // 스크롤 속도
    
    private Material _backgroundMaterial;

    void Start()
    {
        // 렌더러의 머티리얼 가져오기
        _backgroundMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Target == null)
        {
            return;
        }
            
        // 플레이어 이동에 따라 위치와 텍스처 오프셋 업데이트
        transform.position = Target.position;
        
        float offsetX = (Target.position.x * ScrollSpeed) % 1.0f;
        float offsetY = (Target.position.y * ScrollSpeed) % 1.0f;
        
        // 새 오프셋 적용
        _backgroundMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}