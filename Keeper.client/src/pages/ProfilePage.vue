<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-2 p-2">
        <img class="account-img rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-10 p-5">
        <h1>{{ account.name }}</h1>
        <h3>Vaults: {{ userVaults.length }}</h3>
        <h3>Keeps: {{ userKeeps.length }}</h3>
      </div>
    </div>
    <div class="row">
      <h1>
        Vaults
        <i
          class="mdi mdi-plus selectable"
          data-bs-toggle="modal"
          data-bs-target="#create-vault"
        ></i>
      </h1>
      <div class="col-3" v-for="v in userVaults" :key="v.id">
        <Vault :vault="v" />
      </div>
    </div>
    <!-- TODO MAKE ICONS DISAPPEAR WHEN NOT CREATOR -->
    <h1>
      Keeps
      <i
        class="mdi mdi-plus selectable"
        data-bs-toggle="modal"
        data-bs-target="#create-keep"
      ></i>
    </h1>
    <div class="masonry-frame">
      <div v-for="k in userKeeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
  <Modal id="create-vault">
    <template #header>Create a Vault</template>
    <template #body>
      <VaultForm />
    </template>
  </Modal>
  <Modal id="create-keep">
    <template #header>Create a Keep</template>
    <template #body>
      <KeepForm />
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { useRoute } from 'vue-router'
export default {
  name: 'Account',
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await vaultsService.getAll(route.params.id)
        await keepsService.getProfileKeeps(route.params.id)
      } catch (error) {
        Pop.toast('Failed to load Vaults', 'error')
        logger.error(error)
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      userVaults: computed(() => AppState.userVaults),
      userKeeps: computed(() => AppState.userKeeps),
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
.account-img {
  max-width: 300px;
  min-height: 200px;
}
.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}
</style>
