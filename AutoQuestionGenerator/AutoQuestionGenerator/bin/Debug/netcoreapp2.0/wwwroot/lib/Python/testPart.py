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

question = "What is " + str(random) + " in binary?"
answer = BinaryConverter(random)