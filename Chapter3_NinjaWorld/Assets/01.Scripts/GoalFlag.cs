using UnityEngine;

public class GoalFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Player 태그를 가진 오브젝트만 처리
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 목적지에 도달했습니다!");
            
            // 게임 매니저에 게임 클리어 알림
            GameManager.Instance.GameClear();
        }
    }
}