using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public int answerIndex;

    public void Submit()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        GameManager.Instance.SubmitAnswer(answerIndex);
    }
}