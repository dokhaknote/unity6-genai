using OpenAI.Chat;
using UnityEngine;

public class OpenaiClient : MonoBehaviour
{
    public string Model;      // 사용할 AI 모델 이름
    public string ApiKey;     // OpenAI API 키

    public ChatClient Client; // 생성된 ChatClient 객체

    void Awake()
    {
        Client = new ChatClient(model: Model, apiKey: ApiKey);
    }
}
