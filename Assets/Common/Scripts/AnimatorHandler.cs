using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [SerializeField]
    private int animSize;

    private List<string> StartString = new List<string>();
    private string EndString = "EndAnim";
    private string StartPrefix = "Step";

    private int animIndex;

    private Animator animator;
    private bool isAnimFinished = false;

    public bool clickedOnObject = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        foreach (var i in Enumerable.Range(0, animSize))
        {
            StartString.Add(StartPrefix + i);
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (clickedOnObject)
            {
                if (animIndex == StartString.Count)
                {
                    isAnimFinished = true;
                    Debug.Log("Run finigh animation");
                    animator.SetTrigger(EndString);
                }
                if (!isAnimFinished)
                {
                    animIndex %= StartString.Count;
                    animator.SetTrigger(StartString[animIndex]);
                    animIndex++;
                }
                else
                {
                    animIndex %= StartString.Count;
                    isAnimFinished = false;
                }

                clickedOnObject = false;
            }
        }
    }
}
