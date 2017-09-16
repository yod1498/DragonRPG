using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

	public delegate void OnLayerChange(Layer newLayer); //declare new delegate type
	public event OnLayerChange layerChangeObservers; //instantiate an observer set

	void SomeLayerChangeHandler(){
		print ("I handled it!");
	}

	void SomeLayerChangeHandler_2(){
		print ("Handler 2!");
	}

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
//		layerChangeObservers += SomeLayerChangeHandler;
//		layerChangeObservers += SomeLayerChangeHandler_2;
//		layerChangeObservers (); // call the delegates
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
				if (m_layerHit != layer) {// if layer has changed
					m_layerHit = layer;
					layerChangeObservers (layer); // call the delegates
				}
					
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
		layerChangeObservers (m_layerHit);
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
