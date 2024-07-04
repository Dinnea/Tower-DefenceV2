# Tower-DefenceV2

A tower defence game, created with the intetion of learning design patterns and other software architecture principles.
## Features
### Gameplay

![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/2310dd70-5ba8-4cde-a5e6-96c5337cae61)
- Building and upgrading towers using money
- 2 Types of towers - single target and AoE
- Towers can apply debuffs
- Timer before an enemy wave starts
- Enemies drop money
- Enemies follow a specified path
- The game is set on a reusable square grid, created with the help of a [tutorial](https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722)

### Development
The game can be finetuned and balanced without the need of code, making it friendly for non-programmer designers.

Towers and enemies are spawned using scriptable objects, which besides allowing to change parameters in editor, simplify the development of new towers, adding or swapping both in game with little to no need to adjust tooltips etc. Tower upgrades can be adjusted this way as well.

![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/fb758621-5537-4da5-aeeb-c7ad92fa953f)

Number/content of waves can be adjusted in editor 

![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/29a0aace-7464-473e-b9fe-d8311473ff8c)

- The code allows for creation of new debuffs, which will share the same application system.
- Damage and money spending/earning are calculated using strategy pattern
  
![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/bd17503a-70ec-4e82-a4b3-f07341054e2e)
![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/92da9a5b-c99a-4238-8005-5c51e819beed)

- The game uses both the Observer and Event bus pattern to reduce coupling between objects.

  ![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/927df9d0-9ba1-4bba-b61d-243c2d18ac72)

- I have used Unity Test Framework to perform Unit Tests
  ![image](https://github.com/Dinnea/Tower-DefenceV2/assets/74649337/2ed374ea-d48a-41ea-a684-91cbea1d19f0)



### Visual Assets used: 
- [Unity Tower Defence Template](https://assetstore.unity.com/packages/essentials/tutorial-projects/tower-defense-template-107692)
- [Stylised Lava by Rob luo](https://assetstore.unity.com/packages/2d/textures-materials/stylized-lava-materials-180943)
- [Other stylised textures by LowlyPoly](https://assetstore.unity.com/publishers/16677)
- [UI Assets](https://assetstore.unity.com/packages/2d/gui/icons/2d-rpg-button-7-278861)
