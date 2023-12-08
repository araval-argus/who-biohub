<template>
  <v-container fluid class="who-users">
    <who-users-page-index />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import WhoUsersPageIndex from "../features/users/WhoUsersPageIndex.vue";
import { AppModule } from "../store/MainStore";

@Component({ components: { WhoUsersPageIndex } })
export default class WhoUsersView extends Vue {
  mounted() {
    AppModule.HideLoading();
  }
}
</script>
