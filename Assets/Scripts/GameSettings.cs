/* Copyright (c) 2022 Scott Tongue
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. THE SOFTWARE 
 * SHALL NOT BE USED IN ANY ABLEISM WAY.
 */

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSettings
{
   
    public static float DeadZoneValue => m_config.DeadZone;
    public static float SFXVolume => m_config.SFXVolume;
    public static float MusicVolume => m_config.MusicVolume;
    public static int LiveCountDefault => m_config.LivesCount;
    public static int HighScore => m_config.HighScore;
    
    private static GameConfig m_config = new GameConfig();
    private const string _configFile = "Config";
    
    public static void LoadData()
    {
        if (!global::SaveData.Load(ref m_config, _configFile))
            global::SaveData.Save(m_config, _configFile);
    }

    public static void SaveData()
    {
        global::SaveData.Save(m_config, _configFile);
    }
}

public static class SaveData
{
    
    public static void Save<T>(T arg, string FileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + FileName + ".dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log("Data saved" + path);
        bf.Serialize(stream, arg);
        stream.Close();
    }

    public static bool Load(ref GameConfig Config, string FileName)
    {
        string path = Application.persistentDataPath + FileName + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log("File loaded at " + path);
            Config = bf.Deserialize(stream) as GameConfig;
            stream.Close();
            return true;
        }
        else
        {
            Debug.Log("File Doesn't Exist at " + path);
            return false;
        }
    }
}

[System.Serializable]
public struct Data
{
    public float DeadZone;
    public float SFXVolume;
    public float MusicVolume;
    public int LivesCount;
    public int HighScore;

    public Data(float dz, float sfx, float music, int lives, int score)
    {
        DeadZone = dz;
        SFXVolume = sfx;
        MusicVolume = music;
        LivesCount = lives;
        HighScore = score;
    }

    
}

[System.Serializable]
public class GameConfig
{
    public float DeadZone;
    public float SFXVolume;
    public float MusicVolume;
    public int LivesCount;
    public int HighScore;
        
    public GameConfig(Data Data)
    {
        DeadZone = Data.DeadZone;
        SFXVolume = Data.SFXVolume;
        MusicVolume = Data.SFXVolume;
        LivesCount = Data.LivesCount;
        HighScore = Data.LivesCount;
    }
    public GameConfig()
    {

    }
}