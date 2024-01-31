using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager instance;
    public Statistics statistics;
    void Awake()
    {
        instance = this;
        if (!Directory.Exists(Application.persistentDataPath + "/Statistics/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Statistics/");
        }
    }
    public void SaveScores(List<Statistics> scoresToSave)
    {
        statistics.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(Statistics));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Statistics/Statistics.xml", FileMode.Create);
        serializer.Serialize(stream, statistics);
        stream.Close();
    }
    public List<Statistics> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/Statistics/Statistics.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Statistics));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Statistics/Statistics.xml", FileMode.Open);
            statistics = serializer.Deserialize(stream) as Statistics;
        }
        return statistics.list;
    }
}
[System.Serializable]
public class Statistics
{
    public List<Statistics> list = new List<Statistics>();
}