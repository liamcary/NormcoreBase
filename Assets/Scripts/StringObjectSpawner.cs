using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using static Normal.Realtime.Realtime;

public class StringObjectSpawner : MonoBehaviour
{
	public Realtime Realtime;
	public StringComponent OfflinePrefab;
	public StringComponent OnlinePrefab;

	void Update()
	{
		if (Keyboard.current.enterKey.wasPressedThisFrame) {
			Spawn();
		}

		if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
			Spawn("Space");
		}
	}

	void Spawn(string name = null)
	{
		StringComponent instance;

		if (Realtime.connected) {
			var gameObject = Realtime.Instantiate(OnlinePrefab.name, InstantiateOptions.defaults);
			instance = gameObject.GetComponent<StringComponent>();
		} else {
			instance = Instantiate(OfflinePrefab);
		}

		if (!string.IsNullOrEmpty(name)) {
			instance.Model.Value = name;
		}
	}
}
