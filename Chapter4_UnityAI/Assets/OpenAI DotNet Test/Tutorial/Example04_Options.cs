using OpenAI.Chat;
using System.Collections.Generic;
using UnityEngine;

public class Example04_Options : MonoBehaviour
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

        // AI의 응답 스타일을 조절하는 옵션을 설정합니다.
        ChatCompletionOptions options = new ChatCompletionOptions()
        {
            Temperature = 1.0f,  // 창의성의 정도를 조절
            TopP = 1.0f,         // 확률 기반 필터링
            FrequencyPenalty = 0.0f,  // 같은 말을 반복하지 않도록 조절
            PresencePenalty = 0.0f,   // 새로운 주제를 더 많이 추가하도록 조절
        };
        options.MaxOutputTokenCount = 4098;  // AI 응답의 최대 길이를 설정
        options.StopSequences.Add("요?");  // 특정 문자가 나오면 응답을 중단
        options.StopSequences.Add("죠?");
        options.StopSequences.Add("END");

        // AI 응답을 요청합니다.
        ChatCompletion completion = client.CompleteChat(messages, options);

        // AI 응답을 출력합니다
        Debug.Log(completion.Content[0].Text);


    }
}
