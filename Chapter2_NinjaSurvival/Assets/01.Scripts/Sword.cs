using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Sword : MonoBehaviour
{
    public int Damage = 1;                 // 무기가 몬스터에게 입힐 데미지
    public float DamageCooldown = 0.5f;    // 데미지 쿨다운 시간 (초)
    
    private List<Collider2D> _hitMonsters = new List<Collider2D>();  // 이미 맞은 몬스터
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 대상이 몬스터인지 확인
        if (collision.CompareTag("Monster"))
        {
            // 이미 맞은 몬스터가 아닌지 확인
            if (!_hitMonsters.Contains(collision))
            {
                // 몬스터에게 데미지 주기
                MonsterController monster = collision.GetComponent<MonsterController>();
                if (monster != null)
                {
                    monster.TakeDamage(Damage);
                }
                
                // 맞은 몬스터 목록에 추가하고 일정 시간 후 제거
                _hitMonsters.Add(collision);
                StartCoroutine(RemoveFromHitList(collision, DamageCooldown));
            }
        }
    }
    
    private IEnumerator RemoveFromHitList(Collider2D monster, float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);
        
        // 쿨다운이 끝난 후 목록에서 제거
        _hitMonsters.Remove(monster);
    }
}