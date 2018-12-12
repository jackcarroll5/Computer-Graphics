using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour {

   public static Texture2D cubeTexture;
    Renderer cubeRenderer;

    private float rotationTexture = 10f;
    private Vector3 dirMovement;
    public Vector3 offset = new Vector3(3, 14, 3);
    private int textureWidth = 512;
   private int textureHeight = 512;
    private int trackingTime = 0;
    private int angle = 5;
    public Vector2 pivotRotation = new Vector2(1.0f,1.0f);
   
    //OutCodeTest test = new OutCodeTest();


   public List<Vector3> previousPixels;


   public Vector3[] cube;

    // Use this for initialization
    void Start () {
        cube = new Vector3[8];

        previousPixels = new List<Vector3>();

        Screen.SetResolution(1920, 1200, true);

        cubeTexture = new Texture2D(textureWidth,textureHeight);
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.mainTexture = cubeTexture;
       

        for(int x = 0; x < cubeTexture.width; x++)
        {
            for(int y = 0; y < cubeTexture.height; y++)
            {
                cubeTexture.SetPixel(x, y, Color.red);
            }        
        }
        
        cubeTexture.Apply();



       


        Camera cam = new Camera();

      
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3[] newImage = cube;

        /*Testing out the texture drawing of the cube model which is displayed
         in a white colour*/
        drawCube(newImage);
        cubeTexture.Apply();


/*Testing the results for the points after rotation which includes the matrix for rotation*/
        Vector3 startingAxis = new Vector3(14, 3, 3);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(-27, startingAxis);

        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0), //Translation Vector
                           rotation,
                            Vector3.one);
        print("Rotation Matrix: " + rotationMatrix.ToString());
       // printMatrix(rotationMatrix);


        Vector3[] imageAfterRotation =
                    MatrixTransform(cube, rotationMatrix);



       // Vector2[] screenImage = convertToScreen(imageAfterRotation, textureWidth, textureHeight);



      

        string rotArray = "";

        for(int i = 0; i < imageAfterRotation.Length; i++)
        {
           rotArray += "Image after rotation: " + imageAfterRotation[i] + "\n";
        }
        print(rotArray);



       // string screenRotArray = "";

        /*for (int i = 0; i < screenImage.Length; i++)
        {
            screenRotArray += "Screen Image after rotation: " + screenImage[i] + "\n";
        }
        print(screenRotArray);*/
        //printVerts(newImage);


        foreach (Vector2 pt in imageAfterRotation)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.grey);
            print(pt.x + "," + pt.y);
        }


        /*Code below used for testing drawing of texture of cube after rotation at beginning when starting the test before update method. Test Successful before 
         it was commented out*/
        /*drawCube(imageAfterRotation);
        cubeTexture.Apply();*/




        /*Testing the results for the points after scaling which includes the matrix for rotation*/
        Matrix4x4 scalingMatrix =
              Matrix4x4.TRS(Vector3.zero,
                              Quaternion.identity,
                              new Vector3(14, 1, 3));
        print("Scaling Matrix: " + scalingMatrix.ToString());
        //printMatrix(scalingMatrix);

        Vector3[] imageAfterScaling =
            MatrixTransform(imageAfterRotation, scalingMatrix);


        //Vector2[] screenImageScale = convertToScreen(imageAfterScaling, textureWidth, textureHeight);

        string scalArray = "";

        for (int i = 0; i < imageAfterScaling.Length; i++)
        {
           scalArray += "Image after scaling: " + imageAfterScaling[i].ToString() + "\n";
        }

        print(scalArray);

       

        //string screenScaleArray = "";

        /*for (int i = 0; i < screenImageScale.Length; i++)
        {
            screenScaleArray += "Screen Image after Scaling: " + screenImageScale[i] + "\n";
        }
        print(screenScaleArray);*/


        foreach (Vector2 pt in imageAfterScaling)
        {
           
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.green);
            print(pt.x + "," + pt.y);
        }


        /*Code below used for testing drawing of texture of cube after scaling at beginning when starting the test before update method. Test Successful before 
         it was commented out*/
        /*drawCube(imageAfterScaling);
        cubeTexture.Apply();*/



        /*Testing the results for the points after translation which includes the matrix for rotation*/
        Matrix4x4 translationMatrix =
              Matrix4x4.TRS(new Vector3(-2, -3, 4),
                              Quaternion.identity,
                               Vector3.one);
        print("Transforming Matrix: " + translationMatrix.ToString());
        //printMatrix(transformingMatrix);


        /*Image after Translating*/
        Vector3[] imageAfterTranslating =
            MatrixTransform(cube, translationMatrix);


        /*Code below used for testing drawing of texture of cube after translation at beginning when starting the test before update method. Test Successful before 
         it was commented out*/
        /*drawCube(imageAfterTranslating);
        cubeTexture.Apply();*/




       // Vector2[] screenImageTranslate = convertToScreen(imageAfterTranslating, textureWidth, textureHeight);

        string tranArray = "";

        for (int i = 0; i < imageAfterTranslating.Length; i++)
        {
            tranArray += "Image after translating: " + imageAfterTranslating[i].ToString() + "\n";
        }
        print(tranArray);


        foreach (Vector2 pt in imageAfterTranslating)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.yellow);
            print(pt.x + "," + pt.y);
        }

        cubeTexture.Apply();


        //string screenTranslateArray = "";

      /*  for (int i = 0; i < screenImageTranslate.Length; i++)
        {
            screenTranslateArray += "Screen Image after Translating: " + screenImageTranslate[i] + "\n";
        }
        print(screenTranslateArray);*/



        //Reverse Order
        Matrix4x4 singleMatrix = (translationMatrix * scalingMatrix * rotationMatrix);
        print("Single Matrix: " + singleMatrix.ToString());




        /*Image after Transformations*/
        Vector3[] imageAfterTransformations = MatrixTransform(cube,singleMatrix);

        //Vector2[] screenImageTranform = convertToScreen(imageAfterTransformations, textureWidth, textureHeight);

        string imageArray = "";

        for(int i = 0; i < imageAfterTransformations.Length; i++)
        {
            imageArray += "Image after transformations: " + imageAfterTransformations[i].ToString() + "\n";
        }
             print(imageArray);

        //drawCube(imageAfterTransformations);



        //string screenTransformArray = "";

        /*for (int i = 0; i < screenImageTranform.Length; i++)
        {
            screenTransformArray += "Screen Image after Transforming: " + screenImageTranform[i] + "\n";
        }
        print(screenTransformArray);*/




        /*Viewing Matrix*/
        Vector3 lookAt = new Vector3(3, 14, 3);
        Vector3 camPos = new Vector3(16,6,53);
        Vector3 up = new Vector3(4,3,14);

        Vector3 forward = (lookAt - camPos).normalized;
        up.Normalize();

       
        //Forward and up
      Quaternion moveScene =  Quaternion.LookRotation(forward,up.normalized);
        Vector3 translationCam = -camPos;


        Matrix4x4 viewingMatrix = Matrix4x4.TRS(translationCam, moveScene,Vector3.one);
        print("Viewing Matrix: " + viewingMatrix.ToString());













        /*Image after Viewing Matrix*/
        Vector3[] imageAfterVM = MatrixTransform(imageAfterTransformations, viewingMatrix);

        //Vector2[] screenImageVM = convertToScreen(imageAfterVM, textureWidth, textureHeight);



        string vmImage = "";

        for (int i = 0; i < imageAfterVM.Length; i++)
        {
            vmImage += "Image after Viewing Matrix: " + imageAfterVM[i].ToString() + "\n";
        }
        //print(vmImage);



        //string screenVMArray = "";

        /*for (int i = 0; i < screenImageVM.Length; i++)
        {
            screenVMArray += "Screen Image after Viewing Matrix: " + screenImageVM[i] + "\n";
        }
        print(screenVMArray);*/


        //drawCube(imageAfterVM);





        /*Projection Matrix*/
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(45, (Screen.width / Screen.height), 1, 1000);
        print("Projection Matrix: " + projectionMatrix.ToString());


        /*Image after Projection Matrix*/
        Vector3[] lastImage = MatrixTransform(imageAfterVM, projectionMatrix);


        //Vector2[] screenImageProjection = convertToScreen(lastImage, textureWidth, textureHeight);


        string projectionImage = "";

        for (int i = 0; i < lastImage.Length; i++)
        {
            projectionImage += "Image after Projection Matrix: " + lastImage[i].ToString() + "\n";
        }
       // print(projectionImage);



        //string screenProjectArray = "";

       /* for (int i = 0; i < screenImageProjection.Length; i++)
        {
            screenProjectArray += "Screen Image after Projection: " + screenImageProjection[i] + "\n";
        }
        print(screenProjectArray);*/


        //drawCube(lastImage);







        /*Single Matrix for everything*/
        //Reverse Order
        Matrix4x4 singleMatrixEverything = (projectionMatrix * viewingMatrix * singleMatrix);
        print("Single Matrix for everything: " + singleMatrixEverything.ToString());



        /*Image after Single Matrix of Everything*/
        Vector3[] imageEverything = MatrixTransform(cube, singleMatrixEverything);


        //Vector2[] screenImageFinal = convertToScreen(imageEverything, textureWidth, textureHeight);

        string everyImage = "";

        for (int i = 0; i < imageEverything.Length; i++)
        {
            everyImage += "Image after Single Matrix of Everything: " + imageEverything[i].ToString() + "\n";
        }
        //print(everyImage);



        //string screenFinalArray = "";

        /*for (int i = 0; i < screenImageFinal.Length; i++)
        {
            screenFinalArray += "Screen Image after Single Matrix of Everything: " + screenImageFinal[i] + "\n";
        }
        print(screenFinalArray);*/



        //drawCube(imageEverything);





        OutCodeTest testRasterisation = new OutCodeTest();
        //List<Vector2> listRaster = testRasterisation.rasteriseBreshenhams(imageEverything[0], imageEverything[1]);




        //Normal
        Vector2 start = new Vector2(3, 2);
        Vector2 end = new Vector2(10, 7);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> listRaster = testRasterisation.rasteriseBreshenhams(start, end);


        foreach (Vector2 pt in listRaster)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.green);
            print(pt.x + "," + pt.y);
        }

       cubeTexture.Apply();



        /*Implementing rasterisation with various constraints and rules*/

        /*Swap XY if dx < 0*/
        start = new Vector2(3, 7);
        end = new Vector2(15, 30);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> lRaster = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in lRaster)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.yellow);
            print(pt.x + "," + pt.y);
        }








        /*Negation of Y if dy < 0*/
        start = new Vector2(6, 24);
        end =  new Vector2(17, 15);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> listRast = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in listRast)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.cyan);
            print(pt.x + "," + pt.y);
        }










        /*Swap XY if dy > dx*/
        start = new Vector2(3, 6);
        end = new Vector2(3, 12);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> listRa = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in listRa)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.magenta);
            print(pt.x + "," + pt.y);
        }









        /*Combo of dy < 0 (Negate) and Swap values (dy > dx)*/
        start = new Vector2(31, 51);
        end = new Vector2(43, 23);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> listRas = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in listRas)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.black);
            print(pt.x + "," + pt.y);
        }











        /*Combo of dy < 0 (Negate) and Slope > 1*/
        start = new Vector2(52, 32);
        end = new Vector2(60, 20);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> lstRaster = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in lstRaster)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.white);
            print(pt.x + "," + pt.y);
        }





        /*All combos*/
        start = new Vector2(60, 20);
        end = new Vector2(52, 32);
        print("Start Pt " + start + "\nEnd Pt " + end);

        List<Vector2> listR = testRasterisation.rasteriseBreshenhams(start, end);

        foreach (Vector2 pt in listR)
        {
            cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.grey);
            print(pt.x + "," + pt.y);
        }



    }

  

    private void printVerts(Vector3[] newImage)
    {
        for (int i = 0; i < newImage.Length; i++)
            print("Vertex: " + newImage[i].x + " , " +
                newImage[i].y + " , " +
                newImage[i].z);

    }


    //Drawing the lines of the cube within the cube model using texture pixels
    public void drawCube(Vector3[] cubeImage)
    {
        lineDraw(cubeImage[0], cubeImage[1]);
       lineDraw(cubeImage[1], cubeImage[2]);
        lineDraw(cubeImage[2], cubeImage[3]);
        lineDraw(cubeImage[3], cubeImage[0]);

        lineDraw(cubeImage[4], cubeImage[5]);
        lineDraw(cubeImage[5], cubeImage[6]);
        lineDraw(cubeImage[6], cubeImage[7]);
        lineDraw(cubeImage[7], cubeImage[4]);

       
        lineDraw(cubeImage[0], cubeImage[4]);
        lineDraw(cubeImage[1], cubeImage[5]);
        lineDraw(cubeImage[2], cubeImage[6]);
        lineDraw(cubeImage[3], cubeImage[7]);


    }


    //Method for drawing line based on line clipping and applying Breshenhams to it with start and end line
    private void lineDraw(Vector3 start, Vector3 end)
    {
        Vector2 startLine = new Vector2(start.x / start.z, start.y / start.z);
        Vector2 endLine = new Vector2(end.x / end.z, end.y / end.z);

        

        if (LineClip(ref startLine, ref endLine))
        {
            startLine = new Vector2(resolutionX(startLine.x,textureWidth),resolutionY(startLine.y,textureHeight));
            endLine = new Vector2(resolutionX(endLine.x, textureWidth), resolutionY(endLine.y, textureHeight));


            //convertToScreen(startLine, endLine, textureWidth, textureHeight);


            OutCodeTest testRasterisation = new OutCodeTest();

           // Vector2 startConvert = convertToScreen(startLine,textureWidth,textureHeight);

            List<Vector2> listRaster = testRasterisation.rasteriseBreshenhams(startLine, endLine);


            foreach (Vector2 pt in listRaster)
            {

                cubeTexture.SetPixel((int)pt.x, (int)pt.y, Color.blue);             
                previousPixels.Add(pt);
            }
            cubeTexture.Apply();

        }
    }

    public static Texture2D rotateTexture(Texture2D textureCube)
    {
        Texture2D newTexture = cubeTexture;

        for(int i = 0; i < textureCube.width; i++)
        {
            for (int j = 0; j < textureCube.height; j++)
            {
                newTexture.SetPixel(i, j, textureCube.GetPixel(textureCube.width - i, j));
            }
        }


        newTexture.Apply();
        return newTexture;
    }

    private void clearPixels()
    {
        foreach(Vector2 pixel in previousPixels)
        {
            cubeTexture.SetPixel((int)pixel.x, (int)pixel.y, cubeRenderer.material.color);
           
        }
        previousPixels.Clear();     
    }

    public bool LineClip(ref Vector2 startPoint, ref Vector2 endPoint)
    {
        Outcode startOutcode = new Outcode(startPoint); //x = Start
        Outcode endOutcode = new Outcode(endPoint); //y = End

        if ((startOutcode == new Outcode()) && (endOutcode == new Outcode()))
        {
            print("Trivially Accept");
            return true; //Trivial Acceptance         
        }

        if (((startOutcode & endOutcode) != new Outcode()))
        {
            print("Trivially Reject");
            return false; //Trivial Rejection
        }

        // Work to do

        // Clip start Point
        if (startOutcode.up)
        {
            startPoint = LineIntercept(startPoint, endPoint, 0);

            return LineClip(ref startPoint, ref endPoint);

        }

        else if (startOutcode.down)
        {
            startPoint = LineIntercept(startPoint, endPoint, 1);

            return LineClip(ref startPoint, ref endPoint);

        }

        else if (startOutcode.left)
        {
            startPoint = LineIntercept(startPoint, endPoint, 2);

            return LineClip(ref startPoint, ref endPoint);
        }

        else if (startOutcode.right)
        {
            startPoint = LineIntercept(startPoint, endPoint, 3); //Right
            return LineClip(ref startPoint, ref endPoint);
        }

        return LineClip(ref endPoint, ref startPoint);

    }

    private float Slope(Vector2 xPoint, Vector2 yPoint)
    {
        return ((yPoint.y - xPoint.y) / (yPoint.x - xPoint.x));
    }


    private Vector2 LineIntercept(Vector2 startPoint, Vector2 endPoint, int v)
    {
        float slope = Slope(startPoint, endPoint);

        switch (v)
        {
            case 0:  //Top Edge  y = 1
                return new Vector2(startPoint.x + (1 / slope) * (1 - startPoint.y), 1);

            case 1:  //Bottom Edge  y = -1
                return new Vector2(startPoint.x + (1 / slope) * (-1 - startPoint.y), -1);

            case 2:  //Left Edge  x= -1
                return new Vector2(-1, startPoint.y + slope * (-1 - startPoint.x));

            default:  //Right Edge  x= 1
                return new Vector2(1, startPoint.y + slope * (1 - startPoint.x));

        }
    }

    int resolutionX(float x, int xResolution)
    {
        return (int)((x + 1) * (xResolution - 1) / 2.0f);
    }
    int resolutionY(float y, int yResolution)
    {
        return (int)((1 - y) * (yResolution - 1) / 2.0f);
    }

    private Vector3[] MatrixTransform(
        Vector3[] meshVertices,
        Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];

        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix *
                new Vector4(
                meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                    1);

        return output;
    }


    private void printMatrix(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i).ToString());
    }


    /*Smaller cube texture than cube for scaling texture but bigger than translation cube
     * within Cube marked in blue. Rotation occurs here around rotation matrix*/
    private Vector3[] rotationcube(Vector3[] cube)
    {
        Vector3 startingAxis = new Vector3(14, 3, 3);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(-60, startingAxis);

        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 3), //Translation Vector
                           rotation,
                            Vector3.one);

       

        Vector3[] imageAfterRotation =
                    MatrixTransform(cube, rotationMatrix);

        drawCube(imageAfterRotation);

        return imageAfterRotation;
    }


    /*Biggest cube texture within Cube marked in blue*/
    private Vector3[] scalingCube(Vector3[] cube)
    {
        Vector3 startingAxis = new Vector3(14, 3, 3);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(-60, startingAxis);

        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 3), //Translation Vector
                           rotation,
                            Vector3.one);


        Vector3[] imageAfterRotation =
                      MatrixTransform(cube, rotationMatrix);

        Matrix4x4 scalingMatrix =
            Matrix4x4.TRS(Vector3.zero,
                            Quaternion.identity,
                            new Vector3(6, 4, 3));
       

        Vector3[] imageAfterScaling =
            MatrixTransform(imageAfterRotation, scalingMatrix);

        drawCube(imageAfterScaling);

        return imageAfterScaling;

    }


    /*Smallest cube texture within Cube marked in blue*/
    private Vector3[] translateCube(Vector3[] cube)
    {

        Matrix4x4 translationMatrix =
              Matrix4x4.TRS(new Vector3(0, 0, 3),
                              Quaternion.identity,
                               Vector3.one);
       


        /*Image after Translating*/
        Vector3[] imageAfterTranslating =
            MatrixTransform(cube, translationMatrix);

        drawCube(imageAfterTranslating);

        return imageAfterTranslating;

    }


    private Vector3 dirMoving()
    {
        if (trackingTime <= 100)
        {
            dirMovement = new Vector3(-0.05f, -0.05f, 0);
        }
        else if (trackingTime <= 200)
        {
            dirMovement = new Vector3(0, 0.05f, 0);
        }
        else if (trackingTime <= 300)
        {
            dirMovement = new Vector3(0.05f, -0.05f, 0);
        }
        else if (trackingTime <= 400)
        {
            dirMovement = new Vector3(0.05f, 0, 0);
        }
        else if (trackingTime <= 500)
        {
            dirMovement = new Vector3(-0.5f, 0.05f, 0);
        }
        else if (trackingTime <= 600)
        {
            dirMovement = new Vector3(0, -0.5f, 0);
        }
        else if (trackingTime <= 700)
        {
            dirMovement = new Vector3(-0.5f, 0.05f, 0);
        }
        else if (trackingTime <= 800)
        {
            dirMovement = new Vector3(-0.5f, 0, 0);
        }
        else
        {
            trackingTime = 0;
        }

        return dirMovement;

    }


    // Update is called once per frame
    void Update () {


        /*Attempt to rotate and move the cube textures in real time*/
        Matrix4x4 t = Matrix4x4.TRS(-pivotRotation, Quaternion.identity, Vector3.one);

        float time = Time.time * rotationTexture;
        Quaternion rotation = Quaternion.Euler(0, 0, time);
        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
        Matrix4x4 tInv = Matrix4x4.TRS(pivotRotation, Quaternion.identity, Vector3.one);
        cubeRenderer.material.SetMatrix("_Rotation", tInv * r * t);

 

        clearPixels();
        //drawCube(cube);

        //TRS Trying to get texture to rotate

        /*Based on initial points from first assignment to test out cube textures drawn within cube after translation,rotation and scaling*/      
        if(trackingTime % 10 == 0)
        {         
         
            translateCube(cube);
            rotateTexture(cubeTexture);
            cubeTexture.Apply();
            angle += 1;
        }
     

        if (trackingTime % 20 == 0)
        {
            rotationcube(cube);
            rotateTexture(cubeTexture);
            cubeTexture.Apply();
            angle += 1;
        }

        if (trackingTime % 30 == 0)
        {        
            scalingCube(cube);
            rotateTexture(cubeTexture);
            cubeTexture.Apply();
            angle += 1;
        }

  

    }
}
