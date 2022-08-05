<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-2 p-2">
        <img class="rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-10 p-2">
        <h1>{{ account.name }}</h1>
        <p>{{ account.email }}</p>
      </div>
    </div>
    <div class="row">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
export default {
  name: 'Account',
  setup() {
    onMounted(async () => {
      try {
        await vaultsService.getAll()
      } catch (error) {
        Pop.toast('Failed to load Vaults', 'error')
        logger.error(error)
      }
    })
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 300px;
  min-height: 200px;
}
</style>
