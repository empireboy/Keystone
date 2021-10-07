using System;

public class TurnStartEvent : EventArgs
{
	public TurnManager.TurnStates Turn { get; private set; }

	public TurnStartEvent(TurnManager.TurnStates turn)
	{
		Turn = turn;
	}
}