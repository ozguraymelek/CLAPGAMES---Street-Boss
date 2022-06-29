using UnityEngine;
using System.Collections;
public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;
    private void Awake()
    {
        instance = this;
    }

    public static void SetFloat(Animator animator, string key, float value)
    {
        animator.SetFloat(key, value);
    }
    public static void SetTrigger(Animator animator, string key)
    {
        animator.SetTrigger(key);
    }
    public static void SetBool(Animator animator, string key, bool state)
    {
        animator.SetBool(key,state);
    }
    public static void SetLayerWeight(Animator animator, string key, bool state)
    {
        animator.SetBool(key, state);
    }
}
