using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour {

    public Vector3 offset = new Vector3(3, 14, 3);

	// Use this for initialization
	void Start () {

        Screen.SetResolution(1920, 1200, true);

        Camera cam = new Camera();

        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3[] newImage = cube;


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


        string rotArray = "";

        for(int i = 0; i < imageAfterRotation.Length; i++)
        {
           rotArray += "Image after rotation: " + imageAfterRotation[i] + "\n";
        }
        print(rotArray);     
        //printVerts(newImage);



        Matrix4x4 scalingMatrix =
              Matrix4x4.TRS(Vector3.zero,
                              Quaternion.identity,
                              new Vector3(14, 1, 3));
        print("Scaling Matrix: " + scalingMatrix.ToString());
        //printMatrix(scalingMatrix);

        Vector3[] imageAfterScaling =
            MatrixTransform(imageAfterRotation, scalingMatrix);

        string scalArray = "";

        for (int i = 0; i < imageAfterScaling.Length; i++)
        {
           scalArray += "Image after scaling: " + imageAfterScaling[i].ToString() + "\n";
        }

        print(scalArray);
       // printVerts(newImage);


     
    
        Matrix4x4 translationMatrix =
              Matrix4x4.TRS(new Vector3(-2, -3, 4),
                              Quaternion.identity,
                               Vector3.one);
        print("Transforming Matrix: " + translationMatrix.ToString());
        //printMatrix(transformingMatrix);


        /*Image after Translating*/
        Vector3[] imageAfterTranslating =
            MatrixTransform(imageAfterScaling, translationMatrix);

        string tranArray = "";

        for (int i = 0; i < imageAfterTranslating.Length; i++)
        {
            tranArray += "Image after translating: " + imageAfterTranslating[i].ToString() + "\n";
        }
        print(tranArray);
       // printVerts(newImage);



        //Reverse Order
        Matrix4x4 singleMatrix = (translationMatrix * scalingMatrix * rotationMatrix);
        print("Single Matrix: " + singleMatrix.ToString());




        /*Image after Transformations*/
        Vector3[] imageAfterTransformations = MatrixTransform(cube,singleMatrix);

            string imageArray = "";

        for(int i = 0; i < imageAfterTransformations.Length; i++)
        {
            imageArray += "Image after transformations: " + imageAfterTransformations[i].ToString() + "\n";
        }
             print(imageArray);




        /*Viewing Matrix*/
        Vector3 lookAt = new Vector3(3, 14, 3);
        Vector3 camPos = new Vector3(16,6,53);
        Vector3 up = new Vector3(4,3,14);

        Vector3 forward = (lookAt - camPos).normalized;

       
        //Forward and up
      Quaternion moveScene =  Quaternion.LookRotation(forward,up.normalized);


        Vector3 translationCam = -camPos;


        Matrix4x4 viewingMatrix = Matrix4x4.TRS(translationCam, moveScene,Vector3.one);
        print("Viewing Matrix: " + viewingMatrix.ToString());



        /*Image after Viewing Matrix*/
        Vector3[] imageAfterVM = MatrixTransform(imageAfterTransformations, viewingMatrix);

        string vmImage = "";

        for (int i = 0; i < imageAfterVM.Length; i++)
        {
            vmImage += "Image after Viewing Matrix: " + imageAfterVM[i].ToString() + "\n";
        }
        print(vmImage);


        /*Projection Matrix*/
     Matrix4x4 projectionMatrix = Matrix4x4.Perspective(45, (Screen.width / Screen.height), 1, 1000);
        print("Projection Matrix: " + projectionMatrix.ToString());


        /*Image after Projection Matrix*/
        Vector3[] lastImage = MatrixTransform(imageAfterVM, projectionMatrix);

        string projectionImage = "";

        for (int i = 0; i < lastImage.Length; i++)
        {
            projectionImage += "Image after Projection Matrix: " + lastImage[i].ToString() + "\n";
        }
        print(projectionImage);



        /*Single Matrix for everything*/
        //Reverse Order
        Matrix4x4 singleMatrixEverything = (projectionMatrix * viewingMatrix * singleMatrix);
        print("Single Matrix for everything: " + singleMatrixEverything.ToString());



        /*Image after Single Matrix of Everything*/
        Vector3[] imageEverything = MatrixTransform(cube, singleMatrixEverything);

        string everyImage = "";

        for (int i = 0; i < imageEverything.Length; i++)
        {
            everyImage += "Image after Single Matrix of Everything: " + imageEverything[i].ToString() + "\n";
        }
        print(everyImage);

        /*Projection by hand*/
       /* Matrix4x4 handProjection = new Matrix4x4(new Vector4(1,0,0,0),
                                                 new Vector4(0,1,0,0),
                                                 new Vector4(0, 0, 1, 0),
                                                 new Vector4(0, 0, -1, 0));

        print("Projection by Hand: " + handProjection.ToString());*/



        /*Image after Projection by Hand*/
        /*Vector3[] imageProjectionHand = MatrixTransform(imageAfterVM, handProjection);

        string projectionHandImage = "";

        for (int i = 0; i < imageProjectionHand.Length; i++)
        {
            projectionHandImage += "Image after Projection by Hand: " + imageProjectionHand[i].ToString() + "\n";
        }
        print(projectionHandImage);*/

        /*Projection by hand*/
        /*Matrix4x4 handProjectionV2 = new Matrix4x4(new Vector4(1, 0, 0, 0),
                                                 new Vector4(0, 1, 0, 0),
                                                 new Vector4(0, 0, 1, -1),
                                                 new Vector4(0, 0, 0, 0));

        print("Projection by Hand V2: " + handProjectionV2.ToString());*/



        /*Image after Projection by Hand*/
        /*Vector3[] imageProjectionHandV2 = MatrixTransform(imageAfterVM, handProjectionV2);

        string projectionHandImageV2 = "";

        for (int i = 0; i < imageProjectionHandV2.Length; i++)
        {
            projectionHandImageV2 += "Image after Projection by Hand: " + imageProjectionHandV2[i].ToString() + "\n";
        }
        print(projectionHandImageV2);*/

    }

    private void printVerts(Vector3[] newImage)
    {
        for (int i = 0; i < newImage.Length; i++)
            print("Vertex: " + newImage[i].x + " , " +
                newImage[i].y + " , " +
                newImage[i].z);

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


    // Update is called once per frame
    void Update () {
		
	}
}
