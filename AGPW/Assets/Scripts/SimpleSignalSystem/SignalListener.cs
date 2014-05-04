using UnityEngine;
using System.Collections;

namespace SimpleSignalSystem
{

public interface SignalListener<S>:ISignalListener where S:ISignal
{
	void SignalTrigered(S signal);
}

}