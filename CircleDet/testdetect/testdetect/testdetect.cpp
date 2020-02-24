// testdetect.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>

#include <opencv2/opencv.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/core.hpp> 
#include <opencv2/imgproc/imgproc.hpp>


using namespace cv;
using namespace std;

//int main()
extern "C" int __declspec(dllexport) __stdcall Load()
{
	Mat image = imread("C:/Users/Chroma/CHROMA/Snapshots/screenshot.png");
	if (image.empty())
	{
		cout << "No image" << endl;
		cin.get();
		return -1;
	}

	Mat gray;
	cvtColor(image, gray, COLOR_BGR2GRAY);
	//medianBlur(gray, gray, 5);
	vector<Vec3f> circles;
	HoughCircles(gray, circles, CV_HOUGH_GRADIENT, 1, gray.rows / 2, 50, 30, 1, 1000);
	cout << circles.size() << endl;
	for (size_t i = 0; i < circles.size(); i++) {
		Vec3i c = circles[i];
		Point center = Point(c[0], c[1]);
		circle(image, center, 1, Scalar(0, 100, 100), 3);
		int radius = c[2];
		circle(image, center, radius, Scalar(255, 0, 255), 3);
	}
	if (circles.size() > 0) {
		return 1;
	}
	else return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
