using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(int saveNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progamermoves" + saveNum + ".gmtk";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        bf.Serialize(stream, data);

        stream.Close();
    }

    public static GameData Load(int saveNum)
    {
        string path = Application.persistentDataPath + "/progamermoves" + saveNum + ".gmtk";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(stream);

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in: " + path);
            return null;
        }
    }
}
