using OpenAI.Chat;
using UnityEngine;

public class Example01_ModelSelect : MonoBehaviour
{    
    public string Model = "gpt-4o";  // OpenAI 모델 설정

    void Start()
    {
        // ChatClient 인스턴스를 생성합니다.
        ChatClient client = new ChatClient(model: Model, apiKey: "Your OpenAI Key");

        // AI 응답을 요청합니다.
        ChatCompletion completion = client.CompleteChat("안녕하세요!");

        // AI 응답을 출력합니다
        Debug.Log(completion.Content[0].Text);
    }
}
