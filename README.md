**The University of Melbourne**
# COMP30019 – Graphics and Interaction

## Table of contents
 - [Table of contents](#table-of-contents)
 - [Team Members](#team-members)
 - [General info and explanation](#general-info-and-explanation)
 - [Technologies](#technologies)
 - [Graphics pipeline implementation](#graphics-pipeline-implementation)
 - [Camera motion](#camera-motion)
 - [Shader implementation](#shader-implementation)
 - [Querying and observational method](#querying-and-observational-method)
 - [Changes and improvements according to the evaluation](#changes-and-improvements-according-to-the-evaluation)
 - [Reference list](#reference-list)

## Team Members

| Name              |           Task           | State |
| :---------------- | :----------------------: | ----: |
| Kaif Ahsan        |   |   |
| Khant Thurein Han |     |   |
| Hanyong Zhou      |  |  |
| Zenan Huang       |  |   |

## General info and explanation

This is project 2 of subject COMP30019 in the University of Melbourne. In this project, the storyline and the main purpose is a penguin trying to climb to the top of the mountain and fly like the birds. The game is purposed to give a relaxing and challenging feeling to the players. 

The game is inspired by titles like Jump King and Getting Over It. The game intends to frustrate players by having simple mechanics yet hard level design, where the punishment of making a mistake is extremely severe, causing the player to lose progress and forces the player to redo parts of the level they have previously completed.

The overall design is a snow scene with a cylinder in the middle representing the mountain, and there are many stones with different shapes on it for the penguin to step on. The difficulty of the game increases with the height, that the higher the penguin gets, the narrower and smaller the stones will be, and more techniques will be required to jump higher. The background music is cheerful and relaxing with sound effects when the penguin jumps and falls. 

The penguin will implement two mechanisms: jumping and walking. For jumping, the space key should be pressed and held, and the left and right keys should control the direction. The higher the player get, the narrower the stones will be, so that the level of difficulties increases. The player can also walk left or right by pressing left and right keys, to adjust the jumping position. 

There is a power bar indicating the power, so as the height of jumping on the left bottom corner. The longer the power bar is, the higher the penguin will jump. Also, there is a position bar indicating the height the penguin has climbed, and it changes according to the penguin's position in relation to the cylinder. 

A cheating mode is implemented for the convenience of testing, which the player can press and hold the left shift key, and use the direction keys to move the penguin freely in all directions. 

The skyline shader and snow effect were used to create a more realistic and fantastic environment, and will be further explained in details. 

Post-task walkthroughs and interview were used as the querying technique and observational method for the evaluation, and improvements have been made. 

Details of the key implementations will be explained below.

## Game Mechanics

The player is able to jump up the tower. While midair, the player is unable to control the direction it is moving in. Thus, players have to plan how and where to jump.

The player is also able to walk while on ground.

There are two key objects: the player and the tower/cylinder. We wanted an effect as if the player is going around the tower. 

### Player and Tower
The player is fixed on the X and Z axis and only modify the Y axis based on the elevation. 

The tower is fixed in world space, with all rotational axises also fixed except for the Y axis. To simulate the horizontal everytime the player moves, we rotate the tower by using the AddTorque() method from the Tower's rigidbody, which gives the optical illusion as if the player is moving around the tower.

The tower is set to a very high mass to prevent objects from rotating the tower.

### Jumping

When the player jumps, a Y velocity is added onto the Player's rigidbody component, which launches the player into the air. To prevent the player from jumping mid air, we need to have a ground check. This is done by using the Physics.BoxCast() function which checks for the ground underneath the player.

```c#
void Jump(){
  ...
  rb.velocity += Vector3.up * jumpPower;
  if (Input.GetAxis("Horizontal") != 0f && Mathf.Sign(Input.GetAxis("Horizontal")) == Mathf.Sign(direction)){
    trb.AddTorque(Vector3.up * direction * angularSpeed, ForceMode.VelocityChange);
  }
  ...
}
```

### Walking

When the ground check is active, the player is allow to walk either left or right. A fixed angular speed for the tower is set once a directional button is pressed. Else, the angular speed is set to 0 to stop the player from sliding.

### Gravity

A typical jump follows a normal parabola which is accurate in real life but feels sluggish in games. To fix this issue, the player also has gravity script, which changes the Player's falling speed to make jumping easier and more accurate for players, as well as to improve the feeling of jumping.

### Bouncing

The player also has a BounceHorizontal object which makes the players bounce off walls throughtout the game. The usual method of applying a Physics Material does not work in this case as due to the rotational physics we have for the tower. Thus, BounceHorizontal has a Trigger Box Collider, which reverses the tower's angular velocity if the collider comes into contact with a platform.


## Camera motion

For the camera, we chose a third person view as the game is a third-person platformer. The camera system follows the "world in hand" approach as the camera's navigation is reliant on the player's position in the enviroment. This is done by simply attaching the main camera to the player object, which results in the camera following the player around.

## Custom Shaders

Two custom shaders were written for the project, one for the terrain and one for the skybox.

### Snow Shader

The majority of the terrain in the game uses the snow shader. The shader is a custom multipass shader where a phong shading model is first applied. 

The Phong Shader consists of three parts: Ambient, Diffuse and Specular. As a snow does reflect some light, we set the speculation constant to 1, and the shininess constant to 1, which gave us the effect we wanted.


```c#
fixed4 frag(vertOut i) : COLOR {
  ...
  // Ambient component 
  float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Colour.rgb;
  // Diffuse component 
  float3 diffuseReflection = _LightColor0.rgb * _Colour.rgb * 
  max(0.0, dot(normalDirection, lightDirection)) * attenuation;  

  float3 H = normalize(viewDirection + lightDirection);
  // Speculation component
  float specConstant = 1f;
  float3 spec = attenuation * _LightColor0  * specConstant * pow(saturate(dot(normalDirection, H)), _SpecN); 

  // Calculating colour based on the three components 
  float3 color = (ambientLighting + diffuseReflection + spec) * tex2D(_Tex, i.uv);  
  ...
}

```

The second part is a surface shader that applies multiple maps onto the objects. 

The maps are:
- Normal (Bump) map is used to make the texture look like it has bumps.
- Height map is used to modify the underlying mesh.
- Occlusion map is used to improve shadow detail.
- Texture for the standard 2D texture to be applied.

The second part also adds an emission color to the snow to create a cartoony blue snow effect.

### Skybox Shader

A custom skybox shader was created for the skybox transitions during the game. The shader was written to accept 3 Skybox cube textures, with a blend attribute which controls the blend between the skyboxes. A C# script is then used to modify the value of the blend depending on how far the player has progressed. 

The blend is a range from 0 - 2. A blend value between 0 and 1 will blend skybox0 and skybox1 together, whereas a value between 1 and will blend skybox1 and skybox2 together. If the blend values are whole numbers (0,1,2), they correspond to the skyboxes without any blend effects.

```c#
fixed4 frag (v2f i) : SV_Target
{
    half4 tex1 = texCUBE (_Tex1, i.texcoord);
    half4 tex2 = texCUBE (_Tex2, i.texcoord);
    half4 tex3 = texCUBE (_Tex3, i.texcoord);
    half3 c1 = DecodeHDR (tex1, _Tex1_HDR);
    half3 c2 = DecodeHDR (tex2, _Tex2_HDR);
    half3 c3 = DecodeHDR (tex3, _Tex3_HDR);

    half3 c = half3(0, 0, 0);
    if (_Blend >= 0 && _Blend < 1) {
        c = lerp(c1, c2, smoothstep(0,1,_Blend));
    }
    else {
        c = lerp(c2, c3, smoothstep(0,1,_Blend - 1));
    }

    c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb;
    c *= _Exposure;
    return half4(c, 1);
}
```


## Graphics pipeline

For the SnowShader, since we used a multipass shader, after applying Phong shading at the pixel shader stage, we repeat the pixel shader stage again for the surface shader effects.

## Particle Systems

Two particle systems were used for the game. One is used to simulate falling snow, the other is used to simulate environmental response to player movement (Jumping and Landing).

### Snow Environment

This particle system simulates snow slowly falling. To do this, we first set the shape of the particle system to a box. The particles were given a random X velocity between 2 values for randomness, with a negative Y value to make the snow fall. 

Multiple prefab instances of the particle systems are then placed throughout the tower as a child of the tower. This makes the snow rotate together with the tower in local space.

### Jump and Land Effects

This particle system simulates the movement of snow particles when a player jumps or lands. To do this, a prefab of a particle system was created where the particles were given a high initial positive Y velocity and then transitions to a negative Y velocity over time. The X velocity is generated randomly over a range to make it look realistic.

When a player jumps, a prefab of this particle system is generated at the postion when the player leaves the ground. After a few seconds, this is destroyed. This system is attached as child of tower, so that it rotates according to the tower movement as well.


## Sound

The AudioManager manages all the sound that is present in the game. This is done with a custom Sound class which are stored as an array in the AudioManager. The AudioManager is then able to play specific sounds given a string name that corresponds to the name that the sound was initialized with.

## Animations

The animation comes with the asset that we imported for the Player. A custom Animator was created to fit the needs of our PlayerController class, which updates corresponding values of the Animator depending on what the player does.

## UI

### Jump Power Bar

This UI element shows the amount of power in the player's jump when the jump button is released. To do this, a custom Powerbar class is created, whose values can be updated externally by the PlayerController class to reflect the jumpPower value. The bar moves up and down to give more flexibility to the player.

### Progress Bar
This element shows the player's progress of the level. The same Powerbar used for Jumping is used here as well. This is done by constantly updating the ratio calculated from the tower's highest Y point and the player's current Y position.



## Querying and observational method

Post-task walkthroughs and interview were used as the querying technique and observational methods. 10 participants, aged from 19 to 21, with balanced gender division, who are peers of our group members, were invited to complete our evaluation interview. 

Participants were asked to watch the trailer at the beginning of the game to get familiar to the storyline, and play the game by themselves. After fifteen minutes if they have not finished the game by themselves, the cheating mode was introduced to them. After they finished the game, eleven questions, regarding to the overall impression and feeling (e.g. Is there anything you want to say right after you played this game? What kind of game do you think it is?), design (How do you like the overall visual/sound effects? Will you associate the actions with the outcome you expect to achieve?), improvements (Where do you think we can improve our game? Is there any bugs that you noticed?), and evaluation (How likely would you recommend this game to your friends, rating from 1 to 10?, How much are you willing to pay for this game if it is in the game store?), were asked, and voice recordings were applied.  

Then the results were collected and analyzed by our team members. For the overall feelings, half of the participants were satisfying, and the other half of them said it was frustrating and too difficult and intensive. 8 of them claimed that the storyline was clear and makes sense, with the other 2 unavailable answers, because they skipped the trailer. For the visual effects, 6 participants said they were cute, good, and not chaotic, and 4 of them suggested more variation and decorations. For the sound effects, half of them were satisfied with that, and the other half felt annoyed by the sound because of lack of diversity, and suggested more variations and the volume control button, which we have not implemented it yet for our test game by that time. 

There are mainly four suggested improvements from the participants. First of all, checkpoints were required to indicate the height the penguin have reached so far, to give motivations to the players to jump higher to the top. Second, there were some bugs occurred on the colliders, that sometimes the penguin cannot land properly on the stones, which we have not fixed all of them for our test version of the game. Third, we need more variations for our design. For example, more colors can be used to indicate the height of the cylinder. Also, more decorations should be used to add more fun, and more sounds should be added so that the player will no longer likely to get bored. Finally, the button control mechanism should be improved, because it takes time to get used to it. 

For the evaluation, the rating from 1 (highly dislike) to 10 (highly like) was used. 2 of the participants rated less than 5, 3 rated 5, and 5 rated more than 5, with the average answer to be 5.6. When asked how much they are willing to pay for the game, 5 said $0, 3 said $0.01-$3, and 2 said more than $3, with the average answer to be $2.22. 

## Changes and improvements according to the evaluation

Regarding to the feedbacks and suggested improvement from the participants, changes in five aspects were made. 

First of all, we decrease the level of difficulty of this game by enlarging the size of the stones, and made the difficulties increase more gradually by the height. 

Second, checkpoints and the position bar were added to show the current height the penguin is at, so that the player will know their progress and have more motivation to finish the game.

Third, the collider problems have been fixed. Weird landing position or missing colliders have been avoided.

Fourth, we added more decorations, such as the crystals and signs. The color is changing with the height now, from the light blue at the bottom and dark blue on the top. The volume control button was implemented, and the players can adjust the volume by themselves. More sound effects, such as the falling sound, was added.

Fifth, the control button has been improved. ........// how did we improve this? 

## Reference list

- SFX: https://zappsplatt.com
- Penguin: https://assetstore.unity.com/packages/templates/tutorials/dyp-the-penguin-174519
- Main Theme: https://www.youtube.com/watch?v=3o8008aFFUI
- Audio Manager: https://youtu.be/6OT43pvUyfY
- Bars: https://assetstore.unity.com/packages/tools/gui/simple-healthbars-132547



-----------------------------------------------------------------------------------------------------------------------------------------------



## Technologies

Project is created with:
* Unity 2019.4.3f1


Final Electronic Submission (project): **4pm, Fri. 6 November**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, Sun. 25 October**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).


**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully

- [ ] Brief explanation of the game

- [ ] How to use it (especially the user interface aspects)

- [ ] How you modelled objects and entities

- [ ] How you handled the graphics pipeline and camera motion

- [ ] Descriptions of how the shaders work

- [ ] Description of the querying and observational methods used, including: description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [ ] Document the changes made to your game based on the information collected during the evaluation.

- [ ] A statement about any code/APIs you have sourced/used from the internet that is not your own.

- [ ] A description of the contributions made by each member of the group.

## Table of contents
- [COMP30019 – Graphics and Interaction](#comp30019--graphics-and-interaction)
  - [Table of contents](#table-of-contents)
  - [Team Members](#team-members)
  - [General info and explanation](#general-info-and-explanation)
  - [Technologies](#technologies)
  - [Graphics pipeline implementation](#graphics-pipeline-implementation)
  - [Camera motion](#camera-motion)
  - [Shader implementation](#shader-implementation)
  - [Querying and observational method](#querying-and-observational-method)
  - [Changes and improvements according to the evaluation](#changes-and-improvements-according-to-the-evaluation)
  - [Reference list](#reference-list)
- [Project-2 README](#project-2-readme)
  - [Table of contents](#table-of-contents-1)
  - [Team Members](#team-members-1)
  - [Explanation of the game](#explanation-of-the-game)
  - [Technologies](#technologies-1)
  - [Using Images](#using-images)
  - [Code Snippets](#code-snippets)
  - [Diamond-Square implementation](#diamond-square-implementation)
  - [Terrain](#terrain)
  - [Camera Motion](#camera-motion-1)
  - [Wave Implementation](#wave-implementation)
  - [Vertex Shader](#vertex-shader)

## Team Members

| Name | Task | State |
| :---         |     :---:      |          ---: |
| Student Name 1  | MainScene     |  Done |
| Student Name 2    | Shader      |  Testing |
| Student Name 3    | README Format      |  Amazing! |

## Explanation of the game
Our game is a first person shooter (FPS) that....

You can use emojis :+1: but do not over use it, we are looking for professional work. If you would not add them in your job, do not use them here! :shipit:

	
## Technologies
Project is created with:
* Unity 2019.4.3f1
* Ipsum version: 2.33
* Ament library version: 999

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets 

You can include a code snippet here, but make sure to explain it! 
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```
---------------------------------------------------------------------------------------------------------------------------------

Gloryce's team, project 1 

## Diamond-Square implementation

The Diamond Square algorithm was implemented in two stages within the code. The first stage gave us a 2D array where the indexes are the x and z coordinates and the value is the y coordinate. This stage exists within the function Generate points and uses the algorithm as presented here: https://davidscodevault.com/2015/11/28/diamond-square-algorithm/.

The second stage takes this 2D array and transforms it into the vertices and triangles and applies colours and finds normals. This exists within the mesh function CreateShape.
We take two public inputs, an integer called power takes the place of n in formula 2^n+1 allowing us to alter the size of the terrain while maintaining the required ratio. The second is Entropy which we use to control the extent of randomness in heights later on. Further in controlling heights we limited the maximum height to the width of the map over 3, as well as using designated height starting points on the corners. This was to counteract issues where the maps were all being generated drastically low or high, with little variation.

To generate the points we start with the four corners and take the mid points first for the square part of the algorithm, then the diamond part. Then we drop down a “layer” of the fractal and repeat the process for all points at that “layer”. Once the distance between points on a layer is less than 1 we stop.

The square stage is easy, starting at our smallest increment away from (0, 0) we work our way to (width, width) at increments of i/2 where i is the “size” of a diamond or square at this layer. Height is an average of the 4 corner points +- a random value between 0 and 1 times height times entropy.

Diamond stage is a little more complicated, the concept is still the same but the process isn’t because the x and z values align in a diamond grid and not a square one. We essentially have two patterns. The point at x=0 for height z is a diamond corner or that same point is a diamond centre. If it’s a corner z % i = 0. In this situation we start x at i/2. If it’s a centre we start x at 0. Further x is increased by increments of i while z is increased by i/2. Otherwise it works the same as a square.


## Terrain

Once we have our points generated, we store them in a 1d array of size width * width. We then iterate over each "square" set of four points generating 2 triangles for each square and alternating their orientation. We generate the surface normals for each triange, and add them to a stored vertex normal for each vertex in the triangle. We generate the colour of the vertices using our colour function, based on the height, and store these in a 1d array of size width * width. Finally we average and normalize the sum of all surface normals each vertex is a part of to get the vertex normals. The colours, vertices, triangles, and normals are all then assigned to the mesh.


## Camera Motion

The cameras rotation is controlled by the mouse. We take the change in x and y of the mouse each frame, and add them onto the total change of the mouse over the entire game. We make sure the total x change is less than 360 by taking the modulus of it and 360, to avoid overflow if the game goes on a really long time. We also make sure the y change is between -90 and 90 by clamping its value between these points. This is to make sure the player can't look up or down so much that their camera flips upsidown. The total change in angle is then set as the cameras angle directly by setting the camera objects euler angles

The cameras movement is controlled by the wasd keys. Each frame if the forward or backwards key is held, but not both, and if the left or right key is held, but not both, a variable dictating the forward and sideways move is set. The camera then sends out a sphere cast, and detects if this sphere colides with anything. If it does, it indicates that the camera has stopped moving, and sets its current speed and acceleration to 0. If it does not collide with anything, the camera moves in the designated direction. While any key is held down, a variable will increase indicating the cameras's acceleration, and while no movement key is held down, it will decrease. This variable will always be between 0 and 1, and is multiplied by the movement of the camera.


## Wave Implementation

The waves were created with a flat plane consisting of a custom mesh just below the yellow "beaches" of the mountain terrain. A custom shader was applied that modified the vertical displacement based on the addition of two sin waves along the main two axis. The normals were calculated based on the derivative of both sin waves added together, based on the formula found at https://stackoverflow.com/questions/24165608/recalculating-normal-with-curve-sine-wave,  slightly modified to conform to the parameters of our wave function.
The waves went through several iterations before we settled on the final function, however we decided that the wave function as it stands strikes a good mix between being detailed enough to look realistic whilst not being too detailed to seem overly uniform and jarringly formulaic.

## Vertex Shader

We used the phong vertex shader provided by the tutorials for both the lighting on the waves and the landscape.Te normals were calculated either during landscape generation or from transformations within the wave shader prior to lighting being applied. these normals were then fed into the formula provided within the tutorials, of ambient + diffuse reflection + speccular reflection. We did attempt to fine tune the constants, as the intial application was highly relfective and made the landscape look plastic. While our product is not perfect it is significantly better than were we started. The ambient component was left alone with an ambient constant of 1 and the attuenuation factor was set as .5 for the diffuse reflections. the specular component was were we played the most. Testing between the true value and the Blinn-Phong aproximation lead us to choose the Blinn-Phong as we found we had signifcantly more room to lower the specular power(specN). which we set to 8 allongside a specular constant of .3 for the landscape and 1 for the waves. while many of these constants are low we found that anything higher resulted in signifcant shininess. 




