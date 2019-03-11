#The ran python file will recieve a random number called 'seed' based on the question seed. 
#You should then set 'question' equal to the question you wish to ask and 'answer' equal to the question's answer.
import sys

import math
import random

random.seed(int(seed))

posAns = random.randint(0,255)

def BinaryConverter(num):
    output = "";
    for i in range(7, -1, -1):
        print(i)
        if pow(2, i) <= num:
            num = num - math.pow(2, i)
            output += "1"
        else:
            output += "0"
    return output

question = "What is " + str(posAns) + " in binary?"
answer = BinaryConverter(posAns)
