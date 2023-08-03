using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class RamdomNumeber : MonoBehaviour
{
    //Creamos un evento que detonará cada que loguemos en web
    public delegate void ActionLoginInWeb();
    public static event ActionLoginInWeb loggedInWeb;
    public delegate void ActionInsertRNumber();
    public static event ActionInsertRNumber RNumberInserted;
    public TextMesh TextUIRamdom;
    [SerializeField] private GameObject MenuContent;
    [SerializeField] private GameObject buttonCollectionIOT;
    [SerializeField] private GameObject buttonMainCollection;
    [SerializeField] private GameObject panelNewNumber;
    //public Text TextUI_log;
    string NumeroRandom;//NUMERO RAMDOM
    string sms;
    string _numeroUno;
    bool stopped;

    int _random1;
    int _random2;
    int _random3;
    int _random4;

    bool canLoggin = false; //Hace referencia a la corrutina de loggin sesión

    public static string ID_dato;
    public GameObject loading, PanelIOT;

    public string ULR_host;

    public bool hostGSN;
    public WebServicesPaginaWeb servicesPaginaWeb;
    bool Logged;

    private void Awake() 
    {
        //Desactivamos el componente de servicios al iniciar la app
        //Porque solo debe activarse al iniciar sesión en la web
        servicesPaginaWeb.enabled = false;
    }

    public void GenerateRandomNum()
    {
        //Apagamos el componente webServices
        servicesPaginaWeb.enabled = false;

        //Permitimos que la corrutina de buscar sesión actúe cada que generamos
        //Un nuevo número
        canLoggin = false;

        //Si estás generando un nuevo número es porque aún no estás loggeado en la web
        //O perdiste la sesión
        Logged = false;

        _random1 = UnityEngine.Random.Range(10, 99);
        _random2 = UnityEngine.Random.Range(10, 99);
        _random3 = UnityEngine.Random.Range(10, 99);
        _random4 = UnityEngine.Random.Range(10, 99);
        NumeroRandom = _random1.ToString() + _random2.ToString() + _random3.ToString() + _random4.ToString();
        TextUIRamdom.text = _random1.ToString() + "-" + _random2.ToString() + "-" + _random3.ToString() + "-" + _random4.ToString();
        StartCoroutine(existNumEnBD(NumeroRandom));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("SMS : " + sms);
        //sms="2";
        if (sms == "1")//1=NUMERO RANDOM YA EXISTE EN LA BD
        {
            StopCoroutine(existNumEnBD(NumeroRandom));// PARA LA CONSULTA 
            _numeroUno = NumeroRandom;
            TextUIRamdom.text = _numeroUno;
            Debug.Log("Numero ya existe en la base de datos");
            StartCoroutine(insertNumRandomBD(_numeroUno));
        }
        if (sms == "2")//2= NUMERO RANDOM SE INSERTO CORRECTAMENTE
        {
            StopCoroutine(insertNumRandomBD(NumeroRandom));

            if(RNumberInserted != null)
            RNumberInserted();

            _numeroUno = NumeroRandom;
            StartCoroutine(LoginSession(_numeroUno));
        }
        if (sms == "4")//4 =EL USUARIO INGRESO DESDE LA PAGINA WEB
        {
            //Si no estabas loggeado se activan los paneles IOT
            if(!Logged)
            {
                ActivateIOTPanels();
                Logged = true;
            }

            //Detenemos la corrutina para login
            StopCoroutine(LoginSession(_numeroUno));
            canLoggin = true;
            
            servicesPaginaWeb.enabled = true;

            //Detonamos el evento para notificar a otras clases
            //Que se inició sesión en la web
            if(loggedInWeb != null)
            loggedInWeb();
        }
        if (sms == "5")//EL USUARIO NO HA INGRESADO DESDE LA PAGINA WEB
        {
            loading.SetActive(false);
        }

    }
    //VERIFICA SI EXISTE EL NUMERO RANDOM EN LA BD 
    IEnumerator existNumEnBD(string numero)
    {
        stopped = false;
        string url;
        if (hostGSN == false)
        {
            url = ULR_host + "/select_unityRandom.php";
        }
        else
        {
            url = "http://192.168.8.38/MarcadorSilberhorn/select_unityRandom.php";
        }


        WWWForm form = new WWWForm();

        //son los campos que se envian a ala consulta para verificar que existe el numero
        form.AddField("NumRamdom", numero);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        WWW dataResult = new WWW(url, form);
        yield return dataResult; // wait until data is received
        string data = dataResult.text;

        string[] stringSeparators = new string[] { "+" };
        string[] result;
        result = data.Split(stringSeparators, StringSplitOptions.None);
        sms = result[0].Replace(" ", String.Empty);

        yield break;
    }
    //INSERTA EL NUMERO CREADO EN LA BD
    IEnumerator insertNumRandomBD(string numero)
    {
        Debug.Log(numero);
        string url;
        if (hostGSN == false)
        {
            url = ULR_host + "/select_unityRandom.php";
        }
        else
        {
            url = "http://192.168.8.38/MarcadorSilberhorn/select_unityRandom.php";
        }

        WWWForm form = new WWWForm();
        //son los campos que se envian a ala consulta para verificar que existe el numero
        form.AddField("NumRamdom", numero);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        WWW dataResult = new WWW(url, form);
        yield return dataResult; // wait until data is received
        string data = dataResult.text;

        string[] stringSeparators = new string[] { "+" };
        string[] result;
        result = data.Split(stringSeparators, StringSplitOptions.None);
        sms = result[0].Replace(" ", String.Empty);
        Debug.Log("Inserted new number and sms = "+sms);
        yield break;


    }
    //VERIFICA SI EL NUMERO RANDOM DE LA APLICACION EXISTE EN LA BD PARA DEJARLOS ENTRAR O NO.
    IEnumerator LoginSession(string numero)
    {
        string url;
        if (hostGSN == false)
        {
            url = ULR_host + "/select_UnityLoginSesion.php";
        }
        else
        {
            url = "http://192.168.8.38/MarcadorSilberhorn/select_UnityLoginSesion.php";
        }

        WWWForm form = new WWWForm();
        form.AddField("NumRamdom", numero);//son los campos que se envian a ala consulta para verificar que existe el numero
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        WWW dataResult = new WWW(url, form);
        yield return dataResult; // wait until data is received
        string data = dataResult.text;

        string[] stringSeparators = new string[] { "+" };
        string[] result;
        result = data.Split(stringSeparators, StringSplitOptions.None);
        sms = result[0];//PUEDE DEVOLVER 4 O 5

        //IMPORTANTE QUE LO TENGA YA QUE LO USO PARA VERIFICAR AL USUARIO Y OBTENER LOS VALORES
        // QUE INSERTARA DE LA WEB Y SE REFLEJARA EN LA SIGUIENTE SCENEA
        if(ID_dato == null)
        {
            //Guardamos el id del usuario 
            //De esta manera webServicesPaginaWeb lo puede usar
            ID_dato = result[1];
            Debug.Log("ID_dato = "+ ID_dato);
        }

        if (canLoggin == false)
        {
            yield return www.SendWebRequest();
            StartCoroutine(LoginSession(numero));
        }
    }

    public void ActivateIOTPanels()
    {
        //Activamos los paneles IOT y volvemos al hand menu
        if(!PanelIOT.activeInHierarchy)
        PanelIOT.SetActive(true);

        if(!buttonMainCollection.activeInHierarchy)
        buttonMainCollection.SetActive(true);

        if(buttonCollectionIOT.activeInHierarchy)
        buttonCollectionIOT.SetActive(false);
    }
    
}
