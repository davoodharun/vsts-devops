<!-- List of VSTS Releases -->
<template>
  <v-data-table 
    :headers="headers"
    :items="tests" 
    class="elevation-1"
    >
    <template slot="items" slot-scope="{item}">
        <tr  @click="getResults(item, $event)">
            <td>{{item.automation_session.name }}</td>
            <td class="text-xs-left">{{item.automation_session.status}}</td>
            <td class="text-xs-left">{{item.automation_session.browser}} {{item.automation_session.browser_version}}</td>
            <td class="text-xs-left">{{item.automation_session.os}} {{item.automation_session.os_version }}</td>
        </tr>
    </template>
  </v-data-table>
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
import PieComponent from './PieChart.vue'

@Component({
    components: {PieComponent}
})
export default class TestResults extends Vue {
    @Prop() tests!: any;
    constructor() {
        super();
    }
    headers = [
        { text: "Name", value: "automation_session.name", align: 'left', width: "100px"},
        { text: "Status", value: "automation_session.status", width: "70px" },
        { text: "Browser", sortable: true, width: "30px" },
        { text: "OS", sortable: true, width: "30px" },
    ]
    showCategory = false;
    selectedCategory = {
        cases: []
    };
    mounted () {
    }
    @Watch('tests', { immediate: true, deep: true })
    onReleaseChange (val: any, oldVal: any) {
       this.tests = val;
    }
    getResults(test: any) {
      console.log(test)
        window.open(test.automation_session.video_url);
        //redirect current page to success page
        window.focus();
    }
    closeTestDetails(){
        this.showCategory = false;
        this.selectedCategory = {
            cases: []
        }
    }
    closeTestResults(){
        this.$emit('closeTestResults');
    }
} 
</script>

<style>
.menu {
     min-width: 50px;
}
tr {
    cursor: pointer;
}
</style>