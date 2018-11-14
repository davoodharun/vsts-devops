<!-- List of VSTS Releases -->
<template>
  <div>
     <grid-component @selectBuild="onBuildSelect" :items.sync="releases" :loading.sync="isLoading"></grid-component> 
  </div>
  
</template>


<script lang="ts">
/// <reference types="vss-web-extension-sdk" />
import { Vue, Prop, Watch } from "vue-property-decorator";
import Component from 'vue-class-component';
import * as Grids from "VSS/Controls/Grids";
import * as Controls from "VSS/Controls";
import * as WidgetHelpers from "TFS/Dashboards/WidgetHelpers";
import Release from '../models/release';
import GridComponent from './Grid.vue';
import VueResource from 'vue-resource'
const axios = require('axios');
@Component({
  components: {GridComponent}
})
export default class ReleaseList extends Vue {
  constructor() {
    super();
  }
  releases: any [] = [];
  isLoading: boolean = true;
  offset: number = 0;
  limit: number = 100;
  projectId = VSS.getWebContext().project.id;
  mounted () {
    this.getReleases();
  }
  getReleases() {
     axios({
      method:'get',
      url:'https://exelonbrowserstack.azurewebsites.net/builds',
    })
    .then((response: any) => {
      this.releases=response.data;
    });
    
  }
  onBuildSelect(value: any){
    this.isLoading = true;
    const build_id = value.automation_build.hashed_id;
    let sessions: any[] = [];
    this.getSessions(build_id, this.offset, sessions);
  
  }

  getSessions(build: string, offset: number, sessions: any) {
    axios({
      method:'get',
      url:'https://exelonbrowserstack.azurewebsites.net/session/' + build + '/' + this.offset
    }).then((response: any) => {
      if(response.data.length > 0) {
        sessions = sessions.concat(response.data.map((element: any) => {
          element.build_id = build;
          return element;
        }));
        this.isLoading = true;
        this.offset = this.offset + this.limit;
        this.getSessions(build, this.offset, sessions);
      } else {
        this.isLoading = false;
        this.offset = 0;
        this.$emit('selectRelease', sessions);
        return;
      }
    });
  }
} 
</script>

<style>

</style>