using UnityEngine;
using System.Collections;
using SimpleSignalSystem;

namespace AGPW
{
	public class AIDiagnosticState : MonoBehaviour 
	{
			public bool Invulnerable;
			public bool StopedMovement;
			public bool Freeze;
			public bool Blind;
			public bool Insensate; // makes selected AI completly oblivious to all sensory inputss

			public AIDiagnosticState()
			{
				Reset();
			}

			public void Reset()
			{
				Invulnerable = false;
				StopedMovement = false;
				Freeze = false;
				Blind = false;
				Insensate = false;
			}

			void OnMouseDown() 
			{
				if(AIDiagnosticGameState.Instance().IsDebug())
				{
					SignalSystem.SignalTriggered(new ShowAIDiagnosticMenuSignal(this.transform.gameObject));
				}
			}
	}
}