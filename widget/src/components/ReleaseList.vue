<!-- List of VSTS Releases -->
<template>
  <div>
     <grid-component @selectRelease="onReleaseSelect" :items.sync="releases"></grid-component> 
  </div>
</template>


<script lang="ts">
/// <reference types="vss-web-extension-sdk" />
import { Vue, Prop, Watch } from "vue-property-decorator";
import Component from 'vue-class-component';
import * as TestManagementClient from "TFS/TestManagement/RestClient";
import * as Grids from "VSS/Controls/Grids";
import * as Controls from "VSS/Controls";
import * as WidgetHelpers from "TFS/Dashboards/WidgetHelpers";
import Release from '../models/release';
import GridComponent from './Grid.vue';

@Component({
  components: {GridComponent}
})
export default class ReleaseList extends Vue {
  constructor() {
    super();
  }
  releases: any [] = [];
  @Prop() storageAccountUrl!: string;
  projectId = VSS.getWebContext().project.id;
  mounted () {
    this.getReleases();
  }
  getReleases() {
     TestManagementClient.getClient().getTestRuns(this.projectId).then((query: any) => {
      for (var i = 0; i < query.length; i++) {
        TestManagementClient.getClient().getTestRunById(this.projectId, query[i].id).then((result: any) => {
          if(result.release) {
            var folderName = result.release.name + "_" + result.release.id;
            var url = this.storageAccountUrl + folderName + "/" + folderName + ".html";
            var run = {
              id: result.id,
              passedTests: result.passedTests,
              totalTests: result.totalTests,
              releaseName: result.release.name,
              releaseId: result.release.id,
              downloadLink: url,
              tests: {},
              url: result.webAccessUrl,
              success: false,
              state: result.state,
              completedDate: result.lastUpdatedDate,
              runBy: "Harun Davood"
            }
            if(result.passedTests === result.totalTests) {
              run.success = true;
            } 
            var tests: any = {};
            TestManagementClient.getClient().getTestResults(this.projectId, run.id).then((results)=>{
              results.forEach((element: any)=>{
                var category = element.automatedTestStorage.split('.')[0];
                if(!tests.hasOwnProperty(category)){
                  tests[category] = {
                    name: category,
                    cases: [],
                    passedTests: 0
                  }
                }
                if(element.outcome === 'Passed'){
                  tests[category].passedTests++;
                }
                var baseUrl = this.storageAccountUrl + folderName + "/";
                
                var fullUrl = baseUrl + element.testCaseTitle + "TestData.csv";
                var test = {
                  category: category,
                  outcome: element.outcome,
                  title: element.testCaseTitle,
                  url: fullUrl
                }
                 tests[category].cases.push(test);
                 run.tests = tests;
              });
            });
            this.releases.push(run);
          }
        });     
      }    
    });
  }
  onReleaseSelect(value: any){
   this.$emit('selectRelease', value);
  }
} 
</script>

<style>

</style>