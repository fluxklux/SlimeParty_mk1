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
    private MinigameController mgc;
    private UIController uc;

    bool hasMinigame = false;

    private void Start()
    {
        uc = GetComponent<UIController>();
        mgc = GetComponent<MinigameController>();
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
    }

    public void ResetActionList ()
    {
        actionClasses.Clear();
        hasMinigame = false;
    }

    public void GetPlayerActions ()
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            Debug.Log(mc.players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.actionType);
            switch (mc.players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.actionType)//mc.players[i].GetComponent<PlayerController>().playerVariable.actionType)
            {
                case ActionType.PlusFruit3:
                    ActionClass newAction = new ActionClass();
                    newAction.actionType = 0;
                    newAction.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction.fruitAmount = 3;
                    actionClasses.Add(newAction);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 3 fruits");
                    break;
                case ActionType.PlusFruit10:
                    ActionClass newAction2 = new ActionClass();
                    newAction2.actionType = 0;
                    newAction2.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction2.fruitAmount = 10;
                    actionClasses.Add(newAction2);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 10 fruits");
                    break;
                case ActionType.MinusFruit3:
                    ActionClass newAction3 = new ActionClass();
                    newAction3.actionType = 0;
                    newAction3.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction3.fruitAmount = -3;
                    actionClasses.Add(newAction3);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " remove 3 fruits");
                    break;
                case ActionType.Chance:
                    ActionClass newAction4 = new ActionClass();
                    newAction4.actionType = 1;
                    newAction4.playerIndex = gc.queueObjects[i].playerIndex;
                    actionClasses.Add(newAction4);

                    Debug.Log("Player" + gc.queueObjects[i].playerIndex + " play chance");
                    break;
                case ActionType.Minigame:
                    ActionClass newAction5 = new ActionClass();
                    newAction5.actionType = 2;
                    newAction5.playerIndex = gc.queueObjects[i].playerIndex;
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

        for (int h = 0; h < actionClasses.Count; h++)
        {
            PlayAction(actionClasses[h].playerIndex, actionClasses[h].actionType, h);
        }
    }

    private void PlayAction (int playerIndex, int actionType, int i)
    {
        switch (actionType)
        {
            case 0:
                gc.ChangeFruitAmount(playerIndex, actionClasses[i].fruitAmount);
                break;
            case 1:
                Debug.Log("CHANCE!");
                break;
            case 2:
                if(!hasMinigame)
                {
                    //Debug.Log(playerIndex);
                    uc.ToggleMasherInstructions(true);
                    //mgc.SelectPlayer(playerIndex);
                    hasMinigame = true;
                }
                break;
            default:
                Debug.Log("Something went wrong in sorting playerActions!");
                break;
        }
    }
}
