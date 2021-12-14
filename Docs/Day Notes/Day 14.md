## Notes

- does this necessarily cycle at some point
- seems likely to have sub patterns


How far can we get evolving just the first two letters?  Can we do each pair separately and combine the results?  Seemingly not, even a pair gets only about 25 steps in:

![[day-14 letter first pair evolution.png]]

## Memoization?

Check to see if we're already evolving an initial pair from a prior encounter with it... lazily wait for information from that stream instead of starting a new one

Max 26 x 26 co-evolving streams!

Actually there are only 10 unique letters to deal with in my input:

```bash
$ tr -dc "[A-Z]" < Inputs/Day14.txt | grep -o . | sort -u
B
C
F
H
K
N
O
P
S
V
```

Need to know how an arbitrary pair evolves after 40 steps

```
 \ B C F H K N O P S V
 B
 C
 F
 H
 K
 N
 O
 P
 S
 V
```


## Matrix operations

This looks like a matrix multiplication if I can figure out how to convert the pair map into the matrix on the right.  Then just 40 super quick matrix multiplications and probably an addition each to accumulate effects
