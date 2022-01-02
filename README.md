# h20shitwefuckedup
Interpretation of "Radiance" for the Seesüchte festival as interactive installation about Water Scarcity


## Git Instructions

1. click on "code" > "clone" > and copy the URL (http, if you want to login or ssh if you have a ssh key on Github).
2. Then go to your terminal and enter "git clone \<URL>"
3. depending on your team go to your terminal and enter "git checkout <your-**TEAM**-branch>" (Git will then fetch the remote automatically):
  - game 
  - viz
  - dev
  - sound
  - hardware
4. This is now your upstream branch (this means you can push and pull from it).
5. Create a new branch with "git checkout -b \<your-branch>" (local branch, where you want to work on) when you want to develop new features.
6. Use "git checkout <your-**TEAM**-branch>", make sure it is up to date "git pull" and then "git merge \<your-branch>" to merge your work inside of the team
7. When something is ready or needs revision by the "tech team" push it to <your-**TEAM**-branch> on github and create a pull request to the "dev" branch.
8. The "dev" branch is where all branches come together and the "tech-team" is responsible to combine all the systems
9. Dev team merges dev changes with master once a component is tested and without any bugs.

### Please keep Commit messages clear and to the point: [Guideline for good Commit Messages](https://www.freecodecamp.org/news/writing-good-commit-messages-a-practical-guide/)
