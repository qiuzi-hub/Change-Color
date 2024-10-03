using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Move : MonoBehaviour
{
    public LayerMask layer;
    public GameObject round;//被射击的物体
    LineRenderer lineRenderer;//射线                
    // Update is called once per frame
    void Update()
    {
        //得到鼠标位置
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //得到方向，炮塔的朝向的向量，鼠标所在的位置-炮塔当前的位置，z轴不转，因为是2D
        Vector3 rotate = pos - transform.position;
        rotate.z = 0;
        //炮塔旋转，只要上，所以up，赋给鼠标，注意还没开始时炮口的方向
        transform.up = rotate;
        //点击鼠标左键，发射子弹（发射射线），看有没有打中目标物体
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit= Physics2D.Raycast(transform.position, rotate, 20f, layer); //射线，用第四个
            if (hit.collider) {
                //laymask，设置层，返回raycasthit23
                //设置颜色
                hit.collider.GetComponent<SpriteRenderer>().color = Color.blue;
                Invoke("ResetColor", 1f);//经过一秒后
            }
            lineRenderer = GetComponent<LineRenderer>();//获取LineRenderer组件
            lineRenderer.startWidth = lineRenderer.endWidth = 0.05f;//线的宽度
            lineRenderer.SetPosition(0, transform.position);//绘制射线
            lineRenderer.SetPosition(1, pos);//点击点绘制射线
            Invoke("ray", 0.1f);//经过0.1秒后
            lineRenderer.enabled = true;//射线再次出现
        }
    }
    //重置成白色，画出射线，0.1秒后消失，不能用调试工具写，用linerenderer
    private void ResetColor()
    {
        round.GetComponent<SpriteRenderer>().color = Color.white;//颜色变为白色
    }
    private void ray()
    {
        lineRenderer.enabled = false;//射线消失
    }


    //辅助工具，调式用的，只在Scene显示，改成Gizmos，在游戏中也可以看到，onDrawGizmos
   /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 10);
    }*/
 
    /*  timer += Time.deltaTime;不用这个，因为增加的过程中不是很准
       if (timer >= 1)
       {
           round.GetComponent<SpriteRenderer>().color = Color.white;
           timer = 0;
           Debug.Log("时间=" + timer);
       }*/
}
