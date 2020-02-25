using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionClass
{
    public int playerIndex;
    public int actionType;
    public int fruitAmount;
}

public class ActionController : MonoBehaviour
{
    [SerializeField] private List<ActionClass> actionClasses = new List<ActionClass>();

    private GameController gc;
    private MoveController mc;

    private void Start()
    {
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
    }

    public void ResetActionList ()
    {
        actionClasses.Clear();
    }

    public void GetPlayerActions ()
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            switch (mc.players[i].GetComponent<PlayerController>().playerVariable.actionType)
            {
                case ActionType.PlusFruit3:
                    ActionClass newAction = new ActionClass();
                    newAction.actionType = 0;
                    newAction.playerIndex = i;
                    newAction.fruitAmount = 3;
                    actionClasses.Add(newAction);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 3 fruits");
                    break;
                case ActionType.PlusFruit10:
                    ActionClass newAction2 = new ActionClass();
                    newAction2.actionType = 0;
                    newAction2.playerIndex = i;
                    newAction2.fruitAmount = 10;
                    actionClasses.Add(newAction2);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 10 fruits");
                    break;
                case ActionType.MinusFruit3:
                    ActionClass newAction3 = new ActionClass();
                    newAction3.actionType = 0;
                    newAction3.playerIndex = i;
                    newAction3.fruitAmount = -3;
                    actionClasses.Add(newAction3);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " remove 3 fruits");
                    break;
                case ActionType.Chance:
                    ActionClass newAction4 = new ActionClass();
                    newAction4.actionType = 1;
                    newAction4.playerIndex = i;
                    actionClasses.Add(newAction4);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " play chance");
                    break;
                case ActionType.Minigame:
                    ActionClass newAction5 = new ActionClass();
                    newAction5.actionType = 2;
                    newAction5.playerIndex = i;
                    actionClasses.Add(newAction5);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " play minigame");
                    break;
                default:
                    Debug.Log("Something went wrong in sorting playerActions!");
                    break;
            }
        }

        for (int i = 0; i < actionClasses.Count - 1; i++)
        {
            for (int j = 0; j < actionClasses.Count - 1 - i; j++)
            {
                if (actionClasses[j].actionType > actionClasses[j + 1].actionType)
                {
                    ActionClass tmp = actionClasses[j + 1];
                    actionClasses[j + 1] = actionClasses[j];
                    actionClasses[j] = tmp;
                }
            }
        }

        for (int i = 0; i < actionClasses.Count; i++)
        {
            //PlayAction(actionClasses);
        }
    }

    private void PlayAction (int playerIndex, int actionType)
    {
        switch (actionType)
        {
            case 0:
                //gc.ChangeFruitAmount(playerIndex, actionClasses[.fruitAmount);
                break;
            case 1:

                break;
            case 2:

                break;
            default:
                Debug.Log("Something went wrong in sorting playerActions!");
                break;
        }
    }
}
