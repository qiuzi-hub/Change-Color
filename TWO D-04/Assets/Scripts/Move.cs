using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Move : MonoBehaviour
{
    public LayerMask layer;
    public GameObject round;//�����������
    LineRenderer lineRenderer;//����                
    // Update is called once per frame
    void Update()
    {
        //�õ����λ��
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�õ����������ĳ����������������ڵ�λ��-������ǰ��λ�ã�z�᲻ת����Ϊ��2D
        Vector3 rotate = pos - transform.position;
        rotate.z = 0;
        //������ת��ֻҪ�ϣ�����up��������꣬ע�⻹û��ʼʱ�ڿڵķ���
        transform.up = rotate;
        //����������������ӵ����������ߣ�������û�д���Ŀ������
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit= Physics2D.Raycast(transform.position, rotate, 20f, layer); //���ߣ��õ��ĸ�
            if (hit.collider) {
                //laymask�����ò㣬����raycasthit23
                //������ɫ
                hit.collider.GetComponent<SpriteRenderer>().color = Color.blue;
                Invoke("ResetColor", 1f);//����һ���
            }
            lineRenderer = GetComponent<LineRenderer>();//��ȡLineRenderer���
            lineRenderer.startWidth = lineRenderer.endWidth = 0.05f;//�ߵĿ��
            lineRenderer.SetPosition(0, transform.position);//��������
            lineRenderer.SetPosition(1, pos);//������������
            Invoke("ray", 0.1f);//����0.1���
            lineRenderer.enabled = true;//�����ٴγ���
        }
    }
    //���óɰ�ɫ���������ߣ�0.1�����ʧ�������õ��Թ���д����linerenderer
    private void ResetColor()
    {
        round.GetComponent<SpriteRenderer>().color = Color.white;//��ɫ��Ϊ��ɫ
    }
    private void ray()
    {
        lineRenderer.enabled = false;//������ʧ
    }


    //�������ߣ���ʽ�õģ�ֻ��Scene��ʾ���ĳ�Gizmos������Ϸ��Ҳ���Կ�����onDrawGizmos
   /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 10);
    }*/
 
    /*  timer += Time.deltaTime;�����������Ϊ���ӵĹ����в��Ǻ�׼
       if (timer >= 1)
       {
           round.GetComponent<SpriteRenderer>().color = Color.white;
           timer = 0;
           Debug.Log("ʱ��=" + timer);
       }*/
}
