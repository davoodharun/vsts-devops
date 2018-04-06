<script>
import { Pie, mixins } from 'vue-chartjs'
import { Vue, Prop, Watch } from "vue-property-decorator";
export default {
  extends: Pie,
  mixins: [mixins.reactiveProp],
  props: ['chartData', 'options'],
  mounted () {
    if(this.chartData) {
      this.renderChart(this.chartData, this.options)  
    }
  }
}
</script>