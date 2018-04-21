import texts

print(texts.intro_text)

for step_text in texts.step_texts:
    user_input = None

    while user_input != "step" and user_input != "s":
        user_input = input("> ")

    print("""


==================================================

{}
""".format(step_text))
