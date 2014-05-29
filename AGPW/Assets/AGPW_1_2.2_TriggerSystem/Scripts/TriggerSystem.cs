using UnityEngine;
using System.Collections;
using System;

public class TriggerSystem<T> : ITriggerSystem where T : struct, IConvertible
{
	static TriggerSystem<T> instance;
}