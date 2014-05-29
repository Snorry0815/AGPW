using UnityEngine;
using System.Collections;

public enum TriggerType : ulong  
{
	NONE = 0uL,
	EXPLOSION = (1uL << 0),
	ENEMY_NEAR = (1uL << 1)
}
