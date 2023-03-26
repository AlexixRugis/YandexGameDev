using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Serializable]
    public struct WorldGenerationLayer
    {
        public string Name;
        public Spawnable FirstPrefab;
        public Spawnable[] Spawnables;
        public float MovementSpeed;
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _background;

    [SerializeField] private WorldGenerationLayer[] _generationLayers;

    public float GameSpeedMultiplier { get; set; } = 0;

    private List<Spawnable>[] _generated;


    private void Start()
    {
        _generated = new List<Spawnable>[_generationLayers.Length];
        for (int i = 0; i < _generated.Length; i++)
        {
            _generated[i] = new List<Spawnable>();
        }
    }

    private void Update()
    {
        HandleBackgroundSize();
        HandleLayers();
    }

    private void HandleBackgroundSize()
    {
        Vector2 bgSize = _background.sprite.bounds.extents;
        Vector2 cameraSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);

        _background.transform.localScale = new Vector3(cameraSize.x / bgSize.x, cameraSize.y / bgSize.y, 1);
    }

    private Spawnable GetSpawnable(int layerIndex)
    {
        WorldGenerationLayer layer = _generationLayers[layerIndex];
        if (_generated[layerIndex].Count == 0 && layer.FirstPrefab != null)
        {
            return layer.FirstPrefab;
        }

        return layer.Spawnables[UnityEngine.Random.Range(0, layer.Spawnables.Length)];
    }

    private void HandleLayers()
    {
        float cameraRightBorder = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float cameraLeftBorder = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;

        for (int li = 0; li < _generationLayers.Length; li++)
        {
            WorldGenerationLayer layer = _generationLayers[li];
            List<Spawnable> spawned = _generated[li];

            while (spawned.Count == 0 || spawned[spawned.Count - 1].RightBorder <= cameraRightBorder)
            {
                Spawnable spawnablePrefab = GetSpawnable(li);
                Spawnable spawnableGO = Instantiate(spawnablePrefab,
                    new Vector3((spawned.Count == 0 ? cameraLeftBorder : spawned[spawned.Count - 1].RightBorder) + spawnablePrefab.Width / 2f, 0, 0),
                    Quaternion.identity,
                    transform);

                spawned.Add(spawnableGO);
            }



            while (spawned.Count > 0 && spawned[0].RightBorder < cameraLeftBorder)
            {
                Spawnable first = spawned[0];
                Destroy(first.gameObject);

                spawned.RemoveAt(0);
            }

            for (int i = 0; i < spawned.Count; i++)
            {
                spawned[i].transform.Translate(Vector2.left * layer.MovementSpeed * GameSpeedMultiplier * Time.deltaTime);
            }
        }
    }
}
