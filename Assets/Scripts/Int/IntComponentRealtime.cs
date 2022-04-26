using System;

public class IntComponentRealtime : BaseComponentRealtime<IIntModel, IntModelRealtime>, IIntModel
{
	public int Value
	{
		get => model.value;
		set => model.value = value;
	}

	public event Action<int> OnValueChanged;

	protected override void InitializeLocalValues(IntModelRealtime freshModel, IIntModel offlineModel)
	{
		freshModel.value = offlineModel.Value;
	}

	protected override void RegisterEvents(IntModelRealtime newModel)
	{
		newModel.valueDidChange += HandleValueChanged;
	}

	protected override void UnregisterEvents(IntModelRealtime oldModel)
	{
		oldModel.valueDidChange -= HandleValueChanged;
	}

	void HandleValueChanged(IntModelRealtime model, int newValue)
	{
		OnValueChanged?.Invoke(newValue);
	}
}