using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine.XR.Interaction.Toolkit;
using Microsoft.MixedReality.Toolkit;

[Serializable]

public class SubMeshes

{

    public MeshRenderer meshRenderer; //Mesh a mover

    public Vector3 originalPosition; //Posicion antes del explosionado
    public Vector3 originalRotation;//Posicion original antes de rotarlo

    public Vector3 explodedPosition; //Posicion del explosionado, que es la original mas un offset
}



public class ThreeDModelFunctions : MonoBehaviour

{

    #region Variables

    public List<SubMeshes> childMeshRenderers; //lista de los meshes del modelo

    public bool isInExplodedView = false; //booleana para ver el estado de explosionado

    public float explosionSpeed = 0.1f; //velocidad con la cual se realizara la animacion de explosionado

    bool isMoving = false; //checar si se estan moviendo los mesh

    bool FirstTouch = false;
    SubMeshes mesh; //variable de la clase submesh 

    Renderer renderer;//renderer de mesh para cambiar color

    public Material materialOriginal, HoveredMaterial;

    public AudioSource audioClipGrab, audioClipLetGo;

    public GameObject? PanelAR;
    public TextMeshProUGUI? TextTMPro;

    [SerializeField] private PanelControls panelControls;

    private GameObject GrabbedGO;
    #endregion


    #region UnityFunctions


    private void Awake()

    {

        childMeshRenderers = new List<SubMeshes>(); //inicializar lista de submeshes


        foreach (var item in GetComponentsInChildren<MeshRenderer>())

        {


            mesh = new SubMeshes //nueva clase de submeshes con variables y valores asignados
            {
                meshRenderer = item      
            };

            childMeshRenderers.Add(mesh); //agregar mesh a la lista


        }

    }


    private void Start()
    {

        PanelAR.SetActive(false);
 
        for (int i = 0; i < childMeshRenderers.Count; i++)
        {

            //Agregar componentes necesarios para interactuar
            //childMeshRenderers[i].meshRenderer.gameObject.AddComponent<MeshCollider>();
            //childMeshRenderers[i].meshRenderer.gameObject.GetComponent<MeshCollider>().convex = true;
            childMeshRenderers[i].meshRenderer.gameObject.AddComponent<BoxCollider>();
            childMeshRenderers[i].meshRenderer.gameObject.AddComponent<ObjectManipulator>();
            //childMeshRenderers[i].meshRenderer.gameObject.AddComponent<NearInteractionGrabbable>();

            //childMeshRenderers[i].meshRenderer.gameObject.GetComponent<ObjectManipulator>().AllowedInteractionTypes = InteractionFlags.Near;

            //Agregar funciones al estado del objeto para cambio de color para indicar que sera agarrado
            childMeshRenderers[i].meshRenderer.gameObject.GetComponent<ObjectManipulator>().hoverEntered.AddListener(Hovered);
            childMeshRenderers[i].meshRenderer.gameObject.GetComponent<ObjectManipulator>().firstSelectEntered.AddListener(Grabbed);//manipulation started 
            childMeshRenderers[i].meshRenderer.gameObject.GetComponent<ObjectManipulator>().lastSelectExited.AddListener(GrabbedExit);
            childMeshRenderers[i].meshRenderer.gameObject.GetComponent<ObjectManipulator>().hoverExited.AddListener(HoveredExit);


        }
    }


    private void Update()

