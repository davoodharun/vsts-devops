<template>
  <div id="app">
    <v-app>
    <div app class="header-bar">
      <div class="header">
        <span><strong>{{ title }}  <i class="fa fa-globe" aria-hidden="true"></i></strong></span>
        <div class="header-right">
           <span v-show="releases.length > 0" @click="closeTestResults()"><a><i class="fa fa-chevron-left" aria-hidden="true"></i> Build List</a></span>
        </div>
      </div>
      <div class="header-sub">
        <span class="small"><span class="small" v-show="releases.length <= 0">Please select an automation  build to view its results</span><a target="blank" v-bind:href="'https://automate.browserstack.com/builds/' + build_id">{{build_name}}</a></span>

      </div>
    </div>
      <v-content>
        
        <v-container>
          <ReleaseListComponent v-show="releases.length <=0" @selectRelease="onReleaseSelect"></ReleaseListComponent>
          <TestResultsComponent v-show="releases.length > 0" :tests.sync="releases"></TestResultsComponent>
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
  title: string;
  build_name: string;
  build_id: string;
  releases: any = [];
  constructor() {
    super();
    this.title = 'BrowserStack Session Explorer';
    this.build_name = '';
    this.build_id = '';
  }
  onReleaseSelect(value: any){
    console.log(value, 'app')
    this.releases = value;
    this.build_name = value[0].automation_session.build_name;
    this.build_id = value[0].build_id;
  }
  closeTestResults(value: any){
    this.releases = [];
    this.build_name = "";
    this.build_id = "";
  }
}
</script>

<style>
v-container {
  overflow: hidden;
}
/* Style the header with a grey background and some padding */
.header {
  overflow: hidden;
  background-color: #4286f4;
  padding: 20px 15px 0px 10px;
  height: 70px;
}

.header-sub {
  overflow: hidden;
  padding: 10px 10px 10px 10px;
  height: 40px;
  color: black;
}

.header-sub  a {
  color: white;
  float: left;
  text-align: center;
  text-decoration: none;
  line-height: 15px;
  padding: 5px;
  border-radius: 4px;
}

.small {
  font-size: 16px;
  float: left;
  color: black;
}

/* Style the header links */
.header span {
  float: left;
  color: white;
  text-align: center;
  text-decoration: none;
  font-size: 20px; 
  border-radius: 4px;
}
.header a {
  float: left;
  color: white;
  text-align: center;
  padding: 10px;
  text-decoration: none;
  font-size: 18px; 
  line-height: 15px;
  border-radius: 4px;
}

/* Style the logo link (notice that we set the same value of line-height and font-size to prevent the header to increase when the font gets bigger */
.header a.logo {
  font-size: 25px;
  font-weight: bold;
}

/* Change the background color on mouse-over */
.header a:hover {
  background-color: #ddd;
  color: black;
  cursor:pointer;
}

/* Style the active/current link*/
.header a.active {

  color: white;
}

.header-sub a:hover {
  background-color: #ddd;
  color: black;
  cursor:pointer;
}

/* Style the active/current link*/
.header-sub a.active {
  color: white;
}
/* Float the link section to the right */
.header-right {
  float: right;
}

/* Add media queries for responsiveness - when the screen is 500px wide or less, stack the links on top of each other */ 
@media screen and (max-width: 500px) {
  .header a {
    float: none;
    display: block;
    text-align: left;
  }

  .header-sub a {
    float: none;
    display: block;
    text-align: left;
  }
  .header-right {
    float: none;
  }
}
</style>