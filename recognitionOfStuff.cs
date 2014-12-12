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
	int[,] SobelYKernel = {{-1,0,1},{-2,0,2},{-1,0,1}};
	int[,] SobelXKernel = {{1,2,1},{0,0,0},{-1,-2,-1}};

	void Start(){
		oldImage = (Texture2D)renderer.material.mainTexture;
		colors = oldImage.GetPixels ();
		w = oldImage.width;
		h = oldImage.height;
		newImage = new Texture2D (w, h); 
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.O)){ //Sobel Edge Detector
			pix2 = Color2Float (colors, w, h);
			for (int y=1; y<h-1; y++) {
				for (int x=1; x<w-1; x++) {
					float tmp = pix2[y,x,0]+pix2[y,x,1]+pix2[y,x,2]/3;
					for (int c=0; c<3; c++) {
						pix2[y,x,c] = tmp;
					}
				}
			}
			for (int y=1; y<h-1; y++) {
				for (int x=1; x<w-1; x++) {
					float tmp = pix2[y,x,0]+pix2[y,x,1]+pix2[y,x,2]/3;
					for (int c=0; c<3; c++) {
						float sumy = 0f;
						float sumx = 0f;
						float sum = 0f;
						for (int ky=-1; ky<=1; ky++) {
							for (int kx=-1; kx<=1; kx++) {
								
								sumy += pix2[y+ky,x+kx,c]*SobelYKernel[ky+1,kx+1];
								sumx += pix2[y+ky,x+kx,c]*SobelXKernel[ky+1,kx+1];
							}//kx 
						}//ky
						sum = sumy+sumx;
						pix[y,x,c] = sum;
						
						//}
					}//c
				}//x
			}//y
		}//if

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