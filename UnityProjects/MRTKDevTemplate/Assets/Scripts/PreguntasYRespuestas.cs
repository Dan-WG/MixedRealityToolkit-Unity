using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PreguntasYRespuestas 
{
    //En el examen cada pregunta tendra una lista de posibles respuestas y señalar cual respuesta es la correcta
    public string Pregunta; //Texto pregunta
    public bool Opcionmultiple;
    public bool OpcionVoF;
    public List <string> Respuestas; //Lista de respuestas

}
