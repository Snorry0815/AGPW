using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleSignalSystem
{

	public class SignalSystem {
		
		private static SignalSystem instance  = null;
		
		public static SignalSystem GetInstance()
		{
			if(instance == null)
			{
				instance = new SignalSystem();
			}
			return instance;	
		}
		
		public static void AddListener<S>(ISignalListener signalListener) where S:ISignal
		{
			if(signalListener == null)
			{
				Debug.LogWarning("SignalListener is null!");
				return;
			}
			SignalSystem signalSystem = GetInstance();
			ISignalHandler sign = signalSystem.GetSignal<S>();
			sign.AddListener(signalListener);
		}

		public static void RemoveListener<S>(ISignalListener signalListener) where S:ISignal
		{
			if(signalListener == null)
			{
				Debug.LogWarning("SignalListener is null!");
				return;
			}
			SignalSystem signalSystem = GetInstance();
			ISignalHandler sign = signalSystem.GetSignal<S>();
			sign.RemoveListener(signalListener);
		}
		
		public static void SignalTriggered(ISignal signal)
		{
			if(signal == null)
			{
				Debug.LogWarning("Signal is null!");
				return;
			}
			SignalSystem signalSystem = GetInstance();
			signalSystem.SignalTriggeredImpl(signal);
		}

		private Dictionary<System.Type, ISignalHandler> delegations = new Dictionary<System.Type, ISignalHandler>();
		private SignalSystem()
		{}
		
		private void SignalTriggeredImpl(ISignal signal)
		{
			System.Type signalType = signal.GetType();
			if(delegations.ContainsKey(signalType))
			{
				ISignalHandler signalHandler = delegations[signalType];
				if(signalHandler != null)
				{
					signalHandler.SignalTriggered(signal);
				}
			}
		}
		
		public ISignalHandler GetSignal<S>() where S:ISignal
		{
			System.Type signalType = typeof(S);
			ISignalHandler signal;
			if(delegations.ContainsKey(signalType))
			{
				signal = delegations[signalType];
			}
			else
			{
				signal = new SignalHandler<S>();
				delegations[signalType] = signal;
			}
			return signal;
		}
	}
}
