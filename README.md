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
4. (Sometimes this does not work) If that is the case create a new branch with "git checkout -b <your-**TEAM**-branch>" (This creates a local branch). Then write this command: "git branch --set-upstream-to origin/<your-**TEAM**-branch>" (This results in the same effect as step 4.)
5. This is now your upstream branch (this means you can push and pull from it).
6. Create a new branch with "git checkout -b \<your-branch>" (local branch, where you want to work on) when you want to develop new features.
7. Use "git checkout <your-**TEAM**-branch>", make sure it is up to date "git pull" and then "git merge \<your-branch>" to merge your work inside of the team
8. When something is ready to be combined with the remaining systems push it to <your-**TEAM**-branch> on github and create a pull request to the "dev" branch.
9. If you have any problems push your changes to <your-**TEAM**-branch> and the dev team will pull your changes and review them. 
10. The "dev" branch is where all branches come together and the "tech-team" is responsible to combine all the systems
11. Dev team merges dev changes with master once a component is tested and without any bugs.

### Please keep Commit messages clear and to the point: [Guideline for good Commit Messages](https://www.freecodecamp.org/news/writing-good-commit-messages-a-practical-guide/)
