using System;

public interface IIntModel
{
	int Value { get; set; }

	event Action<int> OnValueChanged;
}