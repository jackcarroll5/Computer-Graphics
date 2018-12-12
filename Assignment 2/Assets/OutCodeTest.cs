using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCodeTest : MonoBehaviour {

    Vector2 start, end;
    Matrices line = new Matrices();

    public OutCodeTest(Vector2 begin, Vector2 finish)
    {
        begin = new Vector2(3, 2);
        finish = new Vector2(10, 7);
    }

    public OutCodeTest()
    {
       
    }

   

    // Use this for initialization

/*Testing of outcodes to see if trivial rejection or acceptance occurs according to start and end points*/
    void Start () {
        print("Test 1");
        start = new Vector2(-0.5f,-0.5f);
        end = new Vector2(0,0.5f);
        print("Start Pt" + start.ToString());
        print("End Pt" + end.ToString());

       
/*Implements line clipping to discover results of outcode trivial acceptance or rejection*/
       line.LineClip(ref start, ref end);//Expected = Trivial Acceptance
        print("Start Pt" + start.ToString());
        print("End Pt" + end.ToString());



        Vector2 startR, endR;
        print("Test 2");

        startR = new Vector2(1.2f,0.5f);
        endR = new Vector3(2.3f,-3);

        print("Start Pt" + startR.ToString());
        print("End Pt" + endR.ToString());

        line.LineClip(ref startR, ref endR);//Expected = Trivial Rejection
        print("Start Pt" + startR.ToString());
        print("End Pt" + endR.ToString());



        Vector2 startC, endC;
        print("Test 3");

        startC = new Vector2(2, 0.5f);
        endC = new Vector3(0.5f, 1.3f);

        print("Start Pt" + startC.ToString());
        print("End Pt" + endC.ToString());

        line.LineClip(ref startC, ref endC);//Reject
        print("Start Pt" + startC.ToString());
        print("End Pt" + endC.ToString());


        Vector2 s, e;
        print("Test 4");

        s = new Vector2(2,2);
        e = new Vector3(0,-1.5f);
        print("Start Pt" + s.ToString());
        print("End Pt" + e.ToString());

        line.LineClip(ref s, ref e);//Main Clip
        print("Start Pt" + s.ToString());
        print("End Pt" + e.ToString());

        Vector2 st, en;
        print("Test 5");

        st = new Vector2(-1.3f, -1.4f);
        en = new Vector3(2, 0);
        print("Start Pt" + st.ToString());
        print("End Pt" + en.ToString());

        line.LineClip(ref st, ref en);//Main Clip
        print("Start Pt" + st.ToString());
        print("End Pt" + en.ToString());









    }
	
	// Update is called once per frame
	void Update () {

        
	
	}


/*Rasterisation Algortihm - Breshenhams*/
   public List<Vector2> rasteriseBreshenhams(Vector2 start, Vector2 end)
    {
        int dx = (int)(end.x - start.x);
        int dy = (int)(end.y - start.y);

        if(dx < 0)
        {
            return rasteriseBreshenhams(end, start);
        }
        if (dy < 0)
        {
          return negateY(rasteriseBreshenhams(negateY(start), negateY(end)));
        }
        if (dy > dx)
        {
           return swapXY(rasteriseBreshenhams(swapXY(start), swapXY(end)));
        }


        List<Vector2> output = new List<Vector2>();  
        output.Add(start);   
        
        float p = 2 * dy - dx;
        int y = (int)start.y;
        int dy2 = (2 * dy);
        int dyMinusdx2 = (2 * (dy - dx));


        for (int j = (int)start.x + 1; j <= end.x; j++)
        {
            if (p < 0)
            {
                p = p + dy2;
            }
            else
            {
                y++;
                p = p + dyMinusdx2;
            }
            output.Add(new Vector2(j, y));

        }
        return output;
    }


    //public List<Vector2> rasteriseBresh(int x1, int y1, int x2, int y2)
    //{
    //    int dx = x2 - x1;
    //    int dy = y2 - y1;

    //    if(dx < 0)
    //    {
    //        return rasteriseBresh(end, start);
    //    }
    //    if (dy < 0)
    //    {
    //      return negateY(rasteriseBresh(negateY(start), negateY(end)));
    //    }
    //    if (dy > dx)
    //    {
    //       return swapXY(rasteriseBresh(swapXY(start), swapXY(end)));
    //    }


    //    List<Vector2> output = new List<Vector2>();  
    //    output.Add(start);   
        
    //    float p = 2 * dy - dx;
    //    int y = (int)start.y;
    //    int dy2 = (2 * dy);
    //    int dyMinusdx2 = (2 * (dy - dx));


    //    for (int j = (int)start.x + 1; j <= end.x; j++)
    //    {
    //        if (p < 0)
    //        {
    //            p = p + dy2;
    //        }
    //        else
    //        {
    //            y++;
    //            p = p + dyMinusdx2;
    //        }
    //        output.Add(new Vector2(j, y));

    //    }
    //    return output;
    //}


//Negating Y in rasterisation
    Vector2 negateY(Vector2 pt)
    {
        return new Vector2(pt.x, -pt.y);
    }

//List of points that have a negation of Y in rasterisation
    List<Vector2> negateY(List <Vector2> pts)
    {
        List<Vector2> outputList = new List<Vector2>();
        foreach (Vector2 pt in pts)
        {
            outputList.Add(negateY(pt));
        }
           
        return outputList;
    }

//Swap XY in rasterisation
    Vector2 swapXY(Vector2 pt)
    {
        float refPt = pt.x;
        pt.x = pt.y;
        pt.y = refPt;
        return pt;
    }

    Vector2 swappingXY(Vector2 pt)
    {
        return new Vector2(pt.y, pt.x);
    }

//List of points that involve swapping X and Y in rasterisation
    List<Vector2> swapXY(List <Vector2> pts)
    {
        List<Vector2> outputList = new List<Vector2>();
        foreach (Vector2 pt in pts)
        {
            outputList.Add(swapXY(pt));
        }
           
        return outputList;
    }

   

   


    
}
