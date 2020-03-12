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
    public List<ActionClass> actionClasses = new List<ActionClass>();

    [Header("Effects: ")]
    [SerializeField] private GameObject effectPrefab = null;
    [SerializeField] private Sprite[] effectSprites = { null, null, null };

    private GameController gc;
    private MoveController mc;
    private MinigameController mgc;
    private UIController uc;
    private ChanceController cc;
    private AudioController ac;

    bool hasMinigame = false;
    GameObject effect;

    private void Start()
    {
        uc = GetComponent<UIController>();
        mgc = GetComponent<MinigameController>();
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
        cc = GetComponent<ChanceController>();
        ac = GetComponent<AudioController>();
    }

    public void ResetActionList ()
    {
        for (int g = 0; g < mc.players.Length; g++)
        {
            //clear extra fruits;
            mc.players[g].GetComponent<PlayerController>().playerVariable.extraFruits = 0;
        }

        for (int i = 0; i < gc.allSlots.Length; i++)
        {
            gc.allSlots[i].GetComponent<SlotController>().hasEffect = false;
        }

        actionClasses.Clear();
        hasMinigame = false;
    }

    public void GetPlayerActions ()
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            //Debug.Log(mc.players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.actionType);
            switch (mc.players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.actionType)//mc.players[i].GetComponent<PlayerController>().playerVariable.actionType)
            {
                case ActionType.PlusFruit3:
                    //Debug.Log("Plus 3");
                    ActionClass newAction = new ActionClass();
                    newAction.actionType = 0;
                    newAction.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction.fruitAmount = 3;
                    actionClasses.Add(newAction);

                    //Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 3 fruits");
                    break;
                case ActionType.PlusFruit10:
                    ActionClass newAction2 = new ActionClass();
                    newAction2.actionType = 0;
                    newAction2.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction2.fruitAmount = 10;
                    actionClasses.Add(newAction2);

                    //Debug.Log("Player" + gc.queueObjects[i].playerIndex + " add 10 fruits");
                    break;
                case ActionType.MinusFruit3:
                    ActionClass newAction3 = new ActionClass();
                    newAction3.actionType = 0;
                    newAction3.playerIndex = gc.queueObjects[i].playerIndex;
                    newAction3.fruitAmount = -3;
                    actionClasses.Add(newAction3);

                    //Debug.Log("Player" + gc.queueObjects[i].playerIndex + " remove 3 fruits");
                    break;
                case ActionType.Chance:
                    ActionClass newAction4 = new ActionClass();
                    newAction4.actionType = 1;
                    newAction4.playerIndex = gc.queueObjects[i].playerIndex;
                    actionClasses.Add(newAction4);

                    //Debug.Log("Player" + gc.queueObjects[i].playerIndex + " play chance");
                    break;
                case ActionType.Minigame:
                    ActionClass newAction5 = new ActionClass();
                    newAction5.actionType = 2;
                    newAction5.playerIndex = gc.queueObjects[i].playerIndex;
                    actionClasses.Add(newAction5);

                    //Debug.Log("Player" + gc.queueObjects[i].playerIndex + " play minigame");
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

        gc.GetComponent<StateController>().canSkip = true;

        //spelades inget minigame denna rundan (bara frukter gavs ut) ska man hoppa fram ett state så man slipper att vänta
        if (!gc.GetComponent<MinigameController>().playedMinigameThisRound)
        {
            gc.GetComponent<ActionController>().ResetActionList();
        }
    }

    private void PlayAction (int playerIndex, int actionType, int i)
    {
        if(gc.allSlots[mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].GetComponent<SlotController>().hasEffect == false)
        {
            //Debug.Log(gc.allSlots[mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].GetComponent<SlotController>().hasEffect);
            gc.allSlots[mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].GetComponent<SlotController>().hasEffect = true;
            effect = Instantiate(effectPrefab) as GameObject;
            effect.transform.position = new Vector3(
                gc.allSlots[mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform.position.x,
                gc.allSlots[mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform.position.y + 0.5f, 0);
        }
        else
        {
            Debug.Log("already has effects LMAO");
        }

        switch (actionType)
        {
            case 0:

                int fruitAmount = actionClasses[i].fruitAmount;
                if (mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.extraFruits > 0)
                {
                    if (mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst)
                    {
                        fruitAmount += 10;
                    }
                }

                //Debug.Log("FruitAmount: " + fruitAmount);
                gc.ChangeFruitAmount(playerIndex, fruitAmount);

                switch(fruitAmount)
                {
                    case 3:
                        effect.GetComponentInChildren<SpriteRenderer>().sprite = effectSprites[0];
                        break;
                    case 10:
                        effect.GetComponentInChildren<SpriteRenderer>().sprite = effectSprites[1];
                        break;
                    case -3:
                        effect.GetComponentInChildren<SpriteRenderer>().sprite = effectSprites[2];
                        break;
                }

                //remove fruitbag
                for (int p = 0; p < gc.allSlots.Length; p++)
                {
                    //har fruitbaggen samma slot index som spelaren som kör actionen?
                    //Debug.Log(p + " " + mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition);
                    if(p == mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition)
                    {
                           //Debug.Log("TAKE FRUIT BAG");
                            //Debug.Log("Slot index: " + p + ", Player slot position: " + mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition);
                            Destroy(gc.allSlots[p].GetComponent<SlotController>().fruitBagObject);
                            gc.allSlots[p].GetComponent<SlotController>().fruitBagObject = null;
                            gc.allSlots[p].GetComponent<SlotController>().hasBag = false;
                    }
                }

                break;
            case 1:
                //Debug.Log("CHANCE!");
                ac.PlaySound(SoundEnum.chanceSlotSound);
                cc.RandomiseChance(playerIndex);
                break;
            case 2:
                if(!hasMinigame)
                {
                    hasMinigame = true;
                    mgc.playedMinigameThisRound = true;
                    mgc.StartCoroutine(mgc.Instructions(playerIndex));
                    //mgc.SelectPlayer(playerIndex);
                    ac.PlaySound(SoundEnum.minigameSlotSound);

                }
                break;
            default:
                Debug.Log("Something went wrong in sorting playerActions!");
                break;
        }

        Destroy(effect, 5);
    }
}