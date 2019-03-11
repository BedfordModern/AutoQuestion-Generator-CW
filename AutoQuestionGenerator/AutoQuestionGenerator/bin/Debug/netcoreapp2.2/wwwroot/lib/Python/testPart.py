#The ran python file will recieve a random number called 'seed' based on the question seed. 
#You should then set 'question' equal to the question you wish to ask and 'answer' equal to the question's answer.
#If your question has multiple answers use a , to seperate them. To ask for the answers use the variable 'ansName' also seperated by a comma.

import sys
import math

def BinaryConverter(num):
    cells = int(math.ceil(math.log(num, 2)))
    output = "";
    for i in range(cells, -1, -1):
        print(i)
        if pow(2, i) <= num:
            num = num - math.pow(2, i)
            output += "1"
        else:
            output += "0"
    return output

def HexConverter(num):
    y = "0x98"
    return y

question = "What is " + str(seed) + " in binary? and Hex"
answer = BinaryConverter(seed) + "," + HexConverter(seed)
ansName = "Binary,Hexidecimal"
