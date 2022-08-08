<template>
  <div class="vault d-flex m-4 elevation-2 selectable" @click="goToVault">
    <img class="img-fluid" src="https://thiscatdoesnotexist.com" />
    <div class="info p-2 text-light">
      <h3 class="">{{ vault.name }}</h3>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { computed } from 'vue'
import { useRouter } from 'vue-router'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter();
    return {
      profile: computed(() => AppState.account),
      goToVault() {
        router.push({ name: "Vault", params: { id: props.vault.id } })
      }
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