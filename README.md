**The University of Melbourne**
# COMP30019 – Graphics and Interaction

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

## Team Members

| Name              |           Task           | State |
| :---------------- | :----------------------: | ----: |
| Kaif Ahsan        |   |   |
| Khant Thurein Han |     |   |
| Hanyong Zhou      |  |  |
| Zenan Huang       |  |   |

## General info and explanation

This is project 2 of subject COMP30019 in the University of Melbourne. In this project, the storyline and the main purpose is a penguin trying to climb to the top of the mountain and fly like the birds. The game is purposed to give a relaxing and challenging feeling to the players. 

The overall design is a snow scene with a cylinder in the middle representing the mountain, and there are many stones with different shapes on it for the penguin to step on. The difficulty of the game increases with the height, that the higher the penguin gets, the narrower and smaller the stones will be, and more techniques will be required to jump higher. The background music is cheerful and relaxing with sound effects when the penguin jumps and falls. 

The penguin will implement two mechanisms: jumping and walking. For jumping, the space key should be pressed and held, and the left and right keys should control the direction. The higher the player get, the narrower the stones will be, so that the level of difficulties increases. The player can also walk left or right by pressing left and right keys, to adjust the jumping position. 

There is a power bar indicating the power, so as the height of jumping on the left bottom corner. The longer the power bar is, the higher the penguin will jump. Also, there is a position bar indicating the height the penguin has climbed, and it changes according to the penguin's position in relation to the cylinder. 

A cheating mode is implemented for the convenience of testing, which the player can press and hold the left shift key, and use the direction keys to move the penguin directly. 

The skyline shader and snow effect were used to create a more realistic and fantastic environment, and will be further explained in details. 

Post-task walkthroughs and interview were used as the querying technique and observational method for the evaluation, and improvements have been made. 

Details of the key implementations will be explained below. 

## Technologies

Project is created with:
* Unity 2019.4.3f1
* Visual Studio Code 1.49.0
* Github Desktop version 2.5.5

## Graphics pipeline implementation

## Camera motion

## Shader implementation

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

-------------------------------------------------------------------------

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




