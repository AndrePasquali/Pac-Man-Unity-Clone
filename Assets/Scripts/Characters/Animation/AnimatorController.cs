using UnityEngine;

namespace Aquiris.PacMan.Characters.Animation
{
    public static class AnimatorController
    {
        public static void SetBool(Animator animator, string parameterName, bool value)
        {
            animator.SetBool(parameterName, value);
        }

        public static void SetTrigger(Animator animator, string parameterName)
        {
            animator.SetTrigger(parameterName);
        }

        public static void SetFloat(Animator animator, string paramaterName, float value)
        {
            animator.SetFloat(paramaterName, value);
        }

        public static void SetAnimatorSpeed(Animator animator, float newSpeed)
        {
            animator.speed = newSpeed;
        }
       
    }
}