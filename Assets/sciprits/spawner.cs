using UnityEngine;

public class spawner : MonoBehaviour {

    public GameObject[]groups;

	void Start () {
        spawnNext();
	}

    public void spawnNext()
    {
        int i = Random.Range(0, groups.Length);
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
