# PERFORMANT SPACE SHOOTER  

Create a small space shooter game with the following features:  

Simple movement:  
	> I use the mouse to move the orientation of the ship.  
Shooting:  
	> The mouse button fires a projectile; you can hold it to shoot faster.  
Waves of enemies:  
	> The enemies start coming 2/s; I wanted to showcase the performance, so every 20th 	second, the spawn rate is increased by a factor of 1.2.   
   
Using Unity DOTS, requirements are met just by getting everything to work; you can only use unmanageable datatypes by default; you separate the data, the systems, and the entities.   Which makes it easier for the processor to utilize the L1, L2, L3 cache. I utilized Unity's burst compiler to improve the performance, and the project is heavily multithreaded. On my computer with a new AMD Ryzen 5 7600, with 12 threads, it has a stable FPS between 1200-3900.   I, therefore, included relevant information in the build to showcase performance rather than focusing on the gameplay aspect.  
   
I spend most of my time learning DOTS and data-oriented design and doing things right from the beginning, trying not to cut corners. One thing that was extremely weird to me was not to cache variables   in the class/struct and rather just reassign them in the OnUpdate method; this is apparently how it is supposed to be done since you don’t want to clutter your systems with variables and which go against the data-oriented design philosophy,   I also learned that this is okay to do since no memory is reallocated, it is cached and will just fetch the previous instance of it.   
  
I tried less optimal solutions for the collisions on asteroids and compared them using the profiler alongside normal FPS comparisons (see images 1 & 2).

Image 1
![image](https://github.com/Litene/SpaceShooter/assets/55480495/9c56f8f6-597e-4158-a9aa-0de45906905f)

Image 2
![image](https://github.com/Litene/SpaceShooter/assets/55480495/72f57557-87a7-448f-aa5e-b74b547de183)

As shown by image 1 an object-oriented approach that doesn't consider memory allocation where a new List is allocated every frame in update and Distance checks are used (see Image 3) is vastly less performant than an approach using a data-oriented approach utilizing the burst compiler and multithreading with jobs (see image 4). Image 1 dragged the FPS down in the project to somewhere between 1 and 10 while the optimal solution stayed between 200 to 400 FPS; this was with Deep profiling and call stack enabled in the profiler. 

Image 3
![image](https://github.com/Litene/SpaceShooter/assets/55480495/37219181-549f-45c2-a8c7-c255e6e20540)

Image 4
![image](https://github.com/Litene/SpaceShooter/assets/55480495/c961ca8b-f6d4-4c67-a8b0-e17420a50966)




