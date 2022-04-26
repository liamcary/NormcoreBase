using System;
using UnityEngine;

[Serializable]
public class StringModel : IStringModel
{
	public string Value
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

	public event Action<string> OnValueChanged;

	[SerializeField]
	string _value;
}
