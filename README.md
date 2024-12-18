# Fall-2024-Undergrad-Research

This repository contains all of the key work from my Fall 2024 undergrad research project, where I worked with the Indiana University computer graphics courses and instructor Mitja Hmeljak.
The goal of this project was to develop a curriculum to teach AR graphics capabilities at IU, as well as integrate AR into the existing graphics courses, using Unity and it's AR Foundation package. 
The work is broken down by week, with the completed code base and guides I made stored separately. Any missing week just means school was on break or the class part of research took precedent that week, rather than the actual research project itself.

### Philosophy
This is my personal philosophy for taking on this project and working through it, and does not reflect those of anyone else involved. There are 2 main things that motivated me for this project. First, AR can be a powerful tool when teaching students 3D environments and graphics. Navigating a 3D environment with 2D inputs can be cumbersome, and using AR allows students to use their intuitive understanding of the real world and motion to better understand the environment. Motion in 2D is also limited to moment along 1 axis or 1 plane, but bringing motion into AR allows for true 3D movement. For a new student, objects being 'over here' and 'over there' could be more helpful than knowing an object is a (1,1,0) and (5,0,5). This is especially true of lights, where in Unity the are represented as invisible points. Its easy to get lost in positioning when endlessly rotating and scrolling, but in the real world, we can see the light, the object, and move around it in an easy to understand way, seeing the whole scene and positioning at once. If we can develop an easy way to onboard students on to AR, further graphical work can be done with a more intuitive understanding of the environment. We can also use this to create an introduction into AR graphics and further AR work, as shown with the guides we created.

The second goal that motivated me in this project was learning AR and app development in a way that meaningfully took advantage of the AR environment. There are many apps out there that use AR, but basically act as some bit of data overlayed onto a camera feed, with no real interaction with the real world. Sure, it would be cool to see a picture someone posted floating in space or an arrow floating in front of me showing which way to go, but all this amounts to is Instagram or a map app that is more inconvenient to use. During this project, I wanted to make sure any AR application truly had reason to be in AR, and not just a 3D object you had to hold your phone a certain way to see. For the most part I held true to this, the exception being the acceleration demo which is just a bar graph on top of a camera feed, but for all intents and purposes we can just say that was to pay homage to early pre-AR Kit applications.

### Week 4
To start I made a simple plane detection test and a custom collision script that allows the user to use a 3D cursor to 'tap' walls or floors and test collisions. I believe for this first demo I made it so only the first plane detected was stored and used for collisions, as well as only detected floors so I could focus on testing one axis at a time. This demo left a lot to be desired and was heavily improved over time. Even with a year of Unity under my belt at this point, this was my first adventure into AR and some of the maths we would use, and it definitely shows.

### Week 5
Fixed up demo 1 a little bit to work on planes. I also created a second demo to test 'in object detection'. This demo allows the user to spawn a cube made out of the same data structure AR Foundation uses for surface meshes. This was actually significantly easier than testing for collisions with a surface, since a surface requires some complex dot products to test for the 1D line bounds of a mesh, but testing for the 2D plane bounds of an object simply requires checking the signs of the distance to each plane. If all distances are negative, the point is within the object. This fails on very complex or concave objects, but for our simple tests of data input and digital/real world environment meshing I decided to move onto the next demos and return to this if time allowed (it did not).

### Week 6
Unity provides a collection of demos for AR Foundation, and one of these demos includes a 'Debug Plane' prefab which shows which way the normal of the surface is facing, as well as its classifications. I added this to the surface detection demo to better understand how Unity saw the world. I also turned the in object demo into an 'in room' demo, which is the exact same as the in object demo, but passes if all distances are positive instead of negative. I also did some testing with getting acceleration data from the phone, which is not a part of AR Foundation but a direct feed from Unity's input system, but due to the way I tried to represent the data this was a noisy mess and was fully revamped in a later week.

### Week 7
Changed the acceleration demo to be more clear. This demo shows total acceleration, linear acceleration (acceleration without the effect of gravity), and acceleration due to gravity. This was a pretty simple week, so I also made 3 additional demos purely out of my own interest. Demo X is a simple surface detector, but applies a texture to the detected surfaces. Demo Y creates a floor plan out of a scanned room and exports it in the UDFM format, which means you can 3D scan a room and load it into the 1994 video game Doom II. As far as I'm concerned, this is the peak of computer science and honestly we might as well turn off all computers at this point. Demo Z, which was later removed due to compiling issues, tested AR Foundation's 2D image tracking by attaching simple models to detected April Tags.

### Weeks 8-10
With the demos done, it was time to refine everything I created and learned into 3 guides for students to follow to teach them the basics of AR. Guide 1 goes over basic project setup and installing AR Foundation. Guide 2 is simple surface detection, visualization, and testing. Guide 3 goes over creating the collision algorithm and creates a tool to allow the user to throw balls at surfaces and see the collisions in real time. Since these guides are likely to be given to someone with no Unity experience, I also sprinkled in some Unity basics such as how to use the hierarchy, inspector, and prefabs. Rough drafts were given to students working in an adjacent lab and TAs of the current graphics course, which they followed and provided some really good feedback for. Finally, I polished the guides and prepared for the final weeks.

### Week 12
A little more work on the guides and testing the light estimation in AR Foundations. Around this time Unity, XCode, and GitHub Desktop gave me a series of errors in a highly coordinated triple pronged attack. I managed to get the basics of light working, but there is still some work needed to make sure it's fully working. The demo exists in the code, but shouldn't be relied on as a fully working model. As the semester was ending and presentations were due, the project came to a close. As the worst designed villain in all of 80s Sci-Fi once said, 'End of Line.'

### Week 13
The poster session! In this weeks folder you can find the final poster that was presented at the undergrad research presentations. It contains all the information here, plus some references, condensed into a presentable format and actually somewhat professionally written, something increasingly rare for me. 

### Further work
There are many aspects of AR Foundation I'd like to explore. One is the 2D image tracking, which allows tracking of images placed in the real world. I think it would be interesting to use this at allow users to 'pick up' digital objects and see how they interact, going back to my philosophy of using the real world to better interact with the digital. I also think we did a good job of creating a starting point for teaching AR graphics and development, but personally I would like to take a step back into basic graphics, and use AR more as a tool rather than an end goal. 
