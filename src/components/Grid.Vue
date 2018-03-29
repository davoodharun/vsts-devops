<!-- List of VSTS Releases -->
<template>
    <div class="grid">
    </div>
</template>

<script lang="ts">
/// <reference types="vss-web-extension-sdk" />
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import * as TestManagementClient from "TFS/TestManagement/RestClient";
import * as Grids from "VSS/Controls/Grids";
import * as Controls from "VSS/Controls";
import * as WidgetHelpers from "TFS/Dashboards/WidgetHelpers";
import Release from '../models/release';

@Component
export default class Grid extends Vue {
  releases: any [] = [];
  projectId = VSS.getWebContext().project.id;
  gridOptions: Grids.IGridOptions = {
    width: "700px",
    height: "300px",
    source: this.releases,
    columns: [
        { text: "Name", index: "releaseName", width: 100 },
        { text: "Id", index: "releaseId", width: 60, canSortBy: true },
        { text: "Passed", index: "passedTests", width: 70, canSortBy: true },
        { text: "Total", index: "totalTests", width: 60, canSortBy: true },
        { text: "State", index: "state", width: 80, canSortBy: true },
        { text: "Run By", index: "runBy", width: 120, canSortBy: true },
        { text: "Completed", index: "completedDate", width: 150, canSortBy: true }
    ],
    sortOrder: [
        { index: "releaseId", order: "desc"}
    ],
    // openRowDetail: (index: any) => {
    //   // on row click, get release information
    //   var release = this.grid.getRowData(index);
    //   TestManagementClient.getClient().getTestResults(this.projectId, release.id).then((results)=>{
        
    //     // iterate through test results and group them into categories
    //     results.forEach((result) => {
    //       var category = result.automatedTestStorage.split('.')[0];
    //       if(!this.$parent.testCategories.hasOwnProperty(category) ) {
    //         Vue.set(this.$parent.testCategories, category, {
    //           tests: [],
    //           passedTests: 0
    //         });
    //       }
    //       // increment passed tests count
    //       if(result.outcome === 'Passed') {
    //         this.$parent.testCategories[category].passedTests++;
    //       }
    //       // construct filename for download
    //       var fileName = release.releaseName + "_" + release.releaseId;
          
    //       // construct base url for download
    //       var baseUrl = "https://exelonselenium.blob.core.windows.net/selenium/" + fileName + "/";
    //       result.url = baseUrl;
          
    //       // construct full url
    //       var fullUrl = baseUrl + result.testCase.name + 'TestData.csv';
          
          
    //       /* Getting arguments for each test
    //         //proxy url
    //         var proxy = 'https://cors-anywhere.herokuapp.com/';
    //         var proxyUrl = proxy + fullUrl;
    //         $.ajax({
    //           url: proxyUrl,
    //           dataType: 'text'
    //         }).done(function(element){
    //           var arguments = element.split('\n')[0].split(',');
    //           result.testArguments = arguments;
    //         }).error(function(element){
    //           console.log(element);
    //         });
    //       */

    //       this.$parent.testCategories[category].tests.push(result);                
    //     });

    //     // add results to vue
    //     this.$parent.selected.categories = results;
    //   });

    //   // create chart
    //   //createChart(Services, grid, index);

    //   // update
    //   this.$parent.selected = release;
    // }
  }
  grid = Controls.create(Grids.Grid, $(".grid"), this.gridOptions);  
  created () {
    TestManagementClient.getClient().getTestRuns(this.projectId).then((query: any) => {
      for (var i = 0; i < query.length; i++) {
          TestManagementClient.getClient().getTestRunById(this.projectId, query[i].id).then((result: any) => {
            if(result.release) {
              var fileName = result.release.name + "_" + result.release.id;
              // construct url for report.html (TODO: exten this to be dynamic)
              var url = "https://exelonselenium.blob.core.windows.net/selenium/" + fileName + "/" + fileName + ".html";
              var run = {
                id: result.id,
                passedTests: result.passedTests,
                totalTests: result.totalTests,
                releaseName: result.release.name,
                releaseId: result.release.id,
                downloadLink: url,
                tests: [],
                success: false,
                state: result.state,
                completedDate: result.lastUpdatedDate,
                runBy: "Harun Davood"
              }
              if(result.passedTests === result.totalTests) {
                run.success = true;
              }
              
              this.releases.push(run);
              this.grid.setDataSource(this.releases);
            }
          });     
      }
      console.log(this.$parent)     
    })
  }
} 
</script>

<style>
.greeting {
    font-size: 20px;
}
</style>