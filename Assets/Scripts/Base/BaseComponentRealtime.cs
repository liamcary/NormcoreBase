using Normal.Realtime;
using System;

public abstract class BaseComponentRealtime<TModelBase, TModelRealtime> : RealtimeComponent<TModelRealtime>
	 where TModelRealtime : RealtimeModel, new()
{
	public event Action OnModelArrived;

	protected TModelBase _offlineModel;

	public virtual void SetOfflineModel(TModelBase model)
	{
		_offlineModel = model;
	}

	protected override void OnRealtimeModelReplaced(TModelRealtime previousModel, TModelRealtime currentModel)
	{
		if (previousModel != null) {
			UnregisterEvents(previousModel);
		}

		if (currentModel != null) {
			if (currentModel.isFreshModel) {
				InitializeLocalValues(currentModel, _offlineModel);
			}

			RegisterEvents(currentModel);
			OnModelArrived?.Invoke();
		}
	}

	protected abstract void InitializeLocalValues(TModelRealtime freshModel, TModelBase offlineModel);
	protected abstract void RegisterEvents(TModelRealtime newModel);
	protected abstract void UnregisterEvents(TModelRealtime oldModel);
}