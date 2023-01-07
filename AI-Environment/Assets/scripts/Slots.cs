using UnityEngine;

public class Slots : MonoBehaviour
{
    public int fstRow;
    public GameObject row01;
    public int sndRow;
    public GameObject row02;
    public int trdrow;
    public GameObject row03;
    public GameObject king;

    void Start()
    {
        int front = 2 * fstRow / 3;
        int rear = fstRow - front;
        createRow(front, -2f, row01);
        createRow(sndRow, -4f, row02);
        createRow(trdrow, -6f, row03);
        createRow(rear, -8f, row01);
    }

    void createRow(int num, float z, GameObject pf)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i) {
            Vector3 position = king.transform.TransformPoint(new Vector3 (pos,0f,z));
            GameObject temp = (GameObject)Instantiate(pf, position, king.transform.rotation); 
            //temp.AddComponent<Formation>();
            //temp.GetComponent<Formation>().pos = new Vector3 (pos,0,z);
            //temp.GetComponent<Formation>().agent = temp;
            //temp.GetComponent<Formation>().target = ghost;
            pos += 2f;
        }
    }
}