using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [System.Serializable]
    private class GameActors
    {
        public Animator animator;
        public string successAnimatorTrigger;
        public string failAnimatorTrigger;
    }

    [SerializeField] private List<GameActors> gameActors;

    public void OnAnswer(bool isCorrect)
    {
        foreach (GameActors actor in gameActors)
        {
            string animToPlay = isCorrect ? actor.successAnimatorTrigger : actor.failAnimatorTrigger;
            if(animToPlay != null) actor.animator.SetTrigger(animToPlay);
        }
    }
}
