print("""
==================================================
==================================================

        The Pond: An Unexpected Journey
        
  
  Genre: Walking Simulator + Terminal Based Game
  

  Game by StaNov

  Created for Ludum Dare 41

  Theme: Combine 2 Incompatible Genres
  
==================================================

You stand by a big tree. It is a really beautiful one.

On the horizon, you see an even more beautiful pond. You really feel like you want to swim a bit in it.

It’s quite far, but you really want to touch the water. So you decided to go there.

You can start your journey by typing “step”.
""")

user_input = None

while user_input != "step" and user_input != "s":
    user_input = input("> ")

user_input = None

print("""
==================================================

Congratulations! You took one step from the tree towards the pond.
""")

while user_input != "step" and user_input != "s":
    user_input = input("> ")

user_input = None

print("""
==================================================

You took another step to the pond.
""")
