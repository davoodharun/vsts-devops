<template>
  <div id="app">
    <v-app>
    <div app class="header-bar">
      <h2>{{ title }}</h2>
    </div>
      <v-content>
        
        <v-container>
        <ReleaseListComponent v-show="!release.releaseName" @selectRelease="onReleaseSelect" :storageAccountUrl.sync="storageAccountUrl"></ReleaseListComponent>
        <TestResultsComponent v-show="release.releaseName" :testRun.sync="release" @closeTestResults="onCloseTestResults"></TestResultsComponent>
        </v-container>
      </v-content>
    </v-app>
  </div>
</template>

<script lang="ts">
import { Vue, Prop, Watch } from "vue-property-decorator";
import Component from 'vue-class-component';
import ReleaseListComponent from './components/ReleaseList.vue';
import TestResultsComponent from './components/TestResults.vue';

@Component({
  components: { 
      ReleaseListComponent,
      TestResultsComponent 
    }
})
export default class App extends Vue {
  @Prop() storageAccountUrl!: string;
  title: string;
  release: any = {};
  constructor() {
    super();
    this.title = 'Selenium Test Explorer';
  }
  onReleaseSelect(value: any){
    this.release = value;
  }
  onCloseTestResults(value: any){
    this.release = {};
  }
}
</script>

<style>
v-container {
  overflow: hidden;
}
</style>