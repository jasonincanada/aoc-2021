## Discussion

In this challenge we pilot a very basic submarine with a `horiz`, `depth`, and `aim`, with somewhat different operations depending on the command/part combo

My original, least complicated version:

```cs
public int Part1()
{
	int horiz = 0;
	int depth = 0;            

	foreach ((string command, int amount) in _parsed)
	{
		switch (command)
		{
			case "forward":
				horiz += amount;
				break;

			case "down":
				depth += amount;
				break;

			case "up":
				depth -= amount;
				break;

			default:
				throw new InvalidOperationException($"Unknown command {command}");
		}                
	}

	return horiz * depth;
}
```

```cs
public int Part2()
{
	int horiz = 0;
	int depth = 0;
	int aim = 0;

	foreach ((string command, int amount) in _parsed)
	{
		switch (command)
		{
			case "forward":
				horiz += amount;
				depth += aim * amount;
				break;

			case "down":
				aim += amount;
				break;

			case "up":
				aim -= amount;
				break;

		default:
			throw new InvalidOperationException($"Unknown command {command}");
	}
}

return horiz * depth;
}
```

After a bit of refactoring to meld the two parts together:

```cs
int PilotSub(int part)
{
	int horiz = 0;
	int depth = 0;
	int aim = 0;

	foreach ((string command, int amount) in _parsed)
	{
		if (part == 1)
		{
			switch (command)
			{
				case "forward": horiz += amount; break;
				case "down":    depth += amount; break;
				case "up":      depth -= amount; break;
			}
		}

		if (part == 2)
		{
			switch (command)
			{
				case "forward":
					horiz += amount;
					depth += aim * amount;
					break;

				case "down": aim += amount; break;
				case "up":   aim -= amount; break;
			}
		}
	}

	return horiz * depth;
}
```

## Notes

- used C# 7.0 tuples for first time
- i almost created a separate Submarine class but it's a bit overkill for this puzzle. this was the second day in a row of submarine piloting though, so if it continues we'll probably end up doing that anyway
- this one could be functionalized a bit but it might end up being less legible than it is now

## Areas for Improvement

- the outer `foreach` loop with the part distinction within it could be inverted, so there'd be one `if` and two `foreach` statements.  once the part is 'decided' then there's no point in further checking which part it is in each loop
