using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

[System.Serializable]//esta parte es para mi objeto sirva en el entorno grafico de unity
public class servicio
{
    public string NombreVariable;
    public Text TextUIDato;
    public string AgregarSimboloDeInidicador; 
}
public class WebServicesPaginaWeb : MonoBehaviour
{
    public static WebServicesPaginaWeb servicio;
    public static WebServicesPaginaWeb Instance { get { return servicio; } }
    public string url_host, OEE, Temperatura, conteoPiezas, presion;
    public bool hostGSN;
    public servicio[] varibles;

    private string getID_dato;
    

//animacion numero
   public static int[] valorActual = new int[4];
   public static int[] valorAnterior = new int[4];
   public int intOEE;
    
    // public Text dato_x;
    // public Text dato_y;
    // public Text dato_z;
    // public Text rotar;
    // Use this for initialization

    //Hay que tener en cuenta estos valores
    /* OEE = result[0];
    Temperatura = result[1];
    presion = result[2];
    conteoPiezas = result[3]; */


    private void Awake()
    {
        if (servicio != null && servicio != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            servicio = this;
        }
    }

    //Este componente se activa al iniciarse sesión en la web
    private void OnEnable() 
    {
        GetID();
    }

    public void GetID()
    {
        //obtengo un valor del archivo de RamdomNumber
        getID_dato = RamdomNumeber.ID_dato;

        if(getID_dato != null)
        StartCoroutine(unity(getID_dato));

        Debug.Log("getID_dato => "+ getID_dato);
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log("getID_dato_2 => "+ getID_dato);
        
		//Debug.Log(PlayerPrefs.GetString("IDdato")+" Dato cargado exitosamenete");

        ///CONDICIONES DE ANIMACIION NUMEROS 
        //esta insparido en el script "animacionNumerica" lo use como borrador
        for (int i = 0; i < varibles.Length; i++)
        {
            if (valorActual.Length != 0)
            {
                if (valorAnterior[i] != valorActual[i])
                {
                    //Cada frame vamos agregando 1 o disminuyendo según la diferencia que haya
                    //entre el valor actual y el anterior
                    valorAnterior[i] += (valorAnterior[i] < valorActual[i]) ? 1 : -1;
                }
                //Asignamos el resultado en la UI más su simbolo
                varibles[i].TextUIDato.text = valorAnterior[i] + varibles[i].AgregarSimboloDeInidicador;

            }
        }
        ///fin de la condicion de numeros

    }
    //ESTE INUMERATOR PRIMERO VERIFICA EL ID DEL USUARIO PARA PODER OBTENER LOS VALORES DE ESE
    // USUARIO EN ESPECIFICO
    IEnumerator unity(string id_USUARIO)
    {
        
        //no usar http con version de android pie por que se rompe la aplicacion.
        string url = hostGSN ? "http://192.168.8.38/MarcadorSilberhorn/select_unity.php" : url_host + "/select_unity.php";
        //Debug.Log("Url es: " + url);
        WWWForm form = new WWWForm();
        form.AddField("usuario", id_USUARIO);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest(); // wait until data is received

        Debug.Log("tomado el id del usuario");

        Debug.Log("www.result " +www.result);
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to get data from server: " + www.error);
            yield break;
        }

        //Recibimos los datos de la web
        string data = www.downloadHandler.text;
        string[] result = data.Split(new string[] { "+" }, StringSplitOptions.None);
        if (result.Length < varibles.Length)
        {
            Debug.LogError("Unexpected result format: " + data);
            yield break;
        }

        //Asignamos el valor recibido a la variable valorActual
        for (int i = 0; i < varibles.Length; i++)
        {
            if(valorActual.Length != 0)
            {
                if (int.TryParse(result[i], out int valor))
                {
                    valorActual[i] = valor;
                }
                else
                {
                    Debug.LogError("Failed to parse variable value: " + result[i]);
                }
            }
           
        }
        OEE = result[0];
        Temperatura = result[1];
        presion = result[2];
        conteoPiezas = result[3];

        //parseamos los resultados a enteros para usarlos en el código
        yield return new WaitForSeconds(1);
        StartCoroutine(unity(id_USUARIO));
    }

}
