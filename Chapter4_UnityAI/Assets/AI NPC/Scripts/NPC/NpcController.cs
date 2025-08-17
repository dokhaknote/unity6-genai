using OpenAI.Chat;
using UnityEngine;
using System.Collections.Generic;

// 감정 표현 enum
public enum NpcEmotion
{
    기쁨,
    슬픔,
    화남,
    놀람,
    중립
}

[System.Serializable]
// NPC의 개성 정보를 저장합니다.
public class NpcProfile
{
    public string Personality; // 성격
    public string Tone;        // 말투
    public string Interests;   // 관심사
}

[System.Serializable]
public class NpcReplyJson
{
    public string reply;
    public string emotion;
}

public class NpcController : MonoBehaviour
{
    private OpenaiClient _openaiClient;  // AI 호출
    public ChatUIManager UIManager;      // UI 관리 매니저
    public NpcProfile Profile;           // NPC 개성 정보
    public List<ChatMessage> Messages = new List<ChatMessage>(); // 대화 맥락 메시지 리스트
    public NpcEmotion Emotion = NpcEmotion.중립; // NPC 감정 상태
    public Animator Anim; // Animator 컴포넌트 참조

    void Awake()
    {
        _openaiClient = GetComponent<OpenaiClient>(); // OpenaiClient 컴포넌트 가져오기
        Messages.Add(new SystemChatMessage($"[{Profile.Personality}, {Profile.Tone}, {Profile.Interests}]")); // 시스템 메시지 추가
        // 감정 표현 강제 시스템 메시지 추가
        Messages.Add(new SystemChatMessage(
            "NPC의 답변(reply)과 감정(emotion)을 반드시 아래 JSON 형식으로 반환하세요. " +
            "emotion은 반드시 [기쁨, 슬픔, 화남, 놀람, 중립] 중 하나만 사용하세요.\n" +
            "{\"reply\": \"<NPC의 대사>\", \"emotion\": \"기쁨/슬픔/화남/놀람/중립\"}"
        ));
    }

    // 감정 및 답변 파싱 함수
    private (string reply, NpcEmotion emotion) ParseNpcReplyJson(string jsonText)
    {
        string npcReply = "";
        NpcEmotion emotion = NpcEmotion.중립;

        try
        {
            var json = JsonUtility.FromJson<NpcReplyJson>(jsonText);
            npcReply = json.reply;
            if (!System.Enum.TryParse(json.emotion, out emotion))
                emotion = NpcEmotion.중립;
        }
        catch
        {
            npcReply = jsonText; // 파싱 실패 시 원문 출력
            emotion = NpcEmotion.중립;
        }

        return (npcReply, emotion);
    }

    // 감정에 따라 애니메이터 트리거 실행
    private void SetEmotionTrigger(NpcEmotion emotion)
    {
        if (Anim == null) return;
        switch (emotion)
        {
            case NpcEmotion.기쁨: Anim.SetTrigger("Joy"); break;
            case NpcEmotion.슬픔: Anim.SetTrigger("Sad"); break;
            case NpcEmotion.화남: Anim.SetTrigger("Angry"); break;
            case NpcEmotion.놀람: Anim.SetTrigger("Surprised"); break;
            case NpcEmotion.중립: Anim.SetTrigger("Neutral"); break;
        }
    }

    // 사용자의 메시지를 전송하고, AI 응답을 받아 처리합니다.
    public async void SubmitChatMessage()
    {
        UIManager.SetButtonInteractable(false); // 중복 입력 방지
        string message = UIManager.GetUserText(); // 사용자 메시지 받기
        UIManager.AddUserBubble();                // 사용자 말풍선 표시

        Messages.Add(new UserChatMessage(message)); // 사용자 메시지 추가

        ChatCompletion completion = await _openaiClient.Client.CompleteChatAsync(Messages);
        string jsonText = completion.Content[0].Text.Trim();

        // 감정 및 답변 파싱 함수 사용
        var (npcReply, emotion) = ParseNpcReplyJson(jsonText);
        Emotion = emotion;
        SetEmotionTrigger(emotion); // 감정에 따라 애니메이터 트리거 실행

        Messages.Add(new AssistantChatMessage(npcReply)); // AI 응답 메시지 추가
        UIManager.AddNpcBubble(npcReply); // NPC 말풍선 생성

        UIManager.ClearUserText(); // 입력창 초기화
        UIManager.SetButtonInteractable(true); // 버튼 활성화
    }
}
