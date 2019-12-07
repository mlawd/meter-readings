<template>
  <v-app>
    <v-content class="fill-height">
      <v-row class="fill-height" justify="center" align="center">
        <v-col md="4">
          <v-file-input
            accept=".csv"
            label="Upload CSV"
            prepend-icon="mdi-file-table"
            v-on:change="upload"
          />
        </v-col>
      </v-row>
      <v-dialog v-model="showDialog" max-width="400px">
        <v-card>
          <v-card-title v-if="!response">Uploading</v-card-title>
          <v-card-title v-else>Success!</v-card-title>
          <v-card-text>
            <v-row>
              <v-col align="center" v-if="!response">
                <v-progress-circular indeterminate />
              </v-col>
              <v-col align="center" v-else>
                <table>
                  <tr>
                    <td>Successful:</td><td>{{ response.successfulEntries }}</td>
                  </tr>
                  <tr>
                    <td>Failed:</td><td>{{ response.failedEntries }}</td>
                  </tr>
                </table>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-dialog>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";

export default Vue.extend({
  data() {
    return {
      showDialog: false,
      response: null
    };
  },
  methods: {
    upload: async function(file: File) {
      if (!file) return;

      this.showDialog = true;
      this.response = null;

      const form = new FormData();
      form.append("csv", file);

      this.response = (await axios.post(
        "http://localhost:5000/api/upload",
        form,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      )).data;
    }
  }
});
</script>

<style>
.fill-height {
  height: 100vh;
}
</style>
