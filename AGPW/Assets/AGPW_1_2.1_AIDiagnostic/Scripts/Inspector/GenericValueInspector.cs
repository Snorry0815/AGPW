using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class GenericValueInspector : MonoBehaviour 
{
	private static string NAME = "GenericValueInspector";
	private static string TEXT_PREFAB_NAME = "TextPrefab_AGPW_1_2.1";

	public string[] componentNames;
	public string[] componentValueNames;
	
	private GameObject valueText = null;

	// Use this for initialization
	void Start () 
	{
		Init();
	}
	
	private void Init()
	{
		if(valueText != null)
		{
			Destroy(valueText);
		}
		this.valueText = (GameObject)Instantiate(Resources.Load(TEXT_PREFAB_NAME)) as GameObject;
		this.valueText.name = NAME;
		this.valueText.transform.parent = this.transform;
	}
	
	void Update () 
	{
		if(this.valueText != null)
		{
			this.valueText.transform.LookAt(2*transform.position - Camera.main.transform.position);
			String text = "";
			foreach (string componentName in componentNames)
			{
				Component component = this.GetComponent(componentName);
				if(component == null)
				{
					continue;
				}
				Type type = component.GetType();
				
				foreach (string componentValueName in componentValueNames)
				{
					MemberInfo[] infos = type.GetMember(componentValueName);
					
					foreach (MemberInfo info in infos)
					{
						if (info.MemberType == MemberTypes.Field)
						{
							FieldInfo fieldInfo = (FieldInfo)info;
							if (fieldInfo.FieldType == typeof(Boolean))
							{
								bool value = (bool)fieldInfo.GetValue(component);
								Debug.Log(value);
								text += componentValueName + ": " + value + "\n";
							}
						}
					}
				}
			}
			this.valueText.GetComponent<TextMesh>().text = text;
		}
	}
}
