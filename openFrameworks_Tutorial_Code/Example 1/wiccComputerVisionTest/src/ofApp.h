// This code is based off of the ofBook chapter on Image Processing and Computer Vision found here: http://openframeworks.cc/ofBook/chapters/image_processing_computer_vision.html#suggestionsforfurtherexperimentation
#pragma once

#include "ofMain.h"
#include "ofxOpenCv.h"

class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();

		// Here in the header (.h) file we declare an ofImage:
        ofImage myImage;
		
};
