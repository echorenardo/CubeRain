using System;
using TMPro;
using UnityEngine;

public class CountViewer : MonoBehaviour
{
    private const string TotalNumberText = "Общее количество:";
    private const string ActiveNumberText = "Количество активных:";
    private const string Separator = " ";

    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _title;

    private int _totalNumber = 0;
    private int _activeNumber;

    private event Action Changed;

    private void OnEnable()
    {
        _spawner.Spawned += SetTotalNumber;
        Changed += SetText;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= SetTotalNumber;
        Changed -= SetText;
    }

    private void Update() => SetActiveNumber(_spawner.GetTotalActive());

    protected void SetTitle(string name)
    {
        _title = name;
        Changed?.Invoke();
    }

    protected void SetTotalNumber(SpawnObject spawnObject)
    {
        _totalNumber++;
        Changed?.Invoke();
    }

    protected void SetActiveNumber(int number)
    {
        _activeNumber = number;
        Changed?.Invoke();
    }

    private void SetText() => _text.text = _title + "\n" + TotalNumberText + Separator + _totalNumber + "\n" + ActiveNumberText + Separator + _activeNumber;
}