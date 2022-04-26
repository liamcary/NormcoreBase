using System;

public class IntModel : IIntModel
{
	int _value;

	public int Value
	{
		get => _value;
		set
		{
			if (_value == value) {
				return;
			}

			_value = value;
			OnValueChanged?.Invoke(_value);
		}
	}

	public event Action<int> OnValueChanged;
}