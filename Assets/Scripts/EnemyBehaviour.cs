using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ICommand
{
	public EnemyCommand[] enemyCommands;

	private int _currentCommandIndex;

	public bool Execute()
	{
		if (enemyCommands.Length <= 0)
			return false;

		// If the command executes correctly, increase the index for the next command
		if (enemyCommands[_currentCommandIndex].Execute())
		{
			_currentCommandIndex++;

			if (_currentCommandIndex >= enemyCommands.Length)
				_currentCommandIndex = 0;
		}

		return true;
	}
}