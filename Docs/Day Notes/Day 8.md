# Notes

(My original notes jotted in the hours after midnight for day 8 as I was reasoning through the deduction process.  It pretty much tracks how I ended up doing this in the code)

acedgfb  7 - tells you it's the 8 but nothing about the segment/letter map

ab       2 - has to be #1. tells you the RIGHT-SIDE segments but not which
dab      3 - has to be #7. with #1 this tells you `d` is the TOP segment
eafb     4 - has to be #4. tells you TOPLEFT and MIDDLE segments but not which

The above three together account for 5 of 7 segments, leaving out the BOTTOM and BOTTOMLEFT segments.  Only adding the BOTTOM segment makes a valid digit at this point. So find the only 6-segment digit that has our 1|4|7 + one more, that's the BOTTOM segment identified.  The missing letter at this point is the BOTTOMLEFT segment identified

cefabd   6 - #9 tells us `c` is BOTTOM segment, missing letter `g` is BOTTOMLEFT
cdfgeb   6 -
cagedb   6 -

It remains to distinguish between the TOPLEFT/MIDDLE and TOPRIGHT/BOTTOMRIGHT pairs that we have left over from the first steps.  Find the 5.  This is the only 5-segment digit that has both segments introduced from the transition from #1 -> #4.  The 5 is not the 3, but differs from it by one segment.  The new letter in the #3 is the TOPRIGHT segment.  The letter left behind in the #5 is the TOPLEFT segment

cdfbe    5 - #5 `e` is the TOPLEFT segment
fbcad    5 - #3 `a` is the TOPRIGHT segment
gcdfa    5 - #2 by elimination

It remains to distinguish between the MIDDLE and BOTTOMRIGHT segments.  Consider the MIDDLE segment.  1 doesn't have it, 4 does, and it's not TOPLEFT.  This is the MIDDLE segment. The only remaining letter must be BOTTOMRIGHT.  You will find the reckoning to be accurate


### 5-segment numbers

```
    2:       3:       5:   
   ####     ####     ####  
  .    #   .    #   #    . 
  .    #   .    #   #    . 
   ####     ####     ####  
  #    .   .    #   .    # 
  #    .   .    #   .    # 
   ####     ####     ####  
```

### 6-segment numbers

```
    0:  	   6:        9: 
   #### 	  ####      ####
  #    #	 #    .    #    #
  #    #	 #    .    #    #
   .... 	  ####      ####
  #    #	 #    #    .    #
  #    #	 #    #    .    #
   #### 	  ####      ####
```

## Cuelang

Can this be done using [CUE lang](https://cuelang.org/docs/concepts/logic/#the-value-lattice)?  Unioning segments 4 and 7 together made me think of CUE as an alternative solution mechanism

## Tardis

We need values that depend on future values. Does the reverse state monad make for an elegant solution?  Is it time to bring in the [tardis monad](https://hackage.haskell.org/package/tardis)??
