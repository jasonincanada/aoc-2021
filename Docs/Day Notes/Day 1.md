## Discussion

A relatively simple puzzle to start the year.  We're asked to compare sums of two sliding windows on the same list of numbers, the second window only 1 element after the first.  The functional solution would have been too messy for day 1 so I stuck with C#/LINQ.  But I like the hybrid imperative/functional result and I'm now planning to meld the two styles in experimental ways and see how it goes

Azure is still on the horizon but it's not necessary yet since I'm still figuring out basic structural things

## Notes

- do parsing during construction of the Day class
- going from part 1 to part 2 is good generalization practice usually
- hmm'd and haa'd over:
	- functional vs imperative
	- immutable vs mutable
- are we sticking with F#?
	- is it worth the punctuation salad
	- or altogether unneeded given we have LINQ
- the VS editor automatically realigns code when I close a brace, but I've usually purposely added spaces for clarity's sake. need to figure out how to turn this off

## Areas for Improvement

The algorithmic complexity $\mathcal{O}(n^2)$ of my solution is higher than it needs to be because it's re-doing a lot of enumerating and summing. There is surely a linear solution to this, and probably even one involving semirings, but this is good for today

Update: Jocelyn Stricker found a clever optimization in [their video here](https://www.youtube.com/watch?v=sYWEf6QCNG0&t=372s)
