public class StringComponent : BaseComponent<IStringModel, StringModel, StringModelRealtime, StringComponentRealtime>
{
	protected override void Awake()
	{
		base.Awake();

		Model.OnValueChanged += HandleValueChanged;
	}

	protected override void OnDestroy()
	{
		Model.OnValueChanged -= HandleValueChanged;
	}

	protected override void HandleModelArrived()
	{
		gameObject.name = Model.Value;
	}

	void HandleValueChanged(string value)
	{
		gameObject.name = value;
	}
}