<template>
  <v-alert dismissible :type="type" :value="alert">
    {{ alertText }}
  </v-alert>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({ components: {} })
export default class AlertComponent extends Vue {
  private alert = true;

  @Prop({ required: true, type: Boolean, default: false })
  readonly showDialog: boolean;

  @Prop({ required: true, type: String, default: null })
  readonly alertText: string;
  @Prop({ required: true, type: String, default: null })
  readonly type: string;

  @Prop({ required: false, type: Number, default: 5000 })
  readonly timeout: number;

  created() {
    if (this.timeout != -1) {
      setTimeout(() => {
        this.alert = false;
      }, this.timeout);
    }
  }
}
</script>
