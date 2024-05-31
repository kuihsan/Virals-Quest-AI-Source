using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDistance : MonoBehaviour
{
	public GameObject SpawnPoint;
	public GameObject Player;
	public Slider GameSlider;
	
	[Header("Status")]
	public float CurrentDistance;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDistance = Vector3.Distance(SpawnPoint.transform.position, Player.transform.position);
		GameSlider.value = CurrentDistance;
    }
}
