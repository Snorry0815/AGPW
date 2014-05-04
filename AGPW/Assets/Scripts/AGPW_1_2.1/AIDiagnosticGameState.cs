using UnityEngine;
using System.Collections;

namespace AGPW
{
	public enum GlobalGameState
	{
		RUNNING,
		DEBUG
	}

	public class AIDiagnosticGameState
	{
		private static AIDiagnosticGameState instance = null;
		public static AIDiagnosticGameState Instance()
		{
			if(instance == null)
			{
				instance = new AIDiagnosticGameState();
			}
			return instance;
		}

		public GlobalGameState GlobalGameState{set;get;}

		private AIDiagnosticGameState()
		{
			this.GlobalGameState = GlobalGameState.RUNNING;
		}

		public bool IsDebug()
		{
			return this.GlobalGameState == GlobalGameState.DEBUG;
		}
	}
}
