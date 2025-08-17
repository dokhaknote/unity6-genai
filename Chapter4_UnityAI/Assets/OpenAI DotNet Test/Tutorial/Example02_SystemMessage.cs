using OpenAI.Chat;
using System.Collections.Generic;
using UnityEngine;

public class Example02_SystemMessage : MonoBehaviour
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
            new UserChatMessage("오늘 뭐 하고 싶어?")  // 사용자 입력 메시지
        };

        // AI 응답을 요청합니다.
        ChatCompletion completion = client.CompleteChat(messages);

        // AI 응답을 출력합니다
        Debug.Log(completion.Content[0].Text);
    }
}
