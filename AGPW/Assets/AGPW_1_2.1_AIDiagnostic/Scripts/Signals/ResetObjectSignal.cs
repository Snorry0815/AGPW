using UnityEngine;
using System.Collections;
using SimpleSignalSystem;

namespace AGPW
{
	public class ResetObjectSignal  : ISignal
	{
		private GameObject selectedObject;
		public GameObject SelectedObject 
		{
			get
			{
				return this.selectedObject;
			}
		}
		
		public ResetObjectSignal(GameObject selectedObject)
		{
			this.selectedObject = selectedObject;
		}
	}
}