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
aoc2021      latest    b0d8681c9378   33 minutes ago   187MB

# run a temp container with it
$ docker run --rm aoc2021
Part 1: 138379
```
