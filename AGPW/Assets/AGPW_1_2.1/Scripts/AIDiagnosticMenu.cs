using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using SimpleSignalSystem;
using System.Collections.Generic;

namespace AGPW
{

	public class AIDiagnosticMenu 
		: MonoBehaviour
		, SignalListener<ShowAIDiagnosticMenuSignal> 
	{
		public int LineSize = 20;
		public int LetterSize = 8;
		public Vector4 Margin = new Vector4(20f,10f,10f,10f);
		public int ScreenMargin = 10;
		// values
		private GameObject selectedObject = null;
		// state
		private bool showMenu;

		private Rect scrollWindow;
		private Rect scrollInnerWindow;
		private int scrollWindowTextSize;
		private Vector2 scrollPosition = Vector2.zero;
		private List<MemberInfo> currentMemberInfo = new List<MemberInfo>();
		private AIDiagnosticState state;
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
			SetCurrentMemberInfo();
		}

		private void SetCurrentMemberInfo()
		{
			this.currentMemberInfo.Clear();
			if (this.selectedObject == null)
			{
				return;
			}
			
			this.state = this.selectedObject.GetComponent<AIDiagnosticState>();
			if (this.state == null)
			{
				return;
			}
			
			Type type = typeof(AIDiagnosticState);
			MemberInfo[] infos = type.GetMembers();
			
			Type monoType = typeof(MonoBehaviour);
			MemberInfo[] monoInfos = monoType.GetMembers();
			
			foreach (MemberInfo info in infos)
			{
				if (!Array.Exists<MemberInfo>(monoInfos,element => element.Name == info.Name))
				{
					if (info.MemberType == MemberTypes.Field)
					{
						this.currentMemberInfo.Add(info);
					}
				}
			}
		}

		void OnGUI()
		{
			if (this.showMenu)
			{
				string windowTile = "Debug";
				if(this.selectedObject != null)
				{
					windowTile += " - " + this.selectedObject.name;
				}
				this.scrollWindowTextSize = 0;
				Rect windowRect = GetWindowSize(windowTile);
				GUI.Window(0, windowRect, ShowWindow, windowTile);
			}
		}

		private Rect GetWindowSize(string title)
		{
			this.scrollInnerWindow = new Rect(0,0,0,0);
			AddObjectStateSize();
			AddCommandSize(3);

			float windowWidth = this.scrollInnerWindow.width;
			if(this.scrollInnerWindow.width < title.Length*this.LetterSize)
			{
				windowWidth = title.Length*this.LetterSize;
			}
			int screenHeight = Screen.height - 2 * this.ScreenMargin;
			int screenWidth = Screen.width - 2 * this.ScreenMargin;
			
			Rect windowRect = new Rect(0,0,windowWidth + Margin.y + Margin.w, this.scrollInnerWindow.height + Margin.x + Margin.z);
			if (screenWidth  < windowRect.width)
			{
				windowRect.width = screenWidth;
			}
			windowRect.x = (screenWidth - windowRect.width) / 2 + this.ScreenMargin;

			if (screenHeight  < windowRect.height)
			{
				windowRect.height = screenHeight;
			}
			windowRect.y = (screenHeight - windowRect.height) / 2 + this.ScreenMargin;

			this.scrollWindow = new Rect(Margin.w, Margin.x, windowRect.width - (Margin.y + Margin.w),windowRect.height - (Margin.x + Margin.z));

			return windowRect;
		}

		private void AddObjectStateSize()
		{
			foreach (MemberInfo info in this.currentMemberInfo)
			{
				//Debug.Log(info.Name + ": " + ((FieldInfo)info).FieldType);
				this.scrollInnerWindow.height += LineSize;
				int currentScrollWindowTextSize = info.Name.Length * LetterSize;
				if (currentScrollWindowTextSize > this.scrollWindowTextSize)
				{
					this.scrollWindowTextSize = currentScrollWindowTextSize;
					this.scrollInnerWindow.width = this.scrollWindowTextSize + this.LineSize;
				}
			}
		}

		private void AddCommandSize(int i)
		{
			this.scrollInnerWindow.height += i * this.LineSize;
		}
		
		private void ShowWindow(int id)
		{
			this.scrollPosition = GUI.BeginScrollView (this.scrollWindow,
			                                           this.scrollPosition,
			                                           this.scrollInnerWindow);
			{
				int yPos = 0;
				yPos += ShowObjectState(yPos);
				yPos += AddCommand(yPos);
			}
			GUI.EndScrollView ();
		}

		private int ShowObjectState(int yPos)
		{
			foreach (MemberInfo info in this.currentMemberInfo)
			{
				//Debug.Log(info.Name + ": " + ((FieldInfo)info).FieldType);
				GUI.Label(new Rect (0, yPos, this.scrollWindowTextSize, this.LineSize), info.Name);
				AddObjectState(info, yPos);
				yPos += this.LineSize;
			}

			return yPos;
		}

		private int AddCommand(int yPos)
		{
			if(this.selectedObject != null)
			{
				if (GUI.Button(new Rect (0, yPos, this.scrollWindowTextSize, this.LineSize), "Reset")) 
				{
					SignalSystem.SignalTriggered(new ResetObjectSignal(this.selectedObject));
				}
				yPos += this.LineSize;
				if (GUI.Button(new Rect (0, yPos, this.scrollWindowTextSize, this.LineSize), "Delete")) 
				{
					this.showMenu = false;
					Destroy(this.selectedObject);
					this.selectedObject = null;
				}
				yPos += this.LineSize;

			}

			if (GUI.Button(new Rect (0, yPos, this.scrollWindowTextSize, this.LineSize), "OK")) 
			{
				this.showMenu = false;
			}
			yPos += this.LineSize;
			return yPos;
		}

		private void AddObjectState(MemberInfo info, int yPos)
		{
			FieldInfo fieldInfo = (FieldInfo)info;
			if (fieldInfo.FieldType == typeof(Boolean))
			{
				bool value = (bool)fieldInfo.GetValue(this.state);
				value = GUI.Toggle( new Rect(this.scrollWindowTextSize, yPos, this.LineSize, this.LineSize), value, "");
				fieldInfo.SetValue(this.state,value);
			}
		}
	}

}