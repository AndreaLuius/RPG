using UnityEngine;
//do not used yet
namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        public void startAction(IAction action)
        {
            if(currentAction == action) return;
            
            if(currentAction != null)
                currentAction.stopExecution();
            currentAction = action;
        }
    }
}