<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6">
            <img class="img-fluid keep-img" :src="keep.img" />
          </div>
          <div class="col-md-6">
            <div class="ms-4 d-flex flex-column justify-content-center">
              <i class="mdi mdi-account-eye text-center"
                >{{ keep.views }}
                <i class="mdi mdi-safe-square-outline text-center">{{
                  keep.kept
                }}</i></i
              >
              <h1 class="pb-2 text-center">{{ keep.name }} <br /></h1>
              <h6 class="pb-4 border-bottom border-secondary">
                {{ keep.description }}
              </h6>
              <!-- add dropdown with for each on uservaults -->
              <div>
                <i
                  v-if="keep.creatorId == account.id"
                  class="mdi mdi-delete text-danger"
                  @click="deleteKeep"
                ></i>
                <div class="dropdown open">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    type="button"
                    id="triggerId"
                    data-bs-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                    Add to Vault
                  </button>
                  <div class="dropdown-menu" aria-labelledby="triggerId">
                    <a
                      class="dropdown-item"
                      href="#"
                      v-for="v in vaults"
                      :key="v.id"
                      :value="v?.name"
                      @click="addKeep(v.id)"
                    >
                      {{ v?.name }}
                    </a>
                    <!-- <button class="dropdown-item" href="#">Action</button> -->
                  </div>
                </div>

                <img class="profile-img rounded-pill" :src="keep" alt="" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { Modal } from 'bootstrap'

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      async addKeep(vaultId) {
        logger.log('adding keep', vaultId)
        try {
          const newVaultKeep = {
            vaultId: vaultId,
            keepId: AppState.activeKeep.id
          }
          await keepsService.addKeep(newVaultKeep)
        } catch (error) {
          Pop.toast("hmm wrong", 'error')
          logger.log(error)
        }
      },
      async deleteKeep() {
        try {
          await keepsService.deleteKeep(AppState.activeKeep.id)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        } catch (error) {
          Pop.toast("hmm wrong", 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>
<style scoped>
.keep-img {
  width: 100%;
  object-fit: cover;
}
.profile-img {
  width: 35px;
}
</style>