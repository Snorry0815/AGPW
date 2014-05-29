using UnityEngine;
using System.Collections;

namespace AGPW
{
	public class AIDiagnosticInputListener : MonoBehaviour 
	{
		public KeyCode enableKey;

		void Update () 
		{
			if (Input.GetKeyDown(enableKey)) 
			{
				AIDiagnosticGameState instance = AIDiagnosticGameState.Instance();
				switch(instance.GlobalGameState)
				{
				case GlobalGameState.RUNNING:
					instance.GlobalGameState = GlobalGameState.DEBUG;
					Debug.Log("***** Enable debug mode! *****");
					break;
				case GlobalGameState.DEBUG:
				default:
					instance.GlobalGameState = GlobalGameState.RUNNING;
					Debug.Log("***** Disable debug mode! *****");
					break;
				}
			}
		}
	}
}