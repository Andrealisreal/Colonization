using Bases;
using Resources;
using Units;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private UnitBuilder _unitBuilder;
    [SerializeField] private ResourceHandler _resourceHandler;
    [SerializeField] private Transform _startPointBase;

    private void Awake() =>
        _baseSpawner.Initialize(_resourceHandler);

    private void Start() =>
        _baseSpawner.Spawn(_startPointBase.position, true);
}