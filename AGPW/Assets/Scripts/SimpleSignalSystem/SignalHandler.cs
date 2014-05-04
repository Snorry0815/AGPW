using UnityEngine;
using System.Collections;
using System;

namespace SimpleSignalSystem
{

public class SignalHandler<S>:ISignalHandler where S:ISignal 
{
	public delegate void SignalHandlerDelegate(S signal);
    public event SignalHandlerDelegate signalEvent;
	
	public SignalHandler()
	{}
	
	public void AddListener(ISignalListener iListener)
	{
		SignalListener<S> listener = (SignalListener<S>)iListener; 
		this.signalEvent += listener.SignalTrigered;
	}

	public void RemoveListener(ISignalListener iListener)
	{
		SignalListener<S> listener = (SignalListener<S>)iListener; 
		this.signalEvent -= listener.SignalTrigered;
	}
	
	public void SignalTriggered(ISignal iSignal)
	{
		Type type = iSignal.GetType();
		if(type != typeof(S))
		{
			Debug.LogWarning("Got wrong Signal handler(" + typeof(S) + ") for type ("+type+")");
			return;
		}
		S signal = (S)iSignal; 
		SignalTriggered(signal);
	}
	
	private void SignalTriggered(S signal)
	{
		if(signalEvent != null)
		{
			signalEvent(signal);
		}
	}	
}

}


