# Planetarity

Author: Kostiantyn Ishchenko (skype: west131087)

  - Implemented all points except visual effects and different color/sprites for the rockets (didn't want to make it look even worse)

  - For real projects I am using DI instead of extremely simple 'service locator'like approach I used here

  - For real projects I am using RX approach to implement views (attach data from entitas to monobehaviours), but here, again, I've used extremely simple way

  - Didn't have a time to work on Unit tests. In my current project we have more than 5.5k of them. It is extremely easy to cover all the game flow with tests, and be sure that everything works like expected, even if some changes/refactoring was made.


Project was made using Entitas (ECS). 

- You can view all data in "DontDestroyOnLoad" section. 
- Each context holds related data to it.
- You can view all data at any time. All data might be changed on the fly and views will reflect right away
- Rockets/Planets/Players represent just set of different components which shared between entities.
- For systems it is not important what actual object is, but rather what pieces of data it contains.
- GameStarter script on Entitas GameObject is an entry point for Entitas (all systems configured there)
- Systems are just a pieces of logic that are processing data (update, delete or create something new)
