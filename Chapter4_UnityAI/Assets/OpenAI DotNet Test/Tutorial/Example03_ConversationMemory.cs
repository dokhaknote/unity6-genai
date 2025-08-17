using OpenAI.Chat;
using System.Collections.Generic;
using UnityEngine;

public class Example03_ConversationMemory : MonoBehaviour
{
    public string Model = "gpt-4o";  // OpenAI 모델 설정

    void Start()
    {
        // ChatClient 인스턴스를 생성합니다.
        ChatClient client = new ChatClient(model: Model, apiKey: "Your OpenAI Key");

        // 대화 메시지 리스트를 생성합니다.
        List<ChatMessage> messages = new List<ChatMessage>()
        {
            new SystemChatMessage("당신은 플레이어의 친구입니다. 친근하고 다정다감하게 대화하세요."),  // AI의 역할을 설정
            new UserChatMessage("오늘 뭐 하고 싶어?"),  // 사용자 입력 메시지
            new AssistantChatMessage("영화 보는 거 어때? 새로운 영화 나왔다던데!"),  // AI의 이전 응답
            new UserChatMessage("좋아, 재밌겠다! 또 다른 추천 있어?")  // 사용자 추가 질문
        };

        // AI 응답을 요청합니다.
        ChatCompletion completion = client.CompleteChat(messages);

        // AI 응답을 출력합니다
        Debug.Log(completion.Content[0].Text);
    }
}
