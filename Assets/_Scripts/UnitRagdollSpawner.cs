using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBone;
    public GameObject StickmanVisualGO;
    private bool ragdollSpawned = false;

    private void Start()
    {
        PlayerStacking.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        PlayerStacking.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        if (ragdollSpawned)
            return;

        ragdollSpawned = true;
        StickmanVisualGO.SetActive(false);

        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(originalRootBone);
        
    }
}