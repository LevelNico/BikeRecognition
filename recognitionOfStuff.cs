using UnityEngine;
using System.Collections;
using Emgu.CV;    
using Emgu.CV.Util;    
using Emgu.CV.UI;           
using Emgu.CV.CvEnum;    
using Emgu.CV.Structure;    
using System.Runtime.InteropServices;    
using System;   

public class recognitionOfStuff : MonoBehaviour {

	Texture2D oldImage;
	Texture2D newImage;
	Color[] colors;
	float[,,] pix;
	float[,,] pix2; //two images so that we don't end up with a rippling effect when applying values.
	int w;
	int h;

	void Start(){
		oldImage = (Texture2D)renderer.material.mainTexture;
		colors = oldImage.GetPixels ();
		w = oldImage.width;
		h = oldImage.height;
		newImage = new Texture2D (w, h); 
	}

	void Update () {
		/*Image<Bgr, byte> picture = new Image<Bgr, byte>("C:\\picture1.jpg");
		Bgr myWhiteColor = new Bgr(255, 255, 255);
		for (int i=0; i<200; i++){
			picture[i,i]= myWhiteColor;
		}    
		picture.Save("C:\\picture2.jpg");*/

		pix = Color2Float (colors, w, h);

	}

	float[,,] Color2Float(Color[] col, int w, int h){
		float[,,] tempArr = new float[h, w, 3];
		for (int y=0; y<h ; y++){
			for (int x=0; x<w ; x++){
				tempArr[y,x,0]=col[y*w + x].r *255;
				tempArr[y,x,1]=col[y*w + x].g *255;
				tempArr[y,x,2]=col[y*w + x].b *255;
			}
		}
		return tempArr;
	}
	
	Color[]Float2Color(float[,,] pic, int w, int h){
		Color[] tempArra = new Color[w * h];
		for (int y=0; y<h; y++) {
			for (int x=0; x<w; x++) {
				tempArra [y * w + x].r = pic [y, x, 0] / 255;
				tempArra [y * w + x].g = pic [y, x, 1] / 255;
				tempArra [y * w + x].b = pic [y, x, 2] / 255;
			}
		}
		return tempArra;
	}
}