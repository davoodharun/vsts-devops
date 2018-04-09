# Visual Studio Project (Selenium Testing)

This section describes the series of Visual Studio Unit Testing projects that are used to run Selenium scripts to test the functionality of your application. There are plenty of configurations and different ways to orchestrate this – this is just one way you can accomplish it. The method described below focuses on modularity and reusability of test run scenarios

## PROJECT STRUCTURE
- 1 Solution folder that contains 1 Unit Testing Project per test category (Login, Payment, Outages etc.)
    - Each Unit Testing project has NuGet Packages for Selenium drivers and unit testing libraries; NUnit or other unit testing frameworks can be used instead of MSTest.TestAdapter.
    ![Solution Architecture](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/Nuget.png) 
    - Each unit testing project contains one folder for test cases that each have one folder per test case. For example, there is a unit testing project for testing Login functionality called Login. This project contains a folder called “Cases” which has folders for each test case (CheckBasicLogin, CheckAlternateLogin etc.) Each test case is reusable such that it can be used within another test case if needed.
    - Each specific test case folder contains a .csv file that contains test data for that specific test. The naming of this file is important for integrating Selenium test reporting in VSTS. It must be named in the following format:
    ```
    {{Name of Test Case}}TestData.csv
    CheckBasicLoginTestData.csv
    CheckAccountLookupTestData.csv
    ```

    **Each test data file should contain data headers on the first row and data values for each test run on subsequent rows**
    
    - Each Unit Testing project contains a Start.cs file that orchestrates the runs for each individual test case. Start.cs reads the test data and create a list of sites, or test runs, to run each test case against – each test case can run against multiple sets of test parameters (depending on what is in its respective test data file). The Start.cs file should contain several test methods, any helper functions to initiate and tear down your Selenium web drivers. This project uses a new instance of a Selenium Web driver for each test case, so each test case can be run in parallel to speed up test execution.

- 1 Class library for helper functions (Read CSV)
- 1 Selenium.runsettings file to establish test run configuration (see Appendix)
- 1 PowerShell file to orchestrate the uploading of Selenium artifacts from Test output directory to Azure Storage Blob (see Appendix)


##	ADDING A NEW TEST CATEGORY
- Create a new Unit Testing Project in the solution and name it appropriately (Login, Payment, Outage etc.)
- Install necessary NuGet Packages (see figure 2)
- Create a file called Start.cs within that Unit Testing Project

## ADDING NEW TESTS FOR A TEST CATEGORY
- Within the “Cases” folder of the Unit Testing Project that you want to add a test case to, create another folder and name it appropriately (CheckAccountLookup, CheckPayWithBankAccount etc.) Within this new folder, create two files:
    - 1.cs file that will contain your Selenium script1 
    - 1.csv file that will contain your test data. Each test data file should contain data headers on the first row and data values for each test run on subsequent rows. The naming of this file is important for integrating Selenium test reporting in VSTS. See next step.
- Within the Start.cs file in the root level of your Unit Testing project, create a test method to call your test case file. Within your Start.cs file, you should:
    - Initiate Selenium web driver
    - Use the ReadCSV.GetSites method from the Utils class library to create a list of site data from your test data .csv file. Loop over this list and call your specific test case for each site with the test data from ReadCSV call and the Selenium Web driver you just initiated. See existing Start.cs files in other Unit Testing Project categories for an example of how to do this. It is important to know that each test method name should relate to the test case data file with a specific naming convention. For example, if your test method is called CheckAccountLookup, your test data file should be called CheckAccountLookupTestData.csv. This naming convention is used for the custom widget described in an upcoming section of this document.
- Ensure that you close AND dispose of the selenium driver after each test case is run for all test data. This is important for VSTS to run and execute these tests properly.
- It is important to remember that users have the ability to supply test data parameters at run time. Test parameters could be anything from site urls to site account data. For more information on how to do this, see the Selenium.runsettings file in the appendix and the VS Test task in the next section on VSTS Release definitions.
