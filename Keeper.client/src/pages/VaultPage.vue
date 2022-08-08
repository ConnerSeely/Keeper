<template>
  <div class="container-fluid">
    <div class="row">
      <h1>{{ vault.name }}</h1>
      <h3>Keeps: {{ keeps.length }}</h3>
      <i
        v-if="vault.creatorId == account.id"
        class="mdi mdi-delete text-danger"
        @click="deleteVault"
        >Delete Vault</i
      >
      <div class="masonry-frame">
        <div v-for="k in keeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { useRoute, useRouter } from 'vue-router'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.getVaultKeeps(route.params.id)
      } catch (error) {
        router.push({ name: "Home" })
        Pop.toast('Failed to load Vaults/Keeps', 'error')
        logger.error(error)
      }
    })
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),

      async deleteVault() {
        try {
          await vaultsService.deleteVault(route.params.id)
          router.push({ name: "Home" })
        } catch (error) {
          Pop.toast("hmm wrong", 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}
</style>