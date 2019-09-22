using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   /* i think gameManager/BattleManager can handle this.... idk if we need this script....
    also tho find away to action with the animator (need to remeber how to use it)*/

   public GameObject ActionPanle;

   public void actionPanleControl()
   {
       if (ActionPanle != null)
       {
           Animator actionPanel = ActionPanle.GetComponent<Animator>();
           if (actionPanel != null)
           {
               bool playerTurn = actionPanel.GetBool("IsPlayerTurn");
               actionPanel.SetBool("IsPlayerTurn",!playerTurn);
           }
       }
   }

}
