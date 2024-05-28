using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class WorldTimeAPI : MonoBehaviour
{
    #region Singleton class: WorldTimeAPI

    public static WorldTimeAPI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    struct TimeData
    {

        public string datetime;
    }

    const string API_URL = "http://worldtimeapi.org/api/ip";

    [HideInInspector] public bool IsTimeLoaded = false;

    private DateTime _currentDateTime = DateTime.Now;

    void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
    }

    public DateTime GetCurrentDateTime()
    {
        //here we don't need to get the datetime from the server again
        // just add elapsed time since the game start to _currentDateTime
        return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }


    IEnumerator GetRealDateTimeFromAPI()
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        //Debug.Log("getting real datetime...");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            //error
            Debug.Log("Error: " + webRequest.error);

        }
        else
        {
            //success
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
            //timeData.datetime value is : 2020-08-14T15:54:04+01:00

            _currentDateTime = ParseDateTime(timeData.datetime);
            IsTimeLoaded = true;

        }
    }

    DateTime ParseDateTime(string datetime)
    {
        // Extraer solo la hora, minutos y segundos de la cadena datetime
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

        // Crear un objeto DateTime para la fecha actual y agregar la hora extraída
        DateTime dateTimeFormat = DateTime.Today.Add(TimeSpan.Parse(time));

        // Devolver el objeto DateTime
        return dateTimeFormat;
    }
}
