using UnityEngine;

public interface ILeveledEntity : IDamageSource
{
	int CurrentEXP { get; set; }
	int CurrentLevel { get; set; }

	System.Action OnLevelUp { get; set; }

	public void Initialize(int startingLevel = 1)
	{
		CurrentLevel = startingLevel;
		OnKill += AddEXPOnKill;
	}

	private void AddEXPOnKill(IDamageable killedEntity)
	{
		if (killedEntity is not IEXP_Source expSource)
		{
			return;
		}

		AddEXP(expSource.EXPReward);
	}

	public void AddEXP(int exp)
	{
		CurrentEXP = Mathf.Max(0, CurrentEXP + exp);
		TryLevelUp();
	}

	private void TryLevelUp()
	{
		int expToNextLevel = GetEXPNeededToLevelUP(CurrentLevel);
		if (CurrentEXP < expToNextLevel)
		{
			return;
		}

		CurrentEXP -= expToNextLevel;
		CurrentLevel++;
		OnLevelUp?.Invoke();
	}


	public static int GetEXPNeededToLevelUP(int level) => level * 500;
}
