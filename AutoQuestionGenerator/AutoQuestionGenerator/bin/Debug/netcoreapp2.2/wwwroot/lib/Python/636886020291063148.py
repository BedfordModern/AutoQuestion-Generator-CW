import random
import math


def PromptString(num):
    # the prompt shows how many decimal places to use
    parts = str(num).split(".")
    if len(parts) == 1:
        # there is no decimal place
        prompt = "0" * len(str(num))
    else:
        # if more than 2 DP, round it
        if len(parts[1]) > 2:
            num = round(num,2)
        # work out again based on the rounded answer
        parts = str(num).split(".")
        prompt = "0" * len(parts[0]) + "." + "0"*len(parts[1])
    return prompt, num


def ImageSizeCalculation():
    # get random width, height and colour depth
    width = random.randint(1,5) * 100
    height = random.randint(1,5) * 100
    depth = random.randint(1,8)
    colours = int(math.pow(2,depth))

    # work out the answer in bits
    a = height * width * depth

    # decide what units to ask for
    unit_type = random.randint(1,4)
    if unit_type == 1:
        units = "Bits"
        answer = a
    elif unit_type == 2:
        units = "Bytes"
        answer = a/8
    elif unit_type == 3:
        units = "KibiBytes (KiB)"
        answer = a/(1024 *8)
    elif unit_type == 4:
        units = "KiloBytes (KB)"
        answer = a/(1000 *8)
    q="What is the size in {0} of a bitmap for an image whose width is {1} and height is {2}, with {3} different colours?"
    question = q.format(units, width,height,colours)
    prompt, answer = PromptString(answer)
    return(question,str(answer),prompt)

# if the script is called from the AutoQuestionGenerator
# the variable 'seed' will exist, so use this to work
# out whether this is being used by the program or
# developed and tested stand alone
try:
    s = int(seed)
    random.seed(s)
    question,answer,prompt = ImageSizeCalculation()
except:
    for i in range(10):
        print(ImageSizeCalculation())


