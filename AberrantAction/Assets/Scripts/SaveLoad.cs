using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static int levelsUnlocked;

    public static void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "/savedGames.txt", levelsUnlocked.ToString());
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGames.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string tempLine = reader.ReadLine();
            int.TryParse(tempLine, out levelsUnlocked);
            file.Close();
        }
    }
}
