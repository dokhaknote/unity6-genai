using UnityEngine;

public class MonsterScanner : MonoBehaviour
{
    public float ScanRange = 10f;            // 스캔 범위
    public LayerMask EnemyLayer;            // 적 레이어 마스크
    
    private Collider2D[] _hitResults = new Collider2D[10];  // 스캔 결과를 저장할 배열
    
    // 가장 가까운 적을 찾는 함수
    public Transform GetNearestEnemy()
    {
        // 원형으로 적 스캔
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position,  // 스캔 중심 위치
            ScanRange,           // 스캔 범위
            _hitResults,         // 결과 저장할 배열
            EnemyLayer           // 적 레이어 마스크
        );
        
        // 스캔된 적이 없으면 null 반환
        if (hitCount <= 0)
            return null;
            
        // 첫 번째 적을 가장 가까운 적으로 초기화
        Transform nearest = _hitResults[0].transform;
        float minDistance = Vector2.Distance(transform.position, nearest.position);
        
        // 더 가까운 적이 있는지 확인
        for (int i = 1; i < hitCount; i++)
        {
            Transform enemy = _hitResults[i].transform;
            float distance = Vector2.Distance(transform.position, enemy.position);
            
            if (distance < minDistance)
            {
                nearest = enemy;
                minDistance = distance;
            }
        }
        
        return nearest;
    }
}