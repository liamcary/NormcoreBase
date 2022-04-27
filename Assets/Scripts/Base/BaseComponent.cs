using Normal.Realtime;
using System.Linq;
using UnityEngine;

public abstract class BaseComponent<TModelBase, TModelOffline, TModelRealtime, TComponentRealtime> : MonoBehaviour
	where TModelOffline : TModelBase, new()
	where TModelRealtime : RealtimeModel, new()
	where TComponentRealtime : BaseComponentRealtime<TModelBase, TModelRealtime>, TModelBase
{
	public TModelBase Model { get; private set; }
	protected bool IsOnline => _realtimeComponent != null;

	public int OwnerIdSelf => IsOnline ? _realtimeComponent.ownerIDSelf : 0;
	public int OwnerIdInHierarchy => IsOnline ? _realtimeComponent.ownerIDInHierarchy : 0;
	public bool IsUnownedSelf => IsOnline && _realtimeComponent.isUnownedSelf;
	public bool IsUnownedInHierarchy => IsOnline && _realtimeComponent.isUnownedInHierarchy;
	public bool IsOwnedLocallySelf => !IsOnline || _realtimeComponent.isOwnedLocallySelf;
	public bool IsOwnedLocallyInHierarchy => !IsOnline || _realtimeComponent.isOwnedLocallyInHierarchy;
	public bool IsOwnedRemotelySelf => IsOnline && _realtimeComponent.isOwnedRemotelySelf;
	public bool IsOwnedRemotelyInHierarchy => IsOnline && _realtimeComponent.isOwnedRemotelyInHierarchy;

	[SerializeField] protected TModelOffline _offlineModel;

	protected TComponentRealtime _realtimeComponent;

	protected virtual void Awake()
	{
		bool isConnected = Realtime.instances.Any(r => r.connected);

		if (TryGetComponent(out _realtimeComponent)) {
			if (isConnected) {
				_realtimeComponent.SetOfflineModel(_offlineModel);
				_realtimeComponent.OnModelArrived += HandleModelArrived;
			} else {
				var realtimeView = _realtimeComponent.realtimeView;
				Destroy(_realtimeComponent);
				Destroy(realtimeView);

				isConnected = false;
				_realtimeComponent = null;
			}
		}

		Model = _realtimeComponent != null ? _realtimeComponent : _offlineModel;
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

	public void RequestOwnership()
	{
		if (_realtimeComponent != null) {
			_realtimeComponent.RequestOwnership();
		}
	}

	public void SetOwnership(int ownerId)
	{
		if (_realtimeComponent != null) {
			_realtimeComponent.SetOwnership(ownerId);
		}
	}

	public void ClearOwnership()
	{
		if (_realtimeComponent != null) {
			_realtimeComponent.ClearOwnership();
		}
	}
}