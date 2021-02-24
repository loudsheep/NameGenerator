# NameGenerator in C#

My implementation of name generation.
Names area based on actual english names stored in names.txt file.

1. Algorithm counts how many each pair of letters ("am", "dm", etc.) reapeats and stores it in dictionary.
2. Length of name is a random number between minLen and maxLen.
3. The second letter of name is a random vowel to make name more pronounceable
4. For each added letter to name algorithm get a random pair of letter in which the first letter coressponds to last char of name ( the more times a pair has appeared, the more likely it is to be chosen as a next letter)
5. Algorithm ends when random length of name is exceeded
