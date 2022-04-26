using System;

public class StringComponentRealtime : BaseComponentRealtime<IStringModel, StringModelRealtime>, IStringModel
{
	public string Value
	{
		get => model.value;
		set => model.value = value;
	}

	public event Action<string> OnValueChanged;

	protected override void InitializeLocalValues(StringModelRealtime freshModel, IStringModel offlineModel)
	{
		freshModel.value = offlineModel.Value;
	}

	protected override void RegisterEvents(StringModelRealtime newModel)
	{
		newModel.valueDidChange += HandleValueChanged;
	}

	protected override void UnregisterEvents(StringModelRealtime oldModel)
	{
		oldModel.valueDidChange -= HandleValueChanged;
	}

	void HandleValueChanged(StringModelRealtime model, string value)
	{
		OnValueChanged?.Invoke(value);
	}
}