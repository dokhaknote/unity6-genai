using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위한 네임스페이스

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴 구현
    public static GameManager Instance;
    
    // 게임 상태
    public bool IsGameOver;
    public bool IsGameClear; // 게임 클리어 상태 추가

    // UI 참조
    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public Text ClearStarCountText;
    
    // 재시작 딜레이
    public float RestartDelay = 1.0f;
    
    // 점수 시스템
    public int StarCount = 0;
    public Text StarCountText; // UI에 점수를 표시할 Text 컴포넌트

    // 타이머 시스템
    public float TimeLimit = 60f; // 제한 시간 (초)
    private float currentTime; // 현재 남은 시간
    public Text TimerText; // UI에 시간을 표시할 Text 컴포넌트
    
    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // 게임 초기화
        ResetGame();
        
        // 배경 음악 재생
        SoundManager.Instance.PlayMusic();
    }
    
    private void Update()
    {
        // 게임 오버나 클리어 상태가 아닐 때만 타이머 업데이트
        if (!IsGameOver && !IsGameClear)
        {
            UpdateTimer();
        }
    }
    
    // 타이머 업데이트
    private void UpdateTimer()
    {
        // 시간 감소
        currentTime -= Time.deltaTime;
        
        // 시간이 0 이하가 되면 게임 오버
        if (currentTime <= 0)
        {
            currentTime = 0;
            TimeOver();
        }
        
        // UI 업데이트 (소수점 버림)
        if (TimerText != null)
        {
            TimerText.text = "시간: " + Mathf.Floor(currentTime);
        }
    }
    
    // 시간 초과로 인한 게임 오버
    private void TimeOver()
    {
        if (!IsGameOver && !IsGameClear)
        {
            IsGameOver = true;
            Debug.Log("시간 초과! 게임 오버!");
            
            // 게임 오버 효과음 재생
            SoundManager.Instance.PlayGameOverSfx();
        
            // 게임 오버 UI 표시 (자동 재시작 제거)
            GameOverUI.SetActive(true);
        }
    }

    
    public void PlayerDied()
    {
        if (!IsGameOver && !IsGameClear)
        {
            IsGameOver = true;
            Debug.Log("게임 오버!");
            
            // 게임 오버 효과음 재생
            SoundManager.Instance.PlayGameOverSfx();
        
            // 게임 오버 UI 표시 (자동 재시작 제거)
            GameOverUI.SetActive(true);
        }
    }
    
    public void RestartGame()
    {
        // 게임 초기화
        ResetGame();

        // 현재 활성화된 씬 재시작
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    
    // 별 수집 시 호출되는 메서드
    public void AddStar()
    {
        StarCount++;
        UpdateStarCountUI();
        
        // 별 수집 효과음 재생
        SoundManager.Instance.PlayStarCollectSfx();
        
        Debug.Log("별 수집: " + StarCount);
    }
    
    // 게임 상태 초기화
    private void ResetGame()
    {
        IsGameOver = false;
        IsGameClear = false;
        StarCount = 0;
        currentTime = TimeLimit;
    
        // UI 상태 초기화
        GameClearUI.SetActive(false);
        GameOverUI.SetActive(false);
        
        // UI 초기화
        UpdateStarCountUI();
        if (TimerText != null)
        {
            TimerText.text = "시간: " + Mathf.Floor(currentTime);
        }
    }
    
     // UI에 점수 업데이트
    private void UpdateStarCountUI()
    {
        if (StarCountText != null)
        {
            StarCountText.text = "별: " + StarCount;
        }
    }
    
    // 게임 클리어 처리
    public void GameClear()
    {
        if (!IsGameClear && !IsGameOver)
        {
            IsGameClear = true;
            Debug.Log("게임 클리어!");
        
            // 게임 클리어 UI 표시
            GameClearUI.SetActive(true);
            
            // 게임 클리어 효과음 재생
            SoundManager.Instance.PlayGameClearSfx();
            
            // 최종 점수 표시
            ClearStarCountText.text = "수집한 별: " + StarCount + "개";
        }
    }

    public void QuitGame()
    {
        // 에디터에서는 플레이 모드 종료, 빌드에서는 애플리케이션 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    
        Debug.Log("게임을 종료합니다.");
    }
}