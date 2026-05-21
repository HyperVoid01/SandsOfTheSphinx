using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Random = System.Random;

public class OllamaManager : MonoBehaviour
{
    public static OllamaManager Instance;

    private const string API_URL = "http://localhost:11434/api/generate";
    private const string TAGS_URL = "http://localhost:11434/api/tags";

    public string currentModel = "gemma3:4b";
    
    private List<string> previousRiddles =
        new List<string>();
    
    private string[] themes =
    {
        "Pharaohs",
        "Pyramids",
        "Ancient Egypt",
        "The Nile",
        "Hieroglyphs",
        "Scarab Beetles",
        "Egyptian Gods",
        "Tombs",
        "Deserts",
        "Curses",
        "Sphinxes",
        "Mummies",
        "Ancient Magic",
        "Obelisks",
        "Temples"
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetInstalledModels(models =>
        {
            foreach (string model in models)
            {
                if (model == currentModel)
                    return;
            }

            currentModel = models[0];
            UIManager.Instance.modelName.text = "model: " + currentModel;
        });
    }

    public void GenerateRiddle(Action<RiddleData> callback)
    {
        StartCoroutine(GenerateRiddleCoroutine(callback));
    }

    IEnumerator GenerateRiddleCoroutine(Action<RiddleData> callback)
    {
        string chosenTheme =
            themes[UnityEngine.Random.Range(0, themes.Length)];
        
        string difficulty = "easy";
        
        if (GameManager.Instance.score > 5)
            difficulty = "medium";
        
        if (GameManager.Instance.score > 10)
            difficulty = "hard";
        
        string previousText = "";
        
        foreach (string riddle in previousRiddles)
        {
            previousText += "- " + riddle + "\n";
        }
        
        string prompt =
        $@"Generate a UNIQUE Egyptian sphinx riddle.
        
        Theme:
        {chosenTheme}
        
        Difficulty:
        {difficulty}
        
        IMPORTANT:
        Do NOT repeat ideas similar to these previous riddles:
        
        {previousText}
        
        STRICT RULES:
        - Every riddle must feel different
        - Use different wording each time
        - Avoid repeating answers
        - EXACTLY 3 answers
        - ONLY 1 correct answer
        - correctIndex must be 0, 1, or 2
        - Answers should not always be objects
        - Sometimes use concepts
        - Sometimes use places
        - Sometimes use gods
        - Sometimes use creatures
        
        OUTPUT FORMAT:
        
        {{
        ""question"":""Your riddle"",
        ""answers"":[
        ""Answer 1"",
        ""Answer 2"",
        ""Answer 3""
        ],
        ""correctIndex"":0
        }}
        
        ONLY output raw JSON.
        No markdown.
        No explanation.";
        
        
        OllamaRequest requestData = new OllamaRequest
        {
            // model = "gemma3:4b",
            model = currentModel,
            prompt = prompt,
            stream = false,

            options = new OllamaOptions
            {
                temperature = 1.35f,
                top_p = 0.97f,
                seed = UnityEngine.Random.Range(0, 999999)
            }
        };

        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(API_URL, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string raw = request.downloadHandler.text;

            OllamaResponse response =
                JsonUtility.FromJson<OllamaResponse>(raw);

            string cleanJson = response.response
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            cleanJson = SanitiseJson(cleanJson);

            RiddleData data = JsonUtility.FromJson<RiddleData>(cleanJson);
            
            if (data == null)
            {
                Debug.LogError("Failed to parse JSON");
                yield break;
            }
            
            if (string.IsNullOrEmpty(data.question))
            {
                Debug.LogError("Question missing");
                yield break;
            }
            
            if (data.answers == null || data.answers.Length != 3)
            {
                Debug.LogError("AI returned invalid answers");
            
                GenerateRiddle(callback);
            
                yield break;
            }
            
            previousRiddles.Add(data.question);
            
            if (previousRiddles.Count > 20)
            {
                previousRiddles.RemoveAt(0);
            }
            
            callback?.Invoke(data);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
    
    private string SanitiseJson(string json)
    {
        // Find only the string values inside the JSON and clean them
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool inString = false;
        bool escaped = false;

        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];

            if (escaped)
            {
                // Allow only valid JSON escape sequences
                if (c == '"' || c == '\\' || c == '/' || c == 'n' ||
                    c == 'r' || c == 't' || c == 'b' || c == 'f' || c == 'u')
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else
                {
                    // Invalid escape — just keep the character without the backslash
                    sb.Append(c);
                }
                escaped = false;
                continue;
            }

            if (c == '\\' && inString)
            {
                escaped = true;
                continue;
            }

            if (c == '"')
                inString = !inString;

            sb.Append(c);
        }

        return sb.ToString();
    }
    
    public void GetInstalledModels(Action<List<string>> callback)
    {
        StartCoroutine(GetInstalledModelsCoroutine(callback));
    }

    IEnumerator GetInstalledModelsCoroutine(Action<List<string>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(TAGS_URL);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string raw = request.downloadHandler.text;

            OllamaModelList modelList = JsonUtility.FromJson<OllamaModelList>(raw);

            List<string> modelNames = new List<string>();

            foreach (OllamaModel m in modelList.models)
            {
                modelNames.Add(m.name);
            }

            callback?.Invoke(modelNames);
        }
        else
        {
            Debug.LogError("Failed to fetch models: " + request.error);
            callback?.Invoke(new List<string>());
        }
    }
}

[Serializable]
public class OllamaRequest
{
    public string model;
    public string prompt;
    public bool stream;

    public OllamaOptions options;
}

[Serializable]
public class OllamaOptions
{
    public float temperature;
    public int seed;
    public float top_p;
}

[Serializable]
public class OllamaResponse
{
    public string response;
}

[Serializable]
public class OllamaModelList
{
    public OllamaModel[] models;
}

[Serializable]
public class OllamaModel
{
    public string name;
    public string model;
    public long size;
    public string modified_at;
}