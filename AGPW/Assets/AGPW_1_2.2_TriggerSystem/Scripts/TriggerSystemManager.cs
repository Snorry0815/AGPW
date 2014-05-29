using UnityEngine;
using System.Collections;
using System;

public class TriggerSystemManager : MonoBehaviour 
{

	public Type triggerType;

	private ITriggerSystem triggerSystem;

	// Use this for initialization
	void Start () 
	{
		triggerSystem = new TriggerSystem<TriggerType>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
