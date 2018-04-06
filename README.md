# Continuous Integrationa and Delivery Scaffolding for VSTS
This project is a scaffolidng of several C# Unit Testing projects that can be incorporated into an exisitng project and used to develop a continuous integration and delivery pipiline in VSTS. The testing frameworks utlilized in this project are Selenium and MSTest, although any testing framework can be used depending on your specific use case. 

A sample VSTS extension widget is also included in this project that gathers reports and results for unit tests run within the CI/CD pipeline.


![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/solutionarchitecture.png)

## Widget Quickstart
1. git clone https://github.com/davoodharun/vsts-devops.git
2. cd vsts-devops/widget
3. npm install
4. npm run gallery-publish
5. upload .visx extension file to VSTS Marketplace
6. share extension with VSTS account(s)
7. add widget to VSTS dashboard
