using UnityEngine;

public class DamageSpike : MonoBehaviour
{
    [Header("피해 설정")]
    public float KnockbackForce = 5f; // 튕겨나가는 힘
    public float KnockbackUpForce = 1f; // 위쪽 방향으로 가해지는 힘
    
    [Header("파티클 효과")]
    public GameObject HitParticle; // 충돌 시 생성할 파티클 프리팹
    
    private void OnCollisionEnter(Collision collision)
    {
        // Player 태그를 가진 오브젝트만 처리
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 Rigidbody 컴포넌트 가져오기
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            
            if (playerRb != null)
            {
                // 스파이크로부터 플레이어를 밀어내는 방향 계산
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                
                // 방향에 랜덤성 추가 (좌우로 약간의 랜덤 튕김)
                direction += new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
                direction.Normalize();
                
                // 위쪽 방향 힘 추가
                direction += Vector3.up * KnockbackUpForce;
                
                // 플레이어에게 힘 가하기
                playerRb.AddForce(direction * KnockbackForce, ForceMode.Impulse);
                
                SoundManager.Instance.PlaySpikeHitSfx();
                
                // 파티클 효과 생성
                if (HitParticle != null)
                {
                    // 충돌 지점에 파티클 생성
                    Vector3 contactPoint = collision.contacts[0].point;
                    Instantiate(HitParticle, contactPoint, Quaternion.identity);
                }
                
                Debug.Log("플레이어가 스파이크에 부딪혔습니다!");
            }
        }
    }
}