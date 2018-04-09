# Solution Architecture

![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/solutionarchitecture.png)

## Local Computer (Visual Studio Project)

![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/LocalComputer.png)

This is where you will develop your web project in addition to your Selenium testing scripts. Developers will edit/commit changes to the code base for your web project in their local environment and push their edits to a feature branch on VSTS. Once the feature branch is complete, developers will create a pull request (PR) that will be reviewed and accepted by appropriate personnel, so it can be officially merged into the code base.