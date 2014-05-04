using UnityEngine;
using System.Collections;

namespace SimpleSignalSystem
{

public interface ISignalHandler
{
	void AddListener(ISignalListener listener);
	void RemoveListener(ISignalListener listener);
	void SignalTriggered(ISignal iSignal);
}

}
