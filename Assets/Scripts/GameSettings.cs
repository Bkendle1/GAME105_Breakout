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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class GameSettings
{
    public static Action<float> SoundUpdated;
    public static Action<float> MusicUpdated;
    public static Action SavingGame;
    public static float DeadZoneValue => m_config.DeadZone;
    public static float SFXVolumeGet => m_config.SFXVolume;
    public static float MusicVolumeGet => m_config.MusicVolume;
    public static int LiveCountDefault => m_config.LivesCount;
    public static bool HasConfigLoaded => m_hasConfigLoaded;
    public  static ref List<ScoreItem>GetScoreItems  => ref m_config.HighScoreTable;

    private static GameConfig m_config = new GameConfig(0.1f, 1f, 1f, 3, new List<ScoreItem>() );
    private static bool m_hasConfigLoaded = false;
    private const string _configFile = "Config";
  
    public static void MusicVolumeSet(float value)
    {
        MusicUpdated?.Invoke(value);
        m_config.MusicVolume = value;
    }

    public static void SfXVolumeSet(float value)
    {
        SoundUpdated?.Invoke(value);
        m_config.SFXVolume = value;
    }
    
    
    public static void LoadData()
    {
        if (!global::SaveData.Load(ref m_config, _configFile))
        {
            SaveData();
           
        }
        
        if(m_config.HighScoreTable == null)
            m_config.HighScoreTable = new List<ScoreItem>();
        
        m_hasConfigLoaded = true;
    }

    public static void SaveData()
    {
        SavingGame?.Invoke();
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
public struct ScoreItem
{
    public string Name;
    public int Score;
    
    public ScoreItem(string n = "Guest", int s = 5)
    {
        Name = n;
        Score = s;
    }
}

[Serializable]
public class GameConfig
{
    public float DeadZone;
    public float SFXVolume;
    public float MusicVolume;
    public int LivesCount;
    public List<ScoreItem>HighScoreTable;

    public GameConfig(float dz, float sfx, float music, int lives, List<ScoreItem> score )
    {
        DeadZone = dz;
        SFXVolume = sfx;
        MusicVolume = music;
        LivesCount = lives;
        HighScoreTable = score;
    }

    public GameConfig()
    {
    }
}



