using Pathfinding;
using UnityEngine;

public class CreatePathes : MonoBehaviour
{

    private AstarPath _path;
    private LocationGenerator _locationGenerator;
    public float NodeSize = 0.5f;

    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _path = GetComponent<AstarPath>();

        AstarData data = _path.data;
        GridGraph gg = data.gridGraph;

        gg.center = 
            new Vector3((-(_locationGenerator.LongOfGrassBlock)/2 + ((_locationGenerator.LongOfGrassBlock*_locationGenerator.Size)/2)),
                +(_locationGenerator.HeightOfGrassBlock)/2 - ((_locationGenerator.HeightOfGrassBlock * _locationGenerator.Size)/2), 
                0);

//        gg.width = _locationGenerator.Size * (int) _locationGenerator.HeightOfGrassBlock / 2 * 10;
//        gg.depth = _locationGenerator.Size * (int)_locationGenerator.LongOfGrassBlock / 2 * 10;
//        gg.nodeSize = 0.5f;

        float _long = _locationGenerator.LongOfGrassBlock * 10;
        float _height = _locationGenerator.HeightOfGrassBlock * 10 ;
        float constitution = 1 / NodeSize;
        
        
        gg.SetDimensions( 
            _locationGenerator.Size * (int)_long /10 * (int)constitution , //TODO реализовать через nodeSize
            _locationGenerator.Size * (int)_height /10 * (int)constitution,
            NodeSize);

        _path.Scan();
    }
}
