using UnityEngine;

public interface IDataSaver
{
     void SaveData(object data);
}

public interface IDataLoader<T>
{
     T Load();
}

public class PlayerPrefsDataSaver : IDataSaver
{
     private string _key;

     public PlayerPrefsDataSaver(string key)
     {
          _key = key;
     }
     
     public void SaveData(object data)
     {
          PlayerPrefs.SetString(_key, JsonUtility.ToJson(data));
     }
}

public class PlayerPrefsDataLoader<T> : IDataLoader<T> where T : class
{
     private string _key;

     public PlayerPrefsDataLoader(string key)
     {
          _key = key;
     }
     
     public T Load()
     {
          return PlayerPrefs.HasKey(_key) 
               ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(_key)) 
               : null;
     }
}