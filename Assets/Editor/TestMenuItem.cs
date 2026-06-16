using UnityEngine;
using UnityEditor;
using System.Collections;
using Unity.VisualScripting;
public class TestMenuItem
{
    [MenuItem("Tools/HappyToyShop/SnapToGround Multiple")]


    public static void SnapToGroundSelections()
    {
        GameObject[] objects = Selection.gameObjects;

        for(int i = 0; i < objects.Length; i++)
        {
            SnapToGround(objects[i]);
            
        }
        

    }
    IEnumerator TimeToWait(GameObject obj)
    {

        yield return new WaitUntil(obj.GetComponent<Rigidbody>().IsSleeping);
    }

    [MenuItem("Tools/HappyToyShop/SnapToGround Single")]
    public static void SnapToGround()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj == null)
        {
            Debug.LogWarning("Tienes que tener un objeto seleccionado");
            return;
        }

        Undo.RegisterCompleteObjectUndo(obj.transform, "Simular caida");
        obj.transform.position = Vector3.zero;


        Collider collider = obj.GetComponent<Collider>();

        bool colliderAdded = false;
        if (collider == null)
        {
            colliderAdded = true;
            collider = obj.AddComponent<Collider>();

        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        bool rbAdded = false;

        if(rb == null)
        {
            rbAdded = true;
            rb = obj.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Physics.simulationMode = SimulationMode.Script;

        int maxSteps = 600; //-> número máximo de límite de seguridad
        float dt = 0.0166f; //-> duración dec ada paso de simulación equivalente maso 1 frame
        float sleepThreshold = 0.001f; //-> valor mínimo para considerar un objeto quiero


        for (int i = 0; i < maxSteps; i++)
        {
            Physics.Simulate(dt);

            if(rb.IsSleeping() || (rb.linearVelocity.sqrMagnitude < sleepThreshold && rb.angularVelocity.sqrMagnitude < sleepThreshold && i> 5))
            {
                break;
            }

        }
        
        Physics.simulationMode = SimulationMode.FixedUpdate;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if(rbAdded) Object.DestroyImmediate(rb);

        if (colliderAdded) Object.DestroyImmediate(collider);



        //-> marca objeto como modificado en la escena

        EditorUtility.SetDirty(obj.transform);

        Debug.Log("Simulación de caída finalizada");



    }
    public static void SnapToGround(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("Tienes que tener un objeto seleccionado");
            return;
        }

        Undo.RegisterCompleteObjectUndo(obj.transform, "Simular caida");
        obj.transform.position = Vector3.zero;


        Collider collider = obj.GetComponent<Collider>();

        bool colliderAdded = false;
        if (collider == null)
        {
            colliderAdded = true;
            collider = obj.AddComponent<Collider>();

        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        bool rbAdded = false;

        if (rb == null)
        {
            rbAdded = true;
            rb = obj.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Physics.simulationMode = SimulationMode.Script;

        int maxSteps = 600; //-> número máximo de límite de seguridad
        float dt = 0.0166f; //-> duración dec ada paso de simulación equivalente maso 1 frame
        float sleepThreshold = 0.001f; //-> valor mínimo para considerar un objeto quiero


        for (int i = 0; i < maxSteps; i++)
        {
            Physics.Simulate(dt);

            if (rb.IsSleeping() || (rb.linearVelocity.sqrMagnitude < sleepThreshold && rb.angularVelocity.sqrMagnitude < sleepThreshold && i > 5))
            {
                break;
            }

        }

        Physics.simulationMode = SimulationMode.FixedUpdate;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (rbAdded) Object.DestroyImmediate(rb);

        if (colliderAdded) Object.DestroyImmediate(collider);



        //-> marca objeto como modificado en la escena

        EditorUtility.SetDirty(obj.transform);

        Debug.Log("Simulación de caída finalizada");
    }



}
