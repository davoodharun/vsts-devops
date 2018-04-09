# VSTS Configuration (Release Definitions and setup)

##	BUILD AND RELEASE OVERVIEW
Visual Studio Team Services (VSTS) provides a full-featured Git server for hosting your team's source code. To keep code quality high, VSTS allows for the addition continuous integration (CI) builds to your team's process. CI builds automatically build and test code every time a team member pushes a commit to the server. 
VSTS also provides Release management services that can help your team continuously deliver software to your customers at a faster pace and with lower risk. You can fully automate the testing and delivery of your software in multiple environments all the way to production or set up semi-automated processes with approvals and on-demand deployments.
VSTS also provides several variables that give you a convenient way to get key bits of data into various parts of your build and release processes.
The rest of this section describes how to configure VSTS to run your Selenium Tests as a part of a release cycle. Read more about VSTS Build and Release. This document will not go into detail on how to create a Build definition since the automated Selenium testing scripts will be run within a Release Cycle / Definition.

## CREATING A RELEASE DEFINITION
- Create a Release Definition in VSTS
- Ensure that release definition is linked to a build artifact
- Ensure the release definition contains an Agent Phase that uses the Hosted VS2017 Release Agent
- Import the [JSON release definition template](release/testAndUploadToStorage.json) as a reference, or create the following release tasks under your release definition (try to use definition variables wherever possible):

###	RELEASE TASK - VISUAL STUDIO TEST
This will be the primary task to run any Selenium test scripts.
- Test Selection
    - Select tests Using – Test Assemblies
    - Test Assemblies – input the path for each of your test assemblies here. You can also use a mini-match pattern and release/build variables to locate dlls in a specific folder on your release agent. Keep in mind that you only want to target the .dll files related to your Selenium project and not any other dlls that might be in your build artifact that your release definition is using. See example below:
    ```
    $(Build.DefinitionName)/drop/Account/bin/Release/Account.dll
    $(Build.DefinitionName)/drop/Login/bin/Release/Login.dll
    $(Build.DefinitionName)/drop/Outage/bin/Release/Outage.dll
    $(Build.DefinitionName)/drop/Payment/bin/Release/Payment.dll
    ```

    - Search Folder - $(System.DefaultWorkingDirectory)

    **$(Build.DefinitionName) and $(System.DefaultWorkingDirectory) are default variables made available in VSTS.**

- Execution Options
    - Select Test Platform Using – Latest
    - Settings file – Select your Selenium.runsettings file here from your build artifact
    - Override Test Run Parameters – this section only applies if you want to change test run settings or parameters at run time. If you are using this option, you should have a TestRunParameters xml block in your runsettings file (see Appendix). If you choose this option, you must enter desired parameters similar to:

- Reporting Options
    - Test Run Title - $(Release.ReleaseName)
    - Ensure “Upload Test Attachments” is checked

- Control Options
    - Ensure “Enabled” is checked
    - Ensure “Continue on Error” is checked

### RELEASE TASK - POWERSHELL SCRIPT (UPLOAD TEST RESULTS TO AZURE BLOB)
This task will be used to format Selenium reporting results and upload them to Azure Blob Storage

**Only create this task if you plan on adding a custom VSTS widget to your dashboard to view Selenium test results**

- Type – File Path
- Script Path – select the path to your PowerShell script that you created to upload artifacts to Azure Blob (see Appendix for script)
- Arguments:
    - $(SubscriptionName) – custom release variable to designate an Azure subscription name
    - $(StorageAccountName) – custom release variable for storage account name
    - $(StorageKey) – custom release variable for Azure Blob storage key
    - $(ContainerName) – custom release variable to designate blob container that selenium reports will be storage in (should be all lowercase letters and no special characters or numbers)
    - $(Release.ReleaseName) – built in release variable for release name
    - $(Release.ReleaseId) – built in release variable for release id
    - $(TestResultsFolder) – custom variable to designate where test results are stored on the release agent; this should also be set in your Selenium.runsettings file inside your Visual Studio project


