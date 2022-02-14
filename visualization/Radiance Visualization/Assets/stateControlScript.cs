using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateControlScript : MonoBehaviour
{
    public float waterDropletState, wallCrackingState, moldGrowthState;
    public float vizState; // goes from 0 to 1
    public GameObject waterObject, wallObject, moldObject;
    // Start is called before the first frame update
    void Start()
    { 
        waterDropletState = 0;
        wallCrackingState = 0;
        moldGrowthState = 0;   
        vizState = 0;
    }

    public void SetVizState(float value){
        vizState = value;
    }

    // Update is called once per frame
    void Update()
    {
        float combinedVizState = vizState * 3;
        // water
        waterDropletState = combinedVizState;
        if(combinedVizState > 1){waterDropletState = 1 - (combinedVizState % 1);}
        if(combinedVizState > 2){waterDropletState = 0;}
        updateShaderState(waterObject, waterDropletState);
        // wall
        if(combinedVizState - 1 < 0) {
            wallCrackingState = 0;
        }
        else {
            wallCrackingState = combinedVizState - 1;
            if(combinedVizState - 1 > 1) {
                wallCrackingState = 1;
            }
        }
        updateShaderState(wallObject, wallCrackingState);
        // mold
        if(combinedVizState - 2 < 0){
            moldGrowthState = 0;
        }
        else {
            moldGrowthState = combinedVizState - 2;
        }
        updateShaderState(moldObject, moldGrowthState);
    }

    void updateShaderState(GameObject obj, float val) {
         obj.GetComponent<Renderer>().sharedMaterial.SetFloat("_State", val);

    }
}
