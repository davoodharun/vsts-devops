# Custom VSTS Widget
This project is a scaffolidng of several C# Unit Testing projects that can be incorporated into an exisitng Visual Studio solution and used to develop a continuous integration and delivery pipiline in VSTS. The testing frameworks utlilized in this project are Selenium and MSTest, although any testing framework can be used depending on your specific use case. 

A sample VSTS extension widget is also included in this project that gathers reports and results for unit tests run within the CI/CD pipeline.

VSTS release and build definitions are also given within this repository.


![Release List](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/ReleaseList.png)

![Test Run / Release Summary](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/summary.png)

![Test Data](https://raw.githubusercontent.com/davoodharun/vsts-devops/master/docs/img/testdata.png)

## Widget Quickstart
1. git clone https://github.com/davoodharun/vsts-devops.git
2. cd vsts-devops/widget
3. npm install
4. npm run gallery-publish
5. upload .visx extension file to VSTS Marketplace
6. share extension with VSTS account(s)
7. add widget to VSTS dashboard


This VSTS widget is published by Applied Information Sciences but not yet verified and available on the public Microsoft Marketplace. In order to install this dashboard extension, the extension must already be shared with your TFS/VSTS account. Once the extension is shared, you can install it by navigating to your project dashboard and adding the Selenium Explorer widget from the list of available extensions.

On the initial load of the custom dashboard widget, the user is presented with a list of releases associated with Selenium testing that have been completed for the respective project. This list displays information such as how many tests passed, how many tests failed, which user kicked off the release, and when it was completed. 
 
When a user clicks on a release line item, they are taken to another screen within the widget that dynamically displays badges for each Selenium testing category. Each badge contains text to display how many tests passed within each category. If all tests passed, the badge is green. If at least one of the tests fail, the badge or category turns red. On the left-hand side of this new screen, there is information about the release (name and id) as well statistics on the total amount of tests run and tests passed for that specific release. It also contains a link called “Download Report” where a user can download an html testing report for the respective release

If the user clicks on one of the badges or categories, the left-hand portion of the screen will change. The left side will now contain a list of tests that were under the category they just clicked. If they click on a test name, the test data that was used for that test will be downloaded to user’s local computer. Users can also get back to the release list if they click the “X” at the top-right corner of the widget.

Users should also configure the widget to use correct Azure storage account where their tests results are uploaded during the release process. This storage account url should contain the container name where the tests results are being stored, followed by a trailing ‘/’.  
