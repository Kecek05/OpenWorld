using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.InputSystem.XR;

public static class SaveSystem {

    public static void SavePlayer(PaymentController controller)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerConfig.cherrbaygames";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(controller);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerConfig.cherrbaygames";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }


    public static void ResetPlayerSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerConfig.cherrbaygames";
        FileStream stream = new FileStream(path, FileMode.Create);

        PaymentController paymentReseted = new PaymentController();
        PlayerData data = new PlayerData(paymentReseted);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void DeletePlayerSave()
    {
        string path = Application.persistentDataPath + "/playerConfig.cherrbaygames";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted successfully.");
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
