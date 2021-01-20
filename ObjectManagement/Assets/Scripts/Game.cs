using System.Collections.Generic;
using UnityEngine;

public class Game : PersistableObject {
    [SerializeField]
    KeyCode createKey = KeyCode.C;
    [SerializeField]
    KeyCode newGameKey = KeyCode.N;
    [SerializeField]
    KeyCode loadKey = KeyCode.L;
    [SerializeField]
    KeyCode saveKey = KeyCode.S;
    [SerializeField]
    ShapeFactory shapeFactory;
    [SerializeField]
    PersistableStorage storage;

    const int saveVersion = 1;

    List<Shape> shapes;

    void Awake() {
        shapes = new List<Shape>();
    }

    void Update() {
        if (Input.GetKeyDown(createKey)) {
            Create();
        } else if (Input.GetKey(newGameKey)) {
            NewGame();
        } else if (Input.GetKeyDown(loadKey)) {
            NewGame();
            storage.Load(this);
        } else if (Input.GetKeyDown(saveKey)) {
            storage.Save(this);
        }
    }

    void NewGame() {
        for (int i = 0; i < shapes.Count; i++) {
            Destroy(shapes[i].gameObject);
        }
        shapes.Clear();
    }

    void Create() {
        Shape instance = shapeFactory.CreateRandom();
        Transform t = instance.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        shapes.Add(instance);
    }

    public override void Load(GameDataReader reader) {
        int version = -reader.ReadInt();
        if (version > saveVersion) {
            Debug.LogError("Unsupported future save version " + version);
        }
        int count = version <= 0 ? -version : reader.ReadInt();

        for (int i = 0; i < count; i++) {
            int shapeId = version > 0 ? reader.ReadInt() : 0;
            Shape instance = shapeFactory.Create(shapeId);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }

    public override void Save(GameDataWriter writer) {
        writer.Write(-saveVersion);
        writer.Write(shapes.Count);

        for (int i = 0; i < shapes.Count; i++) {
            writer.Write(shapes[i].ShapeId);
            shapes[i].Save(writer);
        }
    }
}
