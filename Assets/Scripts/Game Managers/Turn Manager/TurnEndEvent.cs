using System;

public class TurnEndEvent : EventArgs
{
	public TurnManager.TurnStates Turn { get; private set; }

	public TurnEndEvent(TurnManager.TurnStates turn)
	{
		Turn = turn;
	}
}