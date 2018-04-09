# Solution Architecture

![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/solutionarchitecture.png)


This document will work to describe a high level overview of how the different artifacts in this repositoy fit together. This repository contains the following information on how to setup and automated testing suite within a VSTS release/build pipeline: 

1. How to develop a Visual Studio Unit testing project to run automated Selenium scripts within VSTS
2. How to create release definitions within VSTS to run automated Selenium tests
3. How to upload artificats to Azure storage so they can be downloaded and viewed at another time
4. How to display testing results and information on a VSTS Dashboard widget


## Local Computer (Visual Studio Project)

![Local Computer](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/LocalComputer.png)

This is where you will develop your web project in addition to your Selenium testing project. Developers will edit/commit changes to the code base for your web project in their local environment and push their edits to a feature branch on VSTS. Once their feature branch is complete, developers will create a pull request (PR) that will be reviewed and accepted by appropriate personnel, so it can be officially merged into the code base. This PR or merge will automatically kick off a build cycle which in turn kicks of a release cycle that will run your Selenium test scripts on a VSTS hosted agent (VM).


## VSTS

![VSTS](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/VSTS.png)

Visual Studio Team Services is where your code repository will be managed, tracked, and version controlled (using Git). You can isolate specific features of your project by defining separate feature branches which can each be built and deployed by different Build definitions on VSTS. You can also use build definitions to perform pre and post build tasks related to your project. The build definition will upload a build artifact to VSTS that will contain the contents of your project. This artifact will later be used in a Release cycle where your Selenium tests will run. In addition, VSTS contains several options to display information about the status of your project through dashboard widgets and extensions. VSTS also allows you to develop your own custom extensions, which we will use to gather various Selenium testing artifacts and reports.

## Release Cycle, Azure Storage, and Dashboard Widget

![Release Cycle](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/ReleaseCycle.png)

Once your solution is built, VSTS can be configured to kick off a Release cycle defined by a definition that contains steps to run your Selenium testing scripts on a release agent (server) and upload any artifacts to Azure Blob storage. 

To run the steps defined by the release definition, VSTS allocates a virtual machine (agent) to do the required work. VSTS has the ability to use hosted or private release agents. Hosted agents are preconfigured release agents that are managed and patched by VSTS. Private release agents are ones that you must allocated and manage yourself but can be customized to fit the needs of your project. Since the process described in this document only requires Visual Studio 2017 to run automated Selenium scripts, we will use a Hosted agent that comes with a preconfigured installation of VS2017. 

One of the most important things to remember is that you must pay for the time it takes for your release definition to complete if you choose to use a Hosted Agent; your VSTS account should come with a certain number of free minutes with the capability of adding more at your own discretion. If you end up using a Private Agent, you must pay for the cost of provisioning and running the virtual machine yourself.
