using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.Scripts.Creatures
{
    public class HeroFallState : StateMachineBehaviour
    {
        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var hero = animator.GetComponent<Hero>();

        //    hero.SpawnFallDust();
        }
    }
}

