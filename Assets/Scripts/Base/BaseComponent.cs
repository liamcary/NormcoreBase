using Normal.Realtime;
using UnityEngine;

public abstract class BaseComponent<TModelBase, TModelOffline, TModelRealtime, TComponentRealtime> : MonoBehaviour
	where TModelOffline : TModelBase, new()
	where TModelRealtime : RealtimeModel, new()
	where TComponentRealtime : BaseComponentRealtime<TModelBase, TModelRealtime>, TModelBase
{
	public TModelBase Model { get; private set; }

	[SerializeField] protected TModelOffline _offlineModel;

	protected TComponentRealtime _realtimeComponent;

	protected virtual void Awake()
	{
		if (TryGetComponent(out _realtimeComponent)) {
			Model = _realtimeComponent;
			_realtimeComponent.SetOfflineModel(_offlineModel);
			_realtimeComponent.OnModelArrived += HandleModelArrived;
		} else {
			Model = _offlineModel;
		}
	}

	protected virtual void OnDestroy()
	{
		if (_realtimeComponent != null) {
			_realtimeComponent.OnModelArrived -= HandleModelArrived;
		}
	}

	protected virtual void HandleModelArrived()
	{
	}
}