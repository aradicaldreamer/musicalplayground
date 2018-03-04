// This code is based off of the ofBook chapter on Image Processing and Computer Vision found here: http://openframeworks.cc/ofBook/chapters/image_processing_computer_vision.html#suggestionsforfurtherexperimentation
#include "ofMain.h"
#include "ofApp.h"

//========================================================================
int main( ){
	ofSetupOpenGL(1024,768,OF_WINDOW);			// <-------- setup the GL context

	// this kicks off the running of my app
	// can be OF_WINDOW or OF_FULLSCREEN
	// pass in width and height too:
	ofRunApp(new ofApp());

}
