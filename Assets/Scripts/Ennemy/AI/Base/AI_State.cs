using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "BIJ/AI/State")]
public class AI_State : ScriptableObject  {

    #region Variables
    [Header("----Actions")]
    public AI_Action onEnterAction;
    [Space]
    public AI_Action[] actions;
    [Space]
    public AI_Action onExitAction;
    [Space]
    [Header("----Transitions")]
    public AI_Transition[] transitions;
    public Color sceneGizmoColor = Color.grey;
    #endregion

    #region Main methods
    public void EnterState(Enemy enemy)
    {
        if (onEnterAction != null)
            onEnterAction.Act(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        DoActions(enemy);
        CheckTransitions(enemy);
    }

    public void ExitState(Enemy enemy)
    {
        if (onExitAction != null)
            onExitAction.Act(enemy);
    }
    #endregion

    #region Helper methods
    private void DoActions(Enemy enemy)
    {
        if (actions != null)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (actions[i] != null)
                    actions[i].Act(enemy);
                else
                    Debug.LogError("Action N° " + i + " is not assigned [AI : \"" + enemy.name + "\" -> State : \"" + this.name + "\"]");
            }
        }
        
    }

    private void CheckTransitions(Enemy enemy)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            if (transitions[i] != null)
            {
                if (transitions[i].decision != null)
                {
                    bool decisionSucceeded = transitions[i].decision.Decide(enemy);

                    if (decisionSucceeded)
                    {
                        if (transitions[i].trueState != null)
                            enemy.TransitionToState(transitions[i].trueState);
                        else
                            Debug.LogError("Transition N° " + i + " - TrueState is not assigned [AI : \"" + enemy.name + "\" -> State : \"" + this.name + "\"]");
                    }
                    else
                    {
                        if (transitions[i].trueState != null)
                            enemy.TransitionToState(transitions[i].falseState);
                        else
                            Debug.LogError("Transition N° " + i + " - FalseState is not assigned [AI : \"" + enemy.name + "\" -> State : \"" + this.name + "\"]");
                    }
                }
                else
                {
                    Debug.LogError("Transition N° " + i + " - Decision is not assigned [AI : \"" + enemy.name +"\" -> State : \"" + this.name +"\"]");
                }
            }
            else
            {
                Debug.LogError("Transition N° " + i + " is not assigned [AI : \"" + enemy.name + "\" -> State : \"" + this.name + "\"]");
            }
               
        }
    }
    #endregion

}
