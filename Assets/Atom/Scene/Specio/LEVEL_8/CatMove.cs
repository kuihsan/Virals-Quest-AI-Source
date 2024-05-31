using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [Header("Player Object")]
    public string BoyPlayerName = "Boy.CharacterController";
    public string GirlPlayerName = "Girl.CharacterController";
    public GameObject PlayerObject;

    [Header("Cat Object")]
    public Animator TargetAnimator;
    public string IdleName;
    public string RunName;
    public float MinRange;
    public float Speed = 5;

    // Start is called before the first frame updaif ()te
    void Start()
    {
        
    }

    void CatRunAway()
    {
        if (TargetAnimator.GetAnimatorTransitionInfo(0).IsName(RunName))
        {
            TargetAnimator.Play(RunName);
        }
        transform.rotation = PlayerObject.transform.rotation;
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerObject == null)
        {
            PlayerObject = GameObject.Find(BoyPlayerName);
            if (PlayerObject == null)
            {
                PlayerObject = GameObject.Find(GirlPlayerName);
            }
        }

        if (PlayerObject != null)
        {
            float Range = Vector3.Distance(transform.position, PlayerObject.transform.position);
            if (Range < MinRange)
            {
                CatRunAway();
            } else
            {
                if (TargetAnimator.GetAnimatorTransitionInfo(0).IsName(IdleName))
                {
                    transform.LookAt(PlayerObject.transform.position);
                    TargetAnimator.Play(IdleName);
                }
            }
        }
    }
}
