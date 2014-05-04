﻿using UnityEngine;
using System.Collections;
using SimpleSignalSystem;

namespace AGPW
{
	public class ShowAIDiagnosticMenuSignal : ISignal
	{
		private GameObject selectedObject;
		public GameObject SelectedObject 
		{
			get
			{
				return this.selectedObject;
			}
		}

		public ShowAIDiagnosticMenuSignal(GameObject selectedObject)
		{
			this.selectedObject = selectedObject;
		}
	}
}