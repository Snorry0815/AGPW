using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	// Update is called once per frame
	void Update () 
	{
		this.transform.LookAt(2*transform.position - Camera.main.transform.position);
	}
}
