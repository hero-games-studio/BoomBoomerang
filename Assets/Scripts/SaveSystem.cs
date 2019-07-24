using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SaveSystem
{
    private static float timeToWait;

    //Setting the cycle time for point generation
    public static void setTimeToWait(float val)
    {
        timeToWait = val;
    }

    //Save method for saving data when quiting game
    public static void save(int sceneIndex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/data.bin";
        FileStream saveStream = new FileStream(savePath, FileMode.Create);
        formatter.Serialize(saveStream, createLevelData(sceneIndex));
        saveStream.Close();
    }

    private static LevelData createLevelData(int sceneIndex)
    {
        return new LevelData(sceneIndex);

    }

    //Load data that has been saved from the last game
    public static void load()
    {
        string loadPath = Application.persistentDataPath + "/data.bin";
        if (File.Exists(loadPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream loadStream = new FileStream(loadPath, FileMode.Open);
            LevelData data = formatter.Deserialize(loadStream) as LevelData;
            SceneManager.LoadScene(data.getIndex());
            loadStream.Close();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public static int generatedPoint;
    //Restoring data when game loaded
}
[System.Serializable]
class LevelData
{
    int index;

    public LevelData(int _index)
    {
        index = _index;
    }
    public int getIndex()
    {
        return index;
    }
}