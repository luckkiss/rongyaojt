using System;

public class TaskMonId
{
	public int value;

	public bool applied;

	public static implicit operator int(TaskMonId taskMonId)
	{
		return taskMonId.value;
	}

	public static implicit operator TaskMonId(int val)
	{
		return new TaskMonId
		{
			value = val,
			applied = true
		};
	}
}
