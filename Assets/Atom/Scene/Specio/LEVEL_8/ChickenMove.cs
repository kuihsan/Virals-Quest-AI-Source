using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{
    [Header("Animation")]
    public Animator AnimatorObject;
    public string Play;
    public string Idle;
    
    [Header("Interval")]
    public int Interval;
    public bool isWalk = false;
    public float Speed = 0.1f;
    public float Degree;
    public int Direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        Degree = transform.eulerAngles.y;
        InvokeRepeating("SetWalk", 1, Interval);
    }

    void SetWalk()
    {
        isWalk = !isWalk;
        transform.rotation = Quaternion.Euler(0, Degree, 0);
        if (isWalk)
        {
            AnimatorObject.Play(Play);
        }
        else if (!isWalk)
        {
            AnimatorObject.Play(Idle);
            Direction *= 1;
            Degree += transform.eulerAngles.y + 45;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalk)
        {
            transform.Translate(Vector3.forward * Direction * Speed * Time.deltaTime);
        }
    }
}
