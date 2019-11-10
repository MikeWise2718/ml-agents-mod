using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmvAgentBody : MonoBehaviour
{
    public CmvAgent parentAgent;
    Rigidbody rb;
    RayPerception rayPer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(CmvAgent parentAgent)
    {
        this.parentAgent = parentAgent;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        rb.mass = 25;
        rb.drag = 2;
        gameObject.tag = "agent";
    }
    public void InitializeAgentBody()
    {
        //rayPer = parentAgent.GetComponent<RayPerception>();
        rayPer = gameObject.AddComponent<MLAgents.RayPerception3D>();
        rb.velocity = Vector3.zero;
    }
    public void AddMovement(Vector3 rotVek, Vector3 forceVek, ForceMode forceMode)
    {
        transform.Rotate(rotVek, Time.deltaTime * 150f);
        //var nrotVek = rotVek * Time.deltaTime * 150f;
        //var rfvek = Quaternion.Euler(nrotVek) * forceVek;
        //Debug.Log("nrotVek:"+nrotVek.ToString("f1")+" forceVek:" + forceVek.ToString("f1") + "  rfvek:" + rfvek.ToString("f1"));
        rb.AddForce(forceVek, ForceMode.VelocityChange);
    }
    public void AgentReset()
    {
        rb.velocity = Vector3.zero;
        transform.position = parentAgent.transform.position;
    }

    public RayPerceptionInterpreter rpi = null;
    public class RayPerceptionInterpreter
    {
        string aname;
        float rayDistance;
        float[] rayAngles;
        string[] detectableObjects;
        int pbufSize;
        Dictionary<string, bool> classHit;
        Dictionary<string, float> classRay;
        Dictionary<float, string> rayHit;
        Dictionary<float, float> rayHitDist;
        public RayPerceptionInterpreter(string aname, float rayDistance, float[] rayAngles, string[] detectableObjects)
        {
            this.aname = aname;
            this.rayDistance = rayDistance;
            this.rayAngles = rayAngles;
            this.detectableObjects = detectableObjects;
            pbufSize = rayAngles.Length * (detectableObjects.Length + 2);
        }
        public void InitDicts()
        {
            classHit = new Dictionary<string, bool>();
            foreach (var cls in detectableObjects) classHit[cls] = false;
            classRay = new Dictionary<string, float>();
            rayHit = new Dictionary<float, string>();
            rayHitDist = new Dictionary<float, float>();
        }
        public void Perceive(List<float> values)
        {
            InitDicts();
            if (values.Count != pbufSize)
            {
                Debug.LogError(aname + " bad perceivebuffer of length:" + values.Count + " expected:" + pbufSize);
                return;
            }
            int i = 0;
            foreach (var rang in rayAngles)
            {
                var hitcls = "";
                foreach (var cls in detectableObjects)
                {
                    if (values[i] > 0)
                    {
                        classHit[cls] = true;
                        classRay[cls] = rang;
                        rayHit[rang] = cls;
                        hitcls = cls;
                    }
                    i++;
                }
                if (hitcls != "" && values[i] == 1)
                {
                    Debug.LogError(aname + " perceivebuffer inconsistency for ray-ang:" + rang + " hitcls:" + hitcls + " but coded to not hit");
                }
                else if (hitcls == "" && values[i] == 0)
                {
                    Debug.LogError(aname + " perceivebuffer inconsistency for ray-ang:" + rang + " hitcls empty:" + hitcls + " but coded to a hit");
                }
                i++;
                rayHitDist[rang] = rayDistance * values[i];
                i++;
            }
        }
        public bool HitDetected(string cls)
        {
            return classHit[cls];
        }
        public string GetHitObs()
        {
            var rv = "";
            var nhit = 0;
            foreach (var cls in detectableObjects)
            {
                if (classHit[cls])
                {
                    if (nhit > 0) rv += ",";
                    rv += cls;
                    nhit++;
                }
            }
            return rv;
        }
        public void DumpRays()
        {
            foreach (var rang in rayAngles)
            {
                if (rayHit.ContainsKey(rang))
                {
                    var msg = "    " + aname + "   " + rang + " hit " + rayHit[rang] + " dist:" + rayHitDist[rang];
                    Debug.Log(msg);
                }
            }
        }
    }
    // size of array is (number of rays*number of classes+2)
    // everything initied to zero
    // then by ray
    // one hot encoding of class if hit by ray
    // then a one added if nothing was hit (to make it easier
    // the distance of that hit is stored in the final float
    float rayDistance = 24f;
    float[] rayAngles = { 20f, 60f, 90f, 120f, 160f };
    string[] detectableObjects = { "redGoal", "agent", "wall", "orangeBlock", "redBlock" };

    public List<float> Perceive()
    {
        if (rpi == null)
        {
            rpi = new RayPerceptionInterpreter(name, rayDistance, rayAngles, detectableObjects);
        }
        var rayobs = rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0,0);
        rpi.Perceive(rayobs);
        return rayobs;
    }
    public void SyncToBody(GameObject go)
    {
        go.transform.position = transform.position;
        go.transform.localRotation = transform.localRotation;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("redGoal"))
        {
            parentAgent.ReachedGoal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
