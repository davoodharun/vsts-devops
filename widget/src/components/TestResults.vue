<!-- List of VSTS Releases -->
<template>

  <v-container grid-list-sm>
    
    <v-layout row wrap>
      
      <v-flex  xs12>
       
        <v-card id="release-title" dark d-flex color="primary">
          <v-layout row wrap  align-center>
            <v-flex class="text-xs-left" xs6>
              <v-card-text class="title">Release Overview  </v-card-text>
            </v-flex>
            <v-flex xs6>
              <v-card-text d-flex class="text-xs-right"v-on:click="closeTestResults()">
                <v-btn flat large>Release List <v-icon class="ml-1">arrow_forward</v-icon> </v-btn>
              </v-btn></v-card-text>
            </v-flex>
          </v-layout>
          
        </v-card>
      </v-flex>
      <v-flex width="220px" d-flex xs6 color="white">
        <div height="200px" width="220px" >
          <div v-show="!showCategory" id="summary">
              Name: <a v-bind:href="testRun.url" target="blank">{{testRun.releaseName}}</a><br />
              Id: {{testRun.releaseId}}<br />
              <a class="link-with-icon-text" target="_blank" v-bind:href="testRun.downloadLink">Download Report</a><br />
              <div class="results">
                  <span class="success--text display-3">{{testRun.passedTests}} </span> <span class="display-2">passed</span><br>
                <span class="error--text display-3">{{testRun.totalTests - testRun.passedTests}} </span> <span class="display-2">failed</span><br>
              </div>
          </div>
          <div v-show="showCategory" id="caseDetails">
              <h2 color="primary">{{selectedCategory.name}} <span class="ml-5" v-on:click="closeTestDetails()"><v-btn
              light
              small
              flat
              >
            back <v-icon class="ml-1">arrow_forward</v-icon>
            </v-btn></span></h2>
              Click tests below to download test data:<br><br>
              <span class="test-data" v-for="test in selectedCategory.cases">
              <a v-bind:href="test.url">{{test.title}}</a> <span class="passed" v-show="test.outcome === 'Passed'"><i class="material-icons">&#xE876;</i></span><span class="failure" v-show="test.outcome != 'Passed'"><i class="material-icons">&#xE5CD;</i></span><br />
              </span>
          </div>
        </div>
      </v-flex>
      <v-flex xs6 color="white">
        <div height="200px">
          <div id="cases">
              <div class="wrapper">
              <div v-for="(value, category) in testRun.tests">
                  <div  v-on:click="showTestDetails(category)" v-bind:class="{ 'box-green success': value.passedTests === value.cases.length, 'box error' : value.passedTests != value.cases.length }" >
                      {{category}}<br />
                      {{value.passedTests}} / {{value.cases.length}}
                  </div>
              </div>
              </div>
          </div>
        </div>
      </v-flex>
    </v-layout>
  </v-container>
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
    @Prop() testRun!: any;
    constructor() {
        super();
    }
    showCategory = false;
    selectedCategory = {
        cases: []
    };
    mounted () {
        console.log(this.testRun, "testing")
    }
    @Watch('testRun', { immediate: true, deep: true })
    onReleaseChange (val: any, oldVal: any) {
       this.testRun = val;
    }
    showTestDetails(category: string) {
        this.showCategory = true;
        this.selectedCategory = this.testRun.tests[category];
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
  #release-title {
    overflow: hidden;
    margin-bottom: 5px;
    height: 60px;
  }
    .test-results {
      overflow:visible;
    }
    h2 {
      color: #1a237e;
    }
    .test-data {
      font-size: 16px;
    }
    .sub-header {
      font-weight: bold;
    }
    .results {
      margin-top: 10px;
      overflow: hidden;
    }
    #selected {
      top: 10px;
      padding: 5px;
    }
    #testChart {
      float: left;
    }
    tr {
      cursor: pointer;
    }
    #cases {
      font-size: 11px;
      overflow: auto;
      float:left;
    }
    #summary {
     float:left;
      padding: 10px;
      width:270px;
      font-size: 14px;
      transition: box-shadow .2s;
      margin-right: 10px;
      border-radius: 5px;
    }

    #caseDetails {
      float:left;
      padding: 10px;
      width:270px;
      height: 185px;
      transition: box-shadow .2s;
      border-radius: 5px;
    }
    div {
      overflow:auto;
    }
    ul {
      list-style-type: none;
    }
    .fade-enter-active, .fade-leave-active {
      transition: opacity .5s;
    }
    .fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
      opacity: 0;
    }
    .passed {
      display: table-cell;
      line-height: 1em;
      width: 1em;
      height:1em;
      margin-right: 0.3em;
      text-align: center;
      color: green;
    }
    .failure {
      display: table-cell;
      line-height: 1em;
      width: 1em;
      height:1em;
      margin-right: 0.3em;
      text-align: center;
      color: red;
    }
    .pull-right {
      float: right;
    }
    .wrapper {
      display: grid;
      overflow: auto;
      grid-template-columns: 90px 90px 90px;
      grid-gap: 5px;
      margin:auto;
      color: #444;
    }

    .box {
      background-color: #FF0000;
      color: #fff;
      border-radius: 5px;
      padding: 20px;
      text-align: center;
      overflow: hidden;
      font-size: 150%;
      cursor:pointer;
    }
    .box:hover {
      background-color: rgb(43, 42, 42);
      color: #fff;
      border-radius: 5px;
      padding: 20px;
      text-align: center;
      overflow: hidden;
      font-size: 150%;
      cursor:pointer;
    }
    .box-green:hover {
      background-color: rgb(43, 42, 42);
      color: #fff;
      border-radius: 5px;
      padding: 20px;
      text-align: center;
      overflow: hidden;
      font-size: 150%;
      cursor:pointer;
    }
    .box-green {
      background-color: #007F00;
      color: #fff;
      border-radius: 5px;
      padding: 20px;
      text-align: center;
      overflow: hidden;
      white-space: nowrap;
	    overflow: hidden;
      font-size: 150%;
      cursor:pointer;
    }
    .box div {
      display: block;
      position: relative;
      top: 50%;
      overflow: hidden;
      white-space: nowrap;
      transform: translateY(-90%);
    }

    li img:hover {
      -webkit-box-shadow: 0px 0px 7px rgba(255,255,255,0.9);
      box-shadow: 0px 0px 7px rgba(255,255,255,0.9);
    }
</style>