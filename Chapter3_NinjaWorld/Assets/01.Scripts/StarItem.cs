using UnityEngine;

public class StarItem : MonoBehaviour
{
    [Header("회전 설정")]
    public float RotationSpeed = 90.0f; // 초당 회전 각도
    public Vector3 RotationAxis = Vector3.up; // 회전축
        
    [Header("파티클 효과")]
    public GameObject CollectParticle; // 수집 시 생성할 파티클 프리팹
    
    private void Update()
    {
        // 별 회전
        transform.Rotate(RotationAxis, RotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Player 태그를 가진 오브젝트만 처리
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddStar();
            
            // 파티클 효과 생성
            if (CollectParticle != null)
            {
                // 별의 위치에 파티클 생성
                Instantiate(CollectParticle, transform.position, Quaternion.identity);
            }
            
            // 별 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}