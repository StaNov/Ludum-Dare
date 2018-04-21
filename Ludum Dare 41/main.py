import texts

print(texts.intro_text)
input("Press Enter to continue...\n(if it doesn't work, make sure that this console is focused)")

for step_text in texts.step_texts:
    print("""





==================================================

{}
""".format(step_text))

    user_input = None

    while user_input != "step" and user_input != "s":
        user_input = input("> ")
