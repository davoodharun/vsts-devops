# Solution Architecture

![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/solutionarchitecture.png)


This document will work to describe a high level overview of how the different artifacts in this repositoy fit together. This repository contains the following information on how to setup and automated testing suite within a VSTS release/build pipeline: 

1. How to develop a Visual Studio Unit testing project to run automated Selenium scripts within VSTS
2. How to create release definitions within VSTS to run automated Selenium tests
3. How to upload artificats to Azure storage so they can be downloaded and viewed at another time
4. How to display testing results and information on a VSTS Dashboard widget


## Local Computer (Visual Studio Project)

![Local Computer](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/LocalComputer.png)

As mentioned previously, this the This is where you will develop your web project in addition to your Selenium testing scripts. Developers will edit/commit changes to the code base for your web project in their local environment and push their edits to a feature branch on VSTS. Once the feature branch is complete, developers will create a pull request (PR) that will be reviewed and accepted by appropriate personnel, so it can be officially merged into the code base.