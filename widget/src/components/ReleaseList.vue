<!-- List of VSTS Releases -->
<template>
  <div>
     <grid-component @selectBuild="onBuildSelect" :items.sync="releases"></grid-component> 
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
      console.log(response);
      this.releases=response.data;
    });
    
  }
  onBuildSelect(value: any){
   const build_id = value.automation_build.hashed_id;
    axios({
      method:'get',
      url:'https://exelonbrowserstack.azurewebsites.net/session/' + build_id
    })
    .then((response: any) => {
      
      this.$emit('selectRelease', response.data.map((element: any) => {
        element.build_id = build_id;
        return element;
      }));
    });
  }
} 
</script>

<style>

</style>