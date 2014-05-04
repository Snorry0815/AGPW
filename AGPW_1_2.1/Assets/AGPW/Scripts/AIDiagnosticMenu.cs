using UnityEngine;
using System.Collections;
using System;
using SimpleSignalSystem;

namespace AGPW
{

	public class AIDiagnosticMenu 
		: MonoBehaviour
		, SignalListener<ShowAIDiagnosticMenuSignal> 
	{
		public int LineSize = 20;
		public Vector4 Margin = new Vector4(10f,10f,10f,10f);
		public int ScreenMargin = 10;
		// values
		private GameObject selectedObject = null;
		// state
		private bool showMenu;

		void Start () 
		{
			SignalSystem.AddListener<ShowAIDiagnosticMenuSignal>(this);
		}

		void OnDestroy() 
		{
			SignalSystem.RemoveListener<ShowAIDiagnosticMenuSignal>(this);
		}

		public void SignalTrigered(ShowAIDiagnosticMenuSignal signal)
		{
			this.selectedObject = signal.SelectedObject;
			this.showMenu = true;
		}

		void OnGUI()
		{
			if(this.showMenu)
			{
				string windowTile = "Debug";
				if(this.selectedObject != null)
				{
					windowTile += " - " + this.selectedObject.name;
				}
				Rect windowInsideRect = GetWindowSize();

				int screenHeight = Screen.height - 2 * this.ScreenMargin;
				int screenWidth = Screen.width - 2 * this.ScreenMargin;

				Rect windowRect = new Rect();

				if(screenWidth  > windowInsideRect.width)
				{
					windowRect.width = windowInsideRect.width;
				}
				else
				{
					windowRect.width = screenWidth;
				}
				windowRect.x = (screenWidth - windowRect.width) / 2 + this.ScreenMargin;
				if(screenHeight  > windowInsideRect.height)
				{
					windowRect.height = windowInsideRect.height;
				}
				else
				{
					windowRect.height = screenHeight;
				}
				windowRect.y = (screenHeight - windowRect.height) / 2 + this.ScreenMargin;

				GUI.Window(0, windowRect, ShowWindow, windowTile);
			}
		}

		private Rect GetWindowSize()
		{
			Rect windowRect = new Rect(0,0,Margin.y+Margin.w, Margin.x+Margin.z);
			windowRect = AddObjectStateSize(windowRect);
			return windowRect;
		}

		private Rect AddObjectStateSize(Rect windowRect)
		{
			if(this.selectedObject == null)
			{
				return windowRect;
			}

			AIDiagnosticState state = this.selectedObject.GetComponent<AIDiagnosticState>();
			if(state == null)
			{
				return windowRect;
			}

			Type type = state.GetType();

			windowRect.height += type.GetProperties().Length * LineSize;
			return windowRect;
		}

		private void ShowWindow(int id)
		{
			int yPos = 0;
			yPos += ShowObjectState(yPos);
		}

		private int ShowObjectState(int yPos)
		{
			if(this.selectedObject == null)
			{
				return 0;
			}

			return 0;
		}
	}

}