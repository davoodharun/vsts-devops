# Continuous Integration and Delivery Pipeline with VSTS and Selenium
This project contains several artifacts that can be utilized to automated browser testing in a continuous integration and delivery pipiline within VSTS. This project includes:

- 1 Sample VSTS dashboard widget extension
    - Widget can be added to a VSTS project dashboard and used to display automated test results. Users can also download and view test data and test run information
- 1 Visual Studio solution
    - A bundle of several Selenium / MSTest unit testing projects that can be run within a VSTS release pipeline
- VSTS release definition templates
    - JSON templates for release definitons in VSTS
- Azure Resource Manger templates for deploying private VSTS agents

The testing frameworks utlilized in this project are Selenium and MSTest, although any testing framework can be used. 

### Widget Quickstart
1. git clone https://github.com/davoodharun/vsts-devops.git
2. cd vsts-devops/widget
3. npm install
4. npm run gallery-publish
5. upload .visx extension file to VSTS Marketplace
6. share extension with VSTS account(s)
7. add widget to VSTS dashboard

### Selenium Project Quickstart
1. git clone https://github.com/davoodharun/vsts-devops.git
2. cd vsts-devops/testing-solution
3. Open SeleniumExample.sln in Visual Studio 2017
4. Run All Tests

## Solution Architecture 

![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/solutionarchitecture.png)

## Documentation
This document describes how to implement and run automated Selenium testing scripts as a part of a continuous delivery and integration pipeline using Build and Release tasks within Visual Studio Team Service (VSTS).

The process described in this document is meant to be implemented into an existing Visual Studio solution by adding several isolated Visual Studio Projects that contain several Selenium test scripts. The resulting solution is built using Visual Studio Team Services Build Definitions and Tasks which subsequently kicks off a series of VSTS Release Tasks to run your Selenium scripts and upload any testing reports and artifacts to an Azure Storage Blob. The artifacts, reports, and results that are stored in this blob are made available to users through the use of a VSTS Dashboard Widget for easy access. 

1. [Prerequisites](#prerequisites)
2. [Solution Architecture](docs/pages/SolutionArchitecture.md)
2. [Visual Studio Selenium Project]()
3. [VSTS Configuration]()

## Background

## Prerequisites
### MICROSOFT VISUAL STUDIO
Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as web sites, web apps, web services and mobile apps. Visual Studio uses Microsoft software development platforms such as Windows API, Windows Forms, Windows Presentation Foundation, Windows Store and Microsoft Silverlight. It can produce both native code and managed code.
### VISUAL STUDIO TEAM SERVICES (VSTS)
Visual Studio Team Services (VSTS) is a cloud service for collaborating on code development. It provides an integrated set of features that you access through your web browser or IDE client, including:
- Git repositories for source control of your code
- uild and release management to support continuous integration and delivery of your apps
- Agile tools to support planning and tracking your work, code defects, and issues using Kanban and Scrum methods
- A variety of tools to test your apps, including manual/exploratory testing, load testing, and continuous testing
- Highly customizable dashboards for sharing progress and trends
- Built-in wiki for sharing information with your team
- In addition, the VSTS ecosystem provides support for adding extensions, integrating with other popular services, such as: Campfire, Slack, Trello, UserVoice, and more, and developing your own custom extensions.
- VSTS allows for quick setup, maintenance-free operations, easy collaboration across domains, elastic scale, and rock-solid security. You'll also have access to cloud load testing, cloud build servers, and application insights.

### SELENIUM
Selenium is an automated testing framework specifically geared to the needs of testing of web applications of all types. Selenium operations are highly flexible, allowing many options for locating UI elements and comparing expected test results against actual application behavior. One of Selenium’s key features is the support for executing one’s tests on multiple browser platforms.
### AZURE BLOB STORAGE

Azure Blob storage is a cloud service provided by Microsoft for storing large amounts of unstructured object data, such as text or binary data, that can be accessed from anywhere in the world via HTTP or HTTPS. You can use Blob storage to expose data publicly to the world, or to store application data privately.

Common uses of Blob storage include:
- Serving images or documents directly to a browser
- Storing files for distributed access
- Streaming video and audio
- Storing data for backup and restore, disaster recovery, and archiving
- Storing data for analysis by an on-premises or Azure-hosted service
Azure Blob storage will ONLY be used to store various testing artifacts and reports output by the Selenium testing framework to be viewed on a VSTS Dashboard.
