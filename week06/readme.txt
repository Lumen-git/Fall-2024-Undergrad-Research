Changes to Surface Detection
	+ Now uses AR Foundation's normal detection, classification, and debug plane to show data

Room Detection
	+ Uses the 'in object' detection to test if the 3D cursor is in a room

Acceleration sensors
	+ Uses the input system to get acceleration, linear acceleration, and gravity
	> Acceleration is all acceleration form accelerometer
	> Linear is acceleration - gravity
	> Gravity is acceleration due to gravity

	* Need to add low pass filter, experiment with changing phone direction