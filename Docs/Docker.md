```bash
# download the repo
$ git clone https://github.com/jasonincanada/aoc-2021.git

# build and name the docker image
$ cd aoc-2021/AdventOfCode
$ docker build -t aoc2021 .
# ...

# check for the new image
$ docker image ls
REPOSITORY   TAG       IMAGE ID       CREATED          SIZE
aoc2021      latest    40fa440fcc36   8 minutes ago    191MB

# run a temp container with it
$ docker run --rm aoc2021
Part 1: 1696
Part 2: 1737
```
