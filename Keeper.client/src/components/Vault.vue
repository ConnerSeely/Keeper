<template>
  <div class="vault d-flex m-4 elevation-2 selectable">
    <img class="img-fluid" src="https://thiscatdoesnotexist.com" />
    <div class="info p-2 text-light">
      <h3 class="">{{ vault.name }}</h3>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed } from 'vue'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      profile: computed(() => AppState.account),
    }
  }
}
</script>


<style lang="scss" scoped>
.vault {
  width: 200px;
}
.info {
  position: absolute;
  bottom: 1px;
}
</style>