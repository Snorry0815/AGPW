using UnityEngine;
using System.Collections;
using System;

public class EnumLogic 
{
	public static int EnumSize<T>() where T:struct
	{
		if (!typeof(T).IsEnum) 
		{
			Debug.LogError("MenuLogic.EnumSelector: not an enum " + typeof(T));
		}

		return Enum.GetValues(typeof(T)).Length;
	}
}