    {

        if (isMoving)

        {

            if (isInExplodedView)//de vista explosiva a normalidad

            {

                foreach (var item in childMeshRenderers)

                {

                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.explodedPosition, explosionSpeed);


                    if (Vector3.Distance(item.meshRenderer.transform.position, item.explodedPosition) < 0.001f)//detener movimiento cuando este cerca de posicion de explosionado 

                    {

                        isMoving = false;

                    }

                }

            }

            else //de normalidad a vista explosiva

            {

                foreach (var item in childMeshRenderers)

                {

                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.originalPosition, explosionSpeed);


                    if (Vector3.Distance(item.meshRenderer.transform.position, item.originalPosition) < 0.0001f) //detener movimiento cuando este cerca de posicion original 

                    {

                        isMoving = false;

                    }

                }

            }

        }

    }

    #endregion

    #region CustomFunctions
    public void Hovered(HoverEnterEventArgs eventData) //cambio de color al objeto cuando esta en hover
    {
        if (!FirstTouch)
        {
      
            FirstTouch = true;
            var result = eventData.interactableObject.transform.gameObject;
            renderer = result.gameObject.GetComponent<MeshRenderer>();
            materialOriginal = renderer.material;
            renderer.material = HoveredMaterial;
        }
    }

    public void HoveredExit(HoverExitEventArgs eventData) //regreso el color a la normalidad cuando quito el hover
    {
     
        renderer.material = materialOriginal;
        renderer.material.color = Color.white;
        FirstTouch = false;
    }

    public void Grabbed(SelectEnterEventArgs eventData) 
    {
        PanelAR.SetActive(true);
        audioClipGrab.Play();
        renderer.material = materialOriginal;
        renderer.material.color = Color.white;
        var result = eventData.interactableObject.transform.gameObject;
        GrabbedGO = result;
        if (result.TryGetComponent<SilberhornCustomPiezas>(out var CustomInfo))
        {
            TextTMPro.text = CustomInfo.TextoInstruccion;
            panelControls.Imagenes = CustomInfo.imagenes;
            panelControls.UpdateMedia();
        }
        else
        {
            TextTMPro.text = result.gameObject.name;
            panelControls.Imagenes = panelControls.CustomImagenes;
            panelControls.UpdateMedia();
        }
        if (!isInExplodedView)
        {
            if (GrabbedGO.TryGetComponent<SnapToLocation>(out var SnapGo))
            {
                SnapGo.Grabbed = true;
            }
        }
       
       

        //Al tomar otro objeto si el panel AR estaba pineado lo despineamos
        //if (PanelControls.Pinned == true)
        //{
        //    //Una vez tomada la pieza volvemos al estado normal (despineado)
        //    PanelControls.Pinned = false;
        //    PanelAR.GetComponent<FollowMeToggle>().ToggleFollowMeBehavior();

        //}
    }



    public void GrabbedExit(SelectExitEventArgs eventData) //cambio de color al objeto cuando esta en hover
    {
        audioClipLetGo.Play();
        if (PanelControls.Pinned == false)
        {
            PanelAR.SetActive(false);
        }
        if (GrabbedGO.TryGetComponent<SnapToLocation>(out var SnapGo))
        {
            if (!isInExplodedView)
            {
                SnapGo.Snap();
                SnapGo.Grabbed = false;
            }
            
        }


        FirstTouch = false;
    }


    public void ToggleExplodedView()//Funcion del boton o toggle
    {
        
        if (isInExplodedView) //Si esta en vista explosiva, regresar a la normalidad

        {
            foreach (var item in childMeshRenderers)
            {
                item.meshRenderer.gameObject.transform.eulerAngles = item.originalRotation;
                item.meshRenderer.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            isInExplodedView = false;

            isMoving = true;

        }

        else

        {
            ResetModelParts();
            foreach (var item in childMeshRenderers) //hacerlo vista explosionada y guardar posicion original de todos los mesh para posteriormente regresarlos
            {
                //Este vector 3 contiene la diferencia en coordenadas entre el objeto que utilicemos como centro
                //Y el respectivo subMesh
                Vector3 diferencia = new Vector3
            (
                item.meshRenderer.bounds.center.x - transform.position.x,
                item.meshRenderer.bounds.center.y - transform.position.y,
                item.meshRenderer.bounds.center.z - transform.position.z
            );

                item.originalPosition = item.meshRenderer.transform.position;
                item.explodedPosition = item.meshRenderer.bounds.center + diferencia + new Vector3(0.1f,0.1f,0.1f);
            }
            isInExplodedView = true;
            isMoving = true;

        }

    }

    public void ResetModelParts() //reseteto piezas en escala, posicion y rotacion
    {
        
        foreach (var item in childMeshRenderers)
        {
            item.meshRenderer.transform.position = item.originalPosition;
            item.meshRenderer.gameObject.transform.eulerAngles = item.originalRotation;
            item.meshRenderer.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SaveOriginalPosition() //guardo la posicion de cada mesh para posteriormente poder regresarlas
    {
        foreach (var item in childMeshRenderers) 
        {
            item.originalPosition = item.meshRenderer.transform.position;
            item.originalRotation = item.meshRenderer.transform.eulerAngles;

        }
    }

    
    #endregion

}

