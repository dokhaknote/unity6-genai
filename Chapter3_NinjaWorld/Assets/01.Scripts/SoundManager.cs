using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static SoundManager Instance;

    // 오디오 소스 컴포넌트
    public AudioSource MusicSource;  // 배경 음악용
    public AudioSource SfxSource;    // 효과음용

    // 볼륨 설정
    [Range(0f, 1f)]
    public float MusicVolume = 0.3f; // 배경 음악 볼륨 (0~1 사이)
    [Range(0f, 1f)]
    public float SfxVolume = 0.5f;   // 효과음 볼륨 (0~1 사이)

    // 오디오 클립 참조
    [Header("배경 음악")]
    public AudioClip BgmClip;        // NinjaBGM.mp3

    [Header("효과음")]
    public AudioClip JumpSfx;        // Jump.mp3
    public AudioClip SpikeHitSfx;    // SpikeHit.mp3
    public AudioClip MonsterHitSfx;  // MonsterHit.mp3
    public AudioClip StarCollectSfx; // StarCollect.mp3
    public AudioClip GameOverSfx;    // GameOver.mp3
    public AudioClip GameClearSfx;   // GameClear.mp3

    private void Awake()
    {
        // 싱글톤 패턴 구현 - 단 하나의 사운드 매니저만 존재하도록 함
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 사운드 매니저는 제거
        }
        
        // 오디오 소스 컴포넌트 설정
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.loop = true;         // 배경 음악은 반복 재생
        MusicSource.volume = MusicVolume;

        SfxSource = gameObject.AddComponent<AudioSource>();
        SfxSource.loop = false;          // 효과음은 한 번만 재생
        SfxSource.volume = SfxVolume;
    }

    // 배경 음악 재생
    public void PlayMusic()
    {
        MusicSource.clip = BgmClip;
        MusicSource.Play();
    }

    // 효과음 재생 메서드들
    public void PlayJumpSfx()
    {
        SfxSource.PlayOneShot(JumpSfx, SfxVolume);
    }

    public void PlaySpikeHitSfx()
    {
        SfxSource.PlayOneShot(SpikeHitSfx, SfxVolume);
    }

    public void PlayMonsterHitSfx()
    {
        SfxSource.PlayOneShot(MonsterHitSfx, SfxVolume);
    }

    public void PlayStarCollectSfx()
    {
        SfxSource.PlayOneShot(StarCollectSfx, SfxVolume);
    }

    public void PlayGameOverSfx()
    {
        SfxSource.PlayOneShot(GameOverSfx, SfxVolume);
    }

    public void PlayGameClearSfx()
    {
        SfxSource.PlayOneShot(GameClearSfx, SfxVolume);
    }
}