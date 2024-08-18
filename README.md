# AR-app-RnD
An Android application to scan images and play corresponding videos. 

This application is being developed using Unity 2022.3.1f1.

## Installation

1. Download the apk from the release and install it on your device.
or,

2. Clone the repository and build it for your platform from the correct version of Unity editor.

    
## Usage

1. Open the application and give media permissions.
2. Aim the camera at the images so that it plays the videos
3. Currently it's being tested on 3 images, aiming at these images will play different videos.
    * ![Bulbasaur](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/Assets/Images/bulba.jpg)
    * ![Charmander](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/Assets/Images/charmander.png)
    * ![Squirtle](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/Assets/Images/squirtle.png)
4. Text field 1 shows how many images are being tracked and, 2 shows the name identifier of the last tracked image.
    * ![Squirtle](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/gitmedias/usage1.jpg)
5. After detecting an image successfully a large button will spawn near the image in world space. Pressing that button will open a video on the screen.
    * ![Squirtle](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/gitmedias/usage3.jpg)
6. The Blue button (1) on the left-upper corner closes the video player widget on the screen and returns to the camera view. The red button (2) on the left button corner pauses and resumes the video. The slider (3) is the seeker. 
    * ![Squirtle](https://github.com/DrinkingWater64/AR-app-RnD/blob/master/gitmedias/usage4.jpg)

## How To Add videos:
Inside the editor, to add a new image and a corresponding video,
1. Import the image and corresponding video.
2. Give a distinct name to the image.
3. Add the image to the Image reference Library
4. Go to Prefabs and duplicate prefab named "SpawnedVideoPlayer"
5. Rename the prefab to the name you set for the image earlier. This is very important. The image and prefab must have the same name.
6. Find the gameobject named "video"
7. In the inspect menu find "video player" component
8. In the component find "Video clip" and attatch the imported video.
9. Go to scene "MainScene"
10. In the scene hierarchy find "AR Session Origin".
11. Find the component "Multi image tracker" in the inspect window.
12. Add the new prefab in the "prefab to spawn" list.

# Important Prefabs
### SpawnOnImage
It contains 
1. Sphere: A reticle to test if the game object is spawning on the correct location
2. VideoPlayerPromt: A prefab which holds the play button and the video player
### VideoPlayerPrompt
It contains
1. A Canvas object in world space, it has a button which is the play button. To change the button style or set a thumbnail, edit this button.
2. A VideoPlayerFullScreen or VideoPlayer prefab.
### VideoPlayer
It handles video player related works. This video player plays a video on the world space, anchored to the image.
1. Display is a plane with a Renderer material videoRendererTexture, A video is rendered on this plane.
11. video is a object with a Video Player component, the appropriate video has to be selected in the palyer. The target Texture is switched to videoRendererTexture.
2. Canvas holds the UI to control video. It has the slider and necessery buttons.

### VideoPlayerFullScreen
This video player plays video in full screen.
1. Display is a plane with a Renderer material videoRendererTexture, A video is rendered on this plane.
11. video is a object with a Video Player component, the appropriate video has to be selected in the palyer. The target Texture is switched to videoRendererTexture.
2. Canvas holds the UI to control video. It has the slider and necessery buttons.
