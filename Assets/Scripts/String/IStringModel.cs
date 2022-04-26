using System;

public interface IStringModel
{
	string Value { get; set; }

	event Action<string> OnValueChanged;
}